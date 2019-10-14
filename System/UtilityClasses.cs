using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace MultiThreadCommands
{
    class Groups
    {
        public static List<Groups> List = new List<Groups>();
        
        public int Id;
        public List<int> BotsCatalog = new List<int>();

        public static void CreateList()
        {
            DataBase.Connect();
            DataBase.Reader = DataBase.Query("SELECT * FROM Groups").ExecuteReader();
            while (DataBase.Reader.Read())
            {
                Groups added_group = new Groups
                {
                    Id = (int)DataBase.Reader["gID"],
                };
                added_group.BotsCatalog.Add(Bots.List[0].Id);
                added_group.BotsCatalog.Add((int)DataBase.Reader["gQuester"]);
                added_group.BotsCatalog.Add((int)DataBase.Reader["gCommentator"]);
                List.Add(added_group);
            }
            DataBase.Close();
            Console.WriteLine("[system] Список бесед загружен");
        }

        public static Groups GetGroup(int Peer)
        {
            foreach(Groups group in Groups.List)
            {
                if(group.Id == Peer) { return group; }
            }

            Groups newGroup = new Groups();

            newGroup.Id = Peer;
            newGroup.BotsCatalog.Add(Bots.List[0].Id);
            newGroup.BotsCatalog.Add(-1);
            newGroup.BotsCatalog.Add(-1);

            DataBase.Connect();
            DataBase.Query("INSERT INTO Groups (gID) VALUES ("+Peer+")").ExecuteNonQuery();
            DataBase.Close();

            Groups.List.Add(newGroup);

            Console.WriteLine("[system] Новая беседа добавлена");
            SaveGroup(newGroup);

            return newGroup;
        }

        public static void SaveGroup(Groups group)
        {
            DataBase.Connect();
            DataBase.Query("UPDATE Groups SET "
                + "gQuester = " + group.BotsCatalog[1] + ", "
                + "gCommentator = " + group.BotsCatalog[2] + " "
                + "WHERE gID = " + group.Id
                ).ExecuteNonQuery();
            DataBase.Close();
            Console.WriteLine("[system] Список бесед обновлён");
        }
    }



    class AuthorInfo
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int Friend_Status { get; private set; }
        public int Banned { get; private set; }
        public string Domain { get; private set; }
        public string Photo { get; private set; }

        public static AuthorInfo GetAuthor(int id)
        {
            AuthorInfo author = new AuthorInfo();
            string response = new HttpRequest(Program.Api).Post("users.get?user_ids=" + id + "&fields=friend_status,blacklisted_by_me,domain,photo_100&" + Program.Token).ToString();
            dynamic request = JObject.Parse(response);
            if (Program.json_mode)
            {
                Console.WriteLine(request);
            }
            File.AppendAllText("logs/HttpRequests.log", response);

            author.Id = (int)request.response[0].id;
            author.Name = (string)request.response[0].first_name;
            author.Surname = (string)request.response[0].last_name;
            author.Friend_Status = (int)request.response[0].friend_status;
            author.Banned = (int)request.response[0].blacklisted_by_me;
            author.Domain = (string)request.response[0].domain;
            author.Photo = (string)request.response[0].photo_100;

            return author;
        }
    }
}
