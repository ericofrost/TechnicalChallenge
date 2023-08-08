using CLI.Entity.Dto;

namespace CLI.Interfaces.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Creates a new customer and returns its Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CreateCustomerOutputDto> CreateCustomer(CreateCustomerDto input);

        /// <summary>
        /// Adds a new product to a wishlist and returns the product Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AddProductToWishListOutputDto> AddProductToWishList(AddProducToWishListDto input);

        /// <summary>
        /// Removes a product from the wishlist
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        Task<BaseOutputDto> RemoveProductFromWishList(string productId, string CustomerId);
    }
}
