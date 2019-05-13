using gen_csv.Extensions;
using gen_csv.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace gen_csv.Components
{
    /// <summary>
    /// Форматтер для
    /// csv файла.
    /// </summary>
    public class CsvFormatter : IFormatter<CmdArgsValues>
    {
        /// <summary>
        /// Генераторы случайных значений.
        /// </summary>
        private Dictionary<string, Func<int, string>> _typeValuesGenerators;

        public CsvFormatter()
        {
            var rnd = new Random();

            _typeValuesGenerators = new Dictionary<string, Func<int, string>>()
            {
                { "Date", arg =>  rnd.NextDate(DateTime.Now, DateTime.Now.AddYears(5)).Date.ToShortDateString() },
                { "Integer", arg => rnd.Next(int.MinValue, int.MaxValue).ToString() },
                { "Float", arg =>
                    {
                        var min = int.MinValue;
                        var max = int.MaxValue;
                        var n = rnd.NextDouble() * (max - min) + min;
                        return n.ToString(CultureInfo.InvariantCulture).Replace('.', ',');
                    }
                },
                { "String", len => rnd.GetRandomString(len) }
            };
        }

        /// <summary>
        /// Сформировать csv файл.
        /// </summary>
        /// <param name="csvFileParams">Параметры csv файла.</param>
        public void FormatToFile(CmdArgsValues csvFileParams)
        {
            var rnd = new Random();
            
            using (var writer = new StreamWriter(csvFileParams.FileName, false, csvFileParams.Enc))
            {
                //Генерируем типы колонок.
                var headerTypes = Enumerable.Range(0, csvFileParams.ColumnCount)
                    .Select(i =>
                    {
                        var rndIndex = rnd.Next(0, _typeValuesGenerators.Count);
                        return _typeValuesGenerators.ElementAt(rndIndex).Key;
                    }).ToList();
                //Генерирем назвния колонок с их типами.
                var header = headerTypes.Select(t => string.Format("{0} {1}", rnd.GetRandomString(csvFileParams.Len), t)).ToList();

                writer.Write(string.Join("; ", header));

                writer.WriteLine();

                for (var i = 0; i < csvFileParams.RowCount; i++)
                {
                    //Генерация значений строки csv файла с учетом типа колонки.
                    var rowValues = headerTypes.Select(t => _typeValuesGenerators[t](csvFileParams.Len)).ToList();
                    writer.Write(string.Join("; ", rowValues));
                    writer.WriteLine();
                }
            }
        }

    }
}
