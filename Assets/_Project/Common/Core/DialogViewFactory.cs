using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using Project.Common.UI;
using System.Collections.Generic;

namespace Project.Common.Core
{
    public class DialogViewFactory
    {
        private readonly GameObjectPool _dialogUIElementPool;
        private readonly GameObjectPool _buttonPool;
        private readonly IInstantiator _instantiator;
        private readonly GameObject _dialogBoard;
        private readonly CanvasRepository _canvasRepository;
        private readonly WidescreenController _widescreenController;

        private readonly List<DialogTextUIElement> uIElementsList = new();

        private Transform _buttonParent;
        private Transform _textUIElementParent;

        public DialogBoardWindow DialogBoardWindow { get; private set; }
        public TMPro.TMP_Text NameText { get; private set; }
        public DialogUIButtonGoToNextTake DialogUIButtonGoToNextTake { get; private set; }
        public DialogUIButtonTextSpeedUpAnimation DialogUIButtonTextSpeedUpAnimation { get; private set; }

        public DialogViewFactory(
            GameObject dialogPrefab,
            GameObject dialogTextUIElement,
            GameObject dialogUIButton,
            CanvasRepository canvasRepository,
            WidescreenController widesceenController,
            IInstantiator instantiator)
        {
            _buttonPool = new(instantiator, dialogUIButton);
            _dialogUIElementPool = new(instantiator, dialogTextUIElement);
            _instantiator = instantiator;
            _dialogBoard = dialogPrefab;
            _canvasRepository = canvasRepository;
            _widescreenController = widesceenController;
        }

        public void CreateBoard()
        {
            GameObject boardGameObject = _instantiator.InstantiatePrefab(_dialogBoard);
            DialogUIComponents dialogUIParents = boardGameObject.GetComponent<DialogUIComponents>();
            DialogBoardWindow = boardGameObject.GetComponentInChildren<DialogBoardWindow>();
            DialogUIButtonGoToNextTake = dialogUIParents.DialogUIButtonGoToNextTake;
            DialogUIButtonTextSpeedUpAnimation = dialogUIParents.DialogUIButtonTextSpeedUpAnimation;

            _canvasRepository.Add(boardGameObject);
            boardGameObject.transform.SetParent(null);
            GameObject.DontDestroyOnLoad(boardGameObject);

            _buttonParent = dialogUIParents.ButtonsParent;
            _textUIElementParent = dialogUIParents.TextParent;
            NameText = dialogUIParents.NameText;
            boardGameObject.SetActive(false);
        }

        public async UniTask ShowBoard()
        {
            await UniTask.WhenAll(
                DialogBoardWindow.PlayShowAnimationAsync(), 
                _widescreenController.PlayShowAnimationAsync());
        }

        public async UniTask CloseBoard()
        {
            await UniTask.WhenAll(
                DialogBoardWindow.PlayHideAnimationAsync(),
                _widescreenController.PlayHideAnimationAsync());
        }

        public OptionallyDialogUIButton GetButton(string text)
        {
            GameObject buttonGameObject = _buttonPool.Get();
            OptionallyDialogUIButton dialogUIButton = buttonGameObject.GetComponent<OptionallyDialogUIButton>();

            GameObject.DontDestroyOnLoad(buttonGameObject);
            buttonGameObject.transform.SetParent(_buttonParent);
            dialogUIButton.SetButtonText(text);
            buttonGameObject.SetActive(false);

            return dialogUIButton;
        }

        public void ReleaseButton(OptionallyDialogUIButton dialogUIButton) =>
            _buttonPool.Release(dialogUIButton.gameObject);

        public async UniTask<DialogTextUIElement> GetTextUIElementAsync(string text)
        {
            DialogTextUIElement dialogTextUIElement = DialogBoardWindow.GetComponentInChildren<DialogTextUIElement>();

            if (dialogTextUIElement != null)
            {
                await dialogTextUIElement.ShowTextAsync(text);
                return dialogTextUIElement;
            }

            if (uIElementsList.Count == 1)
            {
                await uIElementsList[0].ShowTextAsync(text);
                return uIElementsList[0];
            }

            dialogTextUIElement = _dialogUIElementPool.Get().GetComponent<DialogTextUIElement>();
            uIElementsList.Add(dialogTextUIElement);
            GameObject.DontDestroyOnLoad(dialogTextUIElement.gameObject);
            dialogTextUIElement.transform.SetParent(_textUIElementParent);

            await dialogTextUIElement.ShowTextAsync(text);

            return dialogTextUIElement;
        }
    }
}