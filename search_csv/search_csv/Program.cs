using CmdParser;
using search_csv.Components;
using System;
using System.IO;

namespace search_csv
{
    class Program
    {
        public static void ProcessCsv(string[] args)
        {
            var cmdArgsValidator = new CmdArgsValidator();
            cmdArgsValidator.AddRule(@"(-col)\s[a-zA-Z]+\b");
            cmdArgsValidator.AddRule(@"(-in)\s[a-zA-Z0-9\\\/:]+\.(csv)\b");
            cmdArgsValidator.AddRule(@"(-out)\s[a-zA-Z0-9\\\/:]+\.(csv)\b");
            cmdArgsValidator.AddRule(@"(-enc)\s[a-zA-Z0-9\-]+\b");
            cmdArgsValidator.AddRule(@"(-exp)\s[a-zA-Z0-9\-]+\b");

            if (!cmdArgsValidator.IsValid(string.Join(" ", args)))
            {
                Console.WriteLine("Параметры коммандной строки заданы неверно.");
                return;
            }
            //Шаблон входной коммандной строки.
            var columnPattern = @"^(((^|\s)(\w+)\s(\w+)(;|$)){1,})$";
            var columnsValidator = new CsvColumnValidator();
            columnsValidator.AddRule(columnPattern);
            
            var parser = new CmdArgsParser(args);
            var cmdArgs = new CmdArgs(parser);

            CsvReader reader;

            try
            {
                reader = new CsvReader(cmdArgs.InFileName, cmdArgs.Encoding, columnsValidator);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            
            using (var writer = new StreamWriter(cmdArgs.OutFileName, false, cmdArgs.Encoding))
            {
                writer.WriteLine(reader.GetHeader());
                writer.Write(reader.GetRowsWith(cmdArgs.ColumnName, cmdArgs.Exp));
            }
        }


        static void Main(string[] args)
        {
            ProcessCsv(args);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
