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
            calc.Text = Properties.Settings.Default.calc.ToString(); // храним введённое выражение
        }

        private void ResultButton_Click(object sender, EventArgs e)
        {
            string calc = (this.calc.Text);
            if (String.IsNullOrEmpty(calc)) // если строка пустая, то выводим сообщение об ошибке 
            {
                MessageBox.Show("Некорректный ввод", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Properties.Settings.Default.calc = calc;
            Properties.Settings.Default.Save(); // сохраняем введённое выражение
            MessageBox.Show(Logic.CaclString(calc));
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.calc.Text = "";
            Properties.Settings.Default.calc = "";
            Properties.Settings.Default.Save(); // сохраняем очистку выражение
        }
    }
    public class Logic
    {
        public static bool Ceredovanie (string text) // проверка чередования
        {
            int p = 1; // кол-во плюсов
            int m = 0; // кол-во минусов
            for (int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] == '+')
                    p++;
                if (text[i] == '-')
                    m++;
                if (p == m || (p - m > 2))
                    return true;
            }
            return false;
        }
        public static bool Check(string text) // проверка на двойные знаки
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
            char[] symbols = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-' }; // нужные символы
            string math; // хранение строки
            int result = 0; // результат

            for (int i = 0; i < text.Length; i++)
            {
                if (Check(text) || Ceredovanie(text))
                {
                    return "Неверная запись выражения";
                }
                if (symbols.Any(x => x.Equals(text[i]))) // если это нужный символ, то продолжаем
                {
                    continue;
                }
                else
                {
                    return "Неверная запись выражения";
                }
            }
            math = text;
            math = math.Replace("-", " -"); // заменяем минусы на пробел с минусом
            math = math.Trim(); // удаление лишних пробелов в начале и конце выражения

            string[] num = math.Split(new char[] { '+', ' ' }, StringSplitOptions.RemoveEmptyEntries); // делим строку на числа
            foreach (string s in num) // суммируем числа
            {
                result += Convert.ToInt32(s);
            }
            Console.WriteLine("\nРезультат");
            return result.ToString();
        }
    }

}
