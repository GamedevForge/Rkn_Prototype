using UnityEngine;
using Cinemachine;

namespace Project.Common.Core
{
    public class SetCameraPosition : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        
        private PlayerComponents _playerComponents;
        
        [Zenject.Inject] private void Construct(PlayerComponents playerComponents) =>
            _playerComponents = playerComponents;

        private void Awake() =>
            _camera.Follow = _playerComponents.CameraTransform;
    }
}