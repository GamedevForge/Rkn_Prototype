using Cysharp.Threading.Tasks;
using StarterAssets;
using UnityEngine;
using DG.Tweening;

namespace Project.Common.Core
{ 
    public class PlayerQuitController : MonoBehaviour
    {
        [SerializeField] private Vector3 _offSet;
        [SerializeField] private float _duration;
        
        private PlayerState _playerState;
        private FirstPersonController _firstPersonController;

        public void Initialize(PlayerState playerState, 
            FirstPersonController firstPersonController)
        {
            _playerState = playerState;
            _firstPersonController = firstPersonController;
        }
    
        public async void OnQuit()
        {
            if (_playerState.IsSitting && _playerState.IsProcessing == false && _playerState.InComputer)
            {
                _playerState.StandUpAtComputer();
            }        
            else if (_playerState.IsSitting && _playerState.IsProcessing == false)
            {
                _playerState.EnableProcessing();
                _firstPersonController.DisableLooked();

                await PlayQuitAnimationAsync();

                _playerState.StandUp();
                _playerState.DisableProcessing();
                _firstPersonController.EnableLooked();
            }
        }

        private async UniTask PlayQuitAnimationAsync()
        {
            Tween tween = gameObject.transform.DOMove(transform.position + _offSet, _duration);
            await tween.AsyncWaitForCompletion();
        }
    }
}
