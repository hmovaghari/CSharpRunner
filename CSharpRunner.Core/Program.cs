using CodingSeb.ExpressionEvaluator;
using Newtonsoft.Json;
//Test

Console.Title = "CSharpRunner";

var compiler = new ExpressionEvaluator()
{
    OptionScriptNeedSemicolonAtTheEndOfLastExpression = true,
    OptionCaseSensitiveEvaluationActive = true,
    CacheTypesResolutions = true,
};

compiler.Namespaces.Add("System.Windows");
compiler.Namespaces.Add("System.Diagnostics");
compiler.EvaluateVariable += Evaluator_EvaluateVariable;

void Evaluator_EvaluateVariable(object? sender, VariableEvaluationEventArg e)
{
    if (e.This != null)
    {
        if (e.Name.Equals("Json"))
        {
            e.Value = JsonConvert.SerializeObject(e.This);
        }
        else if (e.Name.Equals("MethodsNames"))
        {
            e.Value = JsonConvert.SerializeObject(e.This.GetType().GetMethods().Select(m => m.Name));
        }
        else if (e.Name.Equals("PropertiesNames"))
        {
            e.Value = JsonConvert.SerializeObject(e.This.GetType().GetProperties().Select(m => m.Name));
        }
        else if (e.Name.Equals("FieldsNames"))
        {
            e.Value = JsonConvert.SerializeObject(e.This.GetType().GetFields().Select(m => m.Name));
        }
    }
}

compiler.EvaluateFunction += Evaluator_EvaluateFunction;

void Evaluator_EvaluateFunction(object? sender, FunctionEvaluationEventArg e)
{
    throw new NotImplementedException();
}

var code = string.Empty;

if ((args?.Length ?? 0) > 0)
{
    var arg = string.Empty;
    var isWait = false;
    args.ToList().ForEach(a => { arg += a; });
    if (arg.ToLower().EndsWith(".cs") || arg.ToLower().EndsWith(".cs\""))
    {
        string[] lines = File.ReadAllLines(arg.Trim('"'));
        foreach (string line in lines)
        {
            code += line;
            code += Environment.NewLine;
        }
        isWait = true;
    }
    else
    {
        code = arg;
    }
    Compile(compiler, code, isWait);
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

void ShowCSharpRunner()
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

bool RunApp()
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine();
    Console.WriteLine("Write C# Codes : ");
    Console.ResetColor();
    code = Console.ReadLine();
    var isExit = code.ToLower() == "exit";
    if (!isExit)
    {
        Compile(compiler, code, isWait: false);
        isExit = false;
    }
    else
    {
        isExit = true;
    }
    return isExit;
}

void Compile(ExpressionEvaluator compiler, string code, bool isWait)
{
    try
    {
        var _code = compiler.RemoveComments(code);
        Console.WriteLine(compiler.ScriptEvaluate(_code));
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
    finally
    {
        if (isWait)
        {
            Console.Write("Press any key to continue . . .");
            Console.ReadKey();
        }
    }
}