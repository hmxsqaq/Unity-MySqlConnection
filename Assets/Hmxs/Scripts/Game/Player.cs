using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hmxs.Scripts.Game
{
    [Serializable]
    public class Player
    {
        public int playerID;
        public int? guildID;
        public Color playerColor;
        public string username;
        public string password;
        public string email;
        public string registerDate;
        public List<Character> characters;

        public Player(int playerID, int? guildID, Color playerColor, string username, string password, string email, string registerDate, List<Character> characters)
        {
            this.playerID = playerID;
            this.guildID = guildID;
            this.playerColor = playerColor;
            this.username = username;
            this.password = password;
            this.email = email;
            this.registerDate = registerDate;
            this.characters = characters;
        }
    }
}