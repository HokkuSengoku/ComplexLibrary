namespace Social
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.Json;
    using Social.Models;

    public class SocialDataSource
    {
        private List<User> _users;

        private List<Friend> _friends;

        private List<Message> _messages;

        public SocialDataSource(string pathUsers, string pathFriends, string pathMessages)
        {
            GetUsers(pathUsers);
            GetFriends(pathFriends);
            GetMessages(pathMessages);
        }

        public UserContext GetUserContext(string userName)
        {
            var userContext = new UserContext();

            userContext.User = _users.FirstOrDefault(user => user.Name == userName);
            userContext.Friends = GetFriendsAll(userContext.User);
            userContext.OnlineFriends = GetOnlineFriends(userContext.User);
            userContext.Subscribers = GetSubscribers(userContext.User);
            userContext.FriendshipOffers = GetNewFriendshipOffers(userContext.User);
            userContext.News = GetAFeed(userContext.User);

            // todo: заполнить информацию
            return userContext;
        }

        private void GetUsers(string path)
        {
            var text = File.ReadAllText(path);
            User[] user =
                JsonSerializer.Deserialize<User[]>(text);
            _users = user.ToList();
        }

        private void GetFriends(string path)
        {
            var text = File.ReadAllText(path);
            Friend[] friends =
                JsonSerializer.Deserialize<Friend[]>(text);
            _friends = friends.ToList();
        }

        private void GetMessages(string path)
        {
            var text = File.ReadAllText(path);
            Message[] messages =
                JsonSerializer.Deserialize<Message[]>(text);
            _messages = messages.ToList();
        }

        private List<UserInformation> GetFriendsAll(User user)
        {
            var idUser = user.UserId;

            var friends = _friends.Where(friend =>
                (friend.ToUserId == idUser && friend.FromUserId != idUser && friend.Status == 2)
                || (friend.ToUserId != idUser && friend.FromUserId == idUser && friend.Status == 2)).ToList();

            var intersectFriendsToUser = _friends.Where(f => f.ToUserId == idUser && f.FromUserId != idUser && f.Status != 3);
            var intersectFriendsFromUser =
                _friends.Where(f => f.FromUserId == idUser && f.ToUserId != idUser && f.Status != 3);
            var x = from fromUser in intersectFriendsFromUser
                join toUser in intersectFriendsToUser
                    on fromUser.ToUserId equals toUser.FromUserId
                where fromUser.FromUserId == toUser.ToUserId
                select new Friend()
                {
                    ToUserId = toUser.ToUserId,
                    FromUserId = toUser.FromUserId,
                    SendDate = toUser.SendDate,
                    Status = toUser.Status,
                };
            foreach (var item in x)
            {
                friends.Add(item);
            }

            var friendId = friends.Select(x => x.ToUserId == idUser ? x.FromUserId : -1);
            var friendNamess = _users.Where(u => friendId.Contains(u.UserId)).Select(u => new UserInformation
            {
                Name = u.Name,
                Online = u.Online,
                UserId = u.UserId,
            }).ToList();

            return friendNamess;
        }

        private List<UserInformation> GetOnlineFriends(User user)
        {
            var friends = GetFriendsAll(user);
            var friendsOnline = friends.Where(friend => friend.Online == true).Distinct();

            return friendsOnline.ToList();
        }

        private List<UserInformation> GetSubscribers(User user)
        {
            var x = GetFriendsAll(user);
            var subscribers = _friends.Where(friend =>
                ((friend.ToUserId == user.UserId) && friend.Status != 2 && friend.Status != 3)).Distinct();
            var subscribersInfo = from usr in _users
                join subscriber in subscribers
                    on usr.UserId equals subscriber.FromUserId
                select new UserInformation()
                {
                    Name = usr.Name,
                    Online = usr.Online,
                    UserId = usr.UserId,
                };

            subscribersInfo = subscribersInfo.Where(frined => !x.Contains(frined)).Distinct();

            return subscribersInfo.ToList();
        }

        private List<UserInformation> GetNewFriendshipOffers(User user)
        {
            var x = GetFriendsAll(user);
            var newFriendshipOffers = from usr in _users
                join friend in _friends
                    on usr.UserId equals friend.FromUserId
                where friend.SendDate > usr.LastVisit && friend.ToUserId == user.UserId
                select new UserInformation()
                {
                    Name = usr.Name,
                    Online = usr.Online,
                    UserId = usr.UserId,
                };

            newFriendshipOffers = newFriendshipOffers.Where(friend => !x.Contains(friend)).Distinct();

            return newFriendshipOffers.ToList();
        }

        private List<News> GetAFeed(User user)
        {
            var lastVisit = user.LastVisit;
            var friends = GetFriendsAll(user);
            var news = from message in _messages
                join friend in friends
                    on message.AuthorId equals friend.UserId
                where message.SendDate > lastVisit
                select new News()
                {
                    AuthorId = friend.UserId,
                    AuthorName = friend.Name,
                    Likes = message.Likes,
                    Text = message.Text,
                };

            return news.ToList();
        }
    }
}
