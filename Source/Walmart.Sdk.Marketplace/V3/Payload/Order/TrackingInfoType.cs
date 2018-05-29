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

using Walmart.Sdk.Base.Primitive;

namespace Walmart.Sdk.Marketplace.V3.Payload.Order
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Xml;
    using System.Collections.Generic;

    /// <summary>
    /// If using OtherCarrier instead of listed Carriers,
    /// sellers must provide trackingURL. The below assert will be imposed from
    /// version 1.1
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "4.4.0.7")]
    [Serializable]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders", TypeName="trackingInfoType")]
    [XmlRootAttribute("trackingInfoType")]
    public class TrackingInfoType : BasePayload
    {
        [XmlElement("shipDateTime")]
        public System.DateTime ShipDateTime { get; set; }
        [XmlElement("carrierName")]
        public CarrierNameType CarrierName { get; set; }
        [XmlElement("methodCode")]
        public ShippingMethodCodeType MethodCode { get; set; }
        [XmlElement("trackingNumber")]
        public string TrackingNumber { get; set; }
        [XmlElement("trackingURL")]
        public string TrackingURL { get; set; }
    
        /// <summary>
        /// TrackingInfoType class constructor
        /// </summary>
        public TrackingInfoType()
        {
            CarrierName = new CarrierNameType();
        }
    }
}
