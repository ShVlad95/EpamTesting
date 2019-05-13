using System;
using System.Collections.Generic;

namespace gen_csv.Extensions
{
    /// <summary>
    /// Расширение класса Random.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Сгенерировать случайную дату.
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="from">Нижняя граница даты.</param>
        /// <param name="to">Верхняя граница даты.</param>
        /// <returns>Случайная дата между from и to/</returns>
        public static DateTime NextDate(this Random rnd, DateTime from, DateTime to)
        {
            if (from > to)
            {
                var temp = from;
                from = to;
                to = temp;
            }

            var diffTicks = to.Subtract(from).Ticks;
            var subDate = new DateTime(diffTicks);

            var day = rnd.Next(0, subDate.Day);
            var month = rnd.Next(0, subDate.Month);
            var year = rnd.Next(0, subDate.Year);

            var resDate = from.AddDays(day).AddMonths(month).AddYears(year);
            return resDate.Date;
        }
        
        /// <summary>
        /// Получить случайную строку.
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="len">Опциональный парметр длины строки.</param>
        /// <returns>Случайно сгенерированная строка.</returns>
        public static string GetRandomString(this Random rnd, int len)
        {
            List<char> rndSymbols = new List<char>();
            for (int i = 0; i < len; i++)
            {
                rndSymbols.Add((char)rnd.Next(0x0061, 0x7A));//(0x0061, 0x7A) - [a-z] в unicode
            }

            return string.Join("", rndSymbols);
        }
    }
}
