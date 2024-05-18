using Hmxs.Scripts.Game;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts.UI
{
    public class PlayerInfoWindow : MonoBehaviour
    {
        [Required] [SerializeField] private Text usernameText;
        [Required] [SerializeField] private Text emailText;
        [Required] [SerializeField] private Text registerDateText;
        [Required] [SerializeField] private Text guildText;

        private void Update()
        {
            if (GameManager.instance == null) return;
            if (GameManager.instance.GetPlayer() == null) return;

            var player = GameManager.instance.GetPlayer();
            usernameText.text = "Name: " + player.username;
            usernameText.color = player.playerColor;
            emailText.text = "Email: " + player.email;
            registerDateText.text = "Date: " + player.registerDate;
            guildText.text = player.guild == null ? "Guild: 无" : "Guild: " + player.guild.guildName;
        }
    }
}