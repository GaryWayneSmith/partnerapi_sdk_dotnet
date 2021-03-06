﻿/**
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
using System.Threading.Tasks;

namespace Walmart.Sdk.Base.Primitive.Config
{
	public interface IRequestConfig
	{
		ApiFormat ApiFormat { get; }
		string BaseUrl { get; }
		string ServiceName { get; }
		string ChannelType { get; }
		string UserAgent { get; }
		ICredentials Credentials { get; }
		int RequestTimeoutMs { get; }
		string NewCorrelationId();
		string AccessToken { get; }
		string TokenType { get; }
		DateTime Expires { get; }
		bool IsExpired { get; }
		Task ValidateAccessToken();
		string GetContentType(ApiFormat apiFormat);
	}
}
