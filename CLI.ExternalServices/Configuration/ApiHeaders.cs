namespace CLI.ExternalServices.Configuration
{
    /// <summary>
    /// Class with the api headers to be configured by the client
    /// </summary>
    public class ApiHeaders
    {
        /// <summary>
        /// Gets the default headers for json api
        /// </summary>
        public static readonly Dictionary<string, string> DefaultApi = new Dictionary<string, string> { { "Content-Type", "application/json" } };
    }
}
