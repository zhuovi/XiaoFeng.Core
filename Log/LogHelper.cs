﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Text;
using System.ComponentModel;
using XiaoFeng.Config;
using XiaoFeng.IO;
using XiaoFeng.Log;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using XiaoFeng.Threading;
/****************************************************************
*  Copyright © (2017) www.fayelf.com All Rights Reserved.       *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@fayelf.com                                     *
*  Site : www.fayelf.com                                        *
*  Create Time : 2017-10-25 11:59:42                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng
{
    /// <summary>
    /// 日志操作类
    /// Version : 2.5
    /// Update Date : 2019-09-20
    /// </summary>
    public static class LogHelper
    {
        #region 构造器
        /// <summary>
        /// 构造器
        /// </summary>
        static LogHelper()
        {
            Log = LogFactory.Create(typeof(Logger), "LogTask");
        }
        #endregion

        #region 属性
        /// <summary>
        /// 日志对象
        /// </summary>
        private static ILog Log { get; set; }
        /// <summary>
        /// 日志队列
        /// </summary>
        private static ITaskServiceQueue<LogData> LogTaskQueue = new LogTask();
        #endregion

        #region 方法

        #region 记录日志
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logData">日志对象</param>
        public static void WriteLog(LogData logData)
        {
            LogTaskQueue.AddWorkItem(logData);
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="DataSource">日志源</param>
        /// <param name="ClassName">日志类名</param>
        /// <param name="FunctionName">方法名</param>
        /// <param name="Message">日志信息</param>
        public static void WriteLog(LogType logType, string DataSource, string ClassName, string FunctionName, string Message)
        {
            WriteLog(new LogData
            {
                LogType = logType,
                DataSource = DataSource,
                ClassName = ClassName,
                FunctionName = FunctionName,
                Message = Message
            });
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="Message">日志信息</param>
        public static void WriteLog(LogType logType, string Message)
        {
            WriteLog(new LogData
            {
                LogType = logType,
                Message = Message
            });
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="Message">日志信息</param>
        public static void WriteLog(string Message)
        {
            WriteLog(new LogData
            {
                LogType = LogType.Info,
                Message = Message
            });
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="ex">错误信息</param>
        /// <param name="Message">信息</param>
        public static void WriteLog(Exception ex, string Message = "")
        {
            ex = ex ?? new Exception();
            WriteLog(new LogData
            {
                LogType = LogType.Error,
                Message = ex?.Message + (Message.IsNullOrEmpty() ? "" : "[自定义信息:' {0} ']".format(Message)),
                DataSource = ex?.Source,
                ClassName = ex?.TargetSite == null ? "" : ex?.TargetSite?.DeclaringType?.Name,
                FunctionName = ex?.TargetSite == null ? "" : ex?.TargetSite?.Name,
                StackTrace = ex?.StackTrace ?? "",
                Tracking = new StackTrace()
            });
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="ex">错误信息</param>
        /// <param name="Message">信息</param>
        public static void Error(Exception ex, string Message = "") => WriteLog(ex, Message);
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="Message">信息</param>
        public static void Info(string Message) => WriteLog(LogType.Info, Message);
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="Message">信息</param>
        public static void Debug(string Message) => WriteLog(LogType.Debug, Message);
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="Message">信息</param>
        public static void SQL(string Message) => WriteLog(LogType.SQL, Message);
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="Message">信息</param>
        public static void Warn(string Message) => WriteLog(LogType.Warn, Message);
        /// <summary>
        /// 任务日志
        /// </summary>
        /// <param name="Message">信息</param>
        public static void Task(string Message) => WriteLog(LogType.Task, Message);
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="Message">信息</param>
        /// <param name="logType">日志类型</param>
        public static void Record(string Message, LogType logType = LogType.Info) => WriteLog(new LogData
        {
            IsRecord = true,
            Message = Message,
            LogType = logType
        });
        #endregion
        #endregion
    }
}