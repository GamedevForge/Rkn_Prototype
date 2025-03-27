using Sirenix.OdinInspector.Editor;
using StarterAssets;
using UnityEngine;

namespace Project.Common.UI
{
    public class CursorMovement : MonoBehaviour
    {
        [SerializeField] private StarterAssetsInputs _assetsInputs;
        [SerializeField] private RectTransform _monitorCanvasRectTransform;
        [SerializeField] private float _mouseSensivity;

        private float _xOffSet = 0f;
        private float _yOffSet = 0f;

        private float HalfSize => GetComponent<RectTransform>().sizeDelta.x / 2f;

        private void Update()
        {
            _xOffSet = Mathf.Clamp(_xOffSet + _assetsInputs.look.x * _mouseSensivity, 
                -_monitorCanvasRectTransform.sizeDelta.x / 2 + HalfSize, 
                _monitorCanvasRectTransform.sizeDelta.x / 2 - HalfSize);
            
            _yOffSet = Mathf.Clamp(_yOffSet + _assetsInputs.look.y * _mouseSensivity, 
                -_monitorCanvasRectTransform.sizeDelta.y / 2 + HalfSize, 
                _monitorCanvasRectTransform.sizeDelta.y / 2 - HalfSize);

            transform.localPosition = new Vector3(
                _xOffSet, 
                -_yOffSet, 
                transform.localPosition.z);
        }
    }
}
