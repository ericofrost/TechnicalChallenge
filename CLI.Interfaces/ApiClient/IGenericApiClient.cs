namespace CLI.Interfaces.ApiClient
{
    /// <summary>
    /// Base generic interface for api client services
    /// </summary>
    /// <remarks>TIn - Generic Output type</remarks>
    /// <remarks>TOut - Generic Output type</remarks>
    public interface IGenericApiClient
    {
        /// <summary>
        /// Method for POST operations
        /// </summary>
        /// <param name="input">Input data</param>
        /// <returns>Output data</returns>
        Task<TOut> PostAsync<TIn, TOut>(TIn input, string operation = null, params string[] ids) where TIn : class where TOut : class;

        /// <summary>
        /// Method for POS operation that does not return a result
        /// </summary>
        /// <param name="operation">Operation url</param>
        /// <param name="ids">Ids to provide</param>
        Task PostAsync(string operation = null, params string[] ids);

        /// <summary>
        /// Method for GET operations
        /// </summary>
        /// <param name="operation">Operation url</param>
        /// <param name="ids">Ids to provide</param>
        /// <returns>Collection of Output data</returns>
        Task<TOut> GetByIdAsync<TIn, TOut>(string operation = null, params string[] ids) where TIn : class where TOut : class;


        /// <summary>
        /// Method for GET operations
        /// </summary>
        /// <param name="operation">Operation url</param>
        /// <returns>Collection of Output data</returns>
        Task<IEnumerable<TOut>> GetAsync<TIn, TOut>(string operation = null) where TIn : class where TOut : class;

        /// <summary>
        /// Method for PUT operations
        /// </summary>
        /// <param name="input">Input data</param>
        /// <returns>Output data</returns>
        Task<TOut> PutAsync<TIn, TOut>(TIn input, string operation = null, params string[] ids) where TIn : class where TOut : class;

        /// <summary>
        /// Method for DELETE operations
        /// </summary>
        /// <param name="input">Input data</param>
        /// <returns>Output data</returns>
        Task<TOut> DeleteAsync<TIn, TOut>(string operation = null, params string[] ids) where TIn : class where TOut : class;

        /// <summary>
        /// Configures the basic api parameters
        /// </summary>
        /// <param name="apiName">The api name configured in the app.config file</param>
        /// <remarks>This method should be called before any execution attempts</remarks>
        void ConfigureApi(string apiName);
    }
}
