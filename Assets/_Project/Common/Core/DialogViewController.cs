using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Project.Common.Core
{
    public class DialogViewController : IInitializable, IDisposable
    {
        public event Action<TakeData[]> OnOptionallyButtonClicked;
        
        private readonly DialogViewFactory _dialogViewFactory;
        private readonly List<OptionallyDialogUIButton> _activeButtons = new();

        private DialogTextUIElement _currentText;
        private TakeData _currentTakeData;

        public bool TextAnimationIsActive { get; private set; } = false;

        public DialogViewController(DialogViewFactory dialogViewFactory)
        {
            _dialogViewFactory = dialogViewFactory;
        }

        public void Initialize()
        {
            _dialogViewFactory.CreateBoard();
            _dialogViewFactory.DialogUIButtonGoToNextTake.OnClicked += SendPlayerInputUntilGoToNextTake;
        }

        public void Dispose()
        {
            ReleaseAllButtons();
            _dialogViewFactory.DialogUIButtonGoToNextTake.OnClicked -= SendPlayerInputUntilGoToNextTake;
        }

        public UniTask ShowDialogBoard() =>
            _dialogViewFactory.ShowBoard();

        public UniTask HideDialogBoard() =>
            _dialogViewFactory.CloseBoard();

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
            _currentText = await _dialogViewFactory.GetTextUIElementAsync(takeData.Text);
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
                _activeButtons.Remove(button);
                button.OnClicked -= SendOptionallyPlayerInput;
                _dialogViewFactory.ReleaseButton(button);
            }
        }

        private async UniTask HideAllButtonsAsync()
        {
            if ( _activeButtons.Count == 0)
                return;
            
            List<UniTask> tasks = new();

            foreach (var button in _activeButtons)
                tasks.Add(button.PlayCloseAnimationAsync());

            await UniTask.WhenAll(tasks);
        }
    }
}