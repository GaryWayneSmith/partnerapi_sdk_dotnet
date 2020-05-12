/**
Copyright (c) 2018-present, Walmart Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Walmart.Sdk.Base.Primitive;
using Walmart.Sdk.Base.Serialization;

namespace Walmart.Sdk.Base.Http
{
	public class Request : IRequest
	{
		public Primitive.Config.IRequestConfig Config { get; private set; }
		public string EndpointUri { get; set; }
		public HttpRequestMessage HttpRequest { get; }
		public Dictionary<string, string> QueryParams { get; set; } = new Dictionary<string, string>();
		public string CorrelationId { get; private  set; }

		public HttpMethod Method
		{
			get { return HttpRequest.Method; }
			set { HttpRequest.Method = value; }
		}

		public Request(Primitive.Config.IRequestConfig cfg)
		{
			Config = cfg;
			HttpRequest = new HttpRequestMessage();
			CorrelationId = cfg.NewCorrelationId();
		}

		public void AddMultipartContent(byte[] content)
		{
			var multipartContent = new MultipartFormDataContent
			{
				new ByteArrayContent(content)
			};
			HttpRequest.Content = multipartContent;
		}

		public void AddMultipartContent(System.IO.Stream contentStream)
		{
			var multipartContent = new MultipartFormDataContent
			{
				new StreamContent(contentStream)
			};
			HttpRequest.Content = multipartContent;
		}

		public void AddPayload<T>(T payload)
		{
			var data= new SerializerFactory().GetSerializer(Config.ApiFormat).Serialize(payload);
			HttpRequest.Content = new StringContent(data, Encoding.UTF8, Config.GetContentType);
		}


		public void AddPayload(string payload)
		{
			HttpRequest.Content = new StringContent((string)payload, Encoding.UTF8, Config.GetContentType);
		}

		public string BuildQueryParams()
		{
			var list = new List<string>();
			foreach (var param in QueryParams)
			{
				if (param.Value != null)
				{
					list.Add(param.Key + "=" + param.Value);
				}
			}
			if (list.Count > 0)
			{
				return "?" + string.Join("&", list);
			}
			return "";
		}
		public async Task ValidateAccessToken()
		{
			await Config.ValidateAccessToken();
		}

		public void FinalizePreparation()
		{
			HttpRequest.Headers.Add(Headers.USER_AGENT, Config.UserAgent.Replace(" ", "_"));
			HttpRequest.RequestUri = new Uri(Config.BaseUrl + EndpointUri + BuildQueryParams());
			// call to genereate walmart headers should be done when RequestUri already defined
			// we need it's value to generate signature header
			AddWalmartHeaders();
		}

		private void AddWalmartHeaders()
		{
			var creds = Config.Credentials;

			HttpRequest.Headers.Add(Headers.WM_SVC_NAME, Config.ServiceName);
			if (!string.IsNullOrWhiteSpace(Config.ChannelType))
				HttpRequest.Headers.Add(Headers.WM_CONSUMER_CHANNEL_TYPE, Config.ChannelType);
			HttpRequest.Headers.Add(Headers.AUTHORIZATION, creds.Authorization);
			HttpRequest.Headers.Add(Headers.WM_SEC_ACCESS_TOKEN, Config.AccessToken);
			HttpRequest.Headers.Add(Headers.WM_QOS_CORRELATION_ID, CorrelationId);
			// Must go last.
			HttpRequest.Headers.Add(Headers.ACCEPT, Config.GetContentType);
		}


	}
}
