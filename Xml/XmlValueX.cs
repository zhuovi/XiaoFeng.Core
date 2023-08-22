﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

/****************************************************************
 *  Copyright © (2021) www.fayelf.com All Rights Reserved.      *
 *  Author : jacky                                              *
 *  QQ : 7092734                                                *
 *  Email : jacky@fayelf.com                                    *
 *  Site : www.fayelf.com                                       *
 *  Create Time : 2021/4/22 14:58:42                            *
 *  Version : v 1.0.0                                           *
 *  CLR Version : 4.0.30319.42000                               *
 ****************************************************************/
namespace XiaoFeng.Xml
{
    /// <summary>
    /// 类说明
    /// </summary>
    public class XmlValueX
    {
        #region 构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        public XmlValueX()
        {
            
        }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="node">节点</param>
        public XmlValueX(XmlNode node) :this(node as XmlElement){ }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="element">节点</param>
        public XmlValueX(XmlElement element)
        {
            this.Element = element;
            this.Value = element.Value;
            this.Name = element.Name;
            this.NodeType = XmlType.Element;
            this.ChildNodes = new List<XmlValueX>();
            foreach (XmlNode node in element.ChildNodes)
            {
                if(node.NodeType== XmlNodeType.Element)
                    this.ChildNodes.Add(new XmlValueX((XmlElement)node));
            }
            //this.ParentElement = new XmlValueX(element.ParentNode as XmlElement);
        }
        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="attribute">属性</param>
        public XmlValueX(XmlAttribute attribute)
        {
            this.Attribute = attribute;
            this.NodeType = XmlType.Attribute;
            this.Name = attribute.Name;
            this.Value = attribute.Value;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 节点类型
        /// </summary>
        public XmlType NodeType { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 节点
        /// </summary>
        private XmlElement Element { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        private XmlAttribute Attribute { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<XmlValueX> ChildNodes { get; set; }
        /// <summary>
        /// 是否有子节点
        /// </summary>
        public Boolean HasChildNodes
        {
            get
            {
                return this.ChildNodes != null && this.ChildNodes.Count > 0;
            }
        }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public XmlValueX ParentElement
        {
            get
            {
                XmlValueX parent = null;
                if (this.Element != null && this.Element.ParentNode != null && this.Element.ParentNode.Name != "#document")
                    parent = new XmlValueX(this.Element.ParentNode);
                return parent;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取节点数据
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public XmlValueX GetElement(string name)
        {
            if (this.Element == null || !this.Element.HasChildNodes) return null;
            foreach (XmlNode node in this.Element.ChildNodes)
            {
                if (node.Name.EqualsIgnoreCase(name)) return new XmlValueX((XmlElement)node);
            }
            return null;
        }
        /// <summary>
        /// 获取节点属性
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public XmlValueX GetAttribute(string name)
        {
            if (this.Element == null || !this.Element.HasAttributes) return null;
            foreach (XmlAttribute attr in this.Element.Attributes)
            {
                if (attr.Name.EqualsIgnoreCase(name)) return new XmlValueX(attr);
            }
            return null;
        }
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="target">目标对象</param>
        /// <returns></returns>
        public object ParserValue(Type type,object target = null)
        {
            if (this == null || this.Element == null) return null;
            if ((type == this.GetType() || type == null || type == typeof(XmlElement) || type ==typeof(XmlNode)) && target == null) return this;
            if (type == null && target != null)
                type = target.GetType();
            var ValueType = type.GetValueType();
            if (ValueType == ValueTypes.String || ValueType == ValueTypes.Value)
            {
                return this.Value.GetValue(type);
            }
            else if (ValueType == ValueTypes.Class || ValueType == ValueTypes.Struct)
            {
                return ParseObject(this, type, target);
            }
            else if (ValueType == ValueTypes.Array)
            {
                return ParseArray(this, type, target);
            }
            else if (ValueType == ValueTypes.ArrayList || ValueType == ValueTypes.IEnumerable || ValueType == ValueTypes.List)
            {
                return ParseList(this, type, target);
            }
            else if (ValueType == ValueTypes.Enum)
            {
                return this.Value.ToEnum(type);
            }
            else
                return this.Value.GetValue(type);
        }
        /// <summary>
        /// 转成对象
        /// </summary>
        /// <param name="xmlValue">Xml对象</param>
        /// <param name="type">模板类型</param>
        /// <param name="target">目标对象</param>
        /// <returns></returns>
        public object ParseObject(XmlValueX xmlValue, Type type, object target)
        {
            if (xmlValue == null) return null;
            if (type == typeof(object) || type == typeof(XmlValueX)) return xmlValue;
            if (target == null)
                target = Activator.CreateInstance(type);
            type.GetPropertiesAndFields(m =>
            {
                var PropertyType = m.MemberType == MemberTypes.Property ? (m as PropertyInfo).PropertyType : (m as FieldInfo).FieldType;
                var ValueType = PropertyType.GetValueType();
                var Name = m.Name;
                XmlValueX xValue;
                if (m.IsDefined(typeof(XmlElementAttribute), false))
                {
                    Name = m.GetCustomAttribute<XmlElementAttribute>().ElementName;
                    xValue = xmlValue.GetElement(Name.IfEmpty(m.Name));
                }
                else if (m.IsDefined(typeof(XmlArrayAttribute), false))
                {
                    Name = m.GetCustomAttribute<XmlArrayAttribute>().ElementName;
                    xValue = xmlValue.GetElement(Name.IfEmpty(m.Name));
                }
                else if (m.IsDefined(typeof(XmlAttributeAttribute), false))
                {
                    Name = m.GetCustomAttribute<XmlAttributeAttribute>().AttributeName;
                    xValue = xmlValue.GetAttribute(Name.IfEmpty(m.Name));
                }
                else
                {
                    xValue = xmlValue.GetElement(Name);
                }
                
                if (m is PropertyInfo p)
                    p.SetValue(target, xValue?.ParserValue(p.PropertyType));
                else if (m is FieldInfo f)
                    f.SetValue(target, xValue?.ParserValue(f.FieldType));
            });
            return target;
        }
        /// <summary>
        /// 转数组
        /// </summary>
        /// <param name="xmlValue">数据</param>
        /// <param name="type">类型</param>
        /// <param name="target">目标对象</param>
        /// <returns></returns>
        public Array ParseArray(XmlValueX xmlValue, Type type, object target)
        {
            if (xmlValue == null) return null;
            var list = new List<XmlValueX>();
            if (xmlValue.HasChildNodes)
                list = xmlValue.ChildNodes;
            else
            {
                list = xmlValue.ParentElement.ChildNodes.Where(a => a.Name.EqualsIgnoreCase(xmlValue.Name)).ToList();
            }
            var length = list.Count;
            var elmType = type?.GetElementXType();
            if (elmType == null) elmType = typeof(object);
            var arr = Array.CreateInstance(elmType, length);
            list.For(0, length, i =>
             {
                 var item = list[i];
                 object val = null;
                 if (item != null)
                     val = item.ParserValue(elmType, arr.GetValue(i));
                 arr.SetValue(val.GetValue(elmType), i);
                 i++;
             });
            return arr;
        }
        /// <summary>
        /// 转列表
        /// </summary>
        /// <param name="xmlValue">数据</param>
        /// <param name="type">类型</param>
        /// <param name="target">目标对象</param>
        /// <returns></returns>
        public IList ParseList(XmlValueX xmlValue, Type type, object target)
        {
            if (xmlValue == null) return null;
            var lists = new List<XmlValueX>();
            if (xmlValue.HasChildNodes)
                lists = xmlValue.ChildNodes;
            else
            {
                lists = xmlValue.ParentElement.ChildNodes.Where(a => a.Name.EqualsIgnoreCase(xmlValue.Name)).ToList();
            }
            var elmType = type.GetGenericArguments().FirstOrDefault();
            if (elmType == null) elmType = typeof(object);
            // 处理一下type是IList<>的情况
            if (type.IsInterface) type = typeof(List<>).MakeGenericType(elmType);
            // 创建列表
            var list = (target ?? Activator.CreateInstance(type)) as IList;
            foreach (var item in lists)
            {
                object val = null;
                if (item != null) val = item.ParserValue(elmType, null);
                list.Add(val.GetValue(elmType));
            }
            return list;
        }
        #endregion
    }
}