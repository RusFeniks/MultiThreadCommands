using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadCommands
{
    class Bots
    {
        public static List<Bots> List = new List<Bots>();       // Список ботов в оперативной памяти

        public string Name;
        public int Id;
        public string Token;
        public int Class;
        public int GroupsCount;                 // Счетчик групп для автоопределения пиров
        public List<GroupAssoc> groupAssocs;    // База ассоциаций групп и пиров


        public static void CreateList()
        {
            DataBase.Connect();
            DataBase.Reader = DataBase.Query("SELECT * FROM Bots").ExecuteReader();
            while (DataBase.Reader.Read())
            {
                Bots added_bot = new Bots
                {
                    Name = (string)DataBase.Reader["bName"],
                    Id = (int)DataBase.Reader["bID"],
                    Token = "access_token=" + (string)DataBase.Reader["bToken"] + Program.ApiVer,
                    Class = (int)DataBase.Reader["bClass"],
                    GroupsCount = (int)DataBase.Reader["bGroupsCount"],
                    groupAssocs = GroupAssoc.Decode(DataBase.Reader["bGroups"].ToString())
                };
                List.Add(added_bot);
            }
            DataBase.Close();
            Console.WriteLine("[system] Список ботов загружен");
        }

        public static void SaveBot(Bots bot)
        {
            DataBase.Connect();
            DataBase.Query("UPDATE Bots SET " +
                "bGroupsCount = " + bot.GroupsCount + ", " +
                "bGroups = '" + GroupAssoc.Encode(bot.groupAssocs) + "' " +
                "WHERE bID = " + bot.Id).ExecuteNonQuery();
            DataBase.Close();
            Console.WriteLine("[system] Бот " + bot.Name + " обновлён");
        }

        public static List<Bots> GetClassList(int botClass)
        {
            List<Bots> ClassList = new List<Bots>();
            foreach (Bots bot in Bots.List)
            {
                if (bot.Class == botClass) { ClassList.Add(bot); }
            }
            return ClassList;
        }

        public static Bots GetBot (int id)
        {
            foreach(Bots bot in Bots.List)
            {
                if(bot.Id == id) { return bot; }
            }
            return null;
        }
    }

    
    class GroupAssoc
    {
        public int Id { get; set; }
        public int Peer { get; set; }

        public static List<GroupAssoc> Decode(string encryptedString)
        {
            if (encryptedString != "")
            {
                List<GroupAssoc> groupAssocs = new List<GroupAssoc>();
                string[] associations = encryptedString.Split('|');
                foreach (string association in associations)
                {
                    if (association != "")
                    {
                        GroupAssoc groupAssoc = new GroupAssoc();
                        string[] assoc2 = association.Split('=');
                        groupAssoc.Id = Convert.ToInt32(assoc2[0]);
                        groupAssoc.Peer = Convert.ToInt32(assoc2[1]);
                        groupAssocs.Add(groupAssoc);
                    }
                }
                return groupAssocs;
            }
            else { return new List<GroupAssoc>(); }
        }

        public static string Encode(List<GroupAssoc> groupAssocs)
        {
            string Result = "";
            foreach (GroupAssoc groupAssoc in groupAssocs)
            {
                Result += groupAssoc.Id.ToString();
                Result += "=";
                Result += groupAssoc.Peer.ToString();
                Result += "|";
            }
            return Result;
        }

        public static int GetAssoc(int id, Bots bot)
        {
            foreach (GroupAssoc groupAssoc in bot.groupAssocs)
            {
                if (groupAssoc.Id == id) { return groupAssoc.Peer; }
            }
            return -1;
        }

        public static int GetAssoc(Groups group, Bots bot)
        {
            foreach (GroupAssoc groupAssoc in bot.groupAssocs)
            {
                if (groupAssoc.Id == group.Id) { return groupAssoc.Peer; }
            }
            return -1;
        }

        public static void ReWriteAssoc (int group, int peer, Bots bot)
        {
            foreach (GroupAssoc groupAssoc in bot.groupAssocs)
            {
                if(groupAssoc.Id == group) {
                    groupAssoc.Peer = peer;
                    Bots.SaveBot(bot);
                    return;
                }
            }
            bot.groupAssocs.Add(new GroupAssoc { Id = group, Peer = peer });
            Bots.SaveBot(bot);
            return;
        }
    }
}
