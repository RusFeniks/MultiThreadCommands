using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace MultiThreadCommands
{
    class Keywords
    {
        public string Command_Name;
        public string[] Keywords_Array;
        public string ArgumentsRegex;
        public int Bot_Class;


        //ДОПОЛНИТЕЛЬНЫЕ СТРОКИ (К ПРИМЕРУ, ДЛЯ УДОБНОЙ ЗАДАЧИ АРГУМЕНТОВ)
        public static string full = Functions.GenerateSet(File.ReadAllLines("keywords/RPG/Profile/info_full.kw"));

        public static string stat_str = @"(str|стр|сил|сила|strenght)";
        public static string stat_agl = @"(agl|agility|ловкость|агл|лов)";
        public static string stat_int = @"(int|intelligence|интеллект|инт)";
        public static string stat_stm = @"(stm|стм|вын|stamina|выносливость)";
        public static string stat_mst = @"(mst|magic|маг|магия|мст)";
        public static string stat_lck = @"(lck|luck|удача|лак|удч)";

        public static string stat_all = @"(str|стр|сил|сила|strenght|agl|agility|ловкость|агл|лов|int|intelligence|интеллект|инт|stm|стм|вын|stamina|выносливость|mst|magic|маг|магия|мст|lck|luck|удача|лак|удч)";


        public static string[] Names = File.ReadAllLines("keywords/names.kw");
        public static List<Keywords> List = new List<Keywords> {
            new Keywords("test", ""),
            new Keywords("test", ".+"),
            new Keywords("reg", "", "RPG/Profile"),
            new Keywords("reg", ".+", "RPG/Profile"),
            new Keywords("info", "", "RPG/Profile"),
            new Keywords("info", full, "RPG/Profile"),
            new Keywords("addstat",stat_all + @"\s+[0-9]{1,3}","RPG/Profile"),
            new Keywords("quest","","RPG"),
            new Keywords("invitebot",@"[0-9]","RPG"),
            new Keywords("synchbot","","RPG")
        };


        private Keywords (string Name, string Arguments, string Folder = "", int Class = 0)
        {
            Command_Name = Name;
            if(Folder != "")
            {
                Folder += "/";
            }
            string filePath = "keywords/" + Folder + Name + ".kw";
            Keywords_Array = File.ReadAllLines(filePath);
            ArgumentsRegex = Arguments;
            Bot_Class = Class;
        }


        // ВЫПОЛНЯЕТ ПОИСК НАЛИЧИЯ КОМАНДЫ В СООБЩЕНИИ (ВОЗВРАЩАЕТ НАЙДЕННУЮ КОМАНДУ, ЛИБО NULL)
        public static Keywords FindKeywords(string message)
        {
            Keywords Result = null;
            foreach (Keywords key in List)
            {
                if (GeneratorCommand(key.Keywords_Array,key.ArgumentsRegex).IsMatch(message))
                {
                    Result = key;
                    break;
                }
            }
            return Result;
        }

        // ВОЗВРАЩАЕТ REGEX ВЫРАЖЕНИЕ ДЛЯ КОМАНДЫ
        public static Regex GeneratorCommand(string[] command_variations, string args = "")
        {
            string cmd = @"^" + Functions.GenerateSet(Keywords.Names) + @"(\,|\:)?\s+" + Functions.GenerateSet(command_variations);
            if (args != "")
            {
                cmd += @"(\,|\:)?\s+" + args;
            }
            cmd += "$";
            Regex regex = new Regex(cmd, RegexOptions.IgnoreCase);
            return regex;
        }

        public static string GetArguments (string message, Keywords keywords)
        {
            if (keywords.ArgumentsRegex != "")
            {
                string cmd = @"^" + Functions.GenerateSet(Keywords.Names) + @"(\,|\:)?\s+" + Functions.GenerateSet(keywords.Keywords_Array) + @"(\,|\:)?\s+";
                string result = new Regex(cmd, RegexOptions.IgnoreCase).Replace(message, "");
                result = new Regex(@"<br>", RegexOptions.IgnoreCase).Replace(result, "");
                if(result == "")
                {
                    return null;
                }
                else
                {
                    return result;
                }
            } else return null;
        }
    }
}
