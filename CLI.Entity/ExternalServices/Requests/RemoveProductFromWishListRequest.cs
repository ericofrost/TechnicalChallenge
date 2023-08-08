namespace CLI.Entity.ExternalServices.Requests
{
    public class RemoveProductFromWishListRequest
    {
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
