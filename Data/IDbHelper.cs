﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
/****************************************************************
*  Copyright © (2017) www.fayelf.com All Rights Reserved.       *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@fayelf.com                                     *
*  Site : www.fayelf.com                                        *
*  Create Time : 2017-08-10 11:56:36                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Data
{
    /// <summary>
    /// 获取数据库表,表字段接口
    /// Verstion : 1.0.0
    /// Create Time : 2017/8/10 11:56:36
    /// Update Time : 2017/8/10 11:56:36
    /// </summary>
    public interface IDbHelper
    {
        /// <summary>
        /// 驱动类型
        /// </summary>
        DbProviderType ProviderType { get; set; }
        /// <summary>
        /// 数据库连接串配置
        /// </summary>
        ConnectionConfig ConnConfig { get; set; }
        /// <summary>
        /// 获取当前库的所有表
        /// </summary>
        /// <returns></returns>
        List<string> GetTables();
        /// <summary>
        /// 获取当前库的所有视图
        /// </summary>
        /// <returns></returns>
        List<ViewAttribute> GetViews();
        /// <summary>
        /// 获取当前库的所有存储过程
        /// </summary>
        /// <returns></returns>
        List<string> GetProcedures();
        /// <summary>
        /// 获取当前表的所有字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        List<DataColumns> GetColumns(string tableName);
        /// <summary>
        /// 获取当前表的所有字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        DataColumnCollection GetDataColumns(string tableName);
        /// <summary>
        /// 当前表的所有索引
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <returns></returns>
        List<TableIndexAttribute> GetTableIndexs(string tbName);
        /// <summary>
        /// 是否存在表或视图
        /// </summary>
        /// <param name="tableName">表或视图名</param>
        /// <param name="modelType">类型</param>
        /// <returns></returns>
        Boolean ExistsTable(string tableName, ModelType modelType = ModelType.Table);
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="modelType">表model类型</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        Boolean CreateTable(Type modelType, string tableName = "");
        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="modelType">类型</param>
        /// <param name="viewName">视图名称</param>
        /// <returns></returns>
        Boolean CreateView(Type modelType, string viewName = "");
        /// <summary>
        /// 创建表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        Boolean CreateTable<T>(string tableName = "");
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="Columns">显示列</param>
        /// <param name="Condition">条件</param>
        /// <param name="OrderColumnName">排序字段</param>
        /// <param name="OrderType">排序类型ASC或DESC</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">一页多少条</param>
        /// <param name="PageCount">共多少页</param>
        /// <param name="Counts">共多少条</param>
        /// <param name="PrimaryKey">主键</param>
        /// <returns></returns>
        DataTable Select(string tableName, string Columns, string Condition, string OrderColumnName, string OrderType, int PageIndex, int PageSize, out int PageCount, out int Counts, string PrimaryKey = "ID");
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="Columns">显示列</param>
        /// <param name="Condition">条件</param>
        /// <param name="OrderColumnName">排序字段</param>
        /// <param name="OrderType">排序类型ASC或DESC</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">一页多少条</param>
        /// <param name="PageCount">共多少页</param>
        /// <param name="Counts">共多少条</param>
        /// <param name="PrimaryKey">主键</param>
        /// <returns></returns>
        List<T> Select<T>(string tableName, string Columns, string Condition, string OrderColumnName, string OrderType, int PageIndex, int PageSize, out int PageCount, out int Counts, string PrimaryKey = "ID");
    }
}