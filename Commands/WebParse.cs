//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using xNet;

//namespace MultiThreadCommands
//{
//    class WebParse
//    {
//        public static string BashImCmd = @"Касай баш|Касай, баш|Касай, башим|Касай башим|Касай, башорг|Касай башорг";


//        public static void BashIm(Messages command)
//        {
//            string WebPage = new HttpRequest().Get("https://bash.im/random").ToString();
//            MatchCollection Citats = new Regex("<div class=\"text\">" + @".*" + "</div>").Matches(WebPage);
//            string citata = new Regex("<div class=\"text\">").Replace(Citats[new Random().Next(0, Citats.Count)].Value, "");
//            citata = new Regex("</div>").Replace(citata, "");
//            citata = new Regex("<br>|<br \\>|<br />").Replace(citata, "\n");
//            citata = new Regex("&quot;").Replace(citata, "\"");
//            citata = new Regex("&gt;").Replace(citata, ">");
//            citata = new Regex("&lt;").Replace(citata, "<");
//            Messages.SendReply(citata, command.PeerId);
//        }
//    }
//}
