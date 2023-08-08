using AutoMapper;
using CLI.Entity.Dto;
using CLI.Entity.ExternalServices.Requests;
using CLI.Entity.ExternalServices.Responses;
using CLI.Interfaces.ApiClient;
using CLI.Interfaces.Services;

namespace CLI.Services.Customer
{
    /// <summary>
    /// Customer service implementation
    /// </summary>
    public partial class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerApiClient _client;

        /*
         * With more time, I would've avoided using automapper directly here at service layer, it would be used on a façade layer only responsible 
         * for data convertion, generating an extra level of abstraction leaving the service layer completely decoupled from the rest of the application
         */

        public CustomerService(IMapper mapper, ICustomerApiClient client)
        {
            this._mapper = mapper;
            this._client = client;
        }

        public async Task<AddProductToWishListOutputDto> AddProductToWishList(AddProducToWishListDto input)
        {
            var result = new AddProductToWishListOutputDto();

            if (this.IsAddToWishListInputValid(input))
            {
                var serviceInput = this._mapper.Map<AddProductToWishListRequest>(input);

                serviceInput.Id = this.GenerateNewId();

                var response = await this._client.AddProductToWishList(serviceInput);

                //If I had more time, I would create an error entity with codes and proper error messages
                this.CloneErrorMessage(result, response);

                if (string.IsNullOrWhiteSpace(response.ErrorMessage) && response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    result.ProductId = serviceInput.Id;
                }
                else
                {
                    this.SetErrorMessage(result, response.StatusCode.Value);
                }
            }
            else
            {
                this.SetValidationMessage(result);
            }

            return result;
        }

        public async Task<CreateCustomerOutputDto> CreateCustomer(CreateCustomerDto input)
        {
            var result = new CreateCustomerOutputDto();

            if (this.IsCustomerInputValid(input))
            {
                var serviceInput = this._mapper.Map<CreateCustomerRequest>(input);

                serviceInput.TenantId = this.GenerateNewId();

                var response = await this._client.CreateCustomer(serviceInput);

                this.CloneErrorMessage(result, response);

                if (response.Success && response.Id.HasValue && response.Id.Value != Guid.Empty)
                    result.CustomerId = response.Id.Value;

            }
            else
            {
                this.SetValidationMessage(result);
            }

            return result;
        }

        public async Task<BaseOutputDto> RemoveProductFromWishList(string productId, string customerId)
        {
            var result = new BaseOutputDto();

            Guid prodId, custId;

            if (Guid.TryParse(productId, out prodId) && Guid.TryParse(customerId, out custId))
            {
                var response = await this._client.RemoveProductToWishList(new RemoveProductFromWishListRequest { CustomerId = custId, ProductId = prodId });

                this.CloneErrorMessage(result, response);

                if (response.StatusCode.HasValue && response.StatusCode.Value != System.Net.HttpStatusCode.NoContent)
                {
                    this.SetErrorMessage(result, response.StatusCode.Value);
                }
            }
            else
            {
                this.SetValidationMessage(result);
            }

            return result;
        }

        /// <summary>
        /// Clones the error messages
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="output"></param>
        /// <param name="response"></param>
        private void CloneErrorMessage<T, R>(T output, R response) where T : BaseOutputDto where R : BaseResponse
        {
            output.ErrorMessage = response.ErrorMessage;
        }

        private void SetErrorMessage<T>(T output, System.Net.HttpStatusCode code) where T : BaseOutputDto
        {
            output.ErrorMessage += $"\n Error - {code}";
        }


        /// <summary>
        /// Generates new ID for creation operations
        /// </summary>
        /// <returns></returns>
        private Guid GenerateNewId() => Guid.NewGuid();

    }
}
