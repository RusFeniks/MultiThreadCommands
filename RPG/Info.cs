//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using xNet;
//using Newtonsoft.Json.Linq;

//namespace MultiThreadCommands
//{
//    class Info
//    {
//        public static void CharacterCreate(Messages command = null)
//        {
//            if(command == null) { command = Messages.Current; }
//            if (command.Profile == null)
//            {
//                string name = command.Args;
//                if (name == null)
//                {
//                    name = command.Author.Name;
//                }
//                if (User.Add(command.Author.Id, name))
//                {
//                    Functions.SendReply("[id" + command.Author.Id + "|" + name + "]" + ", ваш профиль успешно создан!", command.PeerId, Bots.List[0].Token);
//                }
//            }
//            else
//            {
//                Functions.SendReply("Вы уже зарегестрированы под ником " + command.Profile.Name, command.PeerId, command.MessageID, Bots.List[0].Token);
//            }
//        }

//        public static void CharacterInfo(Messages command = null, bool full = false)
//        {
//            if (command == null) { command = Messages.Current; }
//            if (command.Profile != null)
//            {
//                User player = command.Profile;
//                string reply;
//                if (full)
//                {
//                    reply = "Персонаж: " + "[id" + player.Id + "|" + player.Name + "]" + '\n' +
//                    "Уровень: " + player.Lvl + " [" + player.Exp + "/" + player.Exp_Cap + "]" + '\n' +
//                    "[ ❤ " + player.Hp + "/" + player.Hp_Max + " |🔮 " + player.Mp + "/" + player.Mp_Max + " |⚡ " + player.Ap + "/" + player.Ap_Max + " ]" + '\n' +
//                    "[💰 " + player.Money + " | 🎒 " + player.Inventory_slots + " ]" + '\n' +
//                    "[ Сила: " + player.Str + " | Выносливость: " + player.Stm + " ]" + '\n' +
//                    "[ Ловкость: " + player.Agl + " | Удача: " + player.Lck + " ]" + '\n' +
//                    "[ Интеллект: " + player.Int + " | Магия: " + player.Mst + " ]" + '\n' +
//                    "Очки прокачки: " + player.Stat_Points;
//                }
//                else
//                {
//                    reply = "Персонаж: " + "[id" + player.Id + "|" + player.Name + "]" + '\n' +
//                    "Уровень: " + player.Lvl + " [" + player.Exp + "/" + player.Exp_Cap + "]" + '\n' +
//                    "[ ❤ " + player.Hp + "/" + player.Hp_Max + " |🔮 " + player.Mp + "/" + player.Mp_Max + " |⚡ " + player.Ap + "/" + player.Ap_Max + " ]" + '\n' +
//                    "[💰 " + player.Money + " | 🎒 " + player.Inventory_slots + " ]";
//                }

//                Functions.SendReply(reply, command.PeerId, Bots.List[0].Token);
//            }
//            else
//            {
//                Functions.SendReply("Ваш профиль ещё не создан!\nСоздайте его с помощью команды: !bot reg [желаемое имя]", command.PeerId, command.MessageID, Bots.List[0].Token);
//            }
//        }
//    }
//}
