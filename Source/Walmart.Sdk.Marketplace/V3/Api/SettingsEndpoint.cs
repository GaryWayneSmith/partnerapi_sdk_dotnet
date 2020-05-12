using System.Collections.Generic;
using System.Threading.Tasks;
using Walmart.Sdk.Base.Primitive;
using Walmart.Sdk.Marketplace.V3.Payload.Setting;

namespace Walmart.Sdk.Marketplace.V3.Api
{
	public class SettingsEndpoint : Base.Primitive.BaseEndpoint
	{
		public SettingsEndpoint(Base.Primitive.IEndpointClient client) : base(client)
		{
			payloadFactory = new V3.Payload.PayloadFactory();
		}

		public async Task<List<Carrier>> GetCarrierMethods()
		{
			// to avoid deadlock if this method is executed synchronously
			await new ContextRemover();

			var request = CreateRequest();

			request.ApiFormat = ApiFormat.JSON;
			request.EndpointUri = "/v3/settings/shipping/carriers";
			var response = await client.GetAsync(request);
			var result = await ProcessResponse<List<Carrier>>(response);
			return result; //.Carriers;
		}
	}
}