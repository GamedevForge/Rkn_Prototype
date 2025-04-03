using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Common.UI
{
    [RequireComponent(typeof(Image))]
    public class SearchButton : 
        MonoBehaviour, 
        IPointerClickHandler,
        IPointerEnterHandler, 
        IPointerExitHandler
    {
        public event Action<string> OnClick;
        
        [SerializeField] private TMP_Text _textMeshPro;
        [SerializeField] private Color _baseColor;
        [SerializeField] private Color _onEnterColor;

        private string _info;

        private Image Image => GetComponent<Image>();

        public void Initialize(string name, string info)
        {
            _textMeshPro.text = name;
            _info = info;
        }

        private void Awake() =>
            Image.color = _baseColor;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(_info);
        }

        public void OnPointerEnter(PointerEventData eventData) =>
            Image.color = _onEnterColor;

        public void OnPointerExit(PointerEventData eventData) =>
            Image.color = _baseColor;
    }
}