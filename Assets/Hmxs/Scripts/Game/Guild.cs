using System;

namespace Hmxs.Scripts.Game
{
    [Serializable]
    public class Guild
    {
        public int guildID;
        public string guildName;
        public string establishDate;

        public Guild(int guildID, string guildName, string establishDate)
        {
            this.guildID = guildID;
            this.guildName = guildName;
            this.establishDate = establishDate;
        }
    }
}