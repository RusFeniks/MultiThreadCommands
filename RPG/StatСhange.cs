//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace MultiThreadCommands
//{
//    class StatChange
//    {
//        public static void Money(int money = 0, Messages current = null, User user = null)
//        {
//            if (current == null & user == null) { current = Messages.Current; user = current.Profile; }
//            else if (current != null) { user = current.Profile; }
//            else if (current == null & user != null) { }
//            else { return; }

//            user.Money += money;
//            User.Save(user);
//        }

//        public static void Damage_hp (int hp = 0, Messages current = null, User user = null)
//        {
//            if (current == null & user == null) { current = Messages.Current; user = current.Profile; }
//            else if (current != null) { user = current.Profile; }
//            else if (current == null & user != null) { }
//            else { return; }

//            user.Hp -= hp;
//            if (user.Hp < 1)
//            {
//                if (current != null)
//                    Functions.SendReply("💀 [id" + user.Id + "|" + user.Name + "], вы погибли!\nВаше здоворье опустилось ниже нуля. Но не расстаивайтесь! Богиня дарит вам ещё один шанс.\nВы воскреснете в ближайшем храме, но вам потребуется некоторое время для полного восстановления, а так-же отдать 10% своих сбережений как плату за лечение. Также вы потеряете весь накопленный, за уровень, опыт. Это достойная плата за воскрешение!", current.PeerId, Bots.List[0].Token);
//                user.Hp = 1;
//                user.Mp = 1;
//                if (user.VipStatus == 1)
//                {
//                    user.Ap = -20;
//                } else
//                {
//                    user.Ap = -10;
//                }
//                user.Exp = 0;
//                user.Money -= Convert.ToInt32(Math.Round(user.Money / 100.0 * 10.0));
//            }
//            else if (user.Hp > user.Hp_Max) { user.Hp = user.Hp_Max; }

//            User.Save(user);
//        }

//        public static void Damage_mp(int mp = 0, Messages current = null, User user = null)
//        {
//            if (current == null & user == null) { current = Messages.Current; user = current.Profile; }
//            else if (current != null) { user = current.Profile; }
//            else if (current == null & user != null) { }
//            else { return; }

//            user.Mp -= mp;
//            if (user.Mp < 0)
//            {
//                if (current != null)
//                    Functions.SendReply("🐩 [id" + user.Id + "|" + user.Name + "], ваша магическая сила иссякла. Вы пытались пересечь свой лимит и полностью обессилили!\nВам потребуется какое-то время, чтобы отдохнуть и восстановить силы.", current.PeerId, Bots.List[0].Token);
//                user.Mp = 0;
//                if (user.VipStatus == 1)
//                {
//                    user.Ap = -10;
//                }
//                else
//                {
//                    user.Ap = -5;
//                }
//            }
//            else if (user.Mp > user.Mp_Max) { user.Mp = user.Mp_Max; }

//            User.Save(user);
//        }

//        public static void Damage_ap(int ap = 0, Messages current = null, User user = null)
//        {
//            if (current == null & user == null) { current = Messages.Current; user = current.Profile; }
//            else if (current != null) { user = current.Profile; }
//            else if (current == null & user != null) { }
//            else { return; }

//            user.Ap -= ap;
//            if (user.Ap < -user.Agl)
//            {
//                Damage_hp(user.Hp_Max);
//            }
//            else if (user.Ap < 0)
//            {
//                if (current != null)
//                    Functions.SendReply("🐩 [id" + user.Id + "|" + user.Name + "], вы исчерпали свои силы! \nВам потребуется какое-то время, чтобы отдохнуть.", current.PeerId, Bots.List[0].Token);
//            }
//            else if (user.Ap > user.Ap_Max) { user.Ap = user.Ap_Max; }

//            User.Save(user);
//        }


//        public static void Exp (int exp = 0, Messages current = null, User user = null)
//        {

//            if (current == null & user == null) { current = Messages.Current; user = current.Profile; }
//            else if (current != null) { user = current.Profile; }
//            else if (current == null & user != null) { }
//            else { return; }

//            if (user.VipStatus == 1) { exp *= 2; }
//            user.Exp += exp;
//            if (user.Exp >= user.Exp_Cap)
//            {
//                user.Exp -= user.Exp_Cap;
//                user.Exp_Cap += 15;
//                user.Lvl++;
//                int addedSP = 3;
//                if (user.VipStatus == 1) { addedSP++; }
//                user.Stat_Points += addedSP;
//                if (current != null)
//                    Functions.SendReply("[id" + user.Id + "|" + user.Name + "], поздравляю с получением " + user.Lvl + " уровня!\nВам добавлено " + addedSP + " очка прокачки!", current.PeerId, Bots.List[0].Token);
//            }
//            User.Save(user);
//        }

