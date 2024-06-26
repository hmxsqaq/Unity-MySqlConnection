﻿using UnityEngine;
using UnityEngine.UI;

namespace Ldx.Scripts.Guild
{
    public class GuildInfo : MonoBehaviour
    {
        [SerializeField] private Image m_Image;
        [SerializeField] private Text guildName;
        [SerializeField] private Text guildId;
        [SerializeField] private Text guildPopulation;
        [SerializeField] private Text guildDetail;

        public void Init(GuildInformation information,Sprite sprite)
        {
            m_Image.sprite = sprite;
            guildId.text = "工会ID：" + information.guildID;
            guildName.text = "工会名称："+information.guildName;
            guildPopulation.text = "工会人数："+information.guildMemberNum;
            
            guildDetail.text = "描述：无";
        }
    }
}