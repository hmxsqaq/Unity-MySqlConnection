using System.Collections.Generic;
using System.Linq;
using Hmxs.Scripts.Game;
using Hmxs.Scripts.MySQL;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ldx.Scripts.Guild
{
    public class GuildUI : MonoBehaviour
    {
        [Title("工会信息预制体")] 
        [SerializeField] private Transform guildSlotParent;
        [SerializeField] private GameObject guildSlot;
        [SerializeField] private GuildInfo guildDetail;
        
        private List<GuildSlot> m_GuildsInfo = new();

        //查询工会所有信息以及工会人数
        private string query = 
            "select g.guildid,g.guildname,g.establisheddate,count(p.playerid) from guild g,player p where g.guildid = p.guildid group by p.guildid;";
        
        private void OnEnable()
        {
            InitialGuild();
        }

        public void InitialGuild()
        {
            List<List<string>> res1 = MySqlHelper.ExecuteQueryList(query);
            foreach (var line in res1)
            {
                var obj = Instantiate(guildSlot,guildSlotParent) as GameObject;
                var slot = obj.GetComponent<GuildSlot>();
                if (slot)
                {
                    slot.InitSlot(line,this);
                    m_GuildsInfo.Add(slot);
                }
            }

            if (GameManager.instance.GetPlayer().guild == null) return;
            var guildId = GameManager.instance.GetPlayer().guild.guildID;
            foreach (var slot in m_GuildsInfo.Where(slot => slot.guildInfo.guildID == guildId))
                ShowDetail(ref slot.guildInfo, slot.guildInfo.guildSprite);
        }
        
        public void ShowDetail(ref GuildInformation information,Sprite sprite)
        {
            guildDetail.gameObject.SetActive(true);
            guildDetail.Init(information,sprite);
        }
    }
}
