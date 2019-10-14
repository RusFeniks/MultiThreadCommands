using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultiThreadCommands
{
    class CommunicationCommands
    {
        /*
        public static string HelloCmd1 = "(" + Phrases.GeneratorSeveral(Phrases.treatment) + ")" + @"\s+" + @"\b(" + Phrases.GeneratorSeveral(Phrases.hello) + @")\b";
        public static string HelloCmd2 = @"\b(" + Phrases.GeneratorSeveral(Phrases.hello) + @")\b" + @"\s+" + "(" + Phrases.GeneratorSeveral(Phrases.treatment) + ")";

        public static string ByeByeCmd1 = "(" + Phrases.GeneratorSeveral(Phrases.treatment) + ")" + @"\s+" + @"\b(" + Phrases.GeneratorSeveral(Phrases.bye) + @")\b";
        public static string ByeByeCmd2 = @"\b(" + Phrases.GeneratorSeveral(Phrases.bye) + @")\b" + @"\s+" + "(" + Phrases.GeneratorSeveral(Phrases.treatment) + ")";

        public static string GoodMorningCmd1 = "(" + Phrases.GeneratorSeveral(Phrases.treatment) + ")" + @"\s+" + @"\b(" + Phrases.GeneratorSeveral(Phrases.morning) + @")\b";
        public static string GoodMorningCmd2 = @"\b(" + Phrases.GeneratorSeveral(Phrases.morning) + @")\b" + @"\s+" + "(" + Phrases.GeneratorSeveral(Phrases.treatment) + ")";

        public static string GoodEveningCmd1 = "(" + Phrases.GeneratorSeveral(Phrases.treatment) + ")" + @"\s+" + @"\b(" + Phrases.GeneratorSeveral(Phrases.evening) + @")\b";
        public static string GoodEveningCmd2 = @"\b(" + Phrases.GeneratorSeveral(Phrases.evening) + @")\b" + @"\s+" + "(" + Phrases.GeneratorSeveral(Phrases.treatment) + ")";


        public static void MessageEvents(Command command,string[] message)
        {
            string Name = "";
            dynamic User;
            if (command.PeerId < 2000000000) {
                User = JObject.Parse((new HttpRequest(Program.Api).Post("users.get?user_ids=" + command.PeerId + "&fields=domain&" + Program.Token).ToString()));
            } else
            {
                User = JObject.Parse((new HttpRequest(Program.Api).Post("users.get?user_ids=" + command.Attachments.from + "&fields=domain&" + Program.Token).ToString()));
            }
            Name = "@" + User.response[0].domain + "(" + User.response[0].first_name + ")";
            string reply = Phrases.GeneratorOne(message)+", " + Name + "!";
            reply = reply[0].ToString().ToUpper() + reply.Remove(0, 1);
            Command.SendReply(reply, command.PeerId);
        }
        */
    }
}
