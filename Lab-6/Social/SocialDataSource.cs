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

           // GetUsers("Path/users.json");

            // userContext.Friends = GetUserFriends(userContext.User);
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

        private void GetOnlineFriend(string userName)
        {
            var idUser = _users.Single(user => user.Name == userName).UserId;
            var friends = _friends.Where(friend => (friend.ToUserId == friend.FromUserId && friend.Status != 3)
            || (friend.FromUserId != friend.ToUserId && friend.Status == 2) || (friend.FromUserId != friend.ToUserId && friend.Status == 3));
        }
    }
}
