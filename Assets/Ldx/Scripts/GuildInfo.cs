using UnityEngine;
using UnityEngine.UI;

namespace Ldx.Scripts
{
    public class GuildInfo : MonoBehaviour
    {
        [SerializeField] private Image m_Image;
        [SerializeField] private Text guildName;
        [SerializeField] private Text guildId;
        [SerializeField] private Text guildPopulation;
        [SerializeField] private Text guildDetail;

        public void Init(GuildInformation information)
        {
            m_Image.color = information.color;
            guildId.text = "工会ID：" + information.guildID;
            guildName.text = "工会名称："+information.guildName;
            guildPopulation.text = "工会人数："+information.guildMemberNum;
            
            guildDetail.text = "描述：无";
        }
    }
}