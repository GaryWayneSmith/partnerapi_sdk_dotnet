using System;
using System.Collections.Generic;
using System.Text;
using Walmart.Sdk.Base.Primitive.Config;

namespace Walmart.Sdk.Base.Primitive
{

	// Merchant Credentials
	public class Credentials : ICredentials
	{
		public string ClientID { get; set; }
		public string ClientSecret { get; set; }

		public Credentials(string clientId, string clientSecret)
		{
			ClientID = clientId;
			ClientSecret = clientSecret;
		}

		public string Authorization
		{
			get
			{
				return "Basic " + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ClientID + ":" + ClientSecret));
			}
		}
	}
}
