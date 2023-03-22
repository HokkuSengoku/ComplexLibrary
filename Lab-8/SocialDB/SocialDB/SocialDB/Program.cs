using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialDB.Domain;
using SocialDB.DataAccess;
using SocialDB.DataAccess.EntityFramework;
using SocialDB.Application;

internal class Program
{
    public static void Main(string[] args)
    {
        IHost host = CreateHostBuilder(args).Build(); 

        var dataContext = host.Services.GetService<DataContext>();

        ISocialRepository socialRepository = new EntityFrameworkSocialRepository(dataContext);
        var socialModel = new ConsoleManager((EntityFrameworkSocialRepository)socialRepository);
        socialModel.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) 
        => Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<DataContext>(
                    options => options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnectionString")));
            })
            .UseConsoleLifetime();
}