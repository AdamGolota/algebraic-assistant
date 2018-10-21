using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace algebraic_assistant
{
    public class InvalidEquationException : Exception
    {
        public InvalidEquationException() : base("Equation must have only one '=' sign")
        {

        }
    }
}
