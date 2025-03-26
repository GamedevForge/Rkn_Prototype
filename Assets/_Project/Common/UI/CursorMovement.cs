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

        private float halfSize => GetComponent<RectTransform>().sizeDelta.x / 2f;

        private void Update()
        {
            _xOffSet += _assetsInputs.look.x * _mouseSensivity;
            _yOffSet += _assetsInputs.look.y * _mouseSensivity;

            transform.localPosition = new Vector3(
                Mathf.Clamp(_xOffSet, -_monitorCanvasRectTransform.sizeDelta.x / 2 + halfSize, _monitorCanvasRectTransform.sizeDelta.x / 2 - halfSize), 
                -Mathf.Clamp(_yOffSet, -_monitorCanvasRectTransform.sizeDelta.y / 2 + halfSize, _monitorCanvasRectTransform.sizeDelta.y / 2 - halfSize), 
                transform.localPosition.z);
        }
        /*
        private void Update()
        {
            transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        */
    }
}
