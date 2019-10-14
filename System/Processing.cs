using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadCommands
{
    class Processing
    {
        public static void General ()
        {
            Console.WriteLine("Алгоритм обработки сообщений запущен");
            while (true)
            {
                if (Messages.List.Count > 0)
                {
                    Messages.Current = Messages.List[0];
                    if(Messages.Current.Author.Id != 478153784)
                    {
                        Message(Messages.Current);
                        if (Messages.Current.Keyword != null)
                        {
                            Command(Messages.Current);
                        }
                    }
                    Messages.List.Remove(Messages.Current);
                }
                Thread.Sleep(30);
            }
        }

        public static void Message (Messages current)
        {
            if(current.Profile != null)
            {
                //StatChange.Exp(1);
            }
        }

        public static void Command (Messages current)
        {
            if (current.Keyword.Command_Name == "reg")
            {
                //Info.CharacterCreate(current);
            }
            else if (current.Keyword.Command_Name == "info")
            {
                bool Args = false;
                if (current.Args != null)
                {
                    Args = new Regex(Keywords.full, RegexOptions.IgnoreCase).IsMatch(current.Args);
                }
                //Info.CharacterInfo(current, Args);
            }
            else if (current.Keyword.Command_Name == "addstat")
            {
                //StatChange.AddStat(current);
            }
            else if (current.Keyword.Command_Name == "quest" & current.Profile != null)
            {
                //Questing1.RandomQuest(current);
            }
            else if (current.Keyword.Command_Name == "invitebot")
            {
                RPGSystem.InviteBot(current);
            }
            else if (current.Keyword.Command_Name == "synchbot")
            {
                RPGSystem.GroupSynch(current);
            }
            else if (current.Keyword.Command_Name == "test")
            {
                RPGSystem.SendMsg(current);
            }
        }
    }
}
