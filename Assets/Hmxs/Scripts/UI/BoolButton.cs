using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class BoolButton : MonoBehaviour
    {
        [Required] [SerializeField] private Sprite trueSprite;
        [Required] [SerializeField] private Sprite falseSprite;
        [Required] [SerializeField] private GameObject window;
        [Required] [SerializeField] private Button button;

        public Button GetButton() => button;
        public GameObject GetWindow() => window;

        private void Start()
        {
            button = GetComponent<Button>();

            button.onClick.AddListener(() =>
            {
                button.interactable = false;
                button.image.sprite = falseSprite;
                window.SetActive(true);
            });
        }

        public void SetButton(bool value)
        {
            button.interactable = value;
            button.image.sprite = value ? trueSprite : falseSprite;
            window.SetActive(!value);
        }
    }
}