// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Microsoft.AspNetCore.Http
{
	/// <summary>
	/// 表示一个类，该类对 Microsoft.AspNetCore.HttpRequest 类进行了扩展，在其中添加了确定 HTTP 请求是否为 AJAX 请求的功能，和查询 QueryString 的键值的功能。
	/// </summary>
	public static class RequestExtensions
	{
		/// <summary>
		/// 获取HttpRequest查询字符串集合中指定键的参数值。
		/// </summary>
		/// <param name="query">表示HttpRequest查询字符串集合</param>
		/// <param name="key">在Microsoft.AspNetCore.Http.IQueryCollection中找到的键。</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="query"/> 参数为 null（在 Visual Basic 中为 Nothing）。</exception>
		/// <exception cref="System.ArgumentNullException"><paramref name="key"/> 参数为 null（在 Visual Basic 中为 Nothing）。</exception>
		public static string Item(this IQueryCollection query, string key) {
			if (query == null) throw new System.ArgumentNullException("query");
			if (string.IsNullOrEmpty(key)) throw new System.ArgumentNullException("key");
			if (!query.ContainsKey(key)) return null;
			var v = new StringValues();
			query.TryGetValue(key, out v);
			if (v.Count == 1) return v[0];
			return string.Join(", ", v);
		}

		/// <summary>
		/// 确定指定的 HTTP 请求是否为 AJAX 请求。
		/// </summary>
		/// <param name="request">HTTP 请求。</param>
		/// <returns>如果指定的 HTTP 请求是 AJAX 请求，则为 true；否则为 false。</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="request"/>  参数为 null（在 Visual Basic 中为 Nothing）。</exception>
		public static bool IsAjaxRequest(this HttpRequest request) {
			if (request == null) throw new System.ArgumentNullException("request");
			return (request.Query.Item("X-Requested-With") == "XMLHttpRequest") || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest"));
		}
	}
}
