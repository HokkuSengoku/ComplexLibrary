namespace MusicBrowser.Console.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using MusicBrowser.Console.Domain;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Album> Albums { get; set; }

    public DbSet<Song> Songs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var albumsModel = modelBuilder.Entity<Album>();

        albumsModel
            .ToTable("albums")
            .HasKey(x => x.Id);

        albumsModel
            .Property(x => x.Id)
            .HasColumnName("albumid")
            .UseIdentityColumn();

        albumsModel.Property(x => x.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
        albumsModel.Property(x => x.Date).HasColumnName("date");

        var songsModel = modelBuilder.Entity<Song>();

        songsModel
            .ToTable("songs")
            .HasKey(x => x.Id);

        songsModel
            .Property(x => x.Id)
            .HasColumnName("songid")
            .UseIdentityColumn();

        songsModel.Property(x => x.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
        songsModel.Property(x => x.Duration).HasColumnName("duration").IsRequired();

        songsModel
            .HasOne(s => s.Album)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
