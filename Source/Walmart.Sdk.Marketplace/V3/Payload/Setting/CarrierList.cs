using System.Collections.Generic;

namespace Walmart.Sdk.Marketplace.V3.Payload.Setting
{
	public class CarrierList
	{
		public List<Carrier> Carriers { get; set; }

		public CarrierList()
		{
			Carriers = new List<Carrier>();
		}
	}
}
