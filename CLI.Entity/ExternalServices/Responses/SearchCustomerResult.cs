namespace CLI.Entity.ExternalServices.Responses
{
    public partial class SearchCustomerResult : BaseResponse
    {
        [Newtonsoft.Json.JsonProperty("pageSize", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? PageSize { get; set; }

        [Newtonsoft.Json.JsonProperty("pageNumber", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? PageNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("results", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ICollection<SearchCustomerResultItem> Results { get; set; }

    }
}
