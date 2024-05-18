using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts.UI
{
    public class InfoUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup infoCanvas;
        [SerializeField] private Text infoText;
        [SerializeField] private float fadeSpeed;

        private void Start() => infoCanvas.alpha = 0;

        public void ShowInfo(string text, Color textColor)
        {
            infoCanvas.alpha = 1;
            infoText.text = text;
            infoText.color = textColor;
        }

        private void Update()
        {
            if (infoCanvas.alpha > 0.05)
                infoCanvas.alpha -= Time.deltaTime * fadeSpeed;
            else
            {
                infoCanvas.alpha = 0;
                infoText.text = string.Empty;
            }
        }
    }
}