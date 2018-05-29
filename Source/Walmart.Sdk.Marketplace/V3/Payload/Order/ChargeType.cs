/*
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

//  ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 4.4.0.7
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace Walmart.Sdk.Marketplace.V3.Payload.Order
{
    using System;
    using System.Xml.Serialization;
    using Walmart.Sdk.Base.Primitive;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "4.4.0.7")]
    [Serializable]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders", TypeName="chargeType")]
    [XmlRootAttribute("chargeType")]
    public class ChargeType : BasePayload
    {
        [XmlElement("chargeType", ElementName="chargeType1")]
        public string ChargeType1 { get; set; }
        [XmlElement("chargeName")]
        public string ChargeName { get; set; }
        [XmlElement("chargeAmount")]
        public MoneyType ChargeAmount { get; set; }
        [XmlElement("tax")]
        public TaxType Tax { get; set; }
    
        /// <summary>
        /// ChargeType class constructor
        /// </summary>
        public ChargeType()
        {
            Tax = new TaxType();
            ChargeAmount = new MoneyType();
        }
    }
}
