using System;

namespace Hmxs.Scripts.Game
{
    [Serializable]
    public class PlayerCharacter
    {
        public Character character;
        public int ownerID;
        public int characterLevel;
        public int experiencePoints;
        public int maxHealth;

        public PlayerCharacter(Character character,int ownerID, int characterLevel, int experiencePoints, int maxHealth)
        {
            this.character = character;
            this.ownerID = ownerID;
            this.characterLevel = characterLevel;
            this.experiencePoints = experiencePoints;
            this.maxHealth = maxHealth;
        }
    }
}