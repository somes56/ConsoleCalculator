using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeagenCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Equations = { "1 + 1", "2 * 2", "1 + 2 + 3", "6 / 2", "11 + 23", "11.1 + 23",
                                   "1 + 1 * 3", "( 11.5 + 15.4 ) + 10.1", "23 - ( 29.3 - 12.5 )", "( 1 / 2 ) - 1 + 1"
                                 };

            foreach (var Equation in Equations)
            {
                var Answer = Calculate(Equation);
                Console.WriteLine($"Input : {Equation}, Output : { Answer}");
            }

            Console.ReadKey();
        }

        public static double Calculate(string Equation)
        {
            Equation = Equation.Contains("(") && Equation.Contains(")") ? Bracket(Equation) : Equation;
            Equation = Equation.Contains("*") ? Solve(Equation, "*") : Equation;
            Equation = Equation.Contains("/") ? Solve(Equation, "/") : Equation;
            Equation = Equation.Contains("-") ? Solve(Equation, "-") : Equation;
            Equation = Equation.Contains("+") ? Solve(Equation, "+") : Equation;
            return Convert.ToDouble(Equation);
        }

        public static string Bracket(string Equation)
        {
            try
            {
                string[] EquationSplit = Equation.Split(new string[] { " " }, StringSplitOptions.None);
                bool IsCalculated = false;

                while (!IsCalculated)
                {
                    int indexBracketStart = Array.FindIndex(EquationSplit, x => x.Equals("("));
                    int indexBracketEnd = Array.FindIndex(EquationSplit, x => x.Equals(")"));

                    if (indexBracketStart > -1 && indexBracketEnd > -1)
                    {
                        var EquationInsideBracket = EquationSplit.Where((val, idx) => idx > indexBracketStart & idx < indexBracketEnd).ToArray();
                        var EquationInsideBracketStr = String.Join(" ", EquationInsideBracket.Where(s => !String.IsNullOrEmpty(s)));

                        var Answer = EquationInsideBracketStr.Contains("*") ? Solve(EquationInsideBracketStr, "*") :
                                     EquationInsideBracketStr.Contains("/") ? Solve(EquationInsideBracketStr, "/") :
                                     EquationInsideBracketStr.Contains("-") ? Solve(EquationInsideBracketStr, "-") :
                                     Solve(EquationInsideBracketStr, "+");

                        EquationSplit[indexBracketStart] = Answer.ToString();

                        var EquationSplitToList = EquationSplit.ToList();
                        EquationSplitToList.RemoveRange(indexBracketStart + 1, (indexBracketEnd - indexBracketStart));
                        EquationSplit = EquationSplitToList.ToArray();
                    }
                    else
                    {
                        IsCalculated = true;
                    }
                }

                return String.Join(" ", EquationSplit.Where(s => !String.IsNullOrEmpty(s)));
            }
            catch (Exception e)
            {
                var msg = e.Message.ToString();
                return "";
            }
        }

        public static string Solve(string Equation, string Operator)
        {
            try
            {
                string[] EquationSplit = Equation.Split(new string[] { " " }, StringSplitOptions.None);
                bool IsCalculated = false;

                while (!IsCalculated)
                {
                    int index = Array.FindIndex(EquationSplit, x => x.Equals(Operator));

                    if (index > -1)
                    {
                        double Value1 = Convert.ToDouble(EquationSplit[index - 1]);
                        double Value2 = Convert.ToDouble(EquationSplit[index + 1]);
                        var Answer = Operator == "*" ? Value1 * Value2 : Operator == "/" ? Value1 / Value2 : Operator == "-" ? Value1 - Value2 : Value1 + Value2;

                        EquationSplit[index - 1] = Answer.ToString();

                        EquationSplit = EquationSplit.Where((val, idx) => idx != index).ToArray();
                        EquationSplit = EquationSplit.Where((val, idx) => idx != index).ToArray();
                    }
                    else
                    {
                        IsCalculated = true;
                    }
                }

                return String.Join(" ", EquationSplit.Where(s => !String.IsNullOrEmpty(s)));
            }
            catch (Exception e)
            {
                var msg = e.Message.ToString();
                return "";
            }
        }
    }
}
