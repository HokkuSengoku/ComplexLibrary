namespace StyleCopIntro
{
    public class User
    {
        private string _firstName;
        private string _lastName;

        public User(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public string GetFullName()
        {
            return _firstName + " " + _lastName;
        }
    }
}