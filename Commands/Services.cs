//using newtonsoft.json.linq;
//using system;
//using system.collections.generic;
//using system.linq;
//using system.text;
//using system.text.regularexpressions;
//using system.threading.tasks;
//using xnet;
//using newtonsoft.json.serialization;
//using newtonsoft.json;

//namespace multithreadcommands
//{
//    class services
//    {

//        public static void invite(messages command)
//        {
//            dynamic request;
//            string args = phrases.getargs(command.text, phrases.invite);
//            int id = phrases.getid(args);
//            if (id != -1)
//            {
//                int chat_id = command.peerid;
//                if (chat_id > 2000000000) { chat_id -= 2000000000; }
//                request = jobject.parse(new httprequest(program.api).post("messages.addchatuser?chat_id=" + chat_id + "&user_id=" + id + "&" + program.token).tostring());
//                if (request.response != "1")
//                {
//                    messages.sendreply("мне не удаётся пригласить этого пользователя (id" + id + ") :с \nвозможно его нет у меня в друзьях или он запретил приглашать его в конференции.", command.peerid);
//                }
//            }
//            else
//            {
//                messages.sendreply("вы не верно указали id пользователя. пожалуйста укажите ссылку на его страницу или его числовой id.\nпример: !kasai invite https://vk.com/kasai_kudasai", command.peerid);
//            }
//        }



//        public static void getid(messages command)
//        {
//            string args = phrases.getargs(command.text, phrases.getid);
//            int id = phrases.getid(args);
//            if (id != -1)
//            {
//                messages.sendreply("id [id"+id+"|пользователя]: " + id, command.peerid);
//            }
//            else
//            {
//                messages.sendreply("вы не верно указали ссылку на страницу пользователя.\nпример правильной команды:\n!kasai getid https://vk.com/kasai_kudasai", command.peerid);
//            }
//        }



//        public static void hfcrkflrf (messages command)
//        {
//            if((string)command.attachments.fwd != null)
//            {
//                console.writeline("прикрепления найдены");
//                string messages = command.attachments.fwd;
//                messages = new regex(@"[0-9]+_").replace(messages, "");
//                console.writeline(messages);

//                string request = new httprequest(program.api).post("messages.getbyid?message_ids=" + messages + "&" + program.token).tostring();
//                console.writeline(request);

//            } else
//            {
//                console.writeline("прикрепления отсутсвтуют");
//            }
//        }
//    }
//}
