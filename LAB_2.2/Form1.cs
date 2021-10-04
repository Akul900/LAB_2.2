using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB_2._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string calc;
            try // оборачиваем кусок кода склонный к падению
            {
                 calc = (this.calc.Text);
            }
            catch (FormatException) // тип ошибки, которую перехватываем
            {
                MessageBox.Show("Некорректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // прерываем обработчик клика, если ввели какую-то ерунду
            }
           // var calc1 = calc;
            MessageBox.Show(Logic.CaclString(calc));
        }
    }
    class Logic
    {
        static bool Check(string text)
        {

            for (int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] == '+' && text[i + 1] == '+' || text[i] == '+' && text[i + 1] == '-' || text[i] == '-' && text[i + 1] == '-' || text[i] == '-' && text[i + 1] == '+')
                    return true;
            }
            return false;
        }
        public static string CaclString(string text)
        {
            char[] symbols = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-' };
            string math;
            int result = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (Check(text))
                {
                    return "Неверная запись выражения";
                }
                if (symbols.Any(x => x.Equals(text[i])))
                {
                    continue;
                }
                else
                {
                    return "Неверная запись выражения";
                }
            }
            math = text;
            math = math.Replace("-", " -");
            math = math.Trim();

            string[] num = math.Split(new char[] { '+', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in num)
            {
                result += Convert.ToInt32(s);
            }
            Console.WriteLine("\nРезультат");
            return result.ToString();
        }
       /* static void Main(string[] args)
        {
            string calc;
            Console.Write("Введите пример: ");
            calc = Console.ReadLine();
            Check(calc);
            Console.Write(CaclString(calc));
        }*/
    }

}
