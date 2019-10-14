using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace MultiThreadCommands
{
    class Program
    {
        public static string Api = "https://api.vk.com/method/";
        public static string Token = "", server = "", key = "", ts = "";
        public static string ApiVer = "&v=5.80";

        //----Переменные для консольных команд-----
        public static bool json_mode = false;
        //-----------------------------------------

        static void Main(string[] args)
        {

            //----Настройки окна------------
            Console.SetWindowSize(130, 10);
            Console.Title = "VK Bot";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            //------------------------------

            Console.WriteLine(">> Начало работы...");
            Console.WriteLine("");
            Bots.CreateList();
            Console.WriteLine(" Загружено ботов: " + Bots.List.Count);
            Console.WriteLine(" Основной бот: " + Bots.List[0].Name);
            Token = Bots.List[0].Token;
            Groups.CreateList();
            Console.WriteLine(" Бесед загружено: " + Groups.List.Count);
            User.CreteList();
            Console.WriteLine(" Пользователей загружено: " + User.List.Count);
            Console.WriteLine("");

            try
            {
                dynamic getLongPollServer = JObject.Parse(new HttpRequest(Api).Post("messages.getLongPollServer?need_pts=1&lp_version=3&" + Token).ToString());

                server = getLongPollServer.response.server;
                key = getLongPollServer.response.key;
                ts = getLongPollServer.response.ts;
                Console.WriteLine("Данные получены");
            }
            catch
            {
                Console.WriteLine("Ошибка получения данных");
                Console.ReadLine();
                Environment.Exit(0);
            }

            Console.WriteLine("Сервер:" + server + "\nКлюч: " + key + "\nВременной штамп: " + ts);
            Console.WriteLine("========================================");
            Thread.Sleep(50);

            Thread CallBack = new Thread(Program.CallBack);
            CallBack.Start();
            Thread.Sleep(100);
            Thread MessageProcessing = new Thread(Processing.General);
            MessageProcessing.Start();
            Thread.Sleep(100);
            Thread EventProcessing = new Thread(Events.EventTimer);
            EventProcessing.Start();
            Thread.Sleep(100);

            Console.WriteLine("Консоль доступна");
            Console.WriteLine("");

            while (true)
            {
                string local_command = Console.ReadLine();
                switch (local_command)
                {
                    case "stop":
                        CallBack.Abort();
                        MessageProcessing.Abort();
                        EventProcessing.Abort();
                        Environment.Exit(0);
                        break;
                    case "json":
                        if(json_mode)
                        {
                            json_mode = false;
                            Console.WriteLine("Json режим деактивирован");
                        }
                        else
                        {
                            json_mode = true;
                            Console.WriteLine("Json режим активирован");
                        }
                        break;
                    case "add user":
                        Console.Write("Name: ");
                        User.Add(User.List.Count + 1, Console.ReadLine());
                        break;
                    case "user count":
                        Console.WriteLine("Пользователей зарегестрировано: " + User.List.Count);
                        break;
                    case "stats":
                        Console.WriteLine("Сообщений на обработке: " + Messages.List.Count);
                        Console.WriteLine("Сообщений: " + Messages.MCount);
                        Console.WriteLine("Команд: " + Messages.CCount);
                        Console.WriteLine("Команд обработано: " + Messages.FCount);
                        break;
                }

            }
        }

        public static void CallBack()
        {
            Console.WriteLine("Запущена процедура приёма событий");

            while (true)
            {
                Thread.Sleep(10);
                try
                {
                    dynamic requestUserLongPoll = JObject.Parse(new HttpRequest().Post("https://" + server + "?act=a_check&key=" + key + "&ts=" + ts + "&wait=25&mode=2&version=2").ToString());
                    foreach (JArray update in requestUserLongPoll.updates)
                    {
                        Messages.AddMessage(update);
                    }
                    ts = requestUserLongPoll.ts;
                }
                catch (Exception EX)
                {
                    try
                    {
                        Console.WriteLine(EX.Message);
                        Console.WriteLine(EX.StackTrace);
                        dynamic getLongPollServer = JObject.Parse(new HttpRequest(Api).Post("messages.getLongPollServer?need_pts=1&lp_version=3&" + Token).ToString());
                        server = getLongPollServer.response.server;
                        key = getLongPollServer.response.key;
                        ts = getLongPollServer.response.ts;
                        Console.WriteLine("Данные получены");
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка получения данных");
                    }
                }
            }
        }
    }
}
