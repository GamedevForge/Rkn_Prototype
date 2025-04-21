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

        private readonly List<DialogTextUIElement> uIElementsList = new();

        private Transform _buttonParent;
        private Transform _textUIElementParent;
        private DialogBoardWindow _dialogBoardWindow;

        public TMPro.TMP_Text NameText { get; private set; }
        public DialogUIButtonGoToNextTake DialogUIButtonGoToNextTake { get; private set; }

        public DialogViewFactory(
            GameObject dialogPrefab,
            GameObject dialogTextUIElement,
            GameObject dialogUIButton,
            IInstantiator instantiator)
        {
            _buttonPool = new(instantiator, dialogUIButton);
            _dialogUIElementPool = new(instantiator, dialogTextUIElement);
            _instantiator = instantiator;
            _dialogBoard = dialogPrefab;
        }

        public void CreateBoard()
        {
            GameObject boardGameObject = _instantiator.InstantiatePrefab(_dialogBoard);
            DialogUIComponents dialogUIParents = boardGameObject.GetComponent<DialogUIComponents>();
            DialogUIButtonGoToNextTake = boardGameObject.GetComponentInChildren<DialogUIButtonGoToNextTake>();
            _dialogBoardWindow = boardGameObject.GetComponentInChildren<DialogBoardWindow>();

            boardGameObject.transform.SetParent(null);
            GameObject.DontDestroyOnLoad(boardGameObject);

            _buttonParent = dialogUIParents.ButtonsParent;
            _textUIElementParent = dialogUIParents.TextParent;
            NameText = dialogUIParents.NameText;
            boardGameObject.SetActive(false);
        }

        public UniTask ShowBoard() =>
            _dialogBoardWindow.PlayShowAnimationAsync();

        public UniTask CloseBoard() =>
            _dialogBoardWindow.PlayCloseAnimationAsync();

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
            if (uIElementsList.Count == 1)
            {
                await uIElementsList[0].ShowTextAsync(text);
                return uIElementsList[0];
            }
            
            DialogTextUIElement uIElement = _dialogUIElementPool.Get().GetComponent<DialogTextUIElement>();
            uIElementsList.Add(uIElement);
            GameObject.DontDestroyOnLoad(uIElement.gameObject);
            uIElement.transform.SetParent(_textUIElementParent);

            await uIElement.ShowTextAsync(text);

            return uIElement;
        }
    }
}