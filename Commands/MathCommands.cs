//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using System.Windows.Forms;

///*
// * Команды:
// * 
// * !Сложить { x, y } - складывает две переменных и выводит ответ.
// * 
// * 
// * 
// * */


//namespace MultiThreadCommands
//{
//    class MathCommands
//    {

//        public static string MathSummCmd = @"!(сложить|сложение|сумма|прибавить|сложи|прибавь|add|summ|sum)(\s+\d+(,\d+)?)+";
//        public static string MathSubbCmd = @"!(вычесть|вычитание|разность|отнять|вычти|sub)(\s+\d+(,\d+)?)(\s+\d+(,\d+)?)+";
//        public static string MathImulCmd = @"!(умножить|умножение|произвидение|перемножь|умножь|mul|imul)(\s+\d+(,\d+)?)+";
//        public static string MathDivvCmd = @"!(разделить|деление|частное|раздели|подели|div|idiv)(\s+\d+(,\d+)?)(\s+\d+(,\d+)?)+";

//        public static string MathPowCmd = @"!(степень|возведи|pow)(\s+\d+)+";

//        public static string MultiMathCmd = @"!(считать|считай|посчитай|вычисли|результат)(\s+\d+(,\d+)?)((\s*[+,\-,*,/]\s*)(\d+(,\d+)?))+";



//        public static void MathSumm(Messages command)
//        {
//            MatchCollection conformityCommands = new Regex(MathSummCmd, RegexOptions.IgnoreCase).Matches(command.Text);
//            string reply = "";
//            foreach (Match oneCommand in conformityCommands)
//            {
//                double result = 0;
//                foreach (Match number in new Regex(@"\d+(,\d+)?").Matches(oneCommand.Value))
//                {
//                    result += Convert.ToDouble(number.Value);
//                }
//                reply += "Результат сложения: " + result + '\n';
//            }
//            Messages.SendReply(reply, command.PeerId);
//        }


//        public static void MathSubb(Messages command)
//        {
//            MatchCollection conformityCommands = new Regex(MathSubbCmd, RegexOptions.IgnoreCase).Matches(command.Text);
//            string reply = "";
//            foreach (Match oneCommand in conformityCommands)
//            {
//                double result = 0;
//                int i = 0;
//                foreach (Match number in new Regex(@"\d+(,\d+)?").Matches(oneCommand.Value))
//                {
//                    if(i == 0)
//                    {
//                        result = Convert.ToDouble(number.Value);
//                        i++;
//                    } else
//                    {
//                        result -= Convert.ToDouble(number.Value);
//                    }
//                }
//                reply += "Результат вычитания: " + result + '\n';
//            }
//            Messages.SendReply(reply, command.PeerId);
//        }
//        public static void MathImul(Messages command)
//        {
//            MatchCollection conformityCommands = new Regex(MathImulCmd, RegexOptions.IgnoreCase).Matches(command.Text);
//            string reply = "";
//            foreach (Match oneCommand in conformityCommands)
//            {
//                double result = 1;
//                foreach (Match number in new Regex(@"\d+(,\d+)?").Matches(oneCommand.Value))
//                {
//                    result *= Convert.ToDouble(number.Value);
//                }
//                reply += "Результат умножения: " + result + '\n';
//            }
//            Messages.SendReply(reply, command.PeerId);
//        }
//        public static void MathDivi(Messages command)
//        {
//            MatchCollection conformityCommands = new Regex(MathDivvCmd, RegexOptions.IgnoreCase).Matches(command.Text);
//            string reply = "";
//            foreach (Match oneCommand in conformityCommands)
//            {
//                double result = 1;
//                int i = 0;
//                foreach (Match number in new Regex(@"\d+(,\d+)?").Matches(oneCommand.Value))
//                {
//                    if (i == 0)
//                    {
//                        result = Convert.ToDouble(number.Value);
//                        i++;
//                    }
//                    else
//                    {
//                        result /= Convert.ToDouble(number.Value);
//                    }
//                }
//                reply += "Результат деления: " + result + '\n';
//            }
//            Messages.SendReply(reply, command.PeerId);
//        }


//        public static void MathPow(Messages command)
//        {
//            MatchCollection conformityCommands = new Regex(MathPowCmd, RegexOptions.IgnoreCase).Matches(command.Text);
//            string reply = "";
//            foreach (Match match in conformityCommands)
//            {
//                MatchCollection numbers = new Regex(@"\d+", RegexOptions.IgnoreCase).Matches(match.Value);
//                if(numbers.Count == 1)
//                {
//                    reply += "Результат возведения в квадрат: " + Math.Pow(Convert.ToDouble(numbers[0].Value), 2);
//                } else if (numbers.Count == 2)
//                {
//                    reply += "Результат возведения числа " + numbers[0].Value + " в степень " + numbers[1].Value + " равен: " + Math.Pow(Convert.ToDouble(numbers[0].Value), Convert.ToDouble(numbers[1].Value));
//                } else
//                {
//                    reply += "Ты указал слишком много чисел. Я возьму лишь два из них, вот так: ";
//                    reply += "результат возведения числа " + numbers[0].Value + " в степень " + numbers[1].Value + " равен: " + Math.Pow(Convert.ToDouble(numbers[0].Value), Convert.ToDouble(numbers[1].Value));
//                }
//                reply += '\n';
//            }
//            Messages.SendReply(reply, command.PeerId);
//        }

//        public static void MultiMath(Messages command)
//        {
//            MatchCollection conformityCommands = new Regex(MultiMathCmd, RegexOptions.IgnoreCase).Matches(command.Text);
//            //string reply = "";
//            foreach (Match oneCommand in conformityCommands)
//            {
//                //double result = 1;
//                //int i = 0;
//                string task = oneCommand.Value;
//                Console.WriteLine(task);

//                Match act = new Regex(@"(\d+(,\d+)?)(\s*[*,/]\s*)(\d+(,\d+)?)").Match(task);

//                Console.WriteLine("");

//            }
//        }
//    }
//}
