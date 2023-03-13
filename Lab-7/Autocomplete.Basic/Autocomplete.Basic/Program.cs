namespace Autocomplete.Basic
{
    public class Program
    {
        public static void Main()
        {
            var search = new LiveSearch();

            var control = new HintedControl();
            control.TypingEvent += search.HandleTyping;
            control.Run();
        }
    }
}
