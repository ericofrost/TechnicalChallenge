using CLI.Entity.Configuration;
using Microsoft.Extensions.Configuration;

namespace CLI.ExternalServices.Configuration
{
    /// <summary>
    /// Class responsible for providing all the configuration data the client needs
    /// </summary>
    public class ApiConfigurationManager
    {
        private static Type _headers = typeof(ApiHeaders);

        /// <summary>
        /// Gets the base url based on the client's name
        /// </summary>
        /// <returns>The api's base Url</returns>
        public static string GetApiBaseUrl(IConfiguration configuration)
        {
            var settings = new Settings();

            configuration.GetSection(nameof(Settings)).Bind(settings);

            return settings.ApiBaseUrl;
        }


        /// <summary>
        /// Gets the api Default Headers
        /// </summary>
        /// <param name="apiName">The api's client class name</param>
        /// <returns>Dictionary with the default api headers</returns>
        public static Dictionary<string, string> GetApiDefaultHeaders(string apiName)
        {

            if (_headers.GetFields()?.FirstOrDefault(x => x.Name.Contains(ApiName(apiName))) is null)
            {
                return _headers.GetField(ApiName(nameof(ApiHeaders.DefaultApi)))?.GetValue(Activator.CreateInstance(_headers)) as Dictionary<string, string>;
            }
            return _headers.GetField(ApiName(apiName))?.GetValue(Activator.CreateInstance(_headers)) as Dictionary<string, string>;

        }

        /// <summary>
        /// Removes the client in the api client class names
        /// </summary>
        /// <param name="name">The class name</param>
        /// <returns>Returns the formatted</returns>
        private static string ApiName(string name)
        {
            return name.Replace("Client", string.Empty).Trim();
        }
    }
}
