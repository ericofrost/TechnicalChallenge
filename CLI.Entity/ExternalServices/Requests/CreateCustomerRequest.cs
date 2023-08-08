namespace CLI.Entity.ExternalServices.Requests
{
    /// <summary>
    /// Request model used by CreateCustomer api endpoint
    /// </summary>
    public partial class CreateCustomerRequest
    {
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.AllowNull)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.AllowNull)]
        public string Description { get; set; }

        [Newtonsoft.Json.JsonProperty("tenantId", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public Guid TenantId { get; set; }

        /// <summary>
        /// Represents the status of the customer. False if disabled
        /// </summary>
        [Newtonsoft.Json.JsonProperty("enabled", Required = Newtonsoft.Json.Required.Always)]
        public bool Enabled { get; set; }

    }
}
