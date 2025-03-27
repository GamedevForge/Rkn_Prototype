using StarterAssets;
using UnityEngine;
using Zenject;

namespace Project.Common.Core
{
    public class MouseMovement : MonoBehaviour
    {
        [SerializeField] private StarterAssetsInputs _assetsInputs;
        [SerializeField] private MeshRenderer _mousePadRenderer;
        [SerializeField] private Transform _mousePadTransform;
        [SerializeField] private float _speed;

        private PlayerState _playerState;

        private float _xOffSet = 0f;
        private float _yOffSet = 0f;

        private Vector3 MousePadSize => _mousePadRenderer.bounds.size;
        private Vector3 MousePadPosition => _mousePadTransform.localPosition; 

        [Inject] private void Construct(PlayerState playerState) =>
            _playerState = playerState;

        private void Update()
        {
            if (_playerState.InComputer == false)
                return;

            _xOffSet = Mathf.Clamp(_xOffSet + _assetsInputs.look.x * _speed,
                -MousePadSize.x / 2 + MousePadPosition.x,
                MousePadSize.x / 2 + MousePadPosition.x);

            _yOffSet = Mathf.Clamp(_yOffSet + _assetsInputs.look.y * _speed,
                -MousePadSize.z / 2 + MousePadPosition.z,
                MousePadSize.z / 2 - MousePadPosition.z);

            transform.localPosition = new Vector3(
                _xOffSet,
                transform.localPosition.y,
                -_yOffSet);
        }
    }
}
