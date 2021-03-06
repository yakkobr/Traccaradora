namespace Traccaradora.Web.Clients
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.3.0.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class DeviceAccumulators
    {
        [Newtonsoft.Json.JsonProperty("deviceId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? DeviceId { get; set; }

        /// <summary>in meters</summary>
        [Newtonsoft.Json.JsonProperty("totalDistance", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? TotalDistance { get; set; }

        [Newtonsoft.Json.JsonProperty("hours", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double? Hours { get; set; }

        private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }


    }
}
#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore 472
#pragma warning restore 114
#pragma warning restore 108