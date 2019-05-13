using System;
using System.Text.RegularExpressions;

namespace CmdParser
{
    /// <summary>
    /// Парсер для аргументов командной строки.
    /// </summary>
    public class CmdArgsParser : ICmdArgsParser
    {
        /// <summary>
        /// Коммандная строки.
        /// </summary>
        public readonly string CmdString;

        public CmdArgsParser(string[] cmdArgs)
        {
            CmdString = string.Join(" ", cmdArgs);
        }

        public string GetPropertyStringValueOrNull(string propertyName)
        {
            var searchValPattern = $@"(^|\s)-(?<param>({propertyName}))\s(?<option>[a-zA-Z0-9\.\\\/:-]+)";//(\w+)\\*:*-*\.*(\w+)*)";
            var pair = Regex.Match(CmdString, searchValPattern);
            var value = pair.Groups["option"].Value;
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public T GetPropertyValue<T>(string propertyName, Func<string, T> parser)
        {
            string value = GetPropertyStringValueOrNull(propertyName);

            if (value == null)
                throw new ArgumentException($"Значение параметра {propertyName} отсутствует.");

            return parser(value);
        }
    }
}
