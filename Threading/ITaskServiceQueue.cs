﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XiaoFeng.Event;

/****************************************************************
*  Copyright © (2022) www.fayelf.com All Rights Reserved.       *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@fayelf.com                                     *
*  Site : www.fayelf.com                                        *
*  Create Time : 2022-07-26 10:36:06                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Threading
{
    /// <summary>
    /// 任务服务队列 
    /// </summary>
    public interface ITaskServiceQueue<T>
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        string QueueName { get; }
        /// <summary>
        /// 任务错误事件
        /// </summary>
        event TaskQueueError<T> TaskQueueError;
        /// <summary>
        /// 任务成功事件
        /// </summary>
        event TaskQueueOk<T> TaskQueueOk;
        /// <summary>
        /// 队列是否为空
        /// </summary>
        Boolean IsEmpty { get; }
        /// <summary>
        /// 队列包含元素数量
        /// </summary>
        int Count { get; }
        /// <summary>
        /// 是否存在于队列
        /// </summary>
        /// <param name="t">对象</param>
        /// <returns></returns>
        Boolean Contains(T t);
        /// <summary>
        /// 加入到队列前边
        /// </summary>
        /// <param name="t">对象</param>
        /// <returns></returns>
        Task PrependWorkItem(T t);
        /// <summary>
        /// 加入队列
        /// </summary>
        /// <param name="t">对象</param>
        Task AddWorkItem(T t);
        /// <summary>
        /// 加入队列
        /// </summary>
        /// <param name="func">委托</param>
        Task AddWorkItem(Func<T> func);
        /// <summary>
        /// 加入到队列前边
        /// </summary>
        /// <param name="func">委托</param>
        /// <returns></returns>
        Task PrependWorkItem(Func<T> func);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="workItem">数据</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task ExecuteAsync(T workItem, CancellationToken cancellationToken);
        /// <summary>
        /// 取出队列中数据
        /// </summary>
        /// <returns></returns>
        Task<T> DequeueAsync();
        /// <summary>
        /// 停止消费任务
        /// </summary>
        Task StopAsync();
        /// <summary>
        /// 停止消费任务
        /// </summary>
        void Stop();
        /// <summary>
        /// 输出控制台信息
        /// </summary>
        /// <param name="msg">信息</param>
        void ConsoleWrite(string msg);
    }
}