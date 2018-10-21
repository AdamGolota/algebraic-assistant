using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace algebraic_assistant
{
    public class Equation
    {
        private Polynom left;
        private Polynom right;

        

        public Equation(string equation)
        {
            Parse(equation);
        }

        public override string ToString()
        {
            return left + " = " + right;
        }

        private void Parse(string equation)
        {
            string[] sides = equation.Split('=');
            if (sides.Length != 2)
            {
                throw new InvalidEquationException();
            }
            left = new Polynom(sides[0]);
            right = new Polynom(sides[1]);
            left.brackets = false;
            right.brackets = false;
        }

        public void Simplify()
        {
            right.Simplify();
            left.Simplify();
        }

        private static string Inverse(string term)
        {
            if (term[0] == '-')
            {
                return term.Substring(1);
            }
            else
            {
                return '-' + term;
            }
        }

    }
}
