using CLI.Entity.ExternalServices.Requests;
using CLI.Entity.ExternalServices.Responses;

namespace CLI.Interfaces.ApiClient
{
    /// <summary>
    /// The Customer Api Client with the custom operations needed
    /// </summary>
    public interface ICustomerApiClient
    {
        /// <summary>
        /// Creates a customer to enable adding products to wishlists
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResourceCreatedResponse> CreateCustomer(CreateCustomerRequest request);

        /// <summary>
        /// Adds a product to a wishlist
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResourceCreatedResponse> AddProductToWishList(AddProductToWishListRequest request);

        /// <summary>
        /// Removes a product previously added to a wishlist
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductRemovedResponse> RemoveProductToWishList(RemoveProductFromWishListRequest request);
    }
}
