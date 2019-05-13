using CmdParser;
using System;
using System.Text;

namespace gen_csv.Components
{
    /// <summary>
    /// Обертка для параметров
    /// командной строки.
    /// </summary>
    public class CmdArgsValues
    {
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }
        public int Len { get; set; }
        public string FileName { get; set; }
        public Encoding Enc { get; set; }


        public CmdArgsValues(ICmdArgsParser argsParser)
        {
            FileName = argsParser.GetPropertyStringValueOrNull("out") ?? string.Empty;

            try
            {
                ColumnCount = argsParser.GetPropertyValue("col", s => int.Parse(s));
                RowCount = argsParser.GetPropertyValue("row", s => int.Parse(s));
                Enc = argsParser.GetPropertyValue("enc", s => Encoding.GetEncoding(s));
                Len = argsParser.GetPropertyValue("len", s => int.Parse(s));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
