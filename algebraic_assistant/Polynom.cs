using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace algebraic_assistant
{
    public class Polynom : Multiplier
    {
        private List<Term> terms = new List<Term>();

        public bool brackets = true;

        public Polynom(string polynom, bool isInverse = false) : base(polynom, isInverse)
        {
            Parse(polynom);
        }

        public override string Name
        {
            get
            {
                if (!terms.Any())
                {
                    return "0";
                }
                string result = brackets ? "( " : "";
                result += terms.First();
                foreach (Term t in terms.Skip(1))
                {
                    string sign = t.IsNegative ? " - " : " + ";
                    result += sign + t;
                }
                result += brackets ? " )" : "";
                return result;
            }
        }

        public bool IsMonomial
        {
            get => terms.Count < 2;
        }
        
        public Term ToTerm()
        {
            if (!terms.Any())
            {
                return new Term("0");
            }
            return IsMonomial && !IsInverse ? terms.First() : null;
        }

        public void ReduceSimilars()
        {
            List<List<Term>> listsOfSimilars = new List<List<Term>>();
            while (terms.Any())
            {
                List<Term> listOfSimilars = new List<Term>();
                Term first = terms.First();
                terms = terms.Where(t =>
                {
                    if (Term.AreSimilar(t, first))
                    {
                        listOfSimilars.Add(t);
                        return false;
                    }
                    return true;
                }).ToList();
                listsOfSimilars.Add(listOfSimilars);
            }
            listsOfSimilars.ForEach(list =>
            {
                terms.Add(list.Aggregate((sum, term) => sum + term));
            });
        }

        public void RemoveZeroVals()
        {
            terms = terms.Where(t => t.Coeff != 0).ToList();
        }
    
        public void Simplify()
        {
            terms.ForEach(term => term.Simplify());
            ReduceSimilars();
            RemoveZeroVals();
        }

        private void Parse(string polynom)
        {
            terms.Clear();
            Strip(ref polynom);

            var brContents = ClearBrackets(ref polynom);
            

            var term_matches = Regex.Matches(polynom, @"-?[^+-]+");

            foreach (Match term_match in term_matches)
            {
                string val = term_match.Value;

                for (int i = 0; i < val.Length; i++)
                {
                    if (val[i] == '(')
                    {
                        string content = brContents.Pop();
                        val = val.Insert(i + 1, content);
                        i += content.Length;
                    }
                }
                terms.Add(new Term(val));
            }
        }    
    }
}
