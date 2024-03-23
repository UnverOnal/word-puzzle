using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class HoldableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnClick;
        public event Action OnHold;

        private bool _isPressed;
        private bool _isHoldEventInvoked;
        private float _holdTime;

        private void Update()
        {
            if (_isPressed)
                _holdTime += Time.deltaTime;

            if (_holdTime > 0.35f && !_isHoldEventInvoked)
            {
                OnHold?.Invoke();
                _isHoldEventInvoked = true;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
            
            if(_holdTime < 0.35f)
                OnClick?.Invoke();
            
            _holdTime = 0f;
            _isHoldEventInvoked = false;
        }
    }
}