using UnityEngine;
using UnityEngine.EventSystems;

namespace Hmxs.Scripts.UI
{
    public class JuicyUI : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
    {
        [SerializeField] private float maxScale = 1.1f;
        [SerializeField] private float minScale = 0.9f;
        [SerializeField] [Range(0,1)] private float scalingSpeed = 0.15f;

        private float _target = 1;
        private float _currentScale = 1;
        private Vector3 _originalScale;

        private void Start() => _originalScale = transform.localScale;

        private void Update()
        {
            if (_currentScale.Equals(_target)) return;
            _currentScale = Mathf.Lerp(_currentScale, _target, scalingSpeed * Time.deltaTime * 50);
            transform.localScale = _originalScale * _currentScale;
        }

        public void OnPointerEnter(PointerEventData eventData) => _target = maxScale;
        public void OnPointerDown(PointerEventData eventData) => _target = minScale;

        public void OnPointerExit(PointerEventData eventData) => _target = 1;

        public void OnPointerUp(PointerEventData eventData) => _target = maxScale;
    }
}