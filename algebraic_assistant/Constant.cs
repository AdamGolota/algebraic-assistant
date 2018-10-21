using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace algebraic_assistant
{
    public class Constant : Multiplier
    {
        public readonly decimal value;

        //public decimal Value { get; private set; }


        public Constant(string _name, decimal _value, bool isInverse=false) : base(_name, isInverse)
        {
            value = isInverse ? 1 / _value : _value;
            RecomputeName();
        }
        public Constant(decimal _value, bool isInverse = false) : this(_value.ToString(), _value, isInverse)
        { 

        }

        public Constant RecomputeName()
        {
            this.Name = this.value.ToString();
            return this;
        }

        public static Constant operator *(Constant a, Constant b)
        {
            return new Constant(a.Name + "*" + b.Name, a.value * b.value);
        }

        public static Constant operator +(Constant a, Constant b)
        {
            return new Constant(a.Name + "+" + b.Name, a.value + b.value);
        }

    }
}
