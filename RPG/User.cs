using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadCommands
{
    class User
    {
        public static List<User> List = new List<User>();

        public int Id { get; set; }
        public string Name { get; set; }
        public int Lvl { get; set; }
        public int Exp { get; set; }
        public int Exp_Cap { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Ap { get; set; }
        public int Hp_Max { get; set; }
        public int Mp_Max { get; set; }
        public int Ap_Max { get; set; }
        public int Str { get; set; }
        public int Stm { get; set; }
        public int Agl { get; set; }
        public int Int { get; set; }
        public int Mst { get; set; }
        public int Lck { get; set; }
        public int[] Skills { get; set; }
        public int[] Abills { get; set; }
        public int[] Inventory { get; set; }
        public int Inventory_slots { get; set; }
        public int Money { get; set; }
        public int Stat_Points { get; set; }
        public int VipStatus { get; set; }

        private User() {}

        public static bool Add (int id, string name)
        {
            try
            {
                User NewUser = new User
                {
                    Id = id,
                    Name = name,
                    Lvl = 1,
                    Exp = 0,
                    Exp_Cap = 15,
                    Hp = 8,
                    Mp = 8,
                    Ap = 3,
                    Hp_Max = 8,
                    Mp_Max = 8,
                    Ap_Max = 3,
                    Str = 3,
                    Stm = 2,
                    Agl = 3,
                    Int = 3,
                    Mst = 2,
                    Lck = 2,
                    Inventory_slots = 2,
                    Money = 0,
                    Stat_Points = 3,
                    VipStatus = 0
                };
                List.Add(NewUser);
                DataBase.Connect();
                DataBase.Query("INSERT INTO Players " +
                    "(pId,pName,pLvl,pExp,pHp,pMp,pAp,pStr,pStm,pAgl,pInt,pMst,pLck,pMoney,pStatPoints,pVipStatus)" +
                    " VALUES (" +

                    NewUser.Id + ",'" + 
                    NewUser.Name + "'," + 
                    NewUser.Lvl + "," + 
                    NewUser.Exp + "," + 
                    NewUser.Hp + "," + 
                    NewUser.Mp + "," + 
                    NewUser.Ap + "," + 
                    NewUser.Str + "," + 
                    NewUser.Stm + "," + 
                    NewUser.Agl + "," + 
                    NewUser.Int + "," + 
                    NewUser.Mst + "," + 
                    NewUser.Lck + "," + 
                    NewUser.Money + "," + 
                    NewUser.Stat_Points + "," + 
                    NewUser.VipStatus

                    + ")").ExecuteNonQuery();
                DataBase.Close();
                Console.WriteLine("Новый пользователь добавлен: " + name);
                return true;
            } catch
            {
                return false;
            }
        }

        public static void CreteList ()
        {
            try
            {
                DataBase.Connect();
                DataBase.Command = DataBase.Query("SELECT * FROM Players");
                DataBase.Reader = DataBase.Command.ExecuteReader();
                while (DataBase.Reader.Read())
                {
                    List.Add(new User()
                    {
                        Id = Convert.ToInt32(DataBase.Reader["pId"]),
                        Name = Convert.ToString(DataBase.Reader["pName"]),
                        Lvl = Convert.ToInt32(DataBase.Reader["pLvl"]),
                        Exp = Convert.ToInt32(DataBase.Reader["pExp"]),
                        Exp_Cap = (Convert.ToInt32(DataBase.Reader["pLvl"]) * 15),
                        Hp = Convert.ToInt32(DataBase.Reader["pHp"]),
                        Mp = Convert.ToInt32(DataBase.Reader["pMp"]),
                        Ap = Convert.ToInt32(DataBase.Reader["pAp"]),
                        Hp_Max = (Convert.ToInt32(DataBase.Reader["pStm"]) * 2 + Convert.ToInt32(DataBase.Reader["pStr"])),
                        Mp_Max = (Convert.ToInt32(DataBase.Reader["pMst"]) * 2 + Convert.ToInt32(DataBase.Reader["pInt"])),
                        Ap_Max = Convert.ToInt32(DataBase.Reader["pAgl"]),
                        Str = Convert.ToInt32(DataBase.Reader["pStr"]),
                        Stm = Convert.ToInt32(DataBase.Reader["pStm"]),
                        Agl = Convert.ToInt32(DataBase.Reader["pAgl"]),
                        Int = Convert.ToInt32(DataBase.Reader["pInt"]),
                        Mst = Convert.ToInt32(DataBase.Reader["pMst"]),
                        Lck = Convert.ToInt32(DataBase.Reader["pLck"]),
                        Money = Convert.ToInt32(DataBase.Reader["pMoney"]),
                        Inventory_slots = (Convert.ToInt32(DataBase.Reader["pLvl"]) + 2),
                        Stat_Points = Convert.ToInt32(DataBase.Reader["pStatPoints"]),
                        VipStatus = Convert.ToInt32(DataBase.Reader["pVipStatus"])
                    });
                }
                Console.WriteLine("Пользователи загружены");
                DataBase.Close();
            } catch (Exception Ex)
            {
                Console.WriteLine("При загрузке пользователей из базы данных произошла ошибка!");
                Console.WriteLine(Ex.Message);
            }
        }

        public static void Save (User player)
        {
            DataBase.Connect();
            DataBase.Query("UPDATE Players SET " +
                "pName = '" + player.Name + "'" +
                ",pLvl = " + player.Lvl +
                ",pExp = " + player.Exp +
                ",pHp = " + player.Hp +
                ",pMp = " + player.Mp +
                ",pAp = " + player.Ap +
                ",pStr = " + player.Str +
                ",pStm = " + player.Stm +
                ",pAgl = " + player.Agl +
                ",pInt = " + player.Int +
                ",pMst = " + player.Mst +
                ",pLck = " + player.Lck +
                ",pMoney = " + player.Money +
                ",pStatPoints = " + player.Stat_Points +
                ",pVipStatus = " + player.VipStatus +
                " WHERE pID = " + player.Id).ExecuteNonQuery();
            DataBase.Close();
        }

        public static void SaveAll()
        {
            foreach (User user in List)
            {
                Save(user);
            }
        }

        public static int Find (int id)
        {
            int i = 0;
            foreach (User item in List)
            {
                if(item.Id == id)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }


    }
}
