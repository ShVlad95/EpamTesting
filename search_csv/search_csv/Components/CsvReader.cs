using CmdParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace search_csv.Components
{
    /// <summary>
    /// Csv читатель.
    /// </summary>
    class CsvReader
    {
        public readonly string FileName;
        public readonly Encoding Enc;
        public IValidator<string, string> ColumnsValidator;
        
        public CsvReader(string fileName, Encoding enc, IValidator<string, string> columnsValidator)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"Файл {fileName} не найден.");

            FileName = fileName;
            Enc = enc;
            ColumnsValidator = columnsValidator;
        }

        /// <summary>
        /// Получить название колонок.
        /// </summary>
        /// <returns></returns>
        public string GetHeader()
        {
            var header = string.Empty;
            using (var reader = new StreamReader(FileName, Enc))
            {
                header = reader.ReadLine();
            }

            return header;
        }

        /// <summary>
        /// Получить позицию колонки.
        /// </summary>
        /// <param name="columnName">Название колонки.</param>
        /// <returns>Позиция найденной колонки.</returns>
        public int? GetColumnIndexOrNull(string columnName)
        {
            if (!File.Exists(FileName))
            {
                Console.WriteLine($"Файл {FileName} не найден.");
                return null;
            }

            using (var reader = new StreamReader(FileName, Enc))
            {
                var columns = reader.ReadLine();

                if (!ColumnsValidator.IsValid(columns))
                {
                    Console.WriteLine("Неправильный формат колонок.");
                    return null;
                }

                var columnList = columns?.Split(';', ' ')
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrEmpty(c) && !string.IsNullOrWhiteSpace(c))
                    .ToList();

                return columnList?.IndexOf(columnName);
            }
        }

        /// <summary>
        /// Получить все строки,
        /// со значением колонки columnName равной expr.
        /// </summary>
        /// <param name="columnName">Название колонки.</param>
        /// <param name="expr">Целевое значение колонки.</param>
        /// <returns></returns>
        public string GetRowsWith(string columnName, string expr)
        {
            var ind = GetColumnIndexOrNull(columnName);

            if (ind == null || ind == -1)
                return string.Empty;
            
            var allLines = File.ReadAllLines(FileName, Enc).Select(l => l.Replace("\"\"", "\"").Trim()).ToList();
            
            //Рег выражение для разделения строк файла. Исключен
            //из рассмотрения ; обернутый в двойные кавычки ";".
            var rg = new Regex(";(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))", RegexOptions.CultureInvariant);
            
            //Индекс найденной колонки.
            var targetInd = (int)ind / 2;

            List<string>  searchedLines = allLines
                .Where(row =>
                {
                    var columnValue = rg.Split(row).ElementAt(targetInd).Trim();
                    return String.Compare(columnValue, expr, StringComparison.Ordinal) == 0;
                })
                .ToList();

            return string.Join("\n", searchedLines);
        }
    }
}
