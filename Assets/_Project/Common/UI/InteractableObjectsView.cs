using TMPro;
using UnityEngine;

namespace Project.Common.UI
{
    [RequireComponent (typeof(TMP_Text))]
    public class InteractableObjectsView : MonoBehaviour
    {
        [SerializeField] private GameObject _sliderGameObject;
        [SerializeField] private UnityEngine.UI.Image _sliderImage;
        
        private TMP_Text Text => GetComponent<TMP_Text>();

        private void Awake()
        {
            _sliderImage.type = UnityEngine.UI.Image.Type.Filled;
            _sliderGameObject.SetActive(false);
        }

        public void DrawText(string text) =>
            Text.text = text;

        public void DrawSlider(float value) =>
            _sliderImage.fillAmount = value;

        public void SetSliderActive(bool active) =>
            _sliderGameObject.SetActive(active);
    }
}
