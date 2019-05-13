using CmdParser;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace search_csv
{
    /// <summary>
    /// Класс для валидации
    /// колонок csv файла.
    /// </summary>
    class CsvColumnValidator : IValidator<string, string>
    {
        public string Rules { get; set; }
        public readonly List<string> AvailableColumnTypes;

        public CsvColumnValidator()
        {
            AvailableColumnTypes = new List<string>()
            {
                "Date",
                "Integer",
                "Float",
                "String"
            };
        }

        public void AddRule(string rule)
        {
            if (!(string.IsNullOrEmpty(rule) || string.IsNullOrWhiteSpace(rule)))
                Rules += rule;
        }

        /// <summary>
        /// Проверить правильность
        /// выражения source по правилам Rules.
        /// </summary>
        /// <param name="source">Исходное выражения для проверки.</param>
        /// <returns></returns>
        public bool IsValid(string source)
        {
            //Получтиь все типы колонок.
            var columnTypeList = source
                .Split(';')
                .Select(c => c.Split(' ')
                    .Last().Trim())
                .Where(t => !(string.IsNullOrWhiteSpace(t) && string.IsNullOrEmpty(t)))
                .ToList();
            
            return Regex.IsMatch(source, Rules) && columnTypeList.All(tSource => AvailableColumnTypes.Contains(tSource));
        }
    }
}
