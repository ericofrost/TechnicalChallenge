// See https://aka.ms/new-console-template for more information

using CLI.Core.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");


IHost host = Host.CreateDefaultBuilder(args)
.ConfigureServices(services =>
{
    services.ConfigureDependencyInjection(args);

}).ConfigureHostConfiguration(options =>
{
    options.AddCommandLine(args);
    options.AddJsonFile("appsettings.json", false, true);
})
.Build();

await host.RunAsync();
