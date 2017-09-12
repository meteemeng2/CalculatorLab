using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class RPNCalculatorEngine : CalculatorEngine
    {
        public string Process(string str)
        {
            CalculatorEngine engine = new CalculatorEngine() ;
            Stack rpnStack = new Stack() ;
            string[] parts = str.Split(' '); //////////////////////////////////////////////////////////////////////////////
            for (int i = 0 ; i < parts.Length-1; i++)
            {
                if(isOperator(parts[i]))
                {
                    string second = Convert.ToString(rpnStack.Pop());
                    string first = Convert.ToString(rpnStack.Pop());
                    rpnStack.Push(calculate(parts[i], first, second));
                }
                else
                {
                    rpnStack.Push(parts[i]);
                }
            }
            return Convert.ToString(rpnStack.Pop());
        }
    }
}
