using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class HoldableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnClick;
        public event Action OnHold;

        private bool _isPressed;
        private bool _canPress;
        private bool _isHoldEventInvoked;
        private float _holdTime;

        private Button _button;
        private Button Button => _button ??= GetComponent<Button>();

        private void Update()
        {
            if(!_canPress)
                return;
            
            if (_isPressed)
                _holdTime += Time.deltaTime;

            if (_holdTime > 0.35f && !_isHoldEventInvoked)
            {
                OnHold?.Invoke();
                _isHoldEventInvoked = true;
                _canPress = true;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(!_canPress)
                return;

            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(!_canPress)
                return;
            
            _isPressed = false;
            
            if(_holdTime < 0.35f)
                OnClick?.Invoke();
            
            _holdTime = 0f;
            _isHoldEventInvoked = false;
        }

        public void SetInteractable(bool interactable)
        {
            _canPress = interactable;
            Button.interactable = interactable;
        }
    }
}