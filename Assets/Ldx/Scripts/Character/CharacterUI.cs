using System.Collections.Generic;
using Hmxs.Scripts.Game;
using Hmxs.Scripts.MySQL;
using Ldx.Scripts.Guild;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ldx.Scripts.Character
{
    public class CharacterUI : MonoBehaviour
    {
        [Title("工会信息预制体")] 
        [SerializeField] private Transform characterSlotParent;
        [SerializeField] private GameObject characterSlot;
        [SerializeField] private CharacterInfo characterDetail;

        [SerializeField] private Sprite[] characterSprites;
        
        private void Start()
        {
            InitialCharacters();
        }

        private void InitialCharacters()
        {
            var characters = GameManager.instance.GetPlayer().playerCharacters;
            
            foreach (var character in characters)
            {
                var obj = Instantiate(characterSlot,characterSlotParent) as GameObject;
                var slot = obj.GetComponent<CharacterSlot>();
                if (slot)
                {
                    Sprite sp = characterSprites[character.character.characterID-1];
                    slot.InitSlot(character,sp,this);
                }
            }
            
            characterDetail.gameObject.SetActive(false);
        }
        
        public void ShowDetail(ref PlayerCharacter information,Sprite sprite)
        {
            characterDetail.gameObject.SetActive(true);
            characterDetail.Init(information,sprite);
        }
    }
}