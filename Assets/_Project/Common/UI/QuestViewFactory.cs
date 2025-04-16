using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project.Common.UI
{
    public class QuestViewFactory
    {
        private readonly GameObjectPool _pool;
        private readonly IInstantiator _instantiator;
        private readonly GameObject _questBoardPrefab;
        private readonly GameObject _targetPointerPrefab;

        private Transform _parent;

        public QuestViewFactory(
            GameObject boardPrefab,
            GameObject targetPointerPrefab,
            GameObject questUIElementPrefab,
            IInstantiator instantiator)
        {
            _pool = new(instantiator, questUIElementPrefab);
            _instantiator = instantiator;
            _questBoardPrefab = boardPrefab;
            _targetPointerPrefab = targetPointerPrefab;
        }

        public async UniTask<QuestUIElement> Get(string description, string id)
        {
            GameObject uiElement = _pool.Get();
            QuestUIElement questUIElement = uiElement.GetComponent<QuestUIElement>();
            
            GameObject.DontDestroyOnLoad(uiElement);
            uiElement.transform.SetParent(_parent);

            questUIElement.SetDescription(description, id);
            await questUIElement.PlayOpenAnimationAsync();
            return questUIElement;
        }

        public async UniTask Remove(QuestUIElement questUIElement)
        {
            await questUIElement.PlayCloseAnimationAsync();
            _pool.Release(questUIElement.gameObject);
        }

        public GameObject CreateBoard()
        {
            GameObject board = _instantiator.InstantiatePrefab(_questBoardPrefab);
            _parent = board.GetComponentInChildren<BoardTransform>().Parent;
            board.transform.SetParent(null);
            GameObject.DontDestroyOnLoad(board);
            return board;
        }

        public GameObject CreateTargetPointer()
        {
            GameObject targetPointer = _instantiator.InstantiatePrefab(_targetPointerPrefab);
            targetPointer.transform.SetParent(null);
            GameObject.DontDestroyOnLoad(targetPointer);
            return targetPointer;
        }
    }
}