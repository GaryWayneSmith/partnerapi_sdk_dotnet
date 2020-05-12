using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Walmart.Sdk.Base.Primitive
{
	[XmlRootAttribute("OAuthTokenDTO")]
	public class Token
	{
		[JsonProperty("access_token")]
		[XmlElement("accessToken")]
		public string AccessToken { get; set; }

		[JsonProperty("token_type")]
		[XmlElement("tokenType")]
		public string TokenType { get; set; }

		[JsonProperty("expires_in")]
		[XmlElement("expiresIn")]
		public int Expires { get; set; }
	}
}
