using System.Collections.Generic;
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
        
        private void Start()
        {
            InitialGuild();
        }

        public void InitialGuild()
        {
            List<List<string>> res = MySqlHelper.ExecuteQueryList(query);
            foreach (var line in res)
            {
                var obj = Instantiate(guildSlot,guildSlotParent) as GameObject;
                var slot = obj.GetComponent<GuildSlot>();
                if (slot)
                {
                    slot.InitSlot(line,this);
                }
            }
            
            guildDetail.gameObject.SetActive(false);
        }
        
        public void ShowDetail(ref GuildInformation information,Sprite sprite)
        {
            guildDetail.gameObject.SetActive(true);
            guildDetail.Init(information,sprite);
        }
    }
}
