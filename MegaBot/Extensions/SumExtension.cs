using System.Text.RegularExpressions;

namespace MegaBot.Extensions
{
    public class SumExtension
    {
        /// <summary>
        /// Преобразуем строку, чтобы она начиналась с заглавной буквы
        /// </summary>
        public string Summ(string s)
        {
            string[] ss;
            bool isdigit;
            int sum = 0;
            if (string.IsNullOrEmpty(s))
                return "Введена пустая строка. Введите числа через пробел.";
            //return string.Empty;
            s= Regex.Replace(s, "[ ]+", " ");
            ss = s.Split(' ');
            int currentvalue;
            for (int i = 0; i < ss.Length; i++)
            {
                isdigit = StringIsDigits(ss[0]);
                if (isdigit == false)
                {
                    return "Все или несколько элементов строки не являются числами. Введите только числа через пробел.";break;
                } 
                currentvalue = Convert.ToInt32(ss[i]);
                sum = sum + currentvalue;
            }
            return "Сумма введенных чисел - " + Convert.ToString(sum);
        }
        public static bool StringIsDigits(string sss)
        {
            foreach (var item in sss)
            {
                if (!char.IsDigit(item))
                    return false; //если хоть один символ не число, то выкидываешь "ложь"
            }
            return true; //если ни разу не выбило в цикле, значит, все символы - это цифры
        }
    }
}
