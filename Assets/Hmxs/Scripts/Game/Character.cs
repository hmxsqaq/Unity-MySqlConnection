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
        [Title("Base Info")]
        public string characterName;
        public CharacterAttribute characterAttribute;
        public int characterBaseHp;

        [Title("Character Info")]
        public Player owner;
        public int characterLevel;
        public int characterExp;
        public int characterMaxHp;

        public Character(string characterName, CharacterAttribute characterAttribute, int characterBaseHp, Player owner, int characterLevel, int characterExp, int characterMaxHp)
        {
            this.characterName = characterName;
            this.characterAttribute = characterAttribute;
            this.characterBaseHp = characterBaseHp;
            this.owner = owner;
            this.characterLevel = characterLevel;
            this.characterExp = characterExp;
            this.characterMaxHp = characterMaxHp;
        }
    }
}