using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace TelegramSend
{

    class Program
    {
        static void Main(string[] args)
        {          

            if (args.Length < 3)
            {
                Console.WriteLine("Usage: TelegramSend.dll <token> <group> <string>");
                Console.WriteLine("Example: TelegramSend.dll 123456789:ABC-DEF1234ghIkl-zyx57W2v1u123ew11 -1001155117777 <string>");
                return;
            }

            List<string> args_list = new List<string>(args);

            for (int i = 0; i < args_list.Count; i++)
            {
                if (args_list[i].Contains(".dll"))
                {
                    args_list.RemoveAt(i);
                }
            }

            string token = args_list[0];
            string groupId = args_list[1];
            string stringToSend = args_list[2];

            if (!IsValidToken(token))
            {
                Console.WriteLine("Invalid token");
                return;
            }

            SendMessage(token, groupId, stringToSend);
        }

        static void SendMessage(string token, string groupId, string stringToSend) 
        {
            string url = "https://api.telegram.org/bot" + token + "/sendMessage?chat_id=" + groupId + "&parse_mode=HTML" + "&text=" + stringToSend;
            Console.WriteLine(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
        }

        static bool IsValidToken(string token)
        {
            string url = "https://api.telegram.org/bot" + token + "/getMe";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseFromServer = reader.ReadToEnd();
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
