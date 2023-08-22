﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XiaoFeng.IO;

/****************************************************************
*  Copyright © (2023) www.fayelf.com All Rights Reserved.       *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@fayelf.com                                     *
*  Site : www.fayelf.com                                        *
*  Create Time : 2023-03-02 08:51:03                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.Http
{
	/// <summary>
	/// HttpSocket Http请求操作类
	/// </summary>
	/// <remarks>通过Socket连接实现http请求</remarks>
	public class HttpSocket : Disposable
	{
		#region 构造器
		/// <summary>
		/// 设置请求对象
		/// </summary>
		/// <param name="request">请求对象</param>
		public HttpSocket(HttpRequest request)
		{
			Request = request;
		}

		#endregion

		#region 属性
		/// <summary>
		/// 请求对象
		/// </summary>
		public HttpRequest Request { get; set; }
		/// <summary>
		/// 响应对象
		/// </summary>
		public HttpResponse Response { get; set; }
		/// <summary>
		/// 请求Uri
		/// </summary>
		public Uri RequestUri { get; set; }
		/// <summary>
		/// 转向次数
		/// </summary>
		private int _RedirectCount = 0;
		/// <summary>
		/// 转向次数
		/// </summary>
		public int RedirectCount { get => this._RedirectCount; }
		/// <summary>
		/// 请求客户端
		/// </summary>
		private Stream Client { get; set; }
		#endregion

		#region 方法

		#region 创建请求头
		/// <summary>
		/// 创建请求头
		/// </summary>
		/// <param name="uri">请求网址</param>
		/// <returns></returns>
		public string CreateRequestHeader(Uri uri)
		{
			var headers = new WebHeaderCollection();
			var RequestPath = "";
			if (this.Request.WebProxy != null)
			{
				RequestPath = $"{this.Request.Method.Method.ToUpper()} {uri.Scheme}://{uri.Host}:{uri.Port}{uri.PathAndQuery} HTTP/{this.Request.ProtocolVersion}\r\n";
				headers.Set("Proxy-Connection", "keep-alive");
				var credentials = this.Request.WebProxy.Credentials.GetCredential(uri, "Basic");
				headers.Set(HttpRequestHeader.ProxyAuthorization, $"Basic {(credentials.UserName + ":" + credentials.Password).ToBase64String()}");
			}
			else
			{
				RequestPath = $"{this.Request.Method.Method.ToUpper()} {uri.PathAndQuery} HTTP/{this.Request.ProtocolVersion}\r\n";
			}
			if (this.Request.Credentials != null)
			{
				var credentials = this.Request.Credentials.GetCredential(RequestUri, "Basic");
				headers.Set(HttpRequestHeader.Authorization, $"Basic {(credentials.UserName + ":" + credentials.Password).ToBase64String()}");
			}

			headers.Set(HttpRequestHeader.Accept, this.Request.Accept);
			headers.Set(HttpRequestHeader.AcceptEncoding, this.Request.AcceptEncoding.Multivariate("gzip, deflate, br"));
			headers.Set(HttpRequestHeader.AcceptLanguage, this.Request.AcceptLanguage.Multivariate("zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6"));
			headers.Set(HttpRequestHeader.CacheControl, this.Request.CacheControl == null ? "max-age=0" : this.Request.CacheControl.ToString());
			headers.Set(HttpRequestHeader.Connection, this.Request.KeepAlive ? "keep-alive" : "close");

			if (this.Request.CookieContainer != null && this.Request.CookieContainer.Count > 0)
			{
				var Cookie = new List<string>();
				var cookies = this.Request.CookieContainer.GetCookies(uri);
				for (var i = 0; i < cookies.Count; i++)
				{
					var cookie = cookies[i];
					Cookie.Add($"{cookie.Name}={cookie.Value}");
				}
				headers.Set(HttpRequestHeader.Cookie, Cookie.Join("&"));
			}

			headers.Set(HttpRequestHeader.Host, uri.Host + ":" + uri.Port);
			headers.Set("Origin", $"{uri.Scheme}://{uri.Host}:{uri.Port}");
			headers.Set("Upgrade-Insecure-Requests", "1");
			headers.Set(HttpRequestHeader.UserAgent, this.Request.UserAgent);

			if (this.Request.Referer.IsNotNullOrEmpty())
			{
				headers.Set(HttpRequestHeader.Referer, this.Request.Referer);
			}
			if (this.Request.ContentType.IsNotNullOrWhiteSpace())
			{
				headers.Set(HttpRequestHeader.ContentType, this.Request.ContentType);
			}
			else
			{
				if (this.Request.Method.Method.ToUpper() == "POST")
				{
					headers.Set(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
				}
			}
			if (this.Request.Encoding != null)
			{
				headers.Set(HttpRequestHeader.AcceptCharset, this.Request.Encoding.EncodingName);
			}
			if (this.Request.Expect100Continue)
			{
				headers.Set(HttpRequestHeader.Expect, "100-continue");
			}
			if (this.Request.ContentLength > 0)
			{
				headers.Set(HttpRequestHeader.ContentLength, this.Request.ContentLength.ToString());
			}
			if (this.Request.IfModifiedSince != null)
			{
				headers.Set(HttpRequestHeader.IfModifiedSince, this.Request.IfModifiedSince.GetValueOrDefault().ToString("r"));
			}
			if (this.Request.Headers != null && this.Request.Headers.Count > 0)
			{
				this.Request.Headers.Each(h => headers.Set(h.Key, h.Value));
			}
			return RequestPath + headers.ToString();
		}
		#endregion

		#region 获取网络
		/// <summary>
		/// 获取网络
		/// </summary>
		/// <param name="uri">请求地址</param>
		/// <returns></returns>
		private async Task<Stream> GetClient(Uri uri)
		{
			if (this.Request.CertPath.IsNotNullOrEmpty())
			{
				var cert = this.Request.CertPath.GetBasePath();
				if (File.Exists(cert))
				{
					var x509 = this.Request.CertPassWord.IsNullOrEmpty() ? new X509Certificate2(cert) : new X509Certificate2(cert, this.Request.CertPassWord);
					if (this.Request.ClientCertificates == null) this.Request.ClientCertificates = new X509Certificate2Collection(x509);
					else
						this.Request.ClientCertificates.Add(x509);
				}
			}
			if (this.Request.WebProxy != null)
				uri = this.Request.WebProxy.Address;
			if (this.Client != null) this.Client.Close();
			var socket= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.ReceiveTimeout = this.Request.ReadWriteTimeout;
			socket.SendTimeout = this.Request.ReadWriteTimeout;
			socket.NoDelay = true;

			var state = socket.BeginConnect(uri.Host, uri.Port, null, null);
			if (state.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(Math.Max(this.Request.Timeout, 30)), true))
				return await Task.FromResult<Stream>(null);
			socket.EndConnect(state);
			var netStream = new NetworkStream(socket, true);
            if (uri.Scheme.ToUpper() == "HTTPS")
			{
                var sslStream = new SslStream(netStream, false, (sender, cert, chain, error) =>
                {
                    return error == SslPolicyErrors.None;
                });
				if (this.Request.ClientCertificates != null && this.Request.ClientCertificates.Count > 0)
				{
					sslStream.AuthenticateAsClient(uri.Host, this.Request.ClientCertificates,SslProtocols.Ssl2, false);
				}
				else
					sslStream.AuthenticateAsClient(uri.Host);
				if (sslStream.IsAuthenticated)
				{
                    sslStream.ReadTimeout = this.Request.ReadWriteTimeout;
                    sslStream.WriteTimeout = this.Request.ReadWriteTimeout;
					this.Client = sslStream;
                }
			}
			return await Task.FromResult(this.Client);
		}
        #endregion

        #region 接收消息
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> ReceviceMessageAsync()
		{
			var stream = this.Client;
            if (stream == null) return Array.Empty<byte>();
            int readsize = -1;
            var buffer = new MemoryStream();
            var dataBuffer = new byte[65535];
            do
            {
                try
                {
                    readsize = await stream.ReadAsync(dataBuffer, 0, 65535);
                    await buffer.WriteAsync(dataBuffer, 0, readsize);
                }
                catch (SocketException ex)
                {
					return await Task.FromResult(ex.Message.GetBytes());
                }
                catch (IOException ex)
                {
                    return await Task.FromResult(ex.Message.GetBytes());
                }
                catch (Exception ex)
                {
                    return await Task.FromResult(ex.Message.GetBytes());
                }
            } while (readsize > 0 && ((NetworkStream)stream).DataAvailable);

            if (buffer.Length == 0) return Array.Empty<byte>();
            var bytes = buffer.ToArray();
            buffer.Close();
            buffer.Dispose();
            return bytes;
        }
        #endregion

        #region 发送请求
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponse> SendRequestAsync()
		{
			this.Response = new HttpResponse();
			var url = this.Request.Address;
			if (this.Request.Method.ToString().ToUpper() == "GET" && this.Request.Data != null && this.Request.Data.Count > 0)
			{
				this.Request.Data.Each(k =>
				{
					url += (url.Contains("?") ? "&" : "?") + k.Key + "=" + k.Value.UrlEncode();
				});
			}
			this.RequestUri = new Uri(url);
			this.Response.Request = this.Request;
			this.Response.SetBeginTime();

			return await this.SendRequestAsync(this.RequestUri).ConfigureAwait(false);
		}
		/// <summary>
		/// 发送请求
		/// </summary>
		/// <param name="requestUri">请求地址</param>
		/// <returns></returns>
		private async Task<HttpResponse> SendRequestAsync(Uri requestUri)
		{
			byte[] RequestBody = Array.Empty<byte>();
			if (this.Request.Method.ToString().ToUpper() == "POST")
			{
				RequestBody = this.Request.GetReuqestBody();
			}
			var RequestHeader = this.CreateRequestHeader(requestUri);
			var bytes = RequestHeader.GetBytes(this.Request.Encoding);
			var stream = await this.GetClient(requestUri).ConfigureAwait(false);
			if (stream == null)
			{
				this.Response.Data = "SSl认证失败.".GetBytes();
				return await Task.FromResult(this.Response);
			}
			await this.Client.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
			await this.Client.WriteAsync(RequestBody, 0, RequestBody.Length).ConfigureAwait(false);
			var resonseBytes = await this.ReceviceMessageAsync().ConfigureAwait(false);

			var buffers = new MemoryStream(resonseBytes);
			//发送头
			var Headers = this.GetResponseHeaders(buffers);
			if (Headers.TryGetValue("Location", out var location))
			{
				if (this.Request.AllowAutoRedirect)
				{
					if (!location.IsSite())
						location = $"{requestUri.Scheme}://{requestUri.Host}:{requestUri.Port}{location}";
					var uri = new Uri(location);
					if (Interlocked.Increment(ref this._RedirectCount) <= this.Request.MaximumAutomaticRedirections)
					{
						if (this.Response.ResponseUri == null) this.Response.ResponseUris = new List<Uri>() { requestUri, uri };
						this.Response.ResponseUri = uri;
						return await this.SendRequestAsync(uri).ConfigureAwait(false);
					}
					else
					{
						throw new Exception("转向地址次数超过了设置的最大数量.");
					}
				}
			}
			if (this.Response.IsChunked)
			{
				var End = false;
				var BlockLine = new MemoryStream();
				var Body = false;
				var BodyStream = new MemoryStream();
				while (buffers.CanRead && buffers.Position < buffers.Length)
				{
					var c = buffers.ReadByte();
					if (End)
					{
						if (c == 10)
						{
							if (Body)
							{
								var _bytes = BlockLine.ToArray();
								BodyStream.Write(_bytes, 0, _bytes.Length);
								BlockLine.SetLength(0);
								Body = false;
							}
							else
							{
								if (BlockLine.Length == 0) continue;
								var length = Convert.ToInt32(BlockLine.ToArray().GetString(this.Request.Encoding), 16);
								if (length == 0) break;
								var BodyBytes = new byte[length];
								await buffers.ReadAsync(BodyBytes, 0, BodyBytes.Length).ConfigureAwait(false);
								await BodyStream.WriteAsync(BodyBytes, 0, length).ConfigureAwait(false);
								BlockLine.SetLength(0);
								Body = false;
							}
							End = false;
						}
						else
						{
							BlockLine.WriteByte((byte)c);
							End = false;
						}
					}
					else if (c == 13)
						End = true;
					else
						BlockLine.WriteByte((byte)c);
				}
				Response.Data = BodyStream.ToArray();

				BodyStream.Close();
				BodyStream.Dispose();
				BlockLine.Close();
				BlockLine.Dispose();
			}

			buffers.Close();
			buffers.Dispose();

			this.Response.SetEndTime();

			this.Response.HttpCore = HttpCore.HttpSocket;
			Response.Headers = Headers;
			await Response.InitSocketAsync().ConfigureAwait(false);
			this.Close();
			return this.Response;
		}
		#endregion

		#region 获取响应头
		/// <summary>
		/// 获取响应头
		/// </summary>
		/// <param name="resonseStream">响应流</param>
		/// <returns></returns>
		private IDictionary<string, string> GetResponseHeaders(MemoryStream resonseStream)
		{
			var Headers = new Dictionary<string, string>();
			resonseStream.Seek(0, SeekOrigin.Begin);

			var End = false;
			var BlockLine = new MemoryStream();
			var uri = new Uri(this.Request.Address);
			while (resonseStream.CanRead && resonseStream.Position < resonseStream.Length)
			{
				var c = resonseStream.ReadByte();
				if (End)
				{
					if (c == 10)
					{
						if (BlockLine.Length == 0)
						{
							//进入body
							if (Headers.TryGetValue("Transfer-Encoding", out var transferEncoding))
							{
								//处理chunked
								this.Response.IsChunked = true;
								break;
							}
							else
							{
								var ContentLength = 0L;
								if (Headers.TryGetValue("Content-Length", out var contentLength))
								{
									ContentLength = contentLength.ToCast<long>();
								}
								else
								{
									ContentLength = resonseStream.Length - resonseStream.Position;
								}
								var BodyBytes = new byte[ContentLength];
								resonseStream.Read(BodyBytes, 0, (int)ContentLength);
								Response.Data = BodyBytes;
							}
						}
						else
						{
							var _line = BlockLine.ToArray().GetString(this.Request.Encoding);
							if (_line.StartsWith($"HTTP/{this.Request.ProtocolVersion}"))
							{
								var httpStatus = _line.GetMatchs($@"^{uri.Scheme.ToUpper()}/{this.Request.ProtocolVersion}\s+(?<a>\d+)\s+(?<b>.*?)$");
								if (httpStatus.TryGetValue("a", out var a))
								{
									Response.StatusCode = a.ToCast<int>().ToCast<HttpStatusCode>();
								}
								if (httpStatus.TryGetValue("b", out var b))
								{
									Response.StatusDescription = b;
								}
							}
							else
							{
								var kvs = _line.GetMatchs(@"^(?<k>[^:]+):\s+(?<v>[\s\S]*)$");
								if (kvs != null && kvs.Count > 0 && kvs.TryGetValue("k", out var k) && kvs.TryGetValue("v", out var v) && !Headers.ContainsKey(k))
								{
									Headers.Add(k, v);
								}
							}
							BlockLine.SetLength(0);
						}
						End = false;
						continue;
					}
					else
					{
						BlockLine.WriteByte((byte)c);
						End = false;
					}
				}
				else if (c == 13)
					End = true;
				else
					BlockLine.WriteByte((byte)c);
			}
			return Headers;
		}
		#endregion

		#region 关闭
		/// <summary>
		/// 关闭
		/// </summary>
		public void Close()
		{
			this.Dispose(true);
		}
		#endregion

		#region 释放
		/// <summary>
		/// 释放
		/// </summary>
		public override void Dispose()
		{
			this.Dispose(true);
		}
		/// <summary>
		/// 释放
		/// </summary>
		/// <param name="disposing">标识</param>
		protected override void Dispose(bool disposing)
		{
			if (this.Client != null)
			{
				this.Client.Close();
				this.Client = null;
			}
			base.Dispose(disposing);
		}
		/// <summary>
		/// 析构器
		/// </summary>
		~HttpSocket()
		{
			Dispose(true);
		}
        #endregion

        #endregion

    }
}