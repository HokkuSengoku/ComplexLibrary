namespace Social
{
    using System.Collections.Generic;
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

            // userContext.User = ...
            // userContext.Friends = GetUserFriends(userContext.User);
            // todo: заполнить информацию
            return userContext;
        }

        private void GetUsers(string path)
        {
           // todo: Сделать метод
        }

        private void GetFriends(string path)
        {
            // todo: Сделать метод
        }

        private void GetMessages(string path)
        {
            // todo: Сделать метод
        }
    }
}
