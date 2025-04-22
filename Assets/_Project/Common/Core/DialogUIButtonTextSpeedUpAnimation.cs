using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Project.Common.Core
{
    public class DialogUIButtonTextSpeedUpAnimation : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClicked;
        
        private bool _isActive = false;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isActive) 
                OnClicked?.Invoke();
        }

        public void ActivateButton()
        {
            _isActive = true;
            gameObject.SetActive(_isActive);
        }

        public void DeactivateButton()
        {
            _isActive = false;
            gameObject.SetActive(_isActive);
        }
    }
}