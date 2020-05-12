using System;
using System.Collections.Generic;
using System.Text;

namespace Walmart.Sdk.Marketplace.V3.Payload.Order
{
	using System;
	using System.Xml.Serialization;
	using System.Collections.Generic;
	using Walmart.Sdk.Base.Primitive;

	[XmlTypeAttribute(Namespace = "http://walmart.com/mp/v3/orders", TypeName = "orderLinesType")]
	[XmlRootAttribute("orderLines", Namespace = "http://walmart.com/mp/v3/orders", IsNullable = false)]
	public class OrderLines : BasePayload
	{
		[XmlElement("orderLine")]
		public List<OrderLineType> Lines { get; set; }

		public OrderLines()
		{
			Lines = new List<OrderLineType>();
		}
	}

}