//        public static void AddStat (Messages current = null, User user = null, string Args = "")
//        {
//            if(current == null & user == null) { current = Messages.Current; user = current.Profile; Args = current.Args; }
//            else if(current != null) { user = current.Profile; Args = current.Args; }
//            else if(current == null & user != null & Args != "") { }
//            else { return; }
//            if (user != null)
//            {
//                int points = Convert.ToInt32(new Regex(Keywords.stat_all + @"\s+", RegexOptions.IgnoreCase).Replace(Args, ""));
//                if (points <= user.Stat_Points)
//                {
//                    string stat = new Regex(@"\s+[0-9]+", RegexOptions.IgnoreCase).Replace(Args, "");

//                    if (new Regex(Keywords.stat_agl, RegexOptions.IgnoreCase).IsMatch(stat))
//                    {
//                        user.Stat_Points -= points;
//                        string remainder = "Осталось очков прокачки: " + user.Stat_Points;
//                        user.Agl += points;
//                        user.Ap_Max += points;
//                        if(current != null)
//                            Functions.SendReply("[id" + user.Id + "|" + user.Name + "]" + ", ваша ловкость повышена на " + points + " и теперь составляет " + user.Agl + "\n" + remainder, current.PeerId, Bots.List[0].Token);
//                    }
//                    else if (new Regex(Keywords.stat_int, RegexOptions.IgnoreCase).IsMatch(stat))
//                    {
//                        user.Stat_Points -= points;
//                        string remainder = "Осталось очков прокачки: " + user.Stat_Points;
//                        user.Int += points;
//                        user.Mp_Max += points;
//                        if (current != null)
//                            Functions.SendReply("[id" + user.Id + "|" + user.Name + "]" + ", ваш интеллект повышен на " + points + " и теперь составляет " + user.Int + "\n" + remainder, current.PeerId, Bots.List[0].Token);
//                    }
//                    else if (new Regex(Keywords.stat_lck, RegexOptions.IgnoreCase).IsMatch(stat))
//                    {
//                        user.Stat_Points -= points;
//                        string remainder = "Осталось очков прокачки: " + user.Stat_Points;
//                        user.Lck += points;
//                        if (current != null)
//                            Functions.SendReply("[id" + user.Id + "|" + user.Name + "]" + ", ваша удача повышена на " + points + " и теперь составляет " + user.Lck + "\n" + remainder, current.PeerId, Bots.List[0].Token);
//                    }
//                    else if (new Regex(Keywords.stat_mst, RegexOptions.IgnoreCase).IsMatch(stat))
//                    {
//                        user.Stat_Points -= points;
//                        string remainder = "Осталось очков прокачки: " + user.Stat_Points;
//                        user.Mst += points;
//                        user.Mp_Max += points * 2;
//                        user.Mp += points;
//                        if (current != null)
//                            Functions.SendReply("[id" + user.Id + "|" + user.Name + "]" + ", ваша магия повышена на " + points + " и теперь составляет " + user.Mst + "\n" + remainder, current.PeerId, Bots.List[0].Token);
//                    }
//                    else if (new Regex(Keywords.stat_stm, RegexOptions.IgnoreCase).IsMatch(stat))
//                    {
//                        user.Stat_Points -= points;
//                        string remainder = "Осталось очков прокачки: " + user.Stat_Points;
//                        user.Stm += points;
//                        user.Hp_Max += points * 2;
//                        user.Hp += points;
//                        if (current != null)
//                            Functions.SendReply("[id" + user.Id + "|" + user.Name + "]" + ", ваша выносливость повышена на " + points + " и теперь составляет " + user.Stm + "\n" + remainder, current.PeerId, Bots.List[0].Token);
//                    }
//                    else if (new Regex(Keywords.stat_str, RegexOptions.IgnoreCase).IsMatch(stat))
//                    {
//                        user.Stat_Points -= points;
//                        string remainder = "Осталось очков прокачки: " + user.Stat_Points;
//                        user.Str += points;
//                        user.Hp_Max += points;
//                        if (current != null)
//                            Functions.SendReply("[id" + user.Id + "|" + user.Name + "]" + ", ваша сила повышена на " + points + " и теперь составляет " + user.Str + "\n" + remainder, current.PeerId, Bots.List[0].Token);
//                    }
//                    User.Save(user);
//                }
//                else
//                {
//                    if (current != null)
//                        Functions.SendReply("Вам не хватает очков прокачки! Количество ваших очков прокачки: " + user.Stat_Points, current.PeerId, current.MessageID, Bots.List[0].Token);
//                }
//            } else
//            {
//                if (current != null)
//                    Functions.SendReply("Ваш профиль ещё не создан!\nСоздайте его с помощью команды: !bot reg [желаемое имя]", current.PeerId, current.MessageID, Bots.List[0].Token);
//            }
//        }
//    }
//}
