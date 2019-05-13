using CmdParser;
using System;
using System.Text;

namespace search_csv.Components
{
    /// <summary>
    /// Аргументы коммандной строки.
    /// </summary>
    class CmdArgs
    {
        public string ColumnName { get; set; }
        public string Exp { get; set; }
        public string InFileName { get; set; }
        public string OutFileName { get; set; }
        public Encoding Encoding { get; set; }

        public CmdArgs(ICmdArgsParser parser)
        {
            ColumnName = parser.GetPropertyStringValueOrNull("col") ?? string.Empty;
            Exp = parser.GetPropertyStringValueOrNull("exp") ?? string.Empty;
            InFileName = parser.GetPropertyStringValueOrNull("in") ?? string.Empty;
            OutFileName = parser.GetPropertyStringValueOrNull("out") ?? string.Empty;

            try
            {
                Encoding = parser.GetPropertyValue("enc", s => Encoding.GetEncoding(s));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
