namespace CLI.Entity.ExternalServices.Requests
{
    /// <summary>
    /// Request model used by UpdateCustomer api endpoint
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class UpdateCustomerRequest
    {
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.AllowNull)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.AllowNull)]
        public string Description { get; set; }

        /// <summary>
        /// Represents the status of the customer. False if disabled
        /// </summary>
        [Newtonsoft.Json.JsonProperty("enabled", Required = Newtonsoft.Json.Required.Always)]
        public bool Enabled { get; set; }

    }
}
