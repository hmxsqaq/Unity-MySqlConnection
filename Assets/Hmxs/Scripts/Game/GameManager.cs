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

        public void GameStart(int playerIndex)
        {
            Debug.Log($"Game start with player index: {playerIndex}");
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
            player = new Player(playerIndex, guildId, playerColor, username, password, email, registerDate);
        }
    }
}