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
            Printing_A_User_Greeting(userContext.User);
            userContext.Friends = GetFriendsAll(userContext.User.Name);
            userContext.OnlineFriends = GetOnlineFriends(userContext.User.Name);
            userContext.Subscribers = GetSubscribers(userContext.User.Name);
            userContext.FriendshipOffers = GetNewFriendshipOffers(userContext.User.Name);
            userContext.News = GetAFeed(userContext.Friends, userContext.User);

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

        private List<UserInformation> GetFriendsAll(string userName)
        {
            var idUser = _users.Single(user => user.Name == userName).UserId;
            var friends = _friends.Where(friend => (friend.ToUserId == idUser && friend.FromUserId != idUser && friend.Status == 2)
                                                   || (friend.FromUserId != idUser && friend.Status == 2) || (friend.ToUserId == idUser && friend.FromUserId == idUser && friend.Status != 3));

            var friendsNames = from user in _users
                join friend in friends
                    on user.UserId equals friend.FromUserId
                select new UserInformation()
                {
                    Name = user.Name,
                    Online = user.Online,
                    UserId = user.UserId,
                };

            return friendsNames as List<UserInformation>;
        }

        private List<UserInformation> GetOnlineFriends(string userName)
        {
            var idUser = _users.Single(user => user.Name == userName).UserId;
            var friends = _friends.Where(friend => (friend.ToUserId == idUser && friend.FromUserId != idUser && friend.Status == 2)
                                                   || (friend.FromUserId != idUser && friend.Status == 2) || (friend.ToUserId == idUser && friend.FromUserId == idUser && friend.Status != 3));
            var friendsOnline = from user in _users
                join friend in friends
                    on user.UserId equals friend.FromUserId
                where user.Online == true
                select new UserInformation()
                {
                    Name = user.Name,
                    Online = user.Online,
                    UserId = user.UserId,
                };

            return friendsOnline as List<UserInformation>;
        }

        private List<UserInformation> GetSubscribers(string userName)
        {
            var idUser = _users.Single(user => user.Name == userName).UserId;
            var subscribers = _friends.Where(friend =>
                ((friend.ToUserId == idUser) && friend.Status != 2 && friend.Status != 3));
            var subscribersInfo = from user in _users
                join subscriber in subscribers
                    on user.UserId equals subscriber.FromUserId
                select new UserInformation()
                {
                    Name = user.Name,
                    Online = user.Online,
                    UserId = user.UserId,
                };

            return subscribersInfo as List<UserInformation>;
        }

        private void Printing_A_User_Greeting(User user)
        {
            string gender;
            if (user.Gender == 0)
            {
                gender = "male";
            }
            else
            {
                gender = "female";
            }

            Console.WriteLine($"Hello {user.Name}!\n Your age: {(int)(DateTime.Now - user.DateOfBirth).TotalDays / 365} \n Gender: {gender}");
        }

        private List<UserInformation> GetNewFriendshipOffers(string userName)
        {
            var idUser = _users.Single(user => user.Name == userName).UserId;
            var newFriendshipOffers = from user in _users
                join friend in _friends
                    on user.UserId equals friend.FromUserId
                where friend.SendDate > user.LastVisit && user.UserId != idUser
                select new UserInformation()
                {
                    Name = user.Name,
                    Online = user.Online,
                    UserId = user.UserId,
                };

            return newFriendshipOffers as List<UserInformation>;
        }

        private List<News> GetAFeed(List<UserInformation> friends, User user)
        {
            var lastVisit = user.LastVisit;
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

            return news as List<News>;
        }
    }
}
