using System.Net;
using System.Text.RegularExpressions;
using CLI.ExternalServices.Configuration;
using CLI.ExternalServices.Extensions;
using CLI.Interfaces.ApiClient;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace CLI.ExternalServices.Clients
{
    /// <inheritdoc/>
    /// <inheritdoc cref="IGenericApiClient"/>
    public class GenericApiClient : IGenericApiClient
    {
        protected RestClient _client;

        protected readonly string _defaultUrlRegex;
        private readonly IConfiguration _configuration;
        protected string _apiName;

        /// <summary>
        /// Default Parameterless Controller
        /// </summary>
        public GenericApiClient(IConfiguration configuration)
        {
            _defaultUrlRegex = @"(\{.+?\})";
            this._configuration = configuration;
        }

        /// <inheritdoc/>
        public virtual async Task<TOut> DeleteAsync<TIn, TOut>(string operation = null, params string[] ids) where TIn : class where TOut : class
        {
            var response = (await this.SendRequestAsync<TIn>(Method.Delete, default, operation, ids));

            var result = response?.SerializeResponse<TOut>();

            result ??= (TOut)Activator.CreateInstance(typeof(TOut));

            this.CreateResponseWithStatus(result, response);

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<TOut> GetByIdAsync<TIn, TOut>(string operation = null, params string[] ids) where TIn : class where TOut : class
        {
            return (await this.SendRequestAsync<TIn>(Method.Get, default, operation, ids))?.SerializeResponse<TOut>();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TOut>> GetAsync<TIn, TOut>(string operation = null) where TIn : class where TOut : class
        {
            if (_client != null && this.ValidateUrlParameters(operation, null))
            {
                var request = this.GetRequestInstance(operation, Method.Get);

                var result = await _client.ExecuteAsync(request);

                return result.SerializeResponseCollection<TOut>();
            }

            return default;
        }

        /// <inheritdoc/>
        public async Task<HttpStatusCode> GetAsync(string operation = null)
        {
            if (_client != null && this.ValidateUrlParameters(operation, null))
            {
                var request = this.GetRequestInstance(operation, Method.Get);

                var result = await _client.ExecuteAsync(request);

                return result.StatusCode;
            }

            return HttpStatusCode.BadRequest;
        }

        /// <inheritdoc/>
        public virtual async Task<TOut> PostAsync<TIn, TOut>(TIn input, string operation = null, params string[] ids) where TIn : class where TOut : class
        {
            var response = await this.SendRequestAsync<TIn>(Method.Post, input, operation, ids);

            var result = response?.SerializeResponse<TOut>();

            result ??= (TOut)Activator.CreateInstance(typeof(TOut));

            this.CreateResponseWithStatus(result, response);

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<TOut> PutAsync<TIn, TOut>(TIn input, string operation = null, params string[] ids) where TIn : class where TOut : class
        {
            return (await this.SendRequestAsync<TIn>(Method.Put, input, operation, ids))?.SerializeResponse<TOut>();
        }

        /// <inheritdoc/>
        public virtual async Task PostAsync(string operation = null, params string[] ids)
        {
            await this.SendRequestAsync(Method.Post, operation, ids);
        }

        #region Private Methods

        /// <summary>
        /// Method that creates and sends the base request with default behavior
        /// </summary>
        /// <param name="method">Http verb</param>        
        /// <param name="operation">Operation endpoint</param>
        /// <param name="ids">Ids, if used</param>
        /// <returns>The rest response</returns>
        protected virtual async Task<RestResponse> SendRequestAsync(Method method, string operation = default, params string[] ids)
        {
            if (_client != null && this.ValidateUrlParameters(operation, ids))
            {
                var request = this.GetRequestInstance(operation, method);

                this.SetParameterNamesAndValues(request, operation, ids);

                var result = await _client.ExecuteAsync(request);

                return result;
            }

            return default;
        }

        /// <summary>
        /// Method that creates and sends the base request with default behavior
        /// </summary>
        /// <param name="method">Http verb</param>
        /// <param name="input">Input object</param>
        /// <param name="operation">Operation endpoint</param>
        /// <param name="ids">Ids, if used</param>
        /// <returns>The rest response</returns>
        protected virtual async Task<RestResponse> SendRequestAsync<TIn>(Method method, TIn input = default, string operation = default, params string[] ids) where TIn : class
        {
            if (_client != null && this.ValidateUrlParameters(operation, ids))
            {
                var request = this.GetRequestInstance(operation, method);

                this.SetParameterNamesAndValues(request, operation, ids);

                if (!(input is default(TIn)))
                {
                    request.AddJsonBody(input);
                }

                var result = await _client.ExecuteAsync(request);

                return result;
            }

            return default;
        }

        /// <summary>
        /// Configures the basic api parameters
        /// </summary>
        /// <param name="apiName">The api name configured in the app.config file</param>
        /// <remarks>This method should be called before any execution attempts</remarks>
        public virtual void ConfigureApi(string apiName)
        {
            if (!string.IsNullOrWhiteSpace(apiName))
            {
                _apiName = apiName;

                _client = new RestClient(ApiConfigurationManager.GetApiBaseUrl(this._configuration));

                _client.UseNewtonsoftJson(new Newtonsoft.Json.JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented
                });

                this.ConfigureCustomHeaders();

                return;
            }

            throw new Exception(""); //TODO: Create error handling projetct
        }

        /// <summary>
        /// Gets the request instance based on the method and operation
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="method"></param>
        protected virtual RestRequest GetRequestInstance(string operation, Method method)
        {
            return string.IsNullOrWhiteSpace(operation) ? new RestRequest(default(string), method) : new RestRequest(operation, method);
        }

        /// <summary>
        /// Configures Api's Custom Headers, if there is any
        /// </summary>
        protected virtual void ConfigureCustomHeaders()
        {
            this._client.AddDefaultHeaders(ApiConfigurationManager.GetApiDefaultHeaders(_apiName));
        }

        /// <summary>
        /// Validates if the url parameters were provided properly
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="ids"></param>
        /// <returns>True if the number ids is equal to the number of url parameters expected in the operation, otherwise returns false</returns>
        protected virtual bool ValidateUrlParameters(string operation, string[] ids)
        {
            var result = true;

            if (!string.IsNullOrWhiteSpace(operation))
            {
                var count = Regex.Matches(operation, _defaultUrlRegex).Count;

                if (count > 0 && ids.Length != count)
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Set the request parameters based on the operations parameters
        /// </summary>
        /// <param name="request">The rest request object</param>
        /// <param name="operation">The operations endpoint</param>
        /// <param name="ids">The id provided as parameters</param>
        protected virtual void SetParameterNamesAndValues(RestRequest request, string operation, string[] ids)
        {
            if (!string.IsNullOrWhiteSpace(operation) && this.ValidateUrlParameters(operation, ids))
            {
                var matches = Regex.Matches(operation, _defaultUrlRegex);

                for (int i = 0; i < matches.Count; i++)
                {
                    request.AddParameter(matches[i].Value.RemoveCurlyBraces(), ids[i], ParameterType.UrlSegment);
                }
            }
        }

        /// <summary>
        /// Deals with the properties needed by the base class
        /// </summary>
        /// <param name="result"></param>
        /// <param name="response"></param>
        protected void CreateResponseWithStatus(object result, RestResponse response)
        {
            var prop = result?.GetType()?.GetProperty("StatusCode");
            var prop2 = result?.GetType()?.GetProperty("ErrorMessage");
            var prop3 = result?.GetType()?.GetProperty("Success");

            prop?.SetValue(result, response?.StatusCode);
            prop2?.SetValue(result, response?.ErrorException?.ToString());
            prop3?.SetValue(result, response?.ErrorException == null);
        }

        #endregion Private Methods
    }
}
