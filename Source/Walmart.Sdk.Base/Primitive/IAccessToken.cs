using System;
using System.Threading.Tasks;

namespace Walmart.Sdk.Base.Primitive
{
	public interface IAccessToken
	{
		string AccessToken { get; }
		string TokenType { get; }
		DateTime Expires { get; }
		bool IsExpired { get; }
		IAccessToken Clone();
		Task ValidateAccessToken();
	}
}
