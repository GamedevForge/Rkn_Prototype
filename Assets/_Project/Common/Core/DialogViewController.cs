using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using Project.Common.UI;
using UnityEngine;

namespace Project.Common.Core
{
    public class DialogViewController : IInitializable, IDisposable
    {
        public event Action<TakeData[]> OnOptionallyButtonClicked;
        
        private readonly DialogViewFactory _dialogViewFactory;
        private readonly List<OptionallyDialogUIButton> _activeButtons = new();
        private readonly CanvasRepository _canvasRepository;
        private readonly IProperty<WidescreenAnimation, GameObject> _widescreenAnimation;

        private DialogTextUIElement _currentText;
        private TakeData _currentTakeData;

        public bool TextAnimationIsActive { get; private set; } = false;

        public DialogViewController(
            DialogViewFactory dialogViewFactory,
            CanvasRepository canvasRepository,
            IProperty<WidescreenAnimation, GameObject> widescreenAnimation)
        {
            _dialogViewFactory = dialogViewFactory;
            _canvasRepository = canvasRepository;
            _widescreenAnimation = widescreenAnimation;
        }

        public void Initialize()
        {
            _dialogViewFactory.CreateBoard();
            _dialogViewFactory.DialogUIButtonTextSpeedUpAnimation.DeactivateButton();
            _dialogViewFactory.DialogUIButtonGoToNextTake.OnClicked += SendPlayerInputUntilGoToNextTake;
            _dialogViewFactory.DialogUIButtonTextSpeedUpAnimation.OnClicked += SpeedUpAnimation;
        }

        public void Dispose()
        {
            ReleaseAllButtons();
            _dialogViewFactory.DialogUIButtonGoToNextTake.OnClicked -= SendPlayerInputUntilGoToNextTake;
            _dialogViewFactory.DialogUIButtonTextSpeedUpAnimation.OnClicked -= SpeedUpAnimation;
        }

        public async UniTask ShowDialogBoard(string name)
        {
            _dialogViewFactory.DialogUIButtonGoToNextTake.ActivateButton();
            _canvasRepository.SetStateAllCanvasWithout(false, _dialogViewFactory.DialogBoardWindow.gameObject, _widescreenAnimation.Property1);
            _dialogViewFactory.NameText.text = name + ":";
            await _dialogViewFactory.ShowBoard();
        }

        public async UniTask HideDialogBoard()
        {
            await _dialogViewFactory.DialogUIButtonGoToNextTake.PlayCloseAnimationAsync();
            await _dialogViewFactory.CloseBoard();
            _dialogViewFactory.GetTextUIElementAsync(string.Empty).Forget();
            _canvasRepository.SetStateAllCanvasWithout(true, _dialogViewFactory.DialogBoardWindow.gameObject, _widescreenAnimation.Property1);
        }

        public void ShowGoToNextTakeButton() =>
            _dialogViewFactory.DialogUIButtonGoToNextTake.gameObject.SetActive(true);

        public void HideGoToNextTakeButton() =>
            _dialogViewFactory.DialogUIButtonGoToNextTake.gameObject.SetActive(false);

        public async UniTask ShowNextUIElement(TakeData takeData, string name)
        {
            await HideAllButtonsAsync();
            ReleaseAllButtons();

            _dialogViewFactory.NameText.text = name + ":";

            _currentTakeData = takeData;
            TextAnimationIsActive = true;
            _dialogViewFactory.DialogUIButtonTextSpeedUpAnimation.ActivateButton();
            _currentText = await _dialogViewFactory.GetTextUIElementAsync(takeData.Text);
            _dialogViewFactory.DialogUIButtonTextSpeedUpAnimation.DeactivateButton();
            TextAnimationIsActive = false;

            if (takeData.Type == TakeType.Optionally)
            {
                List<UniTask> tasks = new();
                
                foreach(string key in takeData.DialogOptionsAfterPlayersAnswer.Keys)
                {
                    OptionallyDialogUIButton button = _dialogViewFactory.GetButton(key);
                    button.OnClicked += SendOptionallyPlayerInput;
                    _activeButtons.Add(button);
                    tasks.Add(button.PlayShowAnimationAsync());
                }

                await UniTask.WhenAll(tasks);
            }
        }

        public void SpeedUpAnimation()
        {
            if (_currentText != null) 
                _currentText.SpeedUpAnimation().Forget();
        }

        private void SendOptionallyPlayerInput(string key) =>
            OnOptionallyButtonClicked?.Invoke(_currentTakeData.DialogOptionsAfterPlayersAnswer[key]);

        private void SendPlayerInputUntilGoToNextTake() =>
            OnOptionallyButtonClicked?.Invoke(null);

        private void ReleaseAllButtons()
        {
            if (_activeButtons.Count == 0)
                return;

            foreach (var button in _activeButtons)
            {
                button.OnClicked -= SendOptionallyPlayerInput;
                _dialogViewFactory.ReleaseButton(button);
            }
            _activeButtons.Clear();
        }

        private async UniTask HideAllButtonsAsync()
        {
            if ( _activeButtons.Count == 0)
                return;
            
            List<UniTask> tasks = new();

            foreach (var button in _activeButtons)
                tasks.Add(button.PlayHideAnimationAsync());

            await UniTask.WhenAll(tasks);
        }
    }
}