namespace ClojSharp.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Compiler;
    using ClojSharp.Core;

    public class Program
    {
        public static void Main(string[] args)
        {
            Machine machine = new Machine();
            Parser parser = new Parser(Console.In);

            while (true)
            {
                try
                {
                    var expr = parser.ParseExpression();
                    Console.WriteLine(Machine.Evaluate(expr, machine.RootContext));
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
            }
        }
    }
}
