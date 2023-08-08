namespace CLI.Entity.ExternalServices.Responses
{
    /// <summary>
    /// Response model used by GetById api endpoint
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Product
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.AllowNull)]
        public System.Guid? Id { get; set; }

        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.AllowNull)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Description { get; set; }

    }
}
