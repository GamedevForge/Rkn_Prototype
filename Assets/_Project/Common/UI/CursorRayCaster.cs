using StarterAssets;
using UnityEngine;

namespace Project.Common.UI
{
    public class CursorRayCaster : MonoBehaviour
    {
        [SerializeField] private StarterAssetsInputs _assetsInputs;

        private void Awake()
        {
            _assetsInputs.OnClick += OnLeftClick;
        }

        private void OnDestroy()
        {
            _assetsInputs.OnClick -= OnLeftClick;          
        }

        public void OnLeftClick()
        {
            Debug.Log("Click");
        }
    }
}
