namespace MusicBrowser.Console
{
    using Microsoft.Extensions.Configuration;
    using MusicBrowser.Console.Application;
    using MusicBrowser.Console.DataAccess;
    using MusicBrowser.Console.DataAccess.AdoNet;

    internal class Program
    {
        public static void Main()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnectionString");

            IMusicRepository musicRepository = new AdoNetMusicRepository(connectionString);

            var dataModel = new MusicListModel(musicRepository);
            var list = new ExpandableList(dataModel);

            list.Run();
        }
    }
}
