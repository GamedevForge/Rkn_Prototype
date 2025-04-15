using UnityEngine;

namespace Project.Common.Core.Quest
{
    public class TargetPointerController : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private RectTransform _markerInScreenTransform;
        [SerializeField] private RectTransform _markerOutSideTransform;

        private Camera _camera => Camera.main;
        private float MinX => _camera.ViewportToScreenPoint(new Vector3(0f, 0f, 0f)).x;
        private float MaxX => _camera.ViewportToScreenPoint(new Vector3(1f, 0f, 0f)).x;
        private float MinY => _camera.ViewportToScreenPoint(new Vector3(0f, 0f, 0f)).y;
        private float MaxY => _camera.ViewportToScreenPoint(new Vector3(0f, 1f, 0f)).y;


        private void Update()
        {
            Vector3 screenPointPosition = _camera.WorldToScreenPoint(_target.position);
            Vector2 viewPortPosition = _camera.ScreenToViewportPoint(screenPointPosition);

            Debug.Log(screenPointPosition + " | " + viewPortPosition);

            if (viewPortPosition.x < 0f || viewPortPosition.x > 1f || viewPortPosition.y < 0f || viewPortPosition.y > 1f)
            {
                _markerInScreenTransform.gameObject.SetActive(false);
                _markerOutSideTransform.gameObject.SetActive(true);
                _markerOutSideTransform.position = new Vector2(
                    Mathf.Clamp(screenPointPosition.x, MinX + _markerOutSideTransform.sizeDelta.x / 2, MaxX - _markerOutSideTransform.sizeDelta.x / 2), 
                    Mathf.Clamp(screenPointPosition.y, MinY + _markerOutSideTransform.sizeDelta.y / 2, MaxY - _markerOutSideTransform.sizeDelta.y / 2));
            }
            else
            {
                if (screenPointPosition.z > 0f)
                {
                    _markerInScreenTransform.position = screenPointPosition;
                    _markerInScreenTransform.gameObject.SetActive(true);
                    _markerOutSideTransform.gameObject.SetActive(false);
                }
                else
                {
                    _markerInScreenTransform.gameObject.SetActive(false);
                    _markerOutSideTransform.gameObject.SetActive(true);
                    _markerOutSideTransform.position = new Vector2(
                        (Screen.width - _markerOutSideTransform.sizeDelta.x / 2) * Mathf.Sign(_markerOutSideTransform.position.x),
                        Mathf.Clamp(screenPointPosition.y, MinY + _markerOutSideTransform.sizeDelta.y / 2, MaxY - _markerOutSideTransform.sizeDelta.y / 2));
                }
            }
        }
    }
}
