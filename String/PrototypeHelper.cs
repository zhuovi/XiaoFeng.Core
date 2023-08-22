﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XiaoFeng.Data.SQL;
/****************************************************************
*  Copyright © (2017) www.fayelf.com All Rights Reserved.       *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@fayelf.com                                     *
*  Site : www.fayelf.com                                        *
*  Create Time : 2017-10-31 14:18:38                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public partial class PrototypeHelper
    {
        #region 扩展String IndexOf
        /// <summary>
        /// 扩展String IndexOf
        /// </summary>
        /// <param name="_">字符串</param>
        /// <param name="value">子字符串</param>
        /// <returns></returns>
        public static int IndexOfX(this string _, string value) =>
#if NETSTANDARD2_1
            _.AsSpan().IndexOf(value.AsSpan());
#else
            _.IndexOf(value);
#endif
        #endregion

        #region 扩展String LastIndexOf
        /// <summary>
        /// 扩展String LastIndexOf
        /// </summary>
        /// <param name="_">字符串</param>
        /// <param name="value">子字符串</param>
        /// <returns></returns>
        public static int LastIndexOfX(this string _, string value) =>
#if NETSTANDARD2_1
            _.AsSpan().LastIndexOf(value.AsSpan());
#else
            _.LastIndexOf(value);
#endif
        #endregion

        #region 扩展String Substring
        /// <summary>
        /// 扩展String Substring
        /// </summary>
        /// <param name="_">字符串</param>
        /// <param name="start">开始位置</param>
        /// <param name="length">结束位置</param>
        /// <returns></returns>
        public static string SubstringX(this string _, int start, int length)
        {
            if (length <= 0 || length < start) return string.Empty;
            return
#if NETSTANDARD2_1
            _.AsSpan().Slice(start, length).ToString()
#else
            _.Substring(start, length)
#endif
                ;
        }
        #endregion

        #region 扩展String Substring
        /// <summary>
        /// 扩展String Substring
        /// </summary>
        /// <param name="_">字符串</param>
        /// <param name="start">开始位置</param>
        /// <returns></returns>
        public static string SubstringX(this string _, int start) => _.SubstringX(start, _.Length - start);
        #endregion

        #region 扩展String Replace
        /// <summary>
        /// 扩展String Replace
        /// </summary>
        /// <param name="_">字符串</param>
        /// <param name="oldValue">子字符串</param>
        /// <param name="newValue">替换后子符串</param>
        /// <returns></returns>
        public static string ReplaceX(this string _, string oldValue, string newValue)
        {
#if NETSTANDARD2_1
            var list = new List<string>();
            var strSpan = _.AsSpan();
            var subSpan = oldValue.AsSpan();
            var n = strSpan.IndexOf(subSpan);
            while (n > -1)
            {
                list.Add(strSpan.Slice(0, n).ToString());
                strSpan = strSpan.Slice(n + subSpan.Length);
                n = strSpan.IndexOf(subSpan);
            }
            return list.Join(newValue);
#else
            return _.Replace(oldValue, newValue);
#endif
        }
        #endregion

        #region 字符串是否闭合
        /// <summary>
        /// 字符串是否闭合
        /// </summary>
        /// <param name="_">字符串</param>
        /// <returns></returns>
        public static Boolean IsClosure(this string _)
        {
            var index = -1;
            Pair Current = null;
            int fBreak = 0;
            for (var i = 0; i < _.Length; i++)
            {
                var c = _[i];
                switch (c)
                {
                    case '{':
                    case '[':
                    case '(':
                    case '<':
                        if (Current == null)
                            Current = new Pair(c);
                        else
                        {
                            var cp = new Pair(c);
                            cp.ParentPair = Current;
                            if (Current.ChildPair == null)
                                Current.ChildPair = new List<Pair> { cp };
                            else
                                Current.ChildPair.Add(cp);
                            Current = cp;
                        }
                        break;
                    case '}':
                    case ']':
                    case ')':
                    case '>':
                        if (Current == null)
                        {
                            fBreak = -1;
                            break;
                        }
                        Current.IsPair = true;
                        if (Current.ParentPair == null)
                        {
                            fBreak = 1;
                            index = i;
                            break;
                        }
                        Current = Current.ParentPair;
                        break;
                    default:

                        break;
                }
                if (fBreak != 0) break;
            }
            if (Current == null) return false;
            return index > -1 ? Current.IsPair && _.Substring(index + 1).IsNullOrEmpty() : Current.IsPair;
        }
        #endregion

#if NETSTANDARD2_0
        /// <summary>
        /// 根据提供的字符分隔符将字符串拆分为多个子字符串。
        /// </summary>
        /// <param name="_">字符串</param>
        /// <param name="separator">一个字符，用于分隔此字符串中的子字符串</param>
        /// <param name="options">枚举值之一，用于确定拆分操作是否应省略返回值中的空子字符串</param>
        /// <returns>一个数组，其元素包含此实例中的子字符串，这些子字符串由 separator 分隔</returns>
        public static string[] Split(this String _, char separator, StringSplitOptions options = StringSplitOptions.None)
        {
            if (_.IsNullOrEmpty()) return Array.Empty<string>();
            return _.Split(new char[] { separator }, options);
        }
#endif
    }
}