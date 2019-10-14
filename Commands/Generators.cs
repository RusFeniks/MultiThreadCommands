using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using xNet;
using System.Text.RegularExpressions;
using System.IO;

namespace MultiThreadCommands
{
    class Generators
    {

        /*
        public static void LearnTheOpinion (Messages command)
        {
            Messages.SendReply(Phrases.GeneratorOne(Phrases.LearnTheOpinion_reply), command.PeerId,command.MessageID);
        }



        public static void AlternativeChange (Messages command)
        {
            string alt = new Regex(@"(Касай(\s*\,|)\s+(выбирай|выберай|выбери|сделай выбор|что выберешь|что предпочитаешь|что предпочтёшь|одно из двух))(\s*:|,|)", RegexOptions.IgnoreCase).Replace(command.Text, "");
            alt = new Regex(@"(\?*)$").Replace(alt, "");
            string[] Alt = new string[2];
            Alt[0] = new Regex(@"(\s+.+\s+)+или", RegexOptions.IgnoreCase).Replace(alt, "");
            Alt[1] = new Regex(@"\s+или(\s+.+\s*)+", RegexOptions.IgnoreCase).Replace(alt, "");
            string result = "Я выбираю" + Alt[new Random().Next(0,2)]+"!";
            Messages.SendReply(result, command.PeerId, command.MessageID);
        }

        public static void Reiteration (Messages command)
        {
            string text = new Regex(@"^(Касай(\s*\,|)\s+(повтори|скажи|повтор|повторение|воспроизведи))\s*(:|,|)\s+", RegexOptions.IgnoreCase).Replace(command.Text, "");
            Messages.SendReply(text, command.PeerId);
        }
        */
    }
}
