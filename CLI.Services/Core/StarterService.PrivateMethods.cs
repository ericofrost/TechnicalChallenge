using CLI.Entity.Dto;

namespace CLI.Services.Core
{
    /// <summary>
    /// Partial class to store the private methods
    /// </summary>
    public partial class StarterService
    {
        /// <summary>
        /// Calls the create customer method
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task CreateCustomer(string[] args)
        {
            this.ValidateNumberOfArgs(args, 5);

            var result = await this._service.CreateCustomer(new CreateCustomerDto
            {
                Name = args[2],
                Description = args[3],
                Enabled = bool.TryParse(args[4], out var enabled) ? enabled : true,
            });

            Console.WriteLine($"{nameof(result.CustomerId)} - {result.CustomerId}");
            Console.WriteLine($"Messages - {result.ErrorMessage}");
        }

        /// <summary>
        /// Calls the add product to wishlist method
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task AddProductToWishList(string[] args)
        {
            this.ValidateNumberOfArgs(args, 6);

            Guid id1, id2;

            Guid.TryParse(args[2], out id1);
            Guid.TryParse(args[3], out id2);

            var result = await this._service.AddProductToWishList(new AddProducToWishListDto
            {
                CustomerId = id1,
                ProductId = id2,
                Name = args[4],
                Description = args[5]
            });

            Console.WriteLine($"{nameof(result.ProductId)} - {result.ProductId}");
            Console.WriteLine($"Messages - {result.ErrorMessage}");
        }

        /// <summary>
        /// Calls the remove product from wish list method
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task RemoveProductFromList(string[] args)
        {
            this.ValidateNumberOfArgs(args, 4);

            var result = await this._service.RemoveProductFromWishList(args[2], args[3]);

            Console.WriteLine($"Messages - {result.ErrorMessage}");
        }

        /// <summary>
        /// Validates the number of parameters
        /// </summary>
        /// <param name="args"></param>
        /// <param name="num"></param>
        private void ValidateNumberOfArgs(string[] args, int num)
        {
            if (args.Length != num)
                this.ArumentsNotProvided();
        }

        /// <summary>
        /// Throws an argument exception if the arguments are not entirely provided
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void ArumentsNotProvided()
        {
            throw new ArgumentException("Not enough parameters provided.");
        }

    }
}
