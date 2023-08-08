namespace CLI.Entity.Dto
{
    public class AddProducToWishListDto
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
