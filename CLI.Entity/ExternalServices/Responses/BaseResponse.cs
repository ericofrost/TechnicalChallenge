using System.Net;
using Newtonsoft.Json;

namespace CLI.Entity.ExternalServices.Responses
{
    /// <summary>
    /// Base httpresponse class
    /// </summary>
    public class BaseResponse
    {
        [JsonIgnore]
        public HttpStatusCode? StatusCode { get; set; }

        [JsonIgnore]
        public bool Success { get; set; }

        [JsonIgnore]
        public string ErrorMessage { get; set; }
    }
}
