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

        public QuestUIElement Get(string description, string id)
        {
            GameObject uiElement = _pool.Get();
            QuestUIElement questUIElement = uiElement.GetComponent<QuestUIElement>();
            
            GameObject.DontDestroyOnLoad(uiElement);
            uiElement.transform.SetParent(_parent);

            questUIElement.SetDescription(description, id);
            questUIElement.PlayOpenAnimationAsync().Forget();
            return questUIElement;
        }

        public void Remove(QuestUIElement questUIElement)
        {
            questUIElement.PlayCloseAnimationAsync().Forget();
            _pool.Release(questUIElement.gameObject);
        }

        public GameObject CreateBoard()
        {
            GameObject board = _instantiator.InstantiatePrefab(_questBoardPrefab);
            _parent = board.transform;
            GameObject.DontDestroyOnLoad(board);
            return board;
        }

        public GameObject CreateTargetPointer()
        {
            GameObject targetPointer = _instantiator.InstantiatePrefab(_targetPointerPrefab);
            GameObject.DontDestroyOnLoad(targetPointer);
            return targetPointer;
        }
    }
}