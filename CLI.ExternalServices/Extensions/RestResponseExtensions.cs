using RestSharp;

namespace CLI.ExternalServices.Extensions
{
    /// <summary>
    /// Helper class with IRestResponse extensions to be used over the solution
    /// </summary>
    public static class RestResponseExtensions
    {
        /// <summary>
        /// Serializes the rest response content into the T type
        /// </summary>
        /// <typeparam name="T">Targe type</typeparam>
        /// <param name="response">The rest response object</param>
        /// <returns>T instance</returns>
        public static T SerializeResponse<T>(this RestResponse response) => JsonSerializationHelper.Serialize<T>(response.Content);

        /// <summary>
        /// Serializes the response into a collection of T elements
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        /// <param name="response">The rest response object</param>
        /// <returns>Collection of T objects</returns>
        public static IEnumerable<T> SerializeResponseCollection<T>(this RestResponse response) => JsonSerializationHelper.SerializeCollection<T>(response.Content);
    }
}
