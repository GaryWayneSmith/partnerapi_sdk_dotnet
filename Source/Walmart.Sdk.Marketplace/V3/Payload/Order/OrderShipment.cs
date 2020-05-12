using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Walmart.Sdk.Marketplace.V3.Payload.Order
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "4.4.0.7")]
	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[XmlTypeAttribute(Namespace = "http://walmart.com/mp/v3/orders")]
	[XmlRootAttribute("orderShipment", Namespace = "http://walmart.com/mp/v3/orders", IsNullable = false)]
	public class OrderShipment
	{
		[XmlElement("orderLines", IsNullable = false)]
		public OrderLines OrderLines { get; set; }

		public OrderShipment()
		{
			OrderLines = new OrderLines();
		}
	}
}
