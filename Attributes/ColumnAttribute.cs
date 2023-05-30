﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/****************************************************************
*  Copyright © (2017) www.fayelf.com All Rights Reserved.       *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@fayelf.com                                     *
*  Site : www.fayelf.com                                        *
*  Create Time : 2017-12-08 10:43:37                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng
{
    /// <summary>
    /// 数据库字段属性
    /// Version : 1.0.0
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
    public sealed class ColumnAttribute : Attribute
    {
        #region 构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        public ColumnAttribute()
        {
            this.PrimaryKey = false;
            this.IsNullable = true;
            this.IsUnique = false;
            this.Length = 0;
            this.AutoIncrement = false;
            this.IsIndex = false;
            this.DefaultValue = "";
            this.AutoIncrementSeed = 0;
            this.AutoIncrementStep = 1;
        }
        /// <summary>
        /// 设置字段属性值
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="dataType">字段类型</param>
        /// <param name="length">类型长度</param>
        /// <param name="defaultValue">字段默认值</param>
        /// <param name="description">字段说明</param>
        public ColumnAttribute(string name, string dataType, int length = 0, object defaultValue = null, string description = "")
            : this()
        {
            this.Name = name;
            this.DataType = dataType;
            this.Length = length;
            this.DefaultValue = defaultValue;
            this.Description = description;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public object DataType { get; set; }
        /// <summary>
        /// 字段长度
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// 字段说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue { get; set; }
        /// <summary>
        /// 是否是主键
        /// </summary>
        public Boolean PrimaryKey { get; set; }
        /// <summary>
        /// 是否为空
        /// </summary>
        public Boolean IsNullable { get; set; }
        /// <summary>
        /// 是否唯一
        /// </summary>
        public Boolean IsUnique { get; set; }
        /// <summary>
        /// 是否是自增长列
        /// </summary>
        public Boolean AutoIncrement { get; set; }
        /// <summary>
        /// 自增长步数
        /// </summary>
        public int AutoIncrementStep { get; set; }
        /// <summary>
        /// 自增长种子
        /// </summary>
        public long AutoIncrementSeed { get; set; }
        /// <summary>
        /// 是否是索引
        /// </summary>
        public Boolean IsIndex { get; set; }
        /// <summary>
        /// 小数位数
        /// </summary>
        public int Digit { get; set; } = 0;
        #endregion
    }
}