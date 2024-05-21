using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hmxs.Scripts.MySQL;
using Hmxs.Scripts.UI;
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

        [Title("Panel")]
        [SerializeField] private float transitionSpeed;
        [Required] [SerializeField] private CanvasGroup mainCanvas;

        [Title("Button")]
        [Required] [SerializeField] private BoolButton playerButton;
        [Required] [SerializeField] private BoolButton characterButton;
        [Required] [SerializeField] private BoolButton guildButton;

        [Title("Info")]
        [ReadOnly] [SerializeField] private Player player;
        [ReadOnly] [SerializeField] private List<Character> characters;
        [ReadOnly] [SerializeField] private List<Guild> guilds;

        private BoolButton _currentButton;

        public List<Guild> GetGuilds() => guilds;
        public List<Character> GetCharacters() => characters;
        public Player GetPlayer() => player;

        private void Start()
        {
            playerButton.GetButton().onClick.AddListener(() => ChangeWindow(playerButton));
            characterButton.GetButton().onClick.AddListener(() => ChangeWindow(characterButton));
            guildButton.GetButton().onClick.AddListener(() => ChangeWindow(guildButton));
        }

        public void InitGame(int playerIndex)
        {
            LoadCharacters();
            LoadGuild();
            LoadPlayer(playerIndex);
            LoadWindows();
        }

        private void LoadCharacters()
        {
            var characterData = MySqlHelper.ExecuteQueryList("SELECT * FROM gamesystem.gamecharacter");
            characters = characterData.Select(data =>
            {
                var characterID = int.Parse(data[0]);
                var characterName = data[1];
                var characterAttribute = data[2] switch
                {
                    "木" => CharacterAttribute.木,
                    "水" => CharacterAttribute.水,
                    "火" => CharacterAttribute.火,
                    "土" => CharacterAttribute.土,
                    "雷" => CharacterAttribute.雷,
                    "光" => CharacterAttribute.光,
                    _ => CharacterAttribute.木
                };
                var characterBaseHp = int.Parse(data[3]);
                return new Character(characterID, characterName, characterAttribute, characterBaseHp);
            }).ToList();
        }

        private void LoadGuild()
        {
            var guildData = MySqlHelper.ExecuteQueryList("SELECT * FROM gamesystem.guild");
            guilds = guildData.Select(data =>
            {
                var guildID = int.Parse(data[0]);
                var guildName = data[1];
                var establishDate = data[2];
                return new Guild(guildID, guildName, establishDate);
            }).ToList();
        }

        private void LoadPlayer(int playerIndex)
        {
            var playerData = MySqlHelper.ExecuteQueryList($"SELECT * FROM gamesystem.player WHERE PlayerID = {playerIndex}")[0];

            if (!int.TryParse(playerData[1], out var guildId)) guildId = -1;
            var guild = guilds.Find(g => g.guildID == guildId);

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

            var playerCharacterData = MySqlHelper.ExecuteQueryList(
                $"SELECT pc.* FROM player p JOIN playercharacter pc ON p.PlayerID = pc.PlayerID WHERE p.PlayerID = {playerIndex}");
            var playerCharacters = (from data in playerCharacterData
                let characterID = int.Parse(data[1])
                let character = characters.Find(c => c.characterID == characterID)
                let characterLevel = int.Parse(data[2])
                let experiencePoints = int.Parse(data[3])
                let maxHealth = int.Parse(data[4])
                select new PlayerCharacter(character, playerIndex, characterLevel, experiencePoints, maxHealth)).ToList();
            player = new Player(playerIndex, playerColor, username, password, email, registerDate, playerCharacters, guild);
        }

        private void LoadWindows()
        {
            _currentButton = playerButton;

            playerButton.SetButton(false);
            characterButton.SetButton(true);
            guildButton.SetButton(true);

            StartCoroutine(LoadWindowsCoroutine());
        }

        private IEnumerator LoadWindowsCoroutine()
        {
            mainCanvas.gameObject.SetActive(true);
            mainCanvas.interactable = false;
            mainCanvas.alpha = 0;
            while (mainCanvas.alpha < 0.95f)
            {
                mainCanvas.alpha = Mathf.Lerp(mainCanvas.alpha, 1, Time.deltaTime * transitionSpeed);
                yield return null;
            }
            mainCanvas.alpha = 1;
            mainCanvas.interactable = true;
        }

        private void ChangeWindow(BoolButton target)
        {
            _currentButton.SetButton(true);
            target.SetButton(false);
            _currentButton = target;
        }
    }
}