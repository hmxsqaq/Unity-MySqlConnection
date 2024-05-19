using Hmxs.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Ldx.Scripts.Character
{
    public class CharacterInfo : MonoBehaviour
    {
        [SerializeField] private Image m_Image;
        [SerializeField] private Text characterName;
        [SerializeField] private Text characterId;
        [SerializeField] private Text characterAttribute;
        [SerializeField] private Text characterLevel;
        [SerializeField] private Text characterExperience;
        [SerializeField] private Text characterMaxHealth;

        public void Init(PlayerCharacter information,Sprite sprite)
        {
            m_Image.sprite = sprite;
            characterId.text = "角色ID：" + information.character.characterID;
            characterName.text = "角色名称："+information.character.characterName;
            characterAttribute.text = "角色属性：" + information.character.characterAttribute;
            characterLevel.text = "角色等级：" + information.characterLevel;
            characterExperience.text = "角色经验值：" + information.experiencePoints;
            characterMaxHealth.text = "角色最大生命值：" + information.maxHealth;
        }
    }
}