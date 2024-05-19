using System;
using System.Collections.Generic;
using Hmxs.Scripts.Game;
using Ldx.Scripts.Guild;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Ldx.Scripts.Character
{
    [Serializable]
    public struct CharacterInformation
    {
    }
    
    public class CharacterSlot : MonoBehaviour
    {
        [Title("交互控件")]
        [SerializeField]private Image characterSprite;
        [SerializeField]private Text characterName;
        [SerializeField]private Button characterButton;

        [Title("工会信息")]
        [SerializeField] private PlayerCharacter characterInfo;
            
        public void InitSlot(PlayerCharacter character,Sprite sprite,CharacterUI controller)
        {
            characterInfo = character;
            characterSprite.sprite = sprite;
            characterName.text = character.character.characterName;
            
            characterButton.onClick.AddListener(()=>controller.ShowDetail(ref characterInfo,sprite));
        }
    }
}
