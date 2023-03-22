namespace SocialDB.DataAccess;

using System.Collections.Generic;
using SocialDB.Domain;

public interface ISocialRepository
{
    User AddUser(User user);
    void DeleteUser(User user);
    IEnumerable<User> GetUsers();
    
    Friend AddFriend(Friend friend);
    void DeleteFriend(Friend friend);
    IEnumerable<Friend> GetFriends();
    
    Like AddLike(Like like);
    void DeleteLike(Like like);
    IEnumerable<Like> GetLikes();
    
    Message AddMessage(Message message);
    void DeleteMessage(Message message);
    IEnumerable<Message> GetMessages();
}