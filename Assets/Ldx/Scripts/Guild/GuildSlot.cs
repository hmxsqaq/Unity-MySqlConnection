using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
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
        public Sprite guildSprite;
        public GuildInformation(List<string> info,Sprite sprite)
        {
            guildID = int.Parse(info[0]);
            guildSprite = sprite;
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

        [Title("工会图片")] 
        [SerializeField] private Sprite[] guildSprites;
        
        public void InitSlot(List<string> info,GuildUI controller)// ,Sprite sprite,string name)
        {
            guildInfo = new GuildInformation(info,guildSprites[int.Parse(info[0])]);
                
            guildSprite.sprite = guildInfo.guildSprite;
            guildName.text = guildInfo.guildName;
            
            guildButton.onClick.AddListener(()=>controller.ShowDetail(ref guildInfo,guildInfo.guildSprite));
        }
    }
}
