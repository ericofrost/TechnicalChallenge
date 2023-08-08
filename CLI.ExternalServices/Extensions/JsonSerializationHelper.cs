using Newtonsoft.Json;

namespace CLI.ExternalServices.Extensions
{
    /// <summary>
    /// Helper for Json Serialization operations
    /// </summary>
    public class JsonSerializationHelper
    {
        /// <summary>
        /// Serialize json string into a collection
        /// </summary>
        /// <typeparam name="T">Type to be serialized</typeparam>
        /// <param name="content">Json string to serialize</param>
        /// <returns>A collection of T elements</returns>
        public static IEnumerable<T> SerializeCollection<T>(string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                var serialized = JsonConvert.DeserializeObject<IEnumerable<T>>(content);

                return serialized;
            }

            return default;
        }

        /// <summary>
        /// Serialize json string into a list
        /// </summary>
        /// <typeparam name="T">Type to be serialized</typeparam>
        /// <param name="content">Json string to serialize</param>
        /// <returns>A list of T elements</returns>
        public static List<T> SerializeList<T>(string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                var serialized = JsonConvert.DeserializeObject<List<T>>(content);

                return serialized;
            }

            return default;
        }

        /// <summary>
        /// Serialize json string into an object
        /// </summary>
        /// <typeparam name="T">Type to be serialized</typeparam>
        /// <param name="content">Json string to serialize</param>
        /// <returns>T Instance</returns>
        public static T Serialize<T>(string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                var serialized = JsonConvert.DeserializeObject<T>(content);

                return serialized;
            }

            return default;
        }

        /// <summary>
        /// Serialize json object into a string
        /// </summary>
        /// <typeparam name="T">Type to be serialized</typeparam>
        /// <param name="obj">Json string to serialize</param>
        /// <returns>T Instance</returns>
        public static string DeSerialize<T>(T obj)
        {
            if (obj != null)
            {
                var serialized = JsonConvert.SerializeObject(obj);

                return serialized;
            }

            return default;
        }

        /// <summary>
        /// Serialize json object into a string
        /// </summary>
        /// <typeparam name="T">Type to be serialized</typeparam>
        /// <param name="obj">Json string to serialize</param>
        /// <returns>T Instance</returns>
        public static string DeSerializeIndented<T>(T obj)
        {
            if (obj != null)
            {
                var serialized = JsonConvert.SerializeObject(obj, Formatting.Indented);

                return serialized;
            }

            return default;
        }
    }
}
