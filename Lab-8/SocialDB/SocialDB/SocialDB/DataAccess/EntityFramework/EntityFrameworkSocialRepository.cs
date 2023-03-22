namespace SocialDB.DataAccess.EntityFramework;
using SocialDB.Domain;
public class EntityFrameworkSocialRepository : ISocialRepository
{
    private readonly DataContext _dataContext;

    public EntityFrameworkSocialRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public User AddUser(User user)
    {
        var result = _dataContext.Users.Add(user);
        _dataContext.SaveChanges();

        return result.Entity;
    }

    public void DeleteUser(User user)
    {
        _dataContext.Users.Remove(user);
        _dataContext.SaveChanges();
    }

    public IEnumerable<User> GetUsers()
    {
        return _dataContext.Users;
    }

    public Friend AddFriend(Friend friend)
    {
        var result = _dataContext.Friends.Add(friend);
        _dataContext.SaveChanges();

        return result.Entity;
    }

    public void DeleteFriend(Friend friend)
    {
        _dataContext.Friends.Remove(friend);
        _dataContext.SaveChanges();
    }

    public IEnumerable<Friend> GetFriends()
    {
        return _dataContext.Friends;
    }

    public Like AddLike(Like like)
    {
        var result = _dataContext.Likes.Add(like);
        _dataContext.SaveChanges();

        return result.Entity;
    }

    public void DeleteLike(Like like)
    {
        _dataContext.Likes.Remove(like);
        _dataContext.SaveChanges();
    }

    public IEnumerable<Like> GetLikes()
    {
        return _dataContext.Likes;
    }

    public Message AddMessage(Message message)
    {
        var result = _dataContext.Messages.Add(message);
        _dataContext.SaveChanges();

        return result.Entity;
    }

    public void DeleteMessage(Message message)
    {
        _dataContext.Messages.Remove(message);
        _dataContext.SaveChanges();
    }

    public IEnumerable<Message> GetMessages()
    {
        return _dataContext.Messages;
    }
}