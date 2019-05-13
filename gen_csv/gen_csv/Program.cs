using CmdParser;
using gen_csv.Components;
using System;

namespace gen_csv
{

    class Program
    {
        static void Main(string[] args)
        {
            var cmdArgsValidator = new CmdArgsValidator();
            cmdArgsValidator.AddRule(@"(-col)\s(\d+)\b");
            cmdArgsValidator.AddRule(@"(-row)\s(\d+)\b");
            cmdArgsValidator.AddRule(@"(-out)\s[a-zA-Z\\\/:]+\.(csv)\b");
            cmdArgsValidator.AddRule(@"(-enc)\s[a-zA-Z0-9\-]+\b");
            cmdArgsValidator.AddRule(@"(-len)\s(\d+)\b");

            if (!cmdArgsValidator.IsValid(string.Join(" ", args)))
            {
                Console.WriteLine("Параметры коммандной строки заданы неверно.");
                return;
            }
            var parser = new CmdArgsParser(args);
            var cmdArgs = new CmdArgsValues(parser);
            var csv = new CsvFormatter();
            csv.FormatToFile(cmdArgs);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
