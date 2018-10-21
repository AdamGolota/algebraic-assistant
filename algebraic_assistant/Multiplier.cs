using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace algebraic_assistant
{
    public class Multiplier : Parsed
    {
        public virtual string Name { get; protected set; }

        public bool IsInverse { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public Multiplier(string _name, bool isInverse = false)
        {
            Name = _name;
        }

        public static bool operator ==(Multiplier a, Multiplier b)
        {
            return a.Name == b.Name;
        }
        public static bool operator !=(Multiplier a, Multiplier b)
        {
            return a.Name != b.Name;
        }


    }
}
