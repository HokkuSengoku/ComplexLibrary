namespace Calculator
{
    public interface IParser
    {
        Operation Parse(string inputString);
    }
}