using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/****************************************************************
*  Copyright © (2022) www.fayelf.com All Rights Reserved.       *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@fayelf.com                                     *
*  Site : www.fayelf.com                                        *
*  Create Time : 2022-12-05 17:25:27                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Redis
{
    /// <summary>
    /// Redis接口
    /// </summary>
    public interface IRedisBaseClient
    {
        #region KEY

        #region 删除key
        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="dbNum">库索引</param>
        /// <param name="keys">key集合</param>
        /// <returns>删除成功的数量</returns>
        int DelKey(int? dbNum, params string[] keys);
        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="keys">key集合</param>
        /// <returns>删除成功的数量</returns>
        int DelKey(params string[] keys);
        /// <summary>
        /// 删除key 异步
        /// </summary>
        /// <param name="dbNum">库索引</param>
        /// <param name="keys">key集合</param>
        /// <returns>删除成功的数量</returns>
        Task<int> DelKeyAsync(int? dbNum, params string[] keys);
        /// <summary>
        /// 删除key 异步
        /// </summary>
        /// <param name="keys">key集合</param>
        /// <returns>删除成功的数量</returns>
        Task<int> DelKeyAsync(params string[] keys);
        /// <summary>
        /// 获取key值 并删除 6.2.0后可用
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>删除key的值</returns>
        string GetDelKey(string key, int? dbNum = null);
        /// <summary>
        /// 获取key值 并删除 6.2.0后可用 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>删除key的值</returns>
        Task<string> GetDelKeyAsync(string key, int? dbNum = null);
        #endregion

        #region 序列化key
        /// <summary>
        /// 序列化key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        String DumpKey(string key, int? dbNum = null);
        /// <summary>
        /// 序列化key 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<String> DumpKeyAsync(string key, int? dbNum = null);
        #endregion

        #region 是否存在key
        /// <summary>
        /// 是否存在key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Boolean ExistsKey(string key, int? dbNum = null);
        /// <summary>
        /// 是否存在key 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<Boolean> ExistsKeyAsync(string key, int? dbNum = null);
        #endregion

        #region 设置过期时间
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="seconds">过期时长 单位为秒</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        int SetKeyExpireSeconds(string key, int seconds, int? dbNum = null);
        /// <summary>
        /// 设置过期时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="seconds">过期时长 单位为秒</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        Task<int> SetKeyExpireSecondsAsync(string key, int seconds, int? dbNum = null);
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="milliseconds">过期时长 单位为毫秒</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        int SetKeyExpireMilliseconds(string key, long milliseconds, int? dbNum = null);
        /// <summary>
        /// 设置过期时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="milliseconds">过期时长 单位为毫秒</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        Task<int> SetKeyExpireMillisecondsAsync(string key, long milliseconds, int? dbNum = null);
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="timestamp">过期时长 秒时间戳</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        int SetKeyExpireSecondsTimestamp(string key, int timestamp, int? dbNum = null);
        /// <summary>
        /// 设置过期时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="timestamp">过期时长 秒时间戳</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        Task<int> SetKeyExpireSecondsTimestampAsync(string key, int timestamp, int? dbNum = null);
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="timestamps">过期时长 毫秒时间戳</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        int SetKeyExpireMillisecondsTimestamp(string key, long timestamps, int? dbNum = null);
        /// <summary>
        /// 设置过期时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="timestamps">过期时长 毫秒时间戳</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        Task<int> SetKeyExpireMillisecondsTimestampAsync(string key, long timestamps, int? dbNum = null);
        #endregion

        #region 重命名key
        /// <summary>
        /// 重命名key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="newKey">新key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        Boolean RenameKey(string key, string newKey, int? dbNum = null);
        /// <summary>
        /// 重命名key 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="newKey">新key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        Task<Boolean> RenameKeyAsync(string key, string newKey, int? dbNum = null);
        /// <summary>
        /// 重命名key 当新key不存在时
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="newKey">新key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        Boolean RenameKeyNoExists(string key, string newKey, int? dbNum = null);
        /// <summary>
        /// 重命名key 当新key不存在时 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="newKey">新key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        Task<Boolean> RenameKeyNoExistsAsync(string key, string newKey, int? dbNum = null);
        #endregion

        #region 移动key
        /// <summary>
        /// 将当前数据库的 key 移动到给定的数据库 db 当中
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="destDbNum">目标库索引</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        Boolean MoveKey(string key, int destDbNum, int? dbNum = null);
        /// <summary>
        /// 将当前数据库的 key 移动到给定的数据库 db 当中 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="destDbNum">目标库索引</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        Task<Boolean> MoveKeyAsync(string key, int destDbNum, int? dbNum = null);
        #endregion

        #region 移除过期时间
        /// <summary>
        /// 移除 key 的过期时间，key 将持久保持
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        Boolean RemoveKeyExpire(string key, int? dbNum = null);
        /// <summary>
        /// 移除 key 的过期时间，key 将持久保持 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        Task<Boolean> RemoveKeyExpireAsync(string key, int? dbNum = null);
        #endregion

        #region 获取key剩余过期时间
        /// <summary>
        /// 以秒为单位，返回给定 key 的剩余生存时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回剩余时间</returns>
        int GetKeyExpireSeconds(string key, int? dbNum = null);
        /// <summary>
        /// 以秒为单位，返回给定 key 的剩余生存时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回剩余时间</returns>
        Task<int> GetKeyExpireSecondsAsync(string key, int? dbNum = null);
        /// <summary>
        /// 以毫秒为单位，返回给定 key 的剩余生存时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回剩余时间</returns>
        int GetKeyExpireMilliseconds(string key, int? dbNum = null);
        /// <summary>
        /// 以毫秒为单位，返回给定 key 的剩余生存时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回剩余时间</returns>
        Task<int> GetKeyExpireMillisecondsAsync(string key, int? dbNum = null);
        #endregion

        #region 从当前数据库中随机返回一个 key
        /// <summary>
        /// 从当前数据库中随机返回一个 key
        /// </summary>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回KEY</returns>
        string GetKeyRandom(int? dbNum = null);
        /// <summary>
        /// 从当前数据库中随机返回一个 key
        /// </summary>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回KEY</returns>
        Task<string> GetKeyRandomAsync(int? dbNum);
        #endregion

        #region 返回 key 所储存的值的类型
        /// <summary>
        /// 返回 key 所储存的值的类型
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回数据类型</returns>
        RedisKeyType GetKeyType(string key, int? dbNum = null);
        /// <summary>
        /// 返回 key 所储存的值的类型
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回数据类型</returns>
        Task<RedisKeyType> GetKeyTypeAsync(string key, int? dbNum = null);
        #endregion

        #region 查找key
        /// <summary>
        /// 查找数据库中的数据库键
        /// </summary>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="start">开始位置</param>
        /// <param name="count">返回条数</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        List<string> GetKeys(string pattern, int start = 0, int count = 10, int? dbNum = null);
        /// <summary>
        /// 查找数据库中的数据库键 异步
        /// </summary>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="start">开始位置</param>
        /// <param name="count">遍历条数</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<List<string>> GetKeysAsync(string pattern, int start = 0, int count = 10, int? dbNum = null);
        /// <summary>
        /// 查找所有符合给定模式(pattern)的 key
        /// </summary>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        List<string> SearchKeys(string pattern, int? dbNum = null);
        /// <summary>
        /// 查找所有符合给定模式(pattern)的 key 异步
        /// </summary>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<List<string>> SearchKeysAsync(string pattern, int? dbNum = null);
        #endregion

        #region 复制 Key
        /// <summary>
        /// 复制 Key 6.2.0版本
        /// </summary>
        /// <param name="key">源 key</param>
        /// <param name="destKey">目标 key</param>
        /// <param name="isReplace">存在是否替换</param>
        /// <param name="destDbNum">目标库索引</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Boolean CopyKey(string key, string destKey, Boolean isReplace = false, int? destDbNum = null, int? dbNum = null);
        /// <summary>
        /// 复制 Key 异步 6.2.0版本
        /// </summary>
        /// <param name="key">源 key</param>
        /// <param name="destKey">目标 key</param>
        /// <param name="isReplace">存在是否替换</param>
        /// <param name="destDbNum">目标库索引</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<Boolean> CopyKeyAsync(string key, string destKey, Boolean isReplace = false, int? destDbNum = null, int? dbNum = null);
        #endregion

        #endregion

        #region 基础

        #region 验证密码
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Boolean Auth(string password);
        /// <summary>
        /// 验证密码 异步
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<Boolean> AuthAsync(string password);
        #endregion

        #region PING
        /// <summary>
        /// PING
        /// </summary>
        /// <returns></returns>
        Boolean Ping();
        /// <summary>
        /// PING 异步
        /// </summary>
        /// <returns></returns>
        Task<Boolean> PingAsync();
        #endregion

        #region 打印字符串
        /// <summary>
        /// 打印字符串
        /// </summary>
        /// <param name="echoStr">要打印的字符串</param>
        /// <returns></returns>
        Boolean Echo(string echoStr);
        /// <summary>
        /// 打印字符串 异步
        /// </summary>
        /// <param name="echoStr">要打印的字符串</param>
        /// <returns></returns>
        Task<Boolean> EchoAsync(string echoStr);
        #endregion

        #region 退出
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        Boolean Quit();
        /// <summary>
        /// 退出 异步
        /// </summary>
        /// <returns></returns>
        Task<Boolean> QuitAsync();
        #endregion

        #region 选择数据库
        /// <summary>
        /// 选择数据库
        /// </summary>
        /// <param name="dbNum">库索引</param>
        /// <returns>是否设置成功</returns>
        Boolean Select(int dbNum = 0);
        /// <summary>
        /// 选择数据库 异步
        /// </summary>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<Boolean> SelectAsync(int dbNum = 0);
        #endregion

        #endregion

        #region 哈希(Hash)

        #region 设置Hash
        /// <summary>
        /// 将哈希表 key 中的字段 field 的值设为 value 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>是否设置成功</returns>
        Boolean SetHash<T>(string key, string fieldName, T value, int? dbNum = null);
        /// <summary>
        /// 将哈希表 key 中的字段 field 的值设为 value 异步
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>是否设置成功</returns>
        Task<Boolean> SetHashAsync<T>(string key, string fieldName, T value, int? dbNum = null);
        /// <summary>
        /// 只有在字段 field 不存在时，设置哈希表字段的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Boolean SetHashNoExists<T>(string key, string fieldName, T value, int? dbNum = null);
        /// <summary>
        /// 只有在字段 field 不存在时，设置哈希表字段的值 异步
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>是否设置成功</returns>
        Task<Boolean> SetHashNoExistsAsync<T>(string key, string fieldName, T value, int? dbNum = null);
        /// <summary>
        /// 批量设置Hash
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="values">字段值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Boolean SetHash(string key, Dictionary<string, object> values, int? dbNum = null);
        /// <summary>
        /// 批量设置Hash 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="values">字段值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<Boolean> SetHashAsync(string key, Dictionary<string, object> values, int? dbNum = null);
        #endregion

        #region 获取Hash
        /// <summary>
        /// 获取Hash值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        T GetHash<T>(string key, string field, int? dbNum = null);
        /// <summary>
        /// 获取Hash值 异步
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<T> GetHashAsync<T>(string key, string field, int? dbNum = null);
        /// <summary>
        /// 获取Hash值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>Hash值</returns>
        string GetHash(string key, string field, int? dbNum = null);
        /// <summary>
        /// 获取Hash值 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>Hash值</returns>
        Task<string> GetHashAsync(string key, string field, int? dbNum = null);
        /// <summary>
        /// 获取在哈希表中指定 key 的所有字段和值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Dictionary<string, string> GetHash(string key, int? dbNum = null);
        /// <summary>
        /// 获取在哈希表中指定 key 的所有字段和值 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetHashAsync(string key, int? dbNum = null);
        /// <summary>
        /// 获取在哈希表中指定 key 的所有字段和值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Dictionary<string, T> GetHash<T>(string key, int? dbNum = null);
        /// <summary>
        /// 获取在哈希表中指定 key 的所有字段和值 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<Dictionary<string, T>> GetHashAsync<T>(string key, int? dbNum = null);
        /// <summary>
        /// 获取所有哈希表中的字段
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>所有哈希表中的字段</returns>
        List<string> GetHashKeys(string key, int? dbNum = null);
        /// <summary>
        /// 获取所有哈希表中的字段 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>所有哈希表中的字段</returns>
        Task<List<string>> GetHashKeysAsync(string key, int? dbNum = null);
        /// <summary>
        /// 获取所有哈希表中的字段
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>所有哈希表中的字段</returns>
        List<string> GetHashValues(string key, int? dbNum = null);
        /// <summary>
        /// 获取所有哈希表中的字段 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>所有哈希表中的字段</returns>
        Task<List<string>> GetHashValuesAsync(string key, int? dbNum = null);
        /// <summary>
        /// 获取哈希表中字段的数量
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>哈希表中字段的数量</returns>
        int GetHashKeysCount(string key, int? dbNum = null);
        /// <summary>
        /// 获取哈希表中字段的数量 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>哈希表中字段的数量</returns>
        Task<int> GetHashKeysCountAsync(string key, int? dbNum = null);
        /// <summary>
        /// 获取所有给定字段的值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <param name="fields">字段</param>
        /// <returns>返回所有给定字段的值</returns>
        List<string> GetHashValues(string key, int? dbNum, params object[] fields);
        /// <summary>
        /// 获取所有给定字段的值 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <param name="fields">字段</param>
        /// <returns>返回所有给定字段的值</returns>
        Task<List<string>> GetHashValuesAsync(string key, int? dbNum, params object[] fields);
        /// <summary>
        /// 获取所有给定字段的值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="fields">字段</param>
        /// <returns>返回字段值</returns>
        List<string> GetHashValues(string key, params object[] fields);
        /// <summary>
        /// 获取所有给定字段的值 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="fields">字段</param>
        /// <returns>返回字段值</returns>
        Task<List<string>> GetHashValuesAsync(string key, params object[] fields);
        /// <summary>
        /// 查找Hash中字段名
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="start">开始位置</param>
        /// <param name="count">遍历条数</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>字段名和值</returns>
        Dictionary<string, string> SearchHashMember(string key, string pattern, int start = 0, int count = 10, int? dbNum = null);
        /// <summary>
        /// 查找Hash中字段名
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="start">开始位置</param>
        /// <param name="count">遍历条数</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>字段名和值</returns>
        Dictionary<string, T> SearchHashMember<T>(string key, string pattern, int start = 0, int count = 10, int? dbNum = null);
        /// <summary>
        /// 查找Hash中字段名 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="start">开始位置</param>
        /// <param name="count">遍历条数</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>字段名和值</returns>
        Task<Dictionary<string, string>> SearchHashMemberAsync(string key, string pattern, int start = 0, int count = 10, int? dbNum = null);
        /// <summary>
        /// 查找Hash中字段名 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="start">开始位置</param>
        /// <param name="count">遍历条数</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>字段名和值</returns>
        Task<Dictionary<string, T>> SearchHashMemberAsync<T>(string key, string pattern, int start = 0, int count = 10, int? dbNum = null);
        #endregion

        #region 删除Hash
        /// <summary>
        /// 删除Hash
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        int DelHash(string key, int? dbNum, params object[] fields);
        /// <summary>
        /// 删除Hash 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        Task<int> DelHashAsync(string key, int? dbNum, params object[] fields);
        /// <summary>
        /// 删除Hash
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        int DelHash(string key, params object[] fields);
        /// <summary>
        /// 删除Hash 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        Task<int> DelHashAsync(string key, params object[] fields);
        #endregion

        #region 是否存在Hash
        /// <summary>
        /// 是否存在Hash
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Boolean ExistsHash(string key, string field, int? dbNum = null);
        /// <summary>
        /// 是否存在Hash 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        Task<Boolean> ExistsHashAsync(string key, string field, int? dbNum = null);
        #endregion

        #region 为哈希表 key 中的指定字段的整数值加上增量 increment
        /// <summary>
        /// 为哈希表 key 中的指定字段的整数值加上增量 increment
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="increment">增量值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>增加后的值</returns>
        long HashIncrement(string key, string field, long increment, int? dbNum = null);
        /// <summary>
        /// 为哈希表 key 中的指定字段的整数值加上增量 increment 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="increment">增量值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>增加后的值</returns>
        Task<long> HashIncrementAsync(string key, string field, long increment, int? dbNum = null);
        /// <summary>
        /// 为哈希表 key 中的指定字段的整数值加上增量 increment
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="increment">增量值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>增加后的值</returns>
        float HashIncrement(string key, string field, float increment, int? dbNum = null);
        /// <summary>
        /// 为哈希表 key 中的指定字段的整数值加上增量 increment 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="increment">增量值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>增加后的值</returns>
        Task<float> HashIncrementAsync(string key, string field, float increment, int? dbNum = null);
        /// <summary>
        /// 为哈希表 key 中的指定字段的整数值加上增量 increment
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="increment">增量值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>增加后的值</returns>
        double HashIncrement(string key, string field, double increment, int? dbNum = null);
        /// <summary>
        /// 为哈希表 key 中的指定字段的整数值加上增量 increment 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">字段名</param>
        /// <param name="increment">增量值</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>增加后的值</returns>
        Task<double> HashIncrementAsync(string key, string field, double increment, int? dbNum = null);
        #endregion

        #endregion
    }
}