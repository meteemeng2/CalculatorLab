using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class enginecalculator 
    {
        public string calculate(string operate, string firstOperand, string secondOperand, int maxOutputSize = 8)
        {
            switch (operate)
            {
                case "+":
                    return (Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand)).ToString();
                case "-":
                    return (Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand)).ToString();
                case "X":
                    return (Convert.ToDouble(firstOperand) * Convert.ToDouble(secondOperand)).ToString();
                case "÷":
                    // Not allow devide be zero
                    if (secondOperand != "0")
                    {
                        double result;
                        string[] parts;
                        int remainLength;

                        result = (Convert.ToDouble(firstOperand) / Convert.ToDouble(secondOperand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString("N" + remainLength);
                    }
                    break;
            }
            return "E";
        }
        public string percent(string firstOperand,string secondOperand)
        {
            return Convert.ToString(Convert.ToDouble(firstOperand) * (Convert.ToDouble(secondOperand) / 100));
        }
        public string root(string num)
        {
            double biGnumber = Convert.ToDouble(num);
            double fordotnumber = biGnumber;
            if (biGnumber < 1)
            {
                fordotnumber = 1;
            }
            for(double i = 1; i <= fordotnumber; i++)
            {
                if ((i*i) == biGnumber) {
                    return Convert.ToString(i);
                }
            }
            for (double i = 0; i <= fordotnumber; i += 0.00001)
            {
                if (biGnumber - (i*i) <= 0.0001)
                {
                    string deleettee = Convert.ToString(i);
                    if(deleettee.Length > 5)
                    {
                        deleettee = deleettee.Substring(0, 5);
                    }
                    return deleettee;
                }
            }
            return "Error";
        }
        public string onedividex(string num)
        {
            double j = Convert.ToDouble(num);
            string del = Convert.ToString(1 / j);
            if(del.Length > 5)
            {
                del = del.Substring(0, 5);
            }
            return del;
        }     
    }
}
