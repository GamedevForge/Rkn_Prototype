using Project.Common.Core;
using UnityEngine;
using Zenject;

namespace Project.Common.UI
{
    public class ScopeView : MonoBehaviour
    {
        private ISitAndStandUpEvents _playerState;

        [Inject] private void Construct(PlayerState playerState) =>
            _playerState = playerState;

        private void Awake()
        {
            _playerState.OnSitDownAtComputer += Deactivate;
            _playerState.OnStandUpAtComputer += Activate;
        }

        private void OnDestroy()
        {
            _playerState.OnSitDownAtComputer += Deactivate;
            _playerState.OnStandUpAtComputer += Activate;
        }

        private void Activate() =>
            gameObject.SetActive(true);

        private void Deactivate() =>
            gameObject.SetActive(false);
    }
}
