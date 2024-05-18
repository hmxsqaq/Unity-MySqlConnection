using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Ldx.Scripts.Guild
{
    [Serializable]
    public struct GuildInformation
    {
        public int guildID;
        public string guildName;
        public string establishedDate;
        public int guildMemberNum;
        public Color color;

        public GuildInformation(List<string> info)
        {
            guildID = int.Parse(info[0]);
            color = info[0] switch
            {
                "1" => Color.red,
                "2" => Color.blue,
                "3" => Color.cyan,
                "4" => Color.yellow,
                "5" => Color.magenta,
                _ => Color.white
            };
            guildName = info[1];
            establishedDate = info[2];
            guildMemberNum = int.Parse(info[3]);
        }
    }

    public class GuildSlot : MonoBehaviour
    {
        [Title("交互控件")]
        [SerializeField]private Image guildSprite;
        [SerializeField]private Text guildName;
        [SerializeField]private Button guildButton;

        [Title("工会信息")]
        [SerializeField] private GuildInformation guildInfo;
        
        public void InitSlot(List<string> info,GuildUI controller)// ,Sprite sprite,string name)
        {
            guildInfo = new GuildInformation(info);
                
            guildSprite.color = guildInfo.color;
            guildName.text = guildInfo.guildName;
            
            guildButton.onClick.AddListener(()=>controller.ShowDetail(ref guildInfo));
        }
    }
}
