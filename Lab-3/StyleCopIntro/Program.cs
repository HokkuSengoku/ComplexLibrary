namespace StyleCopIntro
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var me = new User("John", "Smith");
            Console.WriteLine(me.GetFullName());
        }
    }
}