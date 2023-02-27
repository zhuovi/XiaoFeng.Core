# XiaoFeng.Core

![fayelf](https://user-images.githubusercontent.com/16105174/197918392-29d40971-a8a2-4be4-ac17-323f1d0bed82.png)

![GitHub top language](https://img.shields.io/github/languages/top/zhuovi/xiaofeng.core?logo=github)
![GitHub License](https://img.shields.io/github/license/zhuovi/xiaofeng.core?logo=github)
![Nuget Downloads](https://img.shields.io/nuget/dt/xiaofeng.core?logo=nuget)
![Nuget](https://img.shields.io/nuget/v/xiaofeng.core?logo=nuget)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/xiaofeng.core?label=dev%20nuget&logo=nuget)

Nuget：XiaoFeng.Core

| QQ群号 | QQ群 | 公众号 |
| :----:| :----: | :----: |
| 748408911  | ![QQ 群](https://user-images.githubusercontent.com/16105174/198058269-0ea5928c-a2fc-4049-86da-cca2249229ae.png) | ![畅聊了个科技](https://user-images.githubusercontent.com/16105174/198059698-adbf29c3-60c2-4c76-b894-21793b40cf34.jpg) |

源码： https://github.com/zhuovi/xiaofeng.core

教程： https://www.yuque.com/fayelf/xiaofeng

C#公用核心基础库,包含了Json,Xml,ADO.NET数据库操作兼容以下数据库（SQLSERVER,MYSQL,ORACLE,达梦,SQLITE,ACCESS,OLEDB,ODBC等数十种数据库）,正则表达式,QueryableX(ORM)和EF无缝对接,网络日志,调度,IO操作,加密算法(AES,DES,DES3,MD5,RSA,RC4,SHA等常用加密算法),超级好用的配置管理器,应用池,类型转换等功能。

## XiaoFeng.Core

XiaoFeng.Core generator with [XiaoFeng.Core](https://github.com/zhuovi/XiaoFeng.Core).

## Install

.NET CLI

```
$ dotnet add package XiaoFeng.Core --version 2.0.4
```

Package Manager

```
PM> Install-Package XiaoFeng.Core --Version 2.0.4
```

PackageReference

```
<PackageReference Include="XiaoFeng.Core" Version="2.0.4" />
```
Paket CLI

```
> paket add XiaoFeng.Core --version 2.0.4
```

Script & Interactive

```
> #r "nuget: XiaoFeng.Core, 2.0.4"
```

Cake

```
// Install XiaoFeng.Core as a Cake Addin
#addin nuget:?package=XiaoFeng.Core&version=2.0.4

// Install XiaoFeng.Core as a Cake Tool
#tool nuget:?package=XiaoFeng.Core&version=2.0.4
```
# XiaoFeng 扩展方法

## 万能的类型转换扩展方法 ToCast<T>()

当前方法可转换任何值类型包括 对象类型,数组类型.
在转换方法前，首选会验证当前值，类型和要转换的类型是否相同，接着就是验证，它是否符合目标类型的格式，如果不符合会转换成目标类型的默认值，也可以设置默认值。

数据类型相互转换如：
字符串转整型，字符串转日期，字符串转UUID

### 用法示例：
```csharp
using XiaoFeng;

int a = "10".ToCast<int>();
Int64 b = "10".ToCast<Int64>();
double c = "10".ToCast<double>();
DateTime d = "2022-01-19".ToCast<DateTime>();
float e = "".ToCast<float>(1.0);
int f = (int)"".GetValue(typeof(int));
Guid g = "58AFBEB5791311ECBF49FA163E542B11".ToCast<Guid>();
Guid h = "58AFBEB5-7913-11EC-BF49-FA163E542B11".ToCast<Guid>();
```
还有一系列专一处理字符串转相关类型的方法，如：
```csharp
Int16 a = "1".ToInt16();
int b = "2".ToInt32();
Int64 c = "3".ToInt64();
UInt16 d = "4".ToUInt16();
UInt32 e = "5".ToUInt32();
UInt64 f ="6".ToUInt64();
float e = "7.2".ToFloat();
DateTime g = "2022-01-19 12:32".ToDateTime();
double h = "6.3".ToDouble();
byte i = "2".ToByte();
Boolean j = "1".ToBoolean();
Boolean k = "true".ToBoolean();
Boolean l = "False".ToBoolean();
Decimal m = "3.658".ToDecimal();
long n = "2584512".ToLong();
Guid o = "58AFBEB5791311ECBF49FA163E542B11".ToGuid();
Guid p = "58AFBEB5-7913-11EC-BF49-FA163E542B11".ToGuid();
```

## 获取对象基础类型 GetValueType

### 用法实例
```csharp
var a = "a".GetValueType();
var b = 10.GetValueType();
var c = new{a="a",b="b"}.GetValueType();
var d = new Dictionary<String,String>().GetValueType();
```
返回的是一个枚举类型 ValueTypes

```csharp
/// <summary>
/// 值类型枚举
/// </summary>
public enum ValueTypes
{
    /// <summary>
    /// 空
    /// </summary>
    [Description("空")] 
    Null = 0,
    /// <summary>
    /// 值
    /// </summary>
    [Description("值")] 
    Value = 1,
    /// <summary>
    /// 类
    /// </summary>
    [Description("类")] 
    Class = 2,
    /// <summary>
    /// 结构体
    /// </summary>
    [Description("结构体")] 
    Struct = 3,
    /// <summary>
    /// 枚举
    /// </summary>
    [Description("枚举")] 
    Enum = 4,
    /// <summary>
    /// 字符串
    /// </summary>
    [Description("字符串")] 
    String = 5,
    /// <summary>
    /// 数组
    /// </summary>
    [Description("数组")] 
    Array = 6,
    /// <summary>
    /// List
    /// </summary>
    [Description("List")] 
    List = 7,
    /// <summary>
    /// 字典
    /// </summary>
    [Description("字典")] 
    Dictionary = 8,
    /// <summary>
    /// ArrayList
    /// </summary>
    [Description("ArrayList")] 
    ArrayList = 9,
    /// <summary>
    /// 是否是集合类型
    /// </summary>
    [Description("是否是集合类型")] 
    IEnumerable = 10,
    /// <summary>
    /// 字典类型
    /// </summary>
    [Description("字典类型")] 
    IDictionary = 11,
    /// <summary>
    /// 匿名类型
    /// </summary>
    [Description("匿名类型")] 
    Anonymous = 12,
    /// <summary>
    /// DataTable
    /// </summary>
    [Description("DataTable")] 
    DataTable = 13,
    /// <summary>
    /// 其它
    /// </summary>
    [Description("其它")] 
    Other = 20
}
```

# 字符串匹配提取

### 用法实例

IsMatch 当前扩展方法 主要是 当前字符串是否匹配上正则表达式，比如，匹配当前字符串是否是QQ号码，代码如下：
```csharp
if("7092734".IsMatch(@"^\d{5-11}$"))
    Console.WriteLine("是QQ号码格式.");
else
    Console.WriteLine("非QQ号码格式.");
```
输出结果为：是QQ号码格式.
因为 字符串 "7092734"确实是QQ号码。

IsNotMatch 当前方法其实就是 !IsMatch,用法和IsMatch用法一样。
Match 当前扩展方法返回的是Match,使用指定的匹配选项在输入字符串中搜索指定的正则表达式的第一个匹配项。
Matches 当前扩展方法返回的是使用指定的匹配选项在指定的输入字符串中搜索指定的正则表达式的所有匹配项。

这三个方法是最原始最底层的方法，其它扩展都基于当前三个方法中的一个或两个来实现的。

GetMatch 扩展方法返回结果是：提取符合模式的数据所匹配的第一个匹配项所匹配的第一项或a组的数据
GetPatterns 扩展方法返回结果是：提取符合模式的数据所有匹配的第一项数据或a组数据
GetMatchs 扩展方法返回结果是：提取符合模式的数据所匹配的第一项中所有组数据
GetMatches 扩展方法返回结果是：提取符合模式的数据所有匹配项或所有组数据
提取的数据量对比：GetMatch < GetMatchs < GetPatterns < GetMatches

ReplacePattern 扩展方法用途是 使用 正则达式 来替换数据

下边通过实例来讲解这几个方法的使用及返回结果的区别：
```csharp
var a = "abc4d5e6hh5654".GetMatch(@"\d+");
a的值为："4";
var b = "abc4d5e6hh5654".GetPatterns(@"\d+");
b的值为：["4","5","6","5654"];
var c = "abc4d5e6hh5654".GetMatchs(@"(?<a>[a-z]+)(?<b>\d+)");
c的值为：{{"a","abc"},{"b","4"}};
var d = "abc4d5e6hh5654".GetMatches(@"(?<a>[a-z]+)(?<b>\d+)");
d的值为：[{{"a","abc"},{"b","4"}},{{"a","d"},{"b","5"}},{{"a","e"},{"b","6"}},{{"a","hh"},{"b","5654"}}]
var g = "a6b9c53".ReplacePattern(@"\d+","g");
g的值为："agbgcg";
var h = "a6b7c56".RemovePattern(@"\d+");
h的值为："abc";
var i = "a1b2c3".ReplacePattern(@"\d+",m=>{
   var a = a.Groups["a"].Value;
    if(a == "1")return "a1";
    else return "a2";
});
i的值为："aa1ba2ca2";
```

# 配置管理器 XiaoFeng.Config.ConfigSet<>

通过继承当前类可以轻松实现配置文件的操作，缓存，增，删，改，查等功能.

## 用法实例

XiaoFeng类库自动创建一个XiaoFeng.json配置文件 它的类库源码如下

```csharp
    /// <summary>
    /// XiaoFeng总配置
    /// </summary>
    [ConfigFile("Config/XiaoFeng.json", 0, "FAYELF-CONFIG-XIAOFENG", ConfigFormat.Json)]
    public class Setting : ConfigSet<Setting>, ISetting
    {
        #region 构造器
        /// <summary>
        /// 无参构造器
        /// </summary>
        public Setting() : base() { }
        /// <summary>
        /// 设置配置文件名
        /// </summary>
        /// <param name="fileName"></param>
        public Setting(string fileName) : base(fileName) { }
        #endregion

        #region 属性
        /// <summary>
        /// 是否启用调试
        /// </summary>
        [Description("是否启用调试")]
        public bool Debug { get; set; } = true;
        /// <summary>
        /// 最大线程数量
        /// </summary>
        [Description("最大线程数量")]
        public int MaxWorkerThreads { get; set; } = 100;
        /// <summary>
        /// 消费日志空闲时长
        /// </summary>
        [Description("消费日志空闲时长")]
        public int IdleSeconds { get; set; } = 60;
        /// <summary>
        /// 任务队列执行任务超时时间
        /// </summary>
        private int _TaskWaitTimeout = 5 * 60;
        /// <summary>
        /// 任务队列执行任务超时时间
        /// </summary>
        [Description("任务队列执行任务超时时间")]
        public int TaskWaitTimeout {
            get
            {
                if (this._TaskWaitTimeout  == 0)
                    this._TaskWaitTimeout  = 10 * 1000;
                return this._TaskWaitTimeout ;
            }
            set
            {
                this._TaskWaitTimeout  = value;
            }
        }
        /// <summary>
        /// 是否启用数据加密
        /// </summary>
        [Description("是否启用数据加密")]
        public bool DataEncrypt { get; set; } = false;
        /// <summary>
        /// 加密数据key
        /// </summary>
        [Description("加密数据key")]
        public string DataKey { get; set; } = "7092734";
        /// <summary>
        /// 是否开启请求日志
        /// </summary>
        [Description("是否开启请求日志")]
        public bool ServerLogging { get; set; }
        /// <summary>
        /// 是否拦截
        /// </summary>
        [Description("是否拦截")]
        public bool IsIntercept { get; set; }
        /// <summary>
        /// SQL注入串
        /// </summary>
        [Description("SQL注入串")]
        public string SQLInjection { get; set; } = @"insert\s+into |update |delete |select | union | join |exec |execute | exists|'|truncate |create |drop |alter |column |table |dbo\.|sys\.|alert\(|<scr|ipt>|<script|confirm\(|console\.|\.js|<\/\s*script>|now\(\)|getdate\(\)|time\(\)| Directory\.| File\.|FileStream |\.Write\(|\.Connect\(|<\?php|show tables |echo | outfile |Request[\.\(]|Response[\.\(]|eval\s*\(|\$_GET|\$_POST|cast\(|Server\.CreateObject|VBScript\.Encode|replace\(|location|\-\-";
        #endregion
    }
```
生成的JSON文件如下
```json
{
  "Debug"/*是否启用调试*/: true,
  "MaxWorkerThreads"/*最大线程数量*/: 100,
  "IdleSeconds"/*消费日志空闲时长*/: 60,
  "TaskWaitTimeout"/*任务队列执行任务超时时间*/: 300,
  "DataEncrypt"/*是否启用数据加密*/: false,
  "DataKey"/*加密数据key*/: "7092734",
  "ServerLogging"/*是否开启请求日志*/: false,
  "IsIntercept"/*是否拦截*/: false,
  "SQLInjection"/*SQL注入串*/: "insert\\s+into |update |delete |select | union | join |exec |execute | exists|'|truncate |create |drop |alter |column |table |dbo\\.|sys\\.|alert\\(|<scr|ipt>|<script|confirm\\(|console\\.|\\.js|<\\/\\s*script>|now\\(\\)|getdate\\(\\)|time\\(\\)| Directory\\.| File\\.|FileStream |\\.Write\\(|\\.Connect\\(|<\\?php|show tables |echo | outfile |Request[\\.\\(]|Response[\\.\\(]|eval\\s*\\(|\\$_GET|\\$_POST|cast\\(|Server\\.CreateObject|VBScript\\.Encode|replace\\(|location|\\-\\-"
}
```
ConfigFileAttribute 当前属性主要是定义当前配置存放路径，存放格式（JSON，XML），缓存KEY，缓存时长，文件改后会自动更新缓存。

DescriptionAttribute 当前属性是配置文件属性注释

当前配置文件使用方法
```csharp
var set = XiaoFeng.Config.Setting.Current;
//读取节点数据
var debug = set.Debug;
//设置节点数据
set.Debug = false;
//保存当前配置 通过当前 Save 方法 可把 内容更新至配置文件中去
set.Save();
```

# XiaoFeng.HttpHelper 网络请求库

HttpHelper 是Http模拟请求库。

## 使用操作

* GET 请求

``` csharp
var result = await XiaoFeng.Http.HttpHelper.GetHtmlAsync(new XiaoFeng.Http.HttpRequest
{
    Method = HttpMethod.Get,//不设置默认为Get请求
    Address = "http://www.fayelf.com"
});
if (result.StatusCode == System.Net.HttpStatusCode.OK)
{
    /*请求成功*/
    //响应内容
    var value = result.Html;
    //响应内容字节集
    var bytes = result.Data;
}
else
{
    /*请求失败*/
}

```

* POST 表单请求

``` csharp
var result = await XiaoFeng.Http.HttpHelper.GetHtmlAsync(new XiaoFeng.Http.HttpRequest
{
    Method = HttpMethod.Post,
    Address = "http://www.fayelf.com",
    Data=new Dictionary<string, string>
    {
        {"account","jacky" },
        {"password","123456" }
    }
});
if (result.StatusCode == System.Net.HttpStatusCode.OK)
{
    /*请求成功*/
    //响应内容
    var value = result.Html;
    //响应内容字节集
    var bytes = result.Data;
}
else
{
    /*请求失败*/
}
```

* POST BODY请求

``` csharp

var result = await XiaoFeng.Http.HttpHelper.GetHtmlAsync(new XiaoFeng.Http.HttpRequest
{
    Method = HttpMethod.Post,
    ContentType="application/json",
    Address = "http://www.fayelf.com",
    BodyData=@"{""account"":""jacky"",""password"":""123456""}"
});
if (result.StatusCode == System.Net.HttpStatusCode.OK)
{
    /*请求成功*/
    //响应内容
    var value = result.Html;
    //响应内容字节集
    var bytes = result.Data;
}
else
{
    /*请求失败*/
}

```

* POST FORMDATA 请求，就是有表单输入数据也有文件流数据

``` csharp
var result = await XiaoFeng.Http.HttpHelper.GetHtmlAsync(new XiaoFeng.Http.HttpRequest
{
    Method = HttpMethod.Post,
    ContentType = "application/x-www-form-urlencoded",
    Address = "http://www.fayelf.com",
    FormData = new List<XiaoFeng.Http.FormData>
    {
        new XiaoFeng.Http.FormData
        {
             Name="account",Value="jacky", FormType= XiaoFeng.Http.FormType.Text
        },
        new XiaoFeng.Http.FormData
        {
            Name="password",Value="123456",FormType= XiaoFeng.Http.FormType.Text
        },
        new XiaoFeng.Http.FormData
        {
            Name="headimage",Value=@"E://Work/headimage.png", FormType= XiaoFeng.Http.FormType.File
        }
    }
});
if (result.StatusCode == System.Net.HttpStatusCode.OK)
{
    /*请求成功*/
    //响应内容
    var value = result.Html;
    //响应内容字节集
    var bytes = result.Data;
}
else
{
    /*请求失败*/
}

```

* 下载文件

``` csharp

await XiaoFeng.Http.HttpHelper.Instance.DownFileAsync(new XiaoFeng.Http.HttpRequest
{
    Method = HttpMethod.Get,
    Address = "http://www.fayelf.com/test.rar"
}, @"E:/Work/test.rar");

```

# 数据库操作 DataHelper

* XiaoFeng.Data.DataHelper，当前类库支持SQLSERVER,MYSQL,ORACLE,达梦,SQLITE,ACCESS,OLEDB,ODBC等数十种数据库。

## 使用说明

简单实例

```csharp
var data = new XiaoFeng.Data.DataHelper(new XiaoFeng.Data.ConnectionConfig
{
    ProviderType= XiaoFeng.Data.DbProviderType.SqlServer,
    ConnectionString= "server=.;uid=testuser;pwd=123;database=Fay_TestDb;"
});
var dt = data.ExecuteDataTable("select * from F_Tb_Account;");
```
1. 直接执行SQL语句

```csharp
var non1 = data.ExecuteNonQuery("insert into F_Tb_Account(Account,Password) values('jacky','admin');");
```
non1值，如果non1是-1则表示 执行出错，可以通过data.ErrorMessage拿到最后一次执行出错的错误信息
如果non1是大于等于0则表示执行SQL语句后所执行的行数。

2. 返回DataTable

```csharp
var dt = data.ExecuteDataTable("select * from F_Tb_Account;");
```
dt就是一个datatable 。

3. 直接返回首行首列

```csharp
var val1 = data.ExecuteScalar("select Acount from F_Tb_Account;");
```
val1类型是object对象，根据数据库的值不同我们可以自定义转换如：var val2 = (int)val1;也可以用XiaoFeng自带的扩展方法,var val2 = val1.ToCast<int>();

4. 直接返回DataReader

```csharp
var dataReader = data.ExecuteReader("select * from F_Tb_Account;");
```
dataReader就是DataReader对象。

5. 直接返回DataSet

```csharp
var dataSet = data.ExecuteDataSet("select * from F_Tb_Account;select * from F_Tb_Account;");
```
dataSet就是DataSet对象。

6. 执行存储过程

```csharp
var data = data.ExecuteDataTable("proc_name", System.Data.CommandType.StoredProcedure, new System.Data.Common.DbParameter[]
{
    data.MakeParam(@"Account","jacky")
});
```

7. SQL语句带存储参数

```csharp
var data2 = data.ExecuteDataTable("select * from F_Tb_Account where Account=@Account;", new System.Data.Common.DbParameter[]
{
    data.MakeParam(@"@Account","jacky")
});
```

8. 直接转换成对象

```csharp
var models = data.QueryList<Account>("select * from F_Tb_Account");
var model = data.Query<Account>("select * from F_Tb_Account");
```

# 正则表达式 扩展方法

字符串匹配，提取，是否符合规则，替换，移除等都可用是正则表达式来实现的。

## 使用说明

* IsMatch 扩展方法 主要是当前字符串是否匹配上正则表达式，比如，匹配当前字符串是否是QQ号码，代码如下：

```csharp
if("7092734".IsMatch(@"^\d{5-11}$"))
    Console.WriteLine("是QQ号码格式.");
else
    Console.WriteLine("非QQ号码格式.");
```

输出结果为：是QQ号码格式。

因为字符串 "7092734"确实是QQ号码。

IsNotMatch 扩展方法其实就是 !IsMatch,用法和IsMatch用法一样。

Match 扩展方法返回的是Match,使用指定的匹配选项在输入字符串中搜索指定的正则表达式的第一个匹配项。

Matches 当前扩展方法返回的是使用指定的匹配选项在指定的输入字符串中搜索指定的正则表达式的所有匹配项。

这三个方法是最原始最底层的方法，其它扩展都基于当前三个方法中的一个或两个来实现的。

GetMatch 扩展方法返回结果是：提取符合模式的数据所匹配的第一个匹配项所匹配的第一项或a组的数据。

GetPatterns 扩展方法返回结果是：提取符合模式的数据所有匹配的第一项数据或a组数据。

GetMatchs 扩展方法返回结果是：提取符合模式的数据所匹配的第一项中所有组数据。

GetMatches 扩展方法返回结果是：提取符合模式的数据所有匹配项或所有组数据。

提取的数据量对比：GetMatch<GetMatchs<GetPatterns<GetMatches 。

ReplacePattern 扩展方法用途是使用正则达式来替换数据。

下边通过实例来讲解这几个方法的使用及返回结果的区别：

```csharp
var a = "abc4d5e6hh5654".GetMatch(@"\d+");
a的值为："4";
var b = "abc4d5e6hh5654".GetPatterns(@"\d+");
b的值为：["4","5","6","5654"];
var c = "abc4d5e6hh5654".GetMatchs(@"(?<a>[a-z]+)(?<b>\d+)");
c的值为：{{"a","abc"},{"b","4"}};
var d = "abc4d5e6hh5654".GetMatches(@"(?<a>[a-z]+)(?<b>\d+)");
d的值为：[{{"a","abc"},{"b","4"}},{{"a","d"},{"b","5"}},{{"a","e"},{"b","6"}},{{"a","hh"},{"b","5654"}}]
var g = "a6b9c53".ReplacePattern(@"\d+","g");
g的值为："agbgcg";
var h = "a6b7c56".RemovePattern(@"\d+");
h的值为："abc";
var i = "a1b2c3".ReplacePattern(@"\d+",m=>{
   var a = a.Groups["a"].Value;
    if(a == "1")return "a1";
    else return "a2";
});
i的值为："aa1ba2ca2";
```

# XiaoFeng.Threading.JobScheduler 作业调度

作业调度其实就是一个定时器，定时完成某件事，比如：每分钟执行一次，每小时执行一次，每天执行一次，第二周几执行，每月几号几点执行，间隔多少个小时执行一次等。

作业类：XiaoFeng.Threading.Job

主调度类：XiaoFeng.Threading.JobScheduler

## 使用说明

1. 定时只执行一次也就是多久后执行

```csharp
var job = new XiaoFeng.Threading.Job
{
     Async = true,
     Name="作业名称",
      TimerType= XiaoFeng.Threading.TimerType.Once,
        StartTime= DateTime.Now.AddMinutes(5),
    SuccessCallBack = job =>
    {
        /*到时间执行任务*/
    }
};
job.Start();
```
当前作业为5 分钟后执行一次，然后就是销毁，作业从调度中移除。

2. 间隔执行

```csharp
var job = new XiaoFeng.Threading.Job
{
    Async = true,
    Name = "作业名称",
    TimerType = XiaoFeng.Threading.TimerType.Interval,
    Period = 5000,
    StartTime = DateTime.Now.AddMinutes(5),
    SuccessCallBack = job =>
    {
        /*到时间执行任务*/
    }
};
job.Start();
```
当前作业为，5分钟后运行，然后每隔5分钟会再执行一次。

3. 每天定时执行一次

```csharp
var job = new XiaoFeng.Threading.Job
{
    Async = true,
    Name = "作业名称",
    TimerType = XiaoFeng.Threading.TimerType.Day,
    Time = new XiaoFeng.Threading.Time(2, 0, 0),
    StartTime = DateTime.Now.AddMinutes(5),
    SuccessCallBack = job =>
    {
        /*到时间执行任务*/
    }
};
```
当前作业为，5分钟后运行，然后每天2点执行一次。

4. 每周几几点执行,每月几号几点执行

```csharp
var job = new XiaoFeng.Threading.Job
{
    Async = true,
    Name = "作业名称",
    TimerType = XiaoFeng.Threading.TimerType.Week,
    DayOrWeekOrHour = new int[] { 1, 4 },
    Time = new XiaoFeng.Threading.Time(2, 0, 0),
    StartTime = DateTime.Now.AddMinutes(5),
    SuccessCallBack = job =>
    {
        /*到时间执行任务*/
    }
};
job.Start();
```

当前作业为，5分钟后运行，然后每周的周一，周四的两点执行一次。

# XiaoFeng.Xml Xml序列化

XML序列化操作就是把一个数据对象序列化成XML格式的数据，反序列化操作就是把一个XML格式的数据反序列化成一个数据对象。
命名空间:XiaoFeng.Xml
先看序列化配置

```csharp
/// <summary>
/// 序列化设置
/// </summary>
public class XmlSerializerSetting
{
    #region 构造器
    /// <summary>
    /// 无参构造器
    /// </summary>
    public XmlSerializerSetting()
    {

    }
    #endregion

    #region 属性
    /// <summary>
    /// Guid格式
    /// </summary>
    public string GuidFormat { get; set; } = "D";
    /// <summary>
    /// 日期格式
    /// </summary>
    public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff";
    /// <summary>
    /// 是否格式化
    /// </summary>
    public bool Indented { get; set; } = true;
    /// <summary>
    /// 枚举值
    /// </summary>
    public EnumValueType EnumValueType { get; set; } = 0;
    /// <summary>
    /// 解析最大深度
    /// </summary>
    public int MaxDepth { get; set; } = 28;
    /// <summary>
    /// 是否写注释
    /// </summary>
    public bool OmitComment { get; set; } = true;
    /// <summary>
    /// 忽略大小写 key值统一变为小写
    /// </summary>
    public bool IgnoreCase { get; set; } = false;
    /// <summary>
    /// 默认根目录节点名称
    /// </summary>
    public string DefaultRootName { get; set; } = "Root";
    /// <summary>
    /// 默认编码
    /// </summary>
    public Encoding DefaultEncoding { get; set; } = Encoding.UTF8;
    /// <summary>
    /// 获取或设置一个值，该值指示是否 System.Xml.XmlWriter 编写 XML 内容时应移除重复的命名空间声明。 写入器的默认行为是输出写入器的命名空间解析程序中存在的所有命名空间声明。
    /// </summary>
    public NamespaceHandling NamespaceHandling { get; set; }
    /// <summary>
    /// 是否忽略输出XML声明
    /// </summary>
    public Boolean OmitXmlDeclaration { get; set; } = false;
    /// <summary>
    /// 获取或设置要用于换行符的字符串。要用于换行符的字符串。 它可以设置为任何字符串值。 但是，为了确保 XML 有效，应该只指定有效的空格字符，例如空格、制表符、回车符或换行符。 默认值是\r\n （回车符、 换行符）。
    /// </summary>
    public string NewLineChars { get; set; } = Environment.NewLine;
    /// <summary>
    /// 是否忽略数组项未指定KEY的项用节点名称代替
    /// </summary>
    public Boolean OmitArrayItemName { get; set; } = true;
    /// <summary>
    /// 是否忽略空节点
    /// </summary>
    public Boolean OmitEmptyNode { get; set; } = true;
    /// <summary>
    /// 是否忽略命名空间
    /// </summary>
    public Boolean OmitNamespace { get; set; } = true;
    #endregion
}
```

简单使用，扩展了两个方法 EntityToXml(),XmlToEntity();
先看 XMl模型对象

``` csharp
/// <summary>
/// XmlModel 类说明
/// </summary>
[XmlRoot("Root")]
public class XmlModel
{
    #region 构造器
    /// <summary>
    /// 无参构造器
    /// </summary>
    public XmlModel()
    {

    }
    #endregion

    #region 属性
    /// <summary>
    /// 属性1
    /// </summary>
    [XmlCData]
    [XmlElement("NameA")]
    public string FieldName1 { get; set; }
    /// <summary>
    /// 属性2
    /// </summary>
    [XmlConverter(typeof(XiaoFeng.Xml.StringEnumConverter))]
    [XmlElement("NameB")]
    public EnumValueType FieldName2 { get; set;}
    /// <summary>
    /// 属性3
    /// </summary>
    [XmlConverter(typeof(XiaoFeng.Xml.DescriptionConverter))]
    [XmlElement("Namec")]
    public EnumValueType FieldName3 { get; set; }
    /// <summary>
    /// 属性4
    /// </summary>
    public string FieldName4 { get; set; }
    #endregion

    #region 方法

    #endregion
}
```
简单使用

```csharp
var a = new XmlModel
    {
        FieldName1 = "Value1",
        FieldName2 = EnumValueType.Name,
        FieldName3 = EnumValueType.Value,
        FieldName4 = "Value4"
    }.EntityToXml();
//XmlSerializer.Serializer(a) 和a.EntityToXml()是一样的
```
//输出结果
```xml
<?xml version="1.0" encoding="utf-8"?>
<Root>
  <FieldName1><![CDATA[Value1]]></FieldName1>
  <NameB>Name</NameB>
  <Namec>值</Namec>
  <FieldName4>Value4</FieldName4>
</Root>
var b = a.XmlToEntity<XmlModel>();
//XmlSerializer.Deserialize<XmlModel>(a) 和XmlToEntity<XmlModel>()是一样的
```
接下来讲一下序列化时的几个特性

```csharp
//忽略属性值
XmlIgnoreAttribute
//指定节点名称
XmlElementPath
//转换类型
XmlConverterAttribute
//枚举转换器
StringEnumConverter
//说明转换器
DescriptionConverter
```
下边举例讲一下XmlElementPath的使用,当前属性仅支持反序列化时使用，序列化时暂时还不支持当前属性。假设下边有一个 这样的xml

```xml
<?xml version="1.0" encoding="utf-8"?>
<Root>
  <NameA>
    <NameD><![CDATA[Value1]]><NameD>
    <NameC>bbb</NameC>
  </NameA>
  <NameB>Name</NameB>
  <Namec>值</Namec>
  <FieldName4>Value4</FieldName4>
</Root>
```
按正常定义模型时 NameA 子节点 A  B 要定义到一个类中 
实际在这里可以这样定义

```csharp
/// <summary>
  /// XmlModel 类说明
  /// </summary>
  [XmlRoot("Root")]
  public class XmlModel
  {
      #region 构造器
      /// <summary>
      /// 无参构造器
      /// </summary>
      public XmlModel()
      {

      }
      #endregion

      #region 属性
      /// <summary>
      /// 属性1
      /// </summary>
      [XmlCData]
      [XmlElementPath("NameA/NameC")]
      public string A { get; set; }
      /// <summary>
      /// 属性1
      /// </summary>
      [XmlCData]
      [XmlElementPath("NameA/NameD")]
      public string B { get; set; }
      /// <summary>
      /// 属性2
      /// </summary>
      [XmlConverter(typeof(XiaoFeng.Xml.StringEnumConverter))]
      [XmlElement("NameB")]
      public EnumValueType FieldName2 { get; set;}
      /// <summary>
      /// 属性3
      /// </summary>
      [XmlConverter(typeof(XiaoFeng.Xml.DescriptionConverter))]
      [XmlElement("Namec")]
      public EnumValueType FieldName3 { get; set; }
      /// <summary>
      /// 属性4
      /// </summary>
      public string FieldName4 { get; set; }
      #endregion

      #region 方法

      #endregion
  }
```
反序列化结果为:

就是可以直接从子节点取数据反序列化到对象上，不用再单独去定义子模型了。
如果不想定义模型，则XiaoFeng.Xml中提供了一个万能的对象模型就是XmlValue对象。
Xml序列化，反序列化就讲到这里，具体操作还需要自己去实践操作。

# XiaoFeng.Json Json序列化

JSON序列化操作，就是把数据对象序列化成JSON数据，也可以把JSON数据反序列化成数据对象。
命名空间是：XiaoFeng.Json
序列化方法 JsonParser.SerializeObject 也可以用扩展方法 ToJson()
反序列化方法 JsonParser.JsonParser.DeserializeObject<T>() 也可以使用JsonToObject()
简单使用，看代码

```csharp
//序列化
var a = new {
        key1="value1",
        key2 ="value2"
    }.ToJson();
//a的值就是：{"key1":"value1","key2":"value2"}
//反序列化
var b = @"{""key1"":""value1"",""key2"":""value2""}.JsonToObject();
//b的值就是：一个字典形式

```
上边用的是一个匿名对象，反序列化回来的时候因为没有设置对应的类型，所以自动转换成JsonValue类型的值；
下边详细介绍一下 序列配置,在使用Tojson,JsonToObject扩展方法时可以设置配置参数的。配置参数如下：

```csharp
/// <summary>
/// Json格式设置
/// </summary>
public class JsonSerializerSetting
{
    #region 构造器
    /// <summary>
    /// 无参构造器
    /// </summary>
    public JsonSerializerSetting() { }
    #endregion

    #region 属性
    /// <summary>
    /// Guid格式
    /// </summary>
    public string GuidFormat { get; set; } = "D";
    /// <summary>
    /// 日期格式
    /// </summary>
    public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss.fff";
    /// <summary>
    /// 是否格式化
    /// </summary>
    public bool Indented { get; set; } = false;
    /// <summary>
    /// 枚举值
    /// </summary>
    public EnumValueType EnumValueType { get; set; } = 0;
    /// <summary>
    /// 解析最大深度
    /// </summary>
    public int MaxDepth { get; set; } = 28;
    /// <summary>
    /// 是否写注释
    /// </summary>
    public bool IsComment { get; set; } = false;
    /// <summary>
    /// 忽略大小写 key值统一变为小写
    /// </summary>
    public bool IgnoreCase { get; set; } = false;
    /// <summary>
    /// 忽略空节点
    /// </summary>
    public bool OmitEmptyNode { get; set; } = false;
    #endregion
}
```
接下来讲一下序列化时的几个特性

```csharp
//忽略属性值
JsonIgnoreAttribute
//指定节点名称
JsonElement
//转换类型
JsonConverterAttribute
//枚举转换器
StringEnumConverter
//说明转换器
DescriptionConverter
```

下边通过实例讲解一下;下面是一个定义好的JSON模型

```csharp
/// <summary>
/// JsonModel 类说明
/// </summary>
public class JsonModel
{
    #region 构造器
    /// <summary>
    /// 无参构造器
    /// </summary>
    public JsonModel()
    {

    }
    #endregion

    #region 属性
    /// <summary>
    /// 属性1
    /// </summary>
    [JsonElement("NameA")]
    public string FieldName1 { get; set; }
    /// <summary>
    /// 属性2
    /// </summary>
    [JsonConverter(typeof(XiaoFeng.Json.DescriptionConverter))]
    [JsonElement("NameB")]
    public EnumValueType FieldName2 { get; set; }
    /// <summary>
    /// 属性3
    /// </summary>
    [JsonConverter(typeof(XiaoFeng.Json.StringEnumConverter))]
    [JsonElement("NameC")]
    public EnumValueType FieldName3 { get; set; }
    /// <summary>
    /// 属性4
    /// </summary>
    [JsonElement("NameD")]
    public EnumValueType FieldName4 { get; set; }
    #endregion

    #region 方法

    #endregion
}
//使用时
var a = new JsonModel
{
    FieldName1 = "aaaa",
    FieldName2 = EnumValueType.Name,
    FieldName3 = EnumValueType.Value,
	FieldName4 = EnumValueType.Value
}.ToJson();
//当前转换成JSON是：{"NameA":"aaaa","NameB":"名称","NameC":"Value","NameD":0}
//因为 FieldName1 被设置成了NameA FieldName2被设置成了NameB FieldName3被设置成了NameC FieldName4无设置
//两个枚举值不一样，是因为第二个设置的是读取Description内容 就是EnumValueType.Name的属性值DescriptionAttribute中设置的值
//第三个取的是Value是因为设置取的是StringEnumConverter 所以直接就转换成了名称，如果不设置则直接输出对应的数字
//反序列化时也是这样对应到实体模型中去
```

Json序列化，反序列化就讲到这里，具体操作还需要自己去实践操作。


# 作者介绍



* 网址 : http://www.fayelf.com
* QQ : 7092734
* EMail : jacky@fayelf.com