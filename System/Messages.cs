using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using xNet;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace MultiThreadCommands
{
    class Messages
    {
        public static List<Messages> List = new List<Messages>(); // Список со всеми сообщениями, ждущими обработки
        public static int MCount = 0; // Счетчик полученных сообщений
        public static int CCount = 0; // Счетчик полученных команд
        public static int FCount = 0; // Счетчик обработанных команд

        public static Messages Current; // Сообщение, обрабатываемое в текущий момент

        public int ActionType { get; private set; }         // Тип события, для сообщения по умолчанию "4"
        public int MessageID { get; private set; }          // ID сообщения
        public int Flags { get; private set; }              // Флаги сообщения
        public int PeerId { get; private set; }             // "Пир", что-то вроде места, где сообщение было получено
        public int TimeStamp { get; private set; }          // Временная метка получения сообщения
        public string Text { get; private set; }            // Текст сообщения
        public dynamic Attachments { get; private set; }    // Информация о прикреплениях

        public AuthorInfo Author { get; private set; }      // Информация о пользователе отправителе сообщения
        public User Profile { get; private set; }           // Игровой профиль пользователя

        public Keywords Keyword { get; private set; }       // Если сообщение является командой, тут будет лежать "Ключевое слово"
        public string Args { get; private set; }            // Если сообщение является командой, тут находятся аргументы этой команды
        public Groups GroupId { get; private set; }         // Общий ID группы в которой получено сообщение
       

        public static void AddMessage (JArray response)
        {

            if (Program.json_mode)
            {
                Console.WriteLine(response);
            } else
            {
                Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] Получено событие с кодом [" + response[0] + "]");
            }

            if ((int)response[0] == 4)
            {

                Messages message = new Messages
                {
                    ActionType = (int)response[0],
                    MessageID = (int)response[1],
                    Flags = (int)response[2],
                    PeerId = (int)response[3],
                    TimeStamp = (int)response[4],
                    Text = (string)response[5],
                    Attachments = (dynamic)response[6]
                };

                if(message.PeerId > 2000000000)
                {
                    message.Author = AuthorInfo.GetAuthor((int)message.Attachments.from);
                } else
                {
                    message.Author = AuthorInfo.GetAuthor((int)message.PeerId);
                }

                if(message.PeerId > 2000000000)
                {
                    message.GroupId = Groups.GetGroup(message.PeerId);
                }

                message.Keyword = Keywords.FindKeywords(message.Text);
                if(message.Keyword != null)
                {
                    message.Args = Keywords.GetArguments(message.Text, message.Keyword);
                }

                int profile_id = User.Find(message.Author.Id);
                if (profile_id != -1)
                {
                    message.Profile = User.List[profile_id];
                } else
                {
                    message.Profile = null;
                }

                List.Add(message);
                MCount++;
                if(message.Keyword != null) { CCount++; }

                File.AppendAllText("logs/messages.log", response.ToString());

                if (!Program.json_mode)
                {
                    string Lvl = "-";
                    if(message.Profile != null)
                    {
                        Lvl = message.Profile.Lvl.ToString();
                    }
                    Console.WriteLine("   Автор сообщения: [" + Lvl + "] " + message.Author.Name + " " + message.Author.Surname + " (id: " + message.Author.Id + ")");
                    if (message.Text != "")
                    {
                        Console.WriteLine("   Текст сообщения: " + message.Text);
                    }
                    if (message.Keyword != null)
                    {
                        Console.WriteLine("   Команда: " + message.Keyword.Command_Name);
                        if (message.Args != null)
                        {
                            Console.WriteLine("   Аргументы: " + message.Args);
                        }
                    }
                }
            }
            else
            {
                File.AppendAllText("logs/events.log", response.ToString());
            }
        }
    }

}
