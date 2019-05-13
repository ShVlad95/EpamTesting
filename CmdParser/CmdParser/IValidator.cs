namespace CmdParser
{
    public interface IValidator<T, in TArg>
    {
        T Rules { get; set; }
        void AddRule(TArg rule);
        bool IsValid(string source);
    }
}
