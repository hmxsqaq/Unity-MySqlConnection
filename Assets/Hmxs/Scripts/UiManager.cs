using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Button showTableButton;

        private void Start()
        {
            showTableButton.onClick.AddListener(() => MySqlManager.instance.InspectTable("user"));
        }
    }
}