namespace MusicBrowser.Console
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MusicBrowser.Console.Application;
    using MusicBrowser.Console.DataAccess;
    using MusicBrowser.Console.DataAccess.AdoNet;
    using MusicBrowser.Console.DataAccess.EntityFramework;

    internal class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            var dataContext = host.Services.GetService<DataContext>();

            IMusicRepository musicRepository = new EntityFrameworkMusicRepository(dataContext);

            var dataModel = new MusicListModel(musicRepository);
            var list = new ExpandableList(dataModel);

            list.Run();
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
}
