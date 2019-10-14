using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadCommands
{
    class Events
    {

        public static List<int> ChatsList = new List<int>();

        public static void EventTimer ()
        {
            Console.WriteLine("Алгоритм обработки событий запущен");

            DateTime SaveDB_Timer = DateTime.Now;
            DateTime Regen_Timer = DateTime.Now;

            Thread.Sleep(1000);
            while (true)
            {
                // Функция сохранения базы данных (каждые 10 минут)
                if (DateTime.Now > SaveDB_Timer)
                {
                    User.SaveAll();
                    SaveDB_Timer = SaveDB_Timer.AddMinutes(10);
                    Console.WriteLine("База данных обновлена!");
                }

                // Регенерация здоровья, маны, энергии и кап опыта для випов (каждые 3 минуты)
                if (DateTime.Now > Regen_Timer)
                {
                    foreach (User user in User.List)
                    {
                        user.Hp += Convert.ToInt32(Math.Round((user.Stm * 0.1) + (user.Str * 0.2) + (new Random().Next(0, user.Lck) * 0.05)));
                        if (user.Hp >= user.Hp_Max) { user.Hp = user.Hp_Max; }
                        user.Mp += Convert.ToInt32(Math.Round((user.Mst * 0.1) + (user.Int * 0.2) + (new Random().Next(0, user.Lck) * 0.05)));
                        if (user.Mp >= user.Mp_Max) { user.Mp = user.Mp_Max; }
                        user.Ap += Convert.ToInt32(Math.Round((user.Agl * 0.3) + (new Random().Next(0, user.Lck) * 0.05)));
                        if (user.Ap >= user.Ap_Max) { user.Ap = user.Ap_Max; }
                        if(user.VipStatus == 1)
                        {
                            int i = Convert.ToInt32(Math.Round(new Random().Next(0, user.Lck) * 0.5));
                            //StatChange.Exp(exp: i, user: user);
                        }
                    }
                    Console.WriteLine("Цикл регенерации завершён");
                    Regen_Timer = Regen_Timer.AddMinutes(3);
                }


            }
        }
    }
}
