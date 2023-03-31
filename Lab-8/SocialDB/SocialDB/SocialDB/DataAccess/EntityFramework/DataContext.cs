namespace SocialDB.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using SocialDB.Domain;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Like> Likes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var usersModel= modelBuilder.Entity<User>();

        usersModel
            .ToTable("users")
            .HasKey(x => x.UserId);

        usersModel
            .Property(x => x.UserId)
            .HasColumnName("userId")
            .UseIdentityColumn();

        usersModel.Property(x => x.Name).HasColumnName("name").HasMaxLength(255);
        usersModel.Property(x => x.Gender).HasColumnName("gender").IsRequired();
        usersModel.Property(x => x.Online).HasColumnName("online");
        usersModel.Property(x => x.LastVisit).HasColumnName("last_visit").IsRequired();
        usersModel.Property(x => x.DateOfBirth).HasColumnName("date_of_birth").IsRequired();
        

        var friendModel = modelBuilder.Entity<Friend>();

        friendModel
            .ToTable("friends")
            .HasKey(x => x.FriendId);

        friendModel
            .Property(x => x.FriendId)
            .HasColumnName("friendId")
            .UseIdentityColumn();

        friendModel.Property(x => x.FromUserId).HasColumnName("userFrom");
        friendModel.Property(x => x.ToUserId).HasColumnName("userTo");
        friendModel.Property(x => x.Status).HasColumnName("status").IsRequired();
        friendModel.Property(x => x.SendDate).HasColumnName("sendDate").IsRequired();

        modelBuilder.Entity<Friend>()
            .HasOne(f => f.User)
            .WithMany(u => u.Friends)
            .HasForeignKey(f => f.ToUserId)
            .OnDelete(DeleteBehavior.Cascade);

        var messageModel = modelBuilder.Entity<Message>();

        messageModel.ToTable("messages")
            .HasKey(x => x.MessageId);

        messageModel
            .Property(x => x.MessageId)
            .HasColumnName("messageId")
            .UseIdentityColumn();

        messageModel.Property(x => x.AuthorId).HasColumnName("authorId");
        messageModel.Property(x => x.SendDate).HasColumnName("sendDate");
        messageModel.Property(x => x.Text).HasColumnName("text").IsRequired();

        modelBuilder.Entity<Message>()
            .HasOne(m => m.User)
            .WithMany(m => m.Messages)
            .HasForeignKey(m => m.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        var likeModel = modelBuilder.Entity<Like>();


        likeModel.ToTable("likes")
            .HasKey(l => l.LikeId);

        likeModel
            .Property(l => l.LikeId)
            .HasColumnName("id")
            .UseIdentityColumn();

        likeModel.Property(x => x.UserId).HasColumnName("userId");
        likeModel.Property(x => x.MessageId).HasColumnName("messageId");
    }
}