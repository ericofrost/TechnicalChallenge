namespace CLI.Entity.Dto
{
    /// <summary>
    /// Dto for creating a customer
    /// </summary>
    public class CreateCustomerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
