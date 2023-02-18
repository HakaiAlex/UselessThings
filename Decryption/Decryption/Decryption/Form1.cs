using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Decryption
{
    public partial class Form1 : Form
    {
        public static string message;
        public static string key;
        public static string cipher;
        public enum RussianAlphabet
        {
            А = 1,
            Б,
            В,
            Г,
            Д,
            Е,
            Ё,
            Ж,
            З,
            И,
            Й,
            К,
            Л,
            М,
            Н,
            О,
            П,
            Р,
            С,
            Т,
            У,
            Ф,
            Х,
            Ц,
            Ч,
            Ш,
            Щ,
            Ъ,
            Ы,
            Ь,
            Э,
            Ю,
            Я
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            message = textBox1.Text;
            key = textBox2.Text;
            cipher = string.Join(" ", EncryptMessage(message, key));
            label2.Text = cipher;
            textBox1.Text = "";
            textBox2.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == key)
                label8.Text = DecipherMessege(cipher, textBox3.Text);
            else
                label8.Text = "Неверно введен ключ";
            textBox3.Text = "";
        }

        public static string EncryptMessage(string message, String key)
        {
            int[] messangeInNumbers = ConvertToNumeric(message);
            int[] keyInNimbers = ConvertToNumeric(key);
            int[] result = new int[messangeInNumbers.Length];
            string text;
            for (int i = 0, j = 0; i < messangeInNumbers.Length; i++, j++)
            {
                if (j == keyInNimbers.Length)
                    j = 0;
                result[i] = messangeInNumbers[i] + keyInNimbers[j];
            }

            return text = string.Join(" ", result);
        }


        /// <summary>
        /// Расшифровать числа
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecipherMessege(string message, string key)
        {
            int[] messageInNumbers = ConvertToPrimeArray(message);
            int[] keyInNimbers = ConvertToNumeric(key);
            int[] result = new int[messageInNumbers.Length];
            string text;
            for (int i = 0, j = 0; i < messageInNumbers.Length; i++, j++)
            {
                if (j == keyInNimbers.Length)
                    j = 0;
                result[i] = messageInNumbers[i] - keyInNimbers[j];
            }
            return ConvertToString(result);


        }
        /// <summary>
        /// Переводит строку в массив чисел
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int[] ConvertToNumeric(string input)
        {
            // Приводим входную строку к верхнему регистру, чтобы игнорировать разницу в регистрах
            input = RemoveExtraSpaces(input).ToUpper();

            // Создаем массив для хранения результата
            int[] result = new int[input.Length];

            // Проходим по каждому символу входной строки
            for (int i = 0; i < input.Length; i++)
            {
                // Получаем соответствующее значение для символа на основе перечисления EnglishAlphabet
                if (Enum.TryParse<RussianAlphabet>(input[i].ToString(), out RussianAlphabet letter))
                {
                    result[i] = (int)letter;
                }
                else if (input[i] == ' ')
                    result[i] = 0;
                else
                {
                    // Если символ не является буквой русского алфавита, то устанавливаем значение -1
                    // (можно выбрать и другое значение, которое будет означать "неизвестный символ")
                    result[i] = -1;
                }
            }

            return result;
        }

        /// <summary>
        /// Конвертируем числовой массив в строку
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int[] ConvertToPrimeArray(string input)
        {
            var numbers = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var primes = new List<int>();

            foreach (var numberString in numbers)
            {
                if (int.TryParse(numberString, out int number))
                {
                    primes.Add(number);
                }
            }

            return primes.ToArray();
        }

        /// <summary>
        /// Конвертирует массив чисел в строку
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertToString(int[] input)
        {
            // Создаем строку для последующего добавления символов
            StringBuilder sb = new StringBuilder();
            //Перебираем массив
            foreach (int number in input)
            {
                //Если 0 то это пробел 
                if (number == 0)
                {
                    sb.Append(' ');
                }
                //Если -1 то это неизвестный нам символ
                else if (number == -1)
                {
                    sb.Append('?');
                }
                else
                {
                    sb.Append(Enum.GetName(typeof(RussianAlphabet), number));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Удаляет лишние пробелы и символы переноса строки
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveExtraSpaces(string input)
        {
            // Удаление лишних пробелов
            string output = string.Join(" ", input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            // Удаление символов переноса строки
            output = output.Replace("\r", string.Empty);
            output = output.Replace("\n", string.Empty);

            return output;
        }

       
    }
}
