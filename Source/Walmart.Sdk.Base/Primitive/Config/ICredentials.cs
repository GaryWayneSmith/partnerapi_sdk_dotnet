namespace Walmart.Sdk.Base.Primitive.Config
{
	public interface ICredentials
	{
		string ClientID { get; set; }
		string ClientSecret { get; set; }
		string Authorization { get; }
	}
}
