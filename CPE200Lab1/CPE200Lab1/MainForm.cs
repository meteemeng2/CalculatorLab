using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        bool hasDot;
        bool isAllowBack;
        bool isAfterOperater;
        bool isAfterEqual;
        bool isFirst = true;
        bool isAfterMemory = false;
        string firstOperand;
        string operate;
        string secondOperand;
        double memory = 0;
        enginecalculator engine = new enginecalculator();

        private void resetAll(bool TrueIsDeleteM)
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            isFirst = true;
            operate = null;
            isAfterMemory = false;
            if (TrueIsDeleteM)
            {
                memory = 0;
            }
        }

        public void calculatewithengine(bool TrueIsCollectSecond)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (TrueIsCollectSecond)
            {
                secondOperand = lblDisplay.Text;
            }
            string result = engine.calculate(operate, firstOperand, secondOperand);
            while(result[result.Length - 1] is '0' || result[result.Length - 1] is '.')
            {
                result = result.Substring(0, result.Length - 1);
            }
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
                firstOperand = result;
            }
        }

       

        public MainForm()
        {
            InitializeComponent();

            resetAll(true);
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual || isAfterMemory)
            {
                resetAll(false);
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if(lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += ((Button)sender).Text;
            isAfterOperater = false;
            isAllowBack = true;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                operate = ((Button)sender).Text;
                return;
            }
            if (isFirst)
            {
                firstOperand = lblDisplay.Text;
                isFirst = false;
            }
            else
            {
                calculatewithengine(true);
            }
            operate = ((Button)sender).Text;
            switch (operate)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    isAfterOperater = true;
                    break;
            }
            isAllowBack = false;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (isAfterEqual)
            {
                calculatewithengine(false);
            }
            else
            {
                calculatewithengine(true);
            }
            isAfterEqual = true;
            isFirst = true;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll(false);
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (hasDot)
            {
                hasDot = false;
                for (int i = 0; i < lblDisplay.Text.Length; i++)
                {
                    if(lblDisplay.Text[i] is '.')
                    {
                        hasDot = true;
                    }
                }
            }
            
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll(false);
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            }
            else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll(false);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            if (isFirst)
            {
                lblDisplay.Text = "0";
            }
            else
            {
                lblDisplay.Text = engine.percent(firstOperand, lblDisplay.Text);
            }
           
        }

        private void root_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = engine.root(lblDisplay.Text);
        }

        private void onedivideme_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = engine.onedividex(lblDisplay.Text);
        }

        private void M_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Text[1])
            {
                case '+':
                    memory += Convert.ToDouble(lblDisplay.Text);
                    break;
                case '-':
                    memory -= Convert.ToDouble(lblDisplay.Text);
                    break;
                case 'R':
                    lblDisplay.Text = Convert.ToString(memory);
                    isAfterMemory = true;
                    break;
                case 'C':
                    memory = 0;
                    break;
                case 'S':
                    memory = Convert.ToDouble(lblDisplay.Text);
                    break;
                default: break;
            }
        }
    }
}
