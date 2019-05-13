using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CmdParser
{
    public class CmdArgsValidator : IValidator<List<string>, string>
    {
        public List<string> Rules { get; set; }

        public CmdArgsValidator()
        {
            Rules = new List<string>();
        }

        public void AddRule(string rule)
        {
            if (!string.IsNullOrEmpty(rule) && !string.IsNullOrWhiteSpace(rule))
                Rules.Add(rule);
        }

        public bool IsValid(string source)
        {
            return Rules
                .All(rule => Regex.IsMatch(source, rule));
        }
    }
}
