using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace algebraic_assistant
{
    public class Parsed
    {
        protected static void Strip(ref string expression)
        {
            expression = Regex.Replace(expression, @"\s", "");

        }
        protected static Stack<string> ClearBrackets(ref string str)
        {
            int len = str.Length;
            var contents = new Stack<string>();
            for (int i = len - 1; i >= 0; i--)
            {
                if (str[i] == ')')
                {
                    int closeIndex = i;
                    int closeBr = 1;
                    for (i = i - 1 ; i >= 0; i--)
                    {
                        if (str[i] == ')')
                        {
                            closeBr++;
                        }
                        else if (str[i] == '(')
                        {
                            closeBr--;
                        }
                        if (closeBr == 0)
                        {
                            break;
                        }
                    }
                    if (closeBr != 0)
                    {
                        throw new BracketMismatchException();
                    }
                    contents.Push(str.Substring(i + 1, closeIndex - 1 - i));
                    str = str.Remove(i + 1, closeIndex - 1 - i);
                }
            }
            return contents;
        }
    }
}
