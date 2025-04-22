using Project.Common.Core;
using UnityEngine;
using Zenject;

namespace Project.Common.UI
{
    public class ScopeView : MonoBehaviour
    {
        private ISitAndStandUpEvents _playerState;
        private CanvasRepository _canvasRepository;

        [Inject] private void Construct(PlayerState playerState, CanvasRepository canvasRepository)
        {
            _playerState = playerState;
            _canvasRepository = canvasRepository;
        }

        private void Awake()
        {
            _playerState.OnSitDownAtComputer += Deactivate;
            _playerState.OnStandUpAtComputer += Activate;
            _canvasRepository.Add(gameObject);
        }

        private void OnDestroy()
        {
            _playerState.OnSitDownAtComputer += Deactivate;
            _playerState.OnStandUpAtComputer += Activate;
            _canvasRepository.Remove(gameObject);
        }

        private void Activate() =>
            gameObject.SetActive(true);

        private void Deactivate() =>
            gameObject.SetActive(false);
    }
}
