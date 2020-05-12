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
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Walmart.Sdk.Base.Primitive.Config;
using Walmart.Sdk.Base.Serialization;

namespace Walmart.Sdk.Base.Primitive
{
	// Main configuration class, holds all global settings for SDK
	public class BaseConfig : IRequestConfig, IApiClientConfig, IAccessToken
	{
		public ICredentials Credentials { get; private set; }
		public string BaseUrl { get; set; } = "https://marketplace.walmartapis.com";
		virtual public string ServiceName { get; set; } = "";
		public string ChannelType { get; set; }
		public string UserAgent { get; set; }
		public ApiFormat PayloadFormat { get; set; } = ApiFormat.XML;
		// just another name for paylod format we also use it for http request
		// and in this case it helps to determine value of contet-type header, not payload
		public ApiFormat ApiFormat
		{
			get { return PayloadFormat; }
			set { PayloadFormat = value; }
		}
		public int RequestTimeoutMs { get; set; } = 100000; // in milliseconds

		public string AccessToken { get; private set; }
		public string TokenType { get; private set; }
		public DateTime Expires { get; private set; }
		public bool IsExpired
		{
			get
			{
				return DateTime.UtcNow > Expires;
			}
		}

		public IAccessToken Clone()
		{
			return null;
		}

		public string NewCorrelationId()
		{
			return Guid.NewGuid().ToString();
		}

		public string GetContentType
		{
			get
			{
				switch (ApiFormat)
				{
					case Primitive.ApiFormat.JSON:
						return "application/json";
					default:
					case Primitive.ApiFormat.XML:
						return "application/xml";
				}
			}
		}


		public async Task ValidateAccessToken()
		{
			// Do we have a valid token?
			if (!IsExpired)
			{
				Debug.WriteLine("Existing token is value");
				return;
			}

			// Core retreival of token
			using (HttpClient client = new HttpClient())
			{
				using (HttpRequestMessage requestMessage = new HttpRequestMessage())
				{
					string correlationId = Guid.NewGuid().ToString();

					requestMessage.Method = HttpMethod.Post;
					requestMessage.RequestUri = new Uri(BaseUrl + "/v3/token");
					requestMessage.Headers.Add(Headers.USER_AGENT, UserAgent);
					// call to genereate walmart headers should be done when RequestUri already defined
					// we need it's value to generate signature header

					//var creds = Config.Credentials;

					requestMessage.Headers.Add(Headers.WM_SVC_NAME, ServiceName);
					if (!string.IsNullOrWhiteSpace(ChannelType))
						requestMessage.Headers.Add(Headers.WM_CONSUMER_CHANNEL_TYPE, ChannelType);
					requestMessage.Headers.Add(Headers.AUTHORIZATION, Credentials.Authorization);
					requestMessage.Headers.Add(Headers.WM_QOS_CORRELATION_ID, correlationId);
					// Must go last.
					requestMessage.Headers.Add(Headers.ACCEPT, GetContentType);

					requestMessage.Content = new FormUrlEncodedContent(new[]
					{
						new KeyValuePair<string, string>("grant_type", "client_credentials"),
					});

					var response = await client
						.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead)
						.ConfigureAwait(false);

					response.EnsureSuccessStatusCode();

					string result = await response.Content.ReadAsStringAsync();
					//Debug.WriteLine(result);
					Token token = new SerializerFactory()
						.GetSerializer(ApiFormat)
						.Deserialize<Token>(result);
					AccessToken = token.AccessToken;
					TokenType = token.TokenType;
					Expires = DateTime.UtcNow.AddSeconds(token.Expires-30);
				}
			}
		}

		public BaseConfig(string clientId, string clientSecret)
		{
			// generate sdk name from an assembly information
			var assembly = GetType().GetTypeInfo().Assembly;
			UserAgent = string.Format(".Net_{0}_v{1}_{2}", assembly.GetName().Name, assembly.GetName().Version.ToString(), clientId);

			// storing user credentials
			Credentials = new Credentials(clientId, clientSecret);
		}

		public IRequestConfig GetRequestConfig() => this;


	}


}