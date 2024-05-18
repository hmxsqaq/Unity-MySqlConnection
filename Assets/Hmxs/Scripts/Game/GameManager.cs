using System.Collections.Generic;
using Hmxs.Scripts.MySQL;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        [ReadOnly] [SerializeField] private Player player;

        public Player GetPlayer() => player;

        public void InitGame(int playerIndex)
        {
            Debug.Log($"Game start with player index: {playerIndex}");
            LoadPlayer(playerIndex);
        }

        private void LoadPlayer(int playerIndex)
        {
            var playerData = MySqlHelper.ExecuteQueryList($"SELECT * FROM player WHERE PlayerID = {playerIndex}")[0];

            var guildId = playerData[1] == "NULL" ? (int?)null : int.Parse(playerData[1]);
            var playerColor = playerData[2] switch
            {
                "红" => Color.red,
                "黄" => Color.yellow,
                "蓝" => Color.blue,
                "绿" => Color.green,
                "紫" => Color.magenta,
                "橙" => new Color(1, 0.5f, 0),
                _ => Color.white
            };
            var username = playerData[3];
            var password = playerData[4];
            var email = playerData[5];
            var registerDate = playerData[6];
            var characterList = MySqlHelper.ExecuteQueryList($"SELECT * FROM playercharacter WHERE PlayerID = {playerIndex}");
            var characters = new List<Character>();
            // foreach (var characterData in characterList)
            // {
            //     var characterName = characterData[1];
            //     var characterAttribute = characterData[2] switch
            //     {
            //         "木" => CharacterAttribute.木,
            //         "水" => CharacterAttribute.水,
            //         "火" => CharacterAttribute.火,
            //         "土" => CharacterAttribute.土,
            //         "雷" => CharacterAttribute.雷,
            //         "光" => CharacterAttribute.光,
            //         _ => CharacterAttribute.木
            //     };
            //     var characterBaseHp = int.Parse(characterData[3]);
            //     var characterLevel = int.Parse(characterData[4]);
            //     var characterExp = int.Parse(characterData[5]);
            //     var characterMaxHp = int.Parse(characterData[6]);
            //     characters.Add(new Character(characterName, characterAttribute, characterBaseHp, player, characterLevel, characterExp, characterMaxHp));
            // }

            player = new Player(playerIndex, guildId, playerColor, username, password, email, registerDate, characters);
        }
    }
}