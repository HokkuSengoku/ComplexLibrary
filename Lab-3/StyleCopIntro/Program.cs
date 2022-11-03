using System;

namespace StyleCopIntro
{
    class              Program
    {
        static void    Main(string[] args)
        {
            var Me = new user("John", "Smith");
            Console.WriteLine(Me.getFullName());



        }
    }




    class user
    {
        private string FirstName;
        private string last_name;

        public user(string firstName, string lastName)
        {
            FirstName = firstName; last_name = lastName;
        }
        public string getFullName()
        {
            return FirstName + " " + last_name;
        }
    }
}
