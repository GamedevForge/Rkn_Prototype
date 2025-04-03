using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Common.UI
{
    [RequireComponent(typeof(Image))]
    public class SearchButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action<string> OnClick;
        
        [SerializeField] private TMP_Text _textMeshPro;

        private string _info;

        public void Initialize(string name, string info)
        {
            _textMeshPro.text = name;
            _info = info;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(_info);
        }
    }
}