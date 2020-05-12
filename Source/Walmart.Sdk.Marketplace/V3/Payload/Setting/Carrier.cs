using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Walmart.Sdk.Marketplace.V3.Payload.Setting
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "4.4.0.7")]
	[Serializable]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[XmlTypeAttribute(Namespace = "http://walmart.com/mp/v3/settings", TypeName = "carrier")]
	[XmlRootAttribute("carriers")]
	public class Carrier
	{
		[XmlElement("carrierMethodId")]
		public int CarrierMethodId { get; set; }

		[XmlElement("carrierMethodName")]
		public string CarrierMethodName { get; set; }

		[XmlElement("carrierMethodType")]
		public string CarrierMethodType { get; set; }

		[XmlElement("carrierMethodDescription")]
		public string carrierMethodDescription { get; set; }
	}
}
