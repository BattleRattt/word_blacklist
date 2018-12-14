using System;
using System.IO;
using System.Reflection;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace Server
{
    public class Server : BaseScript
    {
        public Server()
        {
            EventHandlers["chatMessage"] += new Action<Player, string, string, string>(CheckMessage);
            EventHandlers["KickPlayer"] += new Action<Player>(DropPlayer);
        }  

        // Check the message of the player, see if he/her is sending toxic and mean messages to the server!
        public void CheckMessage([FromSource] Player source,string author, string color, string message)
        {
            // Add banned words in here!
            string[] bannedwords = File.ReadAllLines("resources/word_blacklist/bannedwords.txt");

            // Loop thru the banned words to compare it with the chat.
            foreach(var GetBannedWords in bannedwords)
            {
                // Check if the message that the player is sending is equal to the banned words.
                message = message.ToLower();
                if (message.Contains(GetBannedWords))
                {
                    TriggerClientEvent("PunishPlayer");
                    API.CancelEvent();
                }
            }

            foreach(var t in bannedwords) { Debug.WriteLine(t); }
        }

        // Kick the player if the chances are equal to 0.
        public void DropPlayer([FromSource] Player source)
        {
            // Kick the player for there inappropriate behavior.
            API.DropPlayer(source.Handle, "You where kicked due to sending inappropriate messages more that 5 times, *clap* *clap*");
        }
    }
}
