using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace algebraic_assistant
{
    public class BracketMismatchException : Exception
    {
        public BracketMismatchException() : base("Mismatched brackets were detected")
        {

        }
    }
}
