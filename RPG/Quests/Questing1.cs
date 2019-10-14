//using System;
//using System.IO;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MultiThreadCommands
//{
//    class Questing1
//    {
//        public static void RandomQuest (Messages current = null)
//        {
//            if(current == null) { current = Messages.Current; }
//            if (current.Profile.Ap > 2 & current.Profile.Hp > 0 & current.Profile.Mp > 0)
//            {
//                User player = current.Profile;
//                string[] word1 = File.ReadAllLines("phrases/random_quest/word1.txt");
//                string[] word2 = File.ReadAllLines("phrases/random_quest/word2.txt");
//                string[] word3 = File.ReadAllLines("phrases/random_quest/word3.txt");

//                string[] word4_1 = File.ReadAllLines("phrases/random_quest/word4_1.txt");
//                string[] word4_2 = File.ReadAllLines("phrases/random_quest/word4_2.txt");
//                string[] word4_3 = File.ReadAllLines("phrases/random_quest/word4_3.txt");
//                string[] word4_4 = File.ReadAllLines("phrases/random_quest/word4_4.txt");

//                string[] word5 = File.ReadAllLines("phrases/random_quest/word5.txt");
//                string[] word6 = File.ReadAllLines("phrases/random_quest/word6.txt");

//                Random R = new Random();

//                string result = "📜 [id" + player.Id + "|" + player.Name + "], " + word1[R.Next(0, word1.Length)] + " " + word2[R.Next(0, word2.Length)] + " " + word3[R.Next(0, word3.Length)] + ". ";

//                int fall_chance = R.Next(player.Lvl, player.Lvl * 2);
//                Console.WriteLine("Шанс неудачи: " + fall_chance);
//                int success_chance = player.Agl + player.Int + R.Next(1, player.Lck);
//                Console.WriteLine("Уровень навыков: " + success_chance);

//                int reward_money = 0;
//                int reward_exp = 0;
//                int reward_hp = 0;
//                int reward_mp = 0;
//                int reward_ap = 0;

//                if (success_chance > fall_chance * 2)
//                {
//                    result += "\n" + word4_1[R.Next(0, word4_1.Length)];

//                    reward_money = R.Next(1, player.Lck) * player.Lvl + 20;
//                    reward_exp = R.Next(1, player.Lck) + 10;

//                    reward_hp = R.Next(1, player.Lvl) - player.Str;
//                    if(reward_hp < 0) { reward_hp = 0; }
//                    reward_mp = R.Next(1, player.Lvl) - player.Int;
//                    if(reward_mp < 0) { reward_mp = 0; }
//                    reward_ap = 1;

//                    StatChange.Exp(reward_exp, current);

//                    result += "\n [ ❤ -"+reward_hp+" 🔮 -"+reward_mp+" ⚡ -"+reward_ap+" ]";
//                    result += "\n [ 💰 "+reward_money+" 📚 "+reward_exp+" ]";
//                }
//                else if (success_chance >= fall_chance)
//                {
//                    result += "\n" + word4_2[R.Next(0, word4_2.Length)];

//                    reward_money = R.Next(1, player.Lck) * player.Lvl + 10;
//                    reward_exp = R.Next(1, player.Lck) + 5;

//                    reward_hp = R.Next(1, player.Lvl) - 3;
//                    if (reward_hp < 0) { reward_hp = 0; }
//                    reward_mp = R.Next(1, player.Lvl) - 3;
//                    if (reward_mp < 0) { reward_mp = 0; }
//                    reward_ap = 2;

//                    StatChange.Exp(reward_exp, current);

//                    result += "\n [ ❤ -" + reward_hp + " 🔮 -" + reward_mp + " ⚡ -" + reward_ap + " ]";
//                    result += "\n [ 💰 " + reward_money + " 📚 " + reward_exp + " ]";
//                }
//                else if (success_chance * 2 < fall_chance)
//                {
//                    result += word5[R.Next(0, word5.Length)] + " " + word6[R.Next(0, word6.Length)];
//                    result += "\n" + word4_4[R.Next(0, word4_4.Length)];

//                    reward_money = R.Next(1, player.Lck) + 0;
//                    reward_exp = R.Next(1, player.Lck) + 0;

//                    reward_hp = R.Next(1, player.Lvl) + 3;
//                    if (reward_hp < 0) { reward_hp = 0; }
//                    reward_mp = R.Next(1, player.Lvl) + 3;
//                    if (reward_mp < 0) { reward_mp = 0; }
//                    reward_ap = 3;

//                    StatChange.Exp(reward_exp, current);

//                    result += "\n [ ❤ -" + reward_hp + " 🔮 -" + reward_mp + " ⚡ -" + reward_ap + " ]";
//                    result += "\n [ 💰 +" + reward_money + " 📚 +" + reward_exp + " ]";
//                }
//                else
//                {
//                    result += word5[R.Next(0, word5.Length)] + " " + word6[R.Next(0, word6.Length)];
//                    result += "\n" + word4_3[R.Next(0, word4_3.Length)];

//                    reward_money = R.Next(1, player.Lck) * player.Lvl + 5;
//                    reward_exp = R.Next(1, player.Lck) + 1;

//                    reward_hp = R.Next(1, player.Lvl);
//                    if (reward_hp < 0) { reward_hp = 0; }
//                    reward_mp = R.Next(1, player.Lvl);
//                    if (reward_mp < 0) { reward_mp = 0; }
//                    reward_ap = 2;


//                    result += "\n [ ❤ -" + reward_hp + " 🔮 -" + reward_mp + " ⚡ -" + reward_ap + " ]";
//                    result += "\n [ 💰 " + reward_money + " 📚 " + reward_exp + " ]";
//                }

//                Functions.SendReply(result, current.PeerId, Bots.List[0].Token);
//                StatChange.Money(reward_money, current);
//                StatChange.Exp(reward_exp, current);
//                StatChange.Damage_ap(reward_ap, current);
//                StatChange.Damage_mp(reward_mp, current);
//                StatChange.Damage_hp(reward_hp, current);
//            } else
//            {
//                Functions.SendReply("[id" + current.Profile.Id + "|" + current.Profile.Name + "], " + "сейчас вы не в состоянии приняться за задание!\nВам надо отдохнуть и восстановить силы, а затем можете вернуться к подвигам.", current.PeerId, Bots.List[0].Token);
//            }
//        }
//    }
//}
