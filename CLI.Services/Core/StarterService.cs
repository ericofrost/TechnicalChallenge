using CLI.Interfaces.Services;
using CLI.Services.Constants;
using Microsoft.Extensions.Hosting;

namespace CLI.Services.Core
{
    /// <summary>
    /// Starter class - Application Entry Point
    /// </summary>
    public partial class StarterService : IHostedService
    {
        private readonly ICustomerService _service;

        public StarterService(ICustomerService service)
        {
            this._service = service;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            string[] args = Environment.GetCommandLineArgs();

            switch (args[1])
            {
                case Commands.CREATE_USER: await this.CreateCustomer(args); break;
                case Commands.ADD_PRODUCT: await this.AddProductToWishList(args); break;
                case Commands.REMOVE_PRODUCT: await this.RemoveProductFromList(args); ; break;

                default: throw new ArgumentException("The action parameter was not found.");
            }

            await this.StopAsync(cancellationToken);
        }



        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Environment.Exit(0);
            });
        }
    }
}
