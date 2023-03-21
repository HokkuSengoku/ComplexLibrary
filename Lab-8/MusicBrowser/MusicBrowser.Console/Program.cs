namespace MusicBrowser.Console
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MusicBrowser.Console.Application;
    using MusicBrowser.Console.DataAccess;
    using MusicBrowser.Console.DataAccess.AdoNet;
    using MusicBrowser.Console.DataAccess.EntityFramework;

    internal class Program
    {
        public static void Main()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnectionString");

            DataContext dataContext = new DataContext(
                new DbContextOptionsBuilder()
                    .UseSqlServer(connectionString)
                    .Options);

            IMusicRepository musicRepository = new EntityFrameworkMusicRepository(dataContext);

// IMusicRepository musicRepository = new AdoNetMusicRepository(connectionString);
            var dataModel = new MusicListModel(musicRepository);
            var list = new ExpandableList(dataModel);

            list.Run();
        }
    }
}
