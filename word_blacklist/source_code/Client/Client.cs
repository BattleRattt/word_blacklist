using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.NaturalMotion;
using CitizenFX.Core.UI;

namespace Client
{
    public class Client : BaseScript
    {
        int chances = 10;

        public Client()
        {
            EventHandlers["PunishPlayer"] += new Action(PunishPlayer);
        }

        public void PunishPlayer()
        {
            chances = chances - 1;

            if (chances == 0)
            {
                // Kick the player.
                TriggerServerEvent("KickPlayer");
            }
            // Send a notification to warn the player for what they are sending in chat!
            Screen.ShowNotification($"You have {chances} chances left before you will be ~r~kicked~w~ for sending inappropriate messages.", false);
        }
    }
}