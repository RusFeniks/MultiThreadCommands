using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using xNet;

namespace MultiThreadCommands
{
    class Functions
    {
        // ГЕНЕРАЦИЯ НАБОРА СЛОВ ИЗ МАССИВА ДЛЯ REGEX
        public static string GenerateSet (string[] mass)
        {
            string result = "";
            result += "(";
            for (int i = 0; i < mass.Length; i++)
            {
                if (i != 0)
                {
                    result += "|";
                }
                result += mass[i];
            }
            result += ")";
            return result;
        }

        // ВОЗВРАЩАЕТ СЛУЧАЙНЫЙ ЭЛЕМЕНТ ИЗ СТРОКОВОГО МАССИВА
        public static string GenerateOne (string[] mass)
        {
            return mass[new Random().Next(0, mass.Length)];
        }

        // ВОЗВРАЩАЕТ ID ПОЛЬЗОВАТЕЛЯ ПО ССЫЛКЕ
        public static int GetId(string url)
        {
            if (new Regex(@"^((http|https)://vk.com/(id)?[a-z,A-Z,0-9,_,-]+|[0-9]+|id[0-9]+)$", RegexOptions.IgnoreCase).IsMatch(url))
            {
                dynamic request;
                string id = new Regex(@"((http|https)://vk.com/(id)?)", RegexOptions.IgnoreCase).Replace(url, "");
                if (!new Regex(@"^id[0-9]+", RegexOptions.IgnoreCase).IsMatch(id))
                {
                    request = JObject.Parse(new HttpRequest(Program.Api).Post("users.get?user_ids=" + id + "&" + Program.Token).ToString());
                    id = (string)request.response[0].id;
                }
                else
                {
                    id = new Regex(@"^(id)", RegexOptions.IgnoreCase).Replace(id, "");
                }
                return Convert.ToInt32(id);
            }
            else { return -1; }
        }


        //============== ФУНКЦИИ ОТПРАВКИ СООБЩЕНЙ ================\\

        public static void SendMessage(string replyText, int destinationChat, int botClass = 0)
        {
            Thread.Sleep(new Random().Next(50, 150));
            Groups group = Groups.GetGroup(destinationChat);
            if (group.BotsCatalog[botClass] > -1)
            {
                if (botClass > 0)
                {
                    destinationChat = GroupAssoc.GetAssoc(destinationChat, Bots.GetBot(group.BotsCatalog[botClass]));
                }
                new HttpRequest(Program.Api).Post("messages.send?peer_id=" + destinationChat + "&message=" + replyText + "&" + Bots.GetBot(group.BotsCatalog[botClass]).Token);
            }
        }

        public static void SendMessage(int replyMessage, string replyText, int destinationChat, int botClass = 0)
        {
            Thread.Sleep(new Random().Next(50, 150));
            Groups group = Groups.GetGroup(destinationChat);
            if (group.BotsCatalog[botClass] > -1)
            {
                if (botClass > 0)
                {
                    destinationChat = GroupAssoc.GetAssoc(destinationChat, Bots.GetBot(group.BotsCatalog[botClass]));
                }
                new HttpRequest(Program.Api).Post("messages.send?peer_id=" + destinationChat + "&message=" + replyText + "&forward_messages=" + replyMessage + "&" + Bots.GetBot(group.BotsCatalog[botClass]).Token);
            }
        }

        //============== ФУНКЦИИ ОТПРАВКИ СООБЩЕНЙ ================\\
    }
}
