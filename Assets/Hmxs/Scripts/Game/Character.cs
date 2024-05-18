using System;
using Sirenix.OdinInspector;

namespace Hmxs.Scripts.Game
{
    public enum CharacterAttribute
    {
        木,
        水,
        火,
        土,
        雷,
        光
    }

    [Serializable]
    public class Character
    {
        public int characterID;
        public string characterName;
        public CharacterAttribute characterAttribute;
        public int characterBaseHp;

        public Character(int characterID, string characterName, CharacterAttribute characterAttribute, int characterBaseHp)
        {
            this.characterID = characterID;
            this.characterName = characterName;
            this.characterAttribute = characterAttribute;
            this.characterBaseHp = characterBaseHp;
        }
    }
}