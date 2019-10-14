using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xNet;

namespace MultiThreadCommands
{
    class RPGSystem
    {
        public static void InviteBot (Messages command = null)
        {
            int botClass = Convert.ToInt32(command.Args);
            if (command == null) { command = Messages.Current; }
            List<Bots> BotClass = Bots.GetClassList(botClass);
            if(botClass == 1)
            {
                if(command.GroupId.BotsCatalog[botClass] == -1)
                {
                    int botNum = new Random().Next(0, BotClass.Count);
                    Console.WriteLine(botNum);
                    command.GroupId.BotsCatalog[botClass] = BotClass[botNum].Id;
                    int chat_id = command.PeerId;
                    if (chat_id > 2000000000) { chat_id -= 2000000000; }
                    //string res = new HttpRequest(Program.Api).Post("messages.addChatUser?chat_id=" + chat_id + "&user_id=" + BotClass[botNum].Id + "&" + Bots.List[0].Token + Program.ApiVer).ToString();
                    BotClass[botNum].GroupsCount++;
                    BotClass[botNum].groupAssocs.Add(new GroupAssoc { Id = command.GroupId.Id, Peer = 2000000000 + BotClass[botNum].GroupsCount});
                    Bots.SaveBot(BotClass[botNum]);
                    Groups.SaveGroup(command.GroupId);
                    //Functions.SendReply("Я откликнулся на твой зов!", GroupAssoc.GetAssoc(command.GroupId.Id, BotClass[botNum]).Peer, BotClass[botNum].Token);
                }
            }
        }

        public static void SendMsg (Messages command = null)
        {
            Functions.SendMessage(command.MessageID, "Текст от лица квестера", command.GroupId.Id, 1);
            Functions.SendMessage(command.MessageID, "Текст от лица комментатора", command.GroupId.Id, 0);
        }

        public static void GroupSynch (Messages command = null)
        {
            if (command == null) { command = Messages.Current; }
            int synch_message = new Random().Next(0,999999) * 15;
            foreach(int botid in command.GroupId.BotsCatalog)
            {
                if (botid > 0)
                {
                    object obj = new SynchObject
                    {
                        message = command,
                        bot = Bots.GetBot(botid),
                        synchMessage = synch_message
                    };
                    Thread synch = new Thread(CallBackSynch);
                    synch.Start(obj);
                }
            }
            Functions.SendMessage(synch_message.ToString(), command.GroupId.Id, 0);
        }

        private static void CallBackSynch (object SynchData)
        {
            SynchObject synchObject = (SynchObject)SynchData;
            Console.WriteLine(synchObject.bot.Name);

            dynamic getLongPollServer = JObject.Parse(new HttpRequest(Program.Api).Post("messages.getLongPollServer?need_pts=1&lp_version=3&" + synchObject.bot.Token).ToString());
            string server = getLongPollServer.response.server;
            string key = getLongPollServer.response.key;
            string ts = getLongPollServer.response.ts;
            bool sync = false;
            while (!sync)
            {
                dynamic requestUserLongPoll = JObject.Parse(new HttpRequest().Post("https://" + server + "?act=a_check&key=" + key + "&ts=" + ts + "&wait=25&mode=2&version=2").ToString());
                foreach (JArray update in requestUserLongPoll.updates)
                {
                    if((int)update[0] == 4)
                    {
                        int PeerId = (int)update[3];
                        if (PeerId > 2000000000)
                        {
                            string text = (string)update[5];
                            if(text == synchObject.synchMessage.ToString())
                            {
                                Console.WriteLine(PeerId);
                                Thread.Sleep(synchObject.bot.Class * 100);
                                GroupAssoc.ReWriteAssoc(synchObject.message.PeerId, PeerId, synchObject.bot);
                                sync = true;
                            }
                        }
                    }
                }
                ts = requestUserLongPoll.ts;
            }
            return;
        }
    }

    class SynchObject
    {
        public Messages message;
        public Bots bot;
        public int synchMessage;
    }
}
