using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace algebraic_assistant
{
    public class Term : Parsed
    {
        private List<Constant> constants = new List<Constant>();
        private List<Multiplier> variables = new List<Multiplier>();
        private List<Polynom> polynoms = new List<Polynom>();
        
        public bool IsNegative { get; private set;}

        public Term(string expression)
        {
            this.Parse(expression);
        }

        public override string ToString()
        {
            
            return String.Join(" * ", Multipliers.Select(m => m.ToString()));
        }

        private void MultiplyBy(Term t)
        {
            constants.AddRange(t.constants);
            variables.AddRange(t.variables);
            polynoms.AddRange(t.polynoms);
        }

        public static Term operator +(Term a, Term b)
        {
            if (Term.AreSimilar(a, b))
            {
                Term term = new Term("");
                term.polynoms = a.polynoms;
                term.variables = a.variables;

                term.constants.Add(a.Coeff + b.Coeff);
                return term;
            }
            else
            {
                return null;
            }
        }

        public Constant Coeff
        {
            get
            {
                int optionalNegative = 1;
                if (IsNegative)
                {
                    optionalNegative = -1;
                }
                if (!constants.Any())
                {
                    return new Constant("1", optionalNegative);
                }
                Constant result = constants.Aggregate((prod, constant) => prod *= constant) * optionalNegative;
                return result;
            }
            
        }

        public List<Multiplier> Multipliers
        {
            get
            {
                var multipliers = new List<Multiplier>();
                constants.ForEach(x => multipliers.Add(x));
                variables.ForEach(x => multipliers.Add(x));
                polynoms.ForEach(x => multipliers.Add(x));
                return multipliers;
            }
        }

        public bool IsConstant
        {
            get => !(variables.Any() || polynoms.Any());
        }

        public Constant ToConstant()
        {
            return IsConstant ? constants.Aggregate((product, c) => c * product) : null;
        }

        public static bool AreSimilar(Term a, Term b)
        {   
            return CompareLists<Polynom>(a.polynoms, b.polynoms) 
                && CompareLists<Multiplier>(a.variables, b.variables);
        }

        private static bool CompareLists<T>(List<T> l1, List<T> l2) where T : Multiplier
        {
            return !l1
                .Select<T, String>(item => item.ToString())
                .Except(l2.Select<T, String>(item => item.ToString()))
                .Any();
        }

        public void ZeroReduce()
        {
            if (Coeff == 0)
            {
                variables.Clear();
                polynoms.Clear();
            }
        }

        public void Simplify()
        {
            polynoms.ForEach(p => p.Simplify());
            RemoveMonomials();
            ReduceSimilars(variables);
        }  

        public void ReduceSimilars(List<Multiplier> list)
        {
            // TODO!

            //foreach (var m in list)
            //{
            //    list = list.Where(item => )
            //    foreach (var m_next in list.)
            //}
        }

        public void RemoveMonomials()
        {
            var newTerms = new List<Term>();
            polynoms = polynoms.Where(polynom =>
            {
                if (polynom.IsMonomial)
                {
                    newTerms.Add(polynom.ToTerm());
                    return false;
                }
                return true;
            }).ToList();

            newTerms.ForEach(t => MultiplyBy(t));
        }


        private void Parse(string expression)
        {
            variables.Clear();
            Strip(ref expression);
            if (String.IsNullOrEmpty(expression))
            {
                return;
            }
            if (expression[0] == '-')
            {
                expression = expression.Remove(0, 1);
                IsNegative = true;
            }

            var brContents = ClearBrackets(ref expression);
            var muliplier_matches = Regex.Matches(expression, @"/?(?:[^*/])+");
            
            foreach (Match match in muliplier_matches)
            {
                string val = match.Value;
                bool isInverse = false;
                if (val[0] == '/')
                {
                    val = val.Remove(0, 1);
                    isInverse = true;
                }
                if (val == "()")
                {
                    polynoms.Add(new Polynom(brContents.Pop()));
                }
                else
                {
                    try
                    {
                        decimal number = Convert.ToDecimal(val);
                        constants.Add(new Constant(val, number, isInverse));
                    }
                    catch (FormatException)
                    {
                        variables.Add(new Multiplier(val, isInverse));
                    }
                }

            }
        }
    }
}
