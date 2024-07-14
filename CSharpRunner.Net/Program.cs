using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingSeb.ExpressionEvaluator;

namespace CSharpRunner.Net
{
    internal class Program
    {
        private static ExpressionEvaluator compiler = new ExpressionEvaluator();
        private static String code = string.Empty;

        static void Main(string[] args)
        {
            Console.Title = "CSharpRunner";

            if ((args?.Length ?? 0) > 0)
            {
                args.ToList().ForEach(a => { code += a; code += " "; });
                Compile(compiler, code);
            }
            else
            {
                ShowCSharpRunner();
                bool isExit;
                do
                {
                    isExit = RunApp();
                } while (true);
            }
        }

        private static bool RunApp()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("Write C# Codes : ");
            Console.ResetColor();
            code = Console.ReadLine();
            var isExit = code.ToLower() == "exit";
            if (!isExit)
            {
                Compile(compiler, code);
                isExit = false;
            }
            else
            {
                isExit = true;
            }
            return isExit;
        }

        private static void ShowCSharpRunner()
        {
            var cSharpRunner = @"
   _____  _  _     _____                             
  / ____|| || |_  |  __ \                            
 | |   |_  __  _| | |__) |   _ _ __  _ __   ___ _ __ 
 | |    _| || |_  |  _  / | | | '_ \| '_ \ / _ \ '__|
 | |___|_  __  _| | | \ \ |_| | | | | | | |  __/ |   
  \_____||_||_|   |_|  \_\__,_|_| |_|_| |_|\___|_|   
";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(cSharpRunner);
            Console.WriteLine();
            Console.WriteLine("  Programming by by hmovaghari, Power by codingseb");
            Console.ResetColor();
        }

        private static void Compile(ExpressionEvaluator compiler, string code)
        {
            try
            {
                Console.WriteLine(compiler.Evaluate(code));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exception: {ex}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"InnerException: {ex.InnerException}");
                Console.WriteLine($"Data: {ex.Data}");
                Console.WriteLine($"Source: {ex.Source}");
                Console.WriteLine($"HelpLink: {ex.HelpLink}");
                Console.WriteLine($"HResult: {ex.HResult}");
                Console.WriteLine($"TargetSite: {ex.TargetSite}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                Console.ResetColor();
            }
        }
    }
}
