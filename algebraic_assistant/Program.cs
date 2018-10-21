using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace algebraic_assistant
{
    class Program
    {
        static void Main(string[] args)
        {
            string equation_str = Console.ReadLine();
            var equation = new Equation(equation_str);
            equation.Simplify();
            Console.WriteLine(equation);
            //try
            //{
                
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    throw e;
            //}
            Console.ReadKey();
        }
    }
}

// Equations:
// x1 + 2 * x2 + 3 * x1 + 4 * x1 + x2 = y * ( x1 - x1)
// (y1 + 2 - 2 + y1 - 2 * y1) + 2 = ((x + y) - (x + z)) * 2 + ((x + y) + (x + z)) * 0