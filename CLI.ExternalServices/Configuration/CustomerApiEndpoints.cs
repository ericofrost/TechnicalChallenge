namespace CLI.ExternalServices.Configuration
{
    /// <summary>
    /// Customer api endpoints constants
    /// </summary>
    public static class CustomerApiEndpoints
    {
        public static readonly string CreateCustomer = "/v1/customers";
        public static readonly string AddProductToWishList = "/v1/customers/{id}/wishListProducts";
        public static readonly string RemoveProductToWishList = "/v1/customers/{id}/wishListProducts/{productId}";
    }
}
