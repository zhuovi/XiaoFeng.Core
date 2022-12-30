using System.Collections.Generic;
using System.Threading.Tasks;
using System;

/****************************************************************
*  Copyright © (2022) www.fayelf.com All Rights Reserved.       *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@fayelf.com                                     *
*  Site : www.fayelf.com                                        *
*  Create Time : 2022-12-26 16:02:01                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Redis
{
    /// <summary>
    /// Redis 客户端操作类
    /// </summary>
    public partial class RedisBaseClient : Disposable
    {
        #region KEY

        #region 删除key
        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="dbNum">库索引</param>
        /// <param name="keys">key集合</param>
        /// <returns>删除成功的数量</returns>
        public int DelKey(int? dbNum, params string[] keys)
        {
            if (keys.Length == 0) return -1;
            return this.Execute(CommandType.DEL, dbNum, result => result.OK ? result.Value.ToInt() : -1, keys);
        }
        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="keys">key集合</param>
        /// <returns>删除成功的数量</returns>
        public int DelKey(params string[] keys)
        {
            if (keys.Length == 0) return -1;
            return this.DelKey(null, keys);
        }
        /// <summary>
        /// 删除key 异步
        /// </summary>
        /// <param name="dbNum">库索引</param>
        /// <param name="keys">key集合</param>
        /// <returns>删除成功的数量</returns>
        public async Task<int> DelKeyAsync(int? dbNum, params string[] keys)
        {
            if (keys.Length == 0) return -1;
            return await this.ExecuteAsync(CommandType.DEL, dbNum, async result => await Task.FromResult(result.OK ? result.Value.ToInt() : -1), keys);
        }
        /// <summary>
        /// 删除key 异步
        /// </summary>
        /// <param name="keys">key集合</param>
        /// <returns>删除成功的数量</returns>
        public async Task<int> DelKeyAsync(params string[] keys)
        {
            if (keys.Length == 0) return -1;
            return await this.DelKeyAsync(null, keys);
        }
        /// <summary>
        /// 获取key值 并删除 6.2.0后可用
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>删除key的值</returns>
        public string GetDelKey(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return string.Empty;
            return this.Execute(CommandType.GETDEL, dbNum, result => result.Value.ToString(), key);
        }
        /// <summary>
        /// 获取key值 并删除 6.2.0后可用 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>删除key的值</returns>
        public async Task<string> GetDelKeyAsync(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return string.Empty;
            return await this.ExecuteAsync(CommandType.GETDEL, dbNum, async result => await Task.FromResult(result.Value.ToString()), key);
        }
        #endregion

        #region 序列化key
        /// <summary>
        /// 序列化key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        public String DumpKey(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return String.Empty;
            return this.Execute(CommandType.DUMP, dbNum, result => (string)result.Value, key);
        }
        /// <summary>
        /// 序列化key 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        public async Task<String> DumpKeyAsync(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return String.Empty;
            return await this.ExecuteAsync(CommandType.DUMP, dbNum, async result => await Task.FromResult((string)result.Value), key);
        }
        #endregion

        #region 是否存在key
        /// <summary>
        /// 是否存在key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        public Boolean ExistsKey(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return false;
            return this.Execute(CommandType.EXISTS, dbNum, result => result.Value.ToInt() > 0, key);
        }
        /// <summary>
        /// 是否存在key 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        public async Task<Boolean> ExistsKeyAsync(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return false;
            return await this.ExecuteAsync(CommandType.EXISTS, dbNum, async result => await Task.FromResult(result.Value.ToInt() > 0), key);
        }
        #endregion

        #region 设置过期时间
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="seconds">过期时长 单位为秒</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        public int SetKeyExpireSeconds(string key, int seconds, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return this.Execute(CommandType.EXPIRE, dbNum, result => result.Value.ToInt(), key, seconds);
        }
        /// <summary>
        /// 设置过期时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="seconds">过期时长 单位为秒</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        public async Task<int> SetKeyExpireSecondsAsync(string key, int seconds, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return await this.ExecuteAsync(CommandType.EXPIRE, dbNum, result => Task.FromResult(result.Value.ToInt()), key, seconds);
        }
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="milliseconds">过期时长 单位为毫秒</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        public int SetKeyExpireMilliseconds(string key, long milliseconds, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return this.Execute(CommandType.PEXPIRE, dbNum, result => result.Value.ToInt(), key, milliseconds);
        }
        /// <summary>
        /// 设置过期时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="milliseconds">过期时长 单位为毫秒</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        public async Task<int> SetKeyExpireMillisecondsAsync(string key, long milliseconds, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return await this.ExecuteAsync(CommandType.PEXPIRE, dbNum, result => Task.FromResult(result.Value.ToInt()), key, milliseconds);
        }
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="timestamp">过期时长 秒时间戳</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        public int SetKeyExpireSecondsTimestamp(string key, int timestamp, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return this.Execute(CommandType.EXPIREAT, dbNum, result => result.Value.ToInt(), key, timestamp);
        }
        /// <summary>
        /// 设置过期时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="timestamp">过期时长 秒时间戳</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        public async Task<int> SetKeyExpireSecondsTimestampAsync(string key, int timestamp, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return await this.ExecuteAsync(CommandType.EXPIREAT, dbNum, result => Task.FromResult(result.Value.ToInt()), key, timestamp);
        }
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="timestamps">过期时长 毫秒时间戳</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        public int SetKeyExpireMillisecondsTimestamp(string key, long timestamps, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return this.Execute(CommandType.PEXPIREAT, dbNum, result => result.Value.ToInt(), key, timestamps);
        }
        /// <summary>
        /// 设置过期时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="timestamps">过期时长 毫秒时间戳</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>设置成功数量 0是不存在 1是设置成功</returns>
        public async Task<int> SetKeyExpireMillisecondsTimestampAsync(string key, long timestamps, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return await this.ExecuteAsync(CommandType.PEXPIREAT, dbNum, result => Task.FromResult(result.Value.ToInt()), key, timestamps);
        }
        #endregion

        #region 重命名key
        /// <summary>
        /// 重命名key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="newKey">新key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        public Boolean RenameKey(string key, string newKey, int? dbNum = null)
        {
            if (key.IsNullOrEmpty() || newKey.IsNullOrEmpty()) return false;
            return this.Execute(CommandType.RENAME, dbNum, result => result.OK, key, newKey);
        }
        /// <summary>
        /// 重命名key 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="newKey">新key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        public async Task<Boolean> RenameKeyAsync(string key, string newKey, int? dbNum = null)
        {
            if (key.IsNullOrEmpty() || newKey.IsNullOrEmpty()) return false;
            return await this.ExecuteAsync(CommandType.RENAME, dbNum, async result => await Task.FromResult(result.OK), key, newKey);
        }
        /// <summary>
        /// 重命名key 当新key不存在时
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="newKey">新key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        public Boolean RenameKeyNoExists(string key, string newKey, int? dbNum = null)
        {
            if (key.IsNullOrEmpty() || newKey.IsNullOrEmpty()) return false;
            return this.Execute(CommandType.RENAMENX, dbNum, result => result.OK, key, newKey);
        }
        /// <summary>
        /// 重命名key 当新key不存在时 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="newKey">新key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        public async Task<Boolean> RenameKeyNoExistsAsync(string key, string newKey, int? dbNum = null)
        {
            if (key.IsNullOrEmpty() || newKey.IsNullOrEmpty()) return false;
            return await this.ExecuteAsync(CommandType.RENAMENX, dbNum, async result => await Task.FromResult(result.OK), key, newKey);
        }
        #endregion

        #region 移动key
        /// <summary>
        /// 将当前数据库的 key 移动到给定的数据库 db 当中
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="destDbNum">目标库索引</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        public Boolean MoveKey(string key, int destDbNum, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return false;
            if (destDbNum < 0) destDbNum = 0;
            if (dbNum.HasValue && dbNum.Value == destDbNum) return false;
            return this.Execute(CommandType.MOVE, dbNum, result => result.OK, key, destDbNum);
        }
        /// <summary>
        /// 将当前数据库的 key 移动到给定的数据库 db 当中 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="destDbNum">目标库索引</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        public async Task<Boolean> MoveKeyAsync(string key, int destDbNum, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return false;
            if (destDbNum < 0) destDbNum = 0;
            if (dbNum.HasValue && dbNum.Value == destDbNum) return false;
            return await this.ExecuteAsync(CommandType.MOVE, dbNum, async result => await Task.FromResult(result.OK), key, destDbNum);
        }
        #endregion

        #region 移除过期时间
        /// <summary>
        /// 移除 key 的过期时间，key 将持久保持
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        public Boolean RemoveKeyExpire(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return false;
            return this.Execute(CommandType.PERSIST, dbNum, result => result.OK, key);
        }
        /// <summary>
        /// 移除 key 的过期时间，key 将持久保持 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回成功状态</returns>
        public async Task<Boolean> RemoveKeyExpireAsync(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return false;
            return await this.ExecuteAsync(CommandType.PERSIST, dbNum, async result => await Task.FromResult(result.OK), key);
        }
        #endregion

        #region 获取key剩余过期时间
        /// <summary>
        /// 以秒为单位，返回给定 key 的剩余生存时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回剩余时间</returns>
        public int GetKeyExpireSeconds(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return this.Execute(CommandType.TTL, dbNum, result => result.OK ? (int)result.Value : -1, key);
        }
        /// <summary>
        /// 以秒为单位，返回给定 key 的剩余生存时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回剩余时间</returns>
        public async Task<int> GetKeyExpireSecondsAsync(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return await this.ExecuteAsync(CommandType.TTL, dbNum, async result => await Task.FromResult(result.OK ? (int)result.Value : -1), key);
        }
        /// <summary>
        /// 以毫秒为单位，返回给定 key 的剩余生存时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回剩余时间</returns>
        public int GetKeyExpireMilliseconds(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return this.Execute(CommandType.PTTL, dbNum, result => result.OK ? (int)result.Value : -1, key);
        }
        /// <summary>
        /// 以毫秒为单位，返回给定 key 的剩余生存时间 异步
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回剩余时间</returns>
        public async Task<int> GetKeyExpireMillisecondsAsync(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return -1;
            return await this.ExecuteAsync(CommandType.PTTL, dbNum, async result => await Task.FromResult(result.OK ? (int)result.Value : -1), key);
        }
        #endregion

        #region 从当前数据库中随机返回一个 key
        /// <summary>
        /// 从当前数据库中随机返回一个 key
        /// </summary>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回KEY</returns>
        public string GetKeyRandom(int? dbNum = null) => this.Execute(CommandType.RANDOMKEY, dbNum, result => result.Value.ToString());
        /// <summary>
        /// 从当前数据库中随机返回一个 key
        /// </summary>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回KEY</returns>
        public async Task<string> GetKeyRandomAsync(int? dbNum) => await this.ExecuteAsync(CommandType.RANDOMKEY, dbNum, async result => await Task.FromResult(result.Value.ToString()));
        #endregion

        #region 返回 key 所储存的值的类型
        /// <summary>
        /// 返回 key 所储存的值的类型
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回数据类型</returns>
        public RedisKeyType GetKeyType(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return RedisKeyType.Error;
            return this.Execute(CommandType.TYPE, dbNum, result => (RedisKeyType)result.Value, key);
        }
        /// <summary>
        /// 返回 key 所储存的值的类型
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dbNum">库索引</param>
        /// <returns>返回数据类型</returns>
        public async Task<RedisKeyType> GetKeyTypeAsync(string key, int? dbNum = null)
        {
            if (key.IsNullOrEmpty()) return RedisKeyType.Error;
            return await this.ExecuteAsync(CommandType.TYPE, dbNum, async result => await Task.FromResult((RedisKeyType)result.Value), key);
        }
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
        public List<string> GetKeys(string pattern, int start = 0, int count = 100, int? dbNum = null) => this.Execute(CommandType.SCAN, dbNum, result => result.Value.ToList<string>(), start, "MATCH", pattern, "COUNT", count);
        /// <summary>
        /// 查找数据库中的数据库键 异步
        /// </summary>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="start">开始位置</param>
        /// <param name="count">遍历条数</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        public async Task<List<string>> GetKeysAsync(string pattern, int start = 0, int count = 10, int? dbNum = null) => await this.ExecuteAsync(CommandType.SCAN, dbNum, async result => await Task.FromResult(result.Value.ToList<string>()), start, "MATCH", pattern, "COUNT", count);
        /// <summary>
        /// 查找所有符合给定模式(pattern)的 key
        /// </summary>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        public List<string> SearchKeys(string pattern, int? dbNum = null) => this.Execute(CommandType.KEYS, dbNum, result => result.Value.ToList<string>(), pattern);
        /// <summary>
        /// 查找所有符合给定模式(pattern)的 key 异步
        /// </summary>
        /// <param name="pattern">模式 支持*和?</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        public async Task<List<string>> SearchKeysAsync(string pattern, int? dbNum = null) => await this.ExecuteAsync(CommandType.KEYS, dbNum, async result => await Task.FromResult(result.Value.ToList<string>()), pattern);
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
        public Boolean CopyKey(string key, string destKey, Boolean isReplace = false, int? destDbNum = null, int? dbNum = null)
        {
            if (key.IsNullOrEmpty() || destKey.IsNullOrEmpty()) return false;
            var list = new List<object>() { key, destKey };
            if (destDbNum.HasValue && destDbNum.Value > 0)
            {
                list.Add("DB");
                list.Add(destDbNum.Value);
            }
            if (isReplace) list.Add("REPLACE");
            return this.Execute(CommandType.COPY, dbNum, result => result.OK, list.ToArray());
        }
        /// <summary>
        /// 复制 Key 异步 6.2.0版本
        /// </summary>
        /// <param name="key">源 key</param>
        /// <param name="destKey">目标 key</param>
        /// <param name="isReplace">存在是否替换</param>
        /// <param name="destDbNum">目标库索引</param>
        /// <param name="dbNum">库索引</param>
        /// <returns></returns>
        public async Task<Boolean> CopyKeyAsync(string key, string destKey, Boolean isReplace = false, int? destDbNum = null, int? dbNum = null)
        {
            if (key.IsNullOrEmpty() || destKey.IsNullOrEmpty()) return false;
            var list = new List<object>() { key, destKey };
            if (destDbNum.HasValue && destDbNum.Value > 0)
            {
                list.Add("DB");
                list.Add(destDbNum.Value);
            }
            if (isReplace) list.Add("REPLACE");
            return await this.ExecuteAsync(CommandType.COPY, dbNum, async result => await Task.FromResult(result.OK), list.ToArray());
        }
        #endregion

        #endregion
    }
}