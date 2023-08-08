namespace CLI.Entity.Dto
{
    /// <summary>
    /// The output dto for creating a new customer
    /// </summary>
    public class CreateCustomerOutputDto : BaseOutputDto
    {
        public Guid CustomerId { get; set; }
    }
}
