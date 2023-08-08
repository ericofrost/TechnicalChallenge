namespace CLI.Entity.ExternalServices.Responses
{
    /// <summary>
    /// Response model returned by api endpoints when a new resource is created
    /// </summary>
    public partial class ResourceCreatedResponse : BaseResponse
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Guid? Id { get; set; }

    }
}
