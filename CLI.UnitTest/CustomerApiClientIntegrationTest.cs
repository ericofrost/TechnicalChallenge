using System.Net;
using CLI.Entity.ExternalServices.Requests;
using CLI.ExternalServices.Clients;
using CLI.Interfaces.ApiClient;
using Microsoft.Extensions.Configuration;

namespace CLI.UnitTest
{
    /// <summary>
    /// Customer client integration test class
    /// </summary>
    public class CustomerApiClientIntegrationTest
    {
        private IConfiguration _configuration;
        private ICustomerApiClient _customerClient;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerApiClientIntegrationTest()
        {
            this.CreateConfiguration();
            this.CreateClientInstance();
        }

        [Fact]
        public async Task CreateACustomer_Success()
        {
            var id = Guid.NewGuid();

            var input = new CreateCustomerRequest
            {
                Enabled = true,
                Description = $"Test_Customer_{id} Description",
                Name = $"Test_Customer_{id}",
                TenantId = id
            };

            var result = await _customerClient.CreateCustomer(input);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task CreateACustomer_SuccessStatus()
        {
            var id = Guid.NewGuid();

            var input = new CreateCustomerRequest
            {
                Enabled = true,
                Description = $"Test_Customer_{id} Description",
                Name = $"Test_Customer_{id}",
                TenantId = id
            };

            var result = await _customerClient.CreateCustomer(input);

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task AddProductToWishList_Success()
        {
            var input = this.GenerateCustomerRequest();

            var result = await _customerClient.CreateCustomer(input);

            var wishListInput = this.GenerateAddProductRequest(result.Id.Value);

            var result2 = await _customerClient.AddProductToWishList(wishListInput);

            //The api returns status no content when the operation is successful, because when tryed to execute again, a Conflict response was returned.
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.Equal(HttpStatusCode.NoContent, result2.StatusCode);
        }

        [Fact]
        public async Task RemoveProductFromWishList_NotFound()
        {
            var input = new RemoveProductFromWishListRequest
            {
                CustomerId = Guid.Parse("f6faa545-57ce-4646-bf10-9b9a96d7b564"),
                ProductId = Guid.Parse("1acb8519-b06f-441e-9145-a4b698df380a")
            };

            var result = await _customerClient.RemoveProductToWishList(input);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task RemoveProductFromWishList_Ok()
        {
            var input = this.GenerateCustomerRequest();

            var result = await _customerClient.CreateCustomer(input);

            var wishListInput = this.GenerateAddProductRequest(result.Id.Value);

            var result2 = await _customerClient.AddProductToWishList(wishListInput);

            var removeInput = new RemoveProductFromWishListRequest
            {
                CustomerId = result.Id.Value,
                ProductId = wishListInput.Id
            };

            var result3 = await _customerClient.RemoveProductToWishList(removeInput);

            //The api returns status no content when the operation is successful, because when tryed to execute again, a Not Found response was returned.
            Assert.Equal(HttpStatusCode.NoContent, result3.StatusCode);
        }

        #region Private Methods

        private CreateCustomerRequest GenerateCustomerRequest()
        {
            var id = Guid.NewGuid();

            var input = new CreateCustomerRequest
            {
                Enabled = true,
                Description = $"Test_Customer_{id} Description",
                Name = $"Test_Customer_{id}",
                TenantId = id
            };

            return input;
        }

        private AddProductToWishListRequest GenerateAddProductRequest(Guid customerId)
        {
            var productId = Guid.NewGuid();

            var wishListInput = new AddProductToWishListRequest
            {
                CustomerId = customerId,
                Id = productId,
                Description = $"Added product {productId}",
                Name = $"Product {productId}"
            };

            return wishListInput;
        }

        /// <summary>
        /// Creates the ICOnfiguration to use within the tests
        /// </summary>
        private void CreateConfiguration()
        {
            this._configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        /// <summary>
        /// Creates the Client Instance to be used in the tests
        /// </summary>
        private void CreateClientInstance()
        {
            var genericClient = new GenericApiClient(_configuration);

            this._customerClient = new CustomerApiClient(genericClient);
        }

        #endregion Private Methods
    }
}