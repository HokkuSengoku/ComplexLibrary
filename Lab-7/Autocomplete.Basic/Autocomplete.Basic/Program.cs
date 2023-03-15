namespace Autocomplete.Basic
{
    using System.Diagnostics;

    public class Program
    {
        public static void Main()
        {
            var search = new LiveSearch();

            var control = new HintedControl();
            control.Hint = "keeek";
            control.TypingEvent += search.HandleTyping;
            control.Run();
        }

        public static void HandleAndTrace(HintedControl control)
        {
            Trace.WriteLine("Control text is " + control.Text);
        }
    }
}
