using CLI.Entity.ExternalServices.Requests;
using CLI.Entity.ExternalServices.Responses;
using CLI.ExternalServices.Configuration;
using CLI.Interfaces.ApiClient;

namespace CLI.ExternalServices.Clients
{
    /// <summary>
    /// Customer Api Client implementation
    /// </summary>
    public class CustomerApiClient : ICustomerApiClient
    {
        private readonly IGenericApiClient _genericApiClient;

        /// <summary>
        /// Default parameter constructor
        /// </summary>
        /// <param name="genericApiClient"></param>
        public CustomerApiClient(IGenericApiClient genericApiClient)
        {
            this._genericApiClient = genericApiClient;
            this._genericApiClient.ConfigureApi(this.GetType().Name);
        }

        /// <inheritdoc/>
        public async Task<ResourceCreatedResponse> AddProductToWishList(AddProductToWishListRequest request)
        {
            var response = new ResourceCreatedResponse();

            try
            {
                response = await this._genericApiClient.PostAsync<AddProductToWishListRequest, ResourceCreatedResponse>(request, CustomerApiEndpoints.AddProductToWishList, request.CustomerId.ToString());
            }
            catch (Exception ex)
            {
                this.SetExceptionMessage(response, ex);
            }

            return response;
        }

        /// <inheritdoc/>
        public async Task<ResourceCreatedResponse> CreateCustomer(CreateCustomerRequest request)
        {
            var response = new ResourceCreatedResponse();

            try
            {
                response = await this._genericApiClient.PostAsync<CreateCustomerRequest, ResourceCreatedResponse>(request, CustomerApiEndpoints.CreateCustomer);
            }
            catch (Exception ex)
            {
                this.SetExceptionMessage(response, ex);
            }

            return response;
        }

        /// <inheritdoc/>
        public async Task<ProductRemovedResponse> RemoveProductToWishList(RemoveProductFromWishListRequest request)
        {
            var response = new ProductRemovedResponse();

            try
            {
                response = await this._genericApiClient.DeleteAsync<RemoveProductFromWishListRequest, ProductRemovedResponse>(CustomerApiEndpoints.RemoveProductToWishList, request.CustomerId.ToString(), request.ProductId.ToString());
            }
            catch (Exception ex)
            {
                this.SetExceptionMessage(response, ex);
            }

            return response;
        }

        /// <summary>
        /// Sets the exception message to the base response message string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="ex"></param>
        private void SetExceptionMessage<T>(T obj, Exception ex) where T : BaseResponse
        {
            if (obj != null)
                obj.ErrorMessage = ex?.ToString();
        }
    }
}
