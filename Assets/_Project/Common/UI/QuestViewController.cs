using Cysharp.Threading.Tasks;
using Project.Common.Configs;
using Project.Common.Core.Quest;
using UnityEngine;
using Zenject;

namespace Project.Common.UI
{
    public class QuestViewController : IInitializable
    {
        private readonly QuestViewFactory _factory;
        private readonly QuestUIElementsRepository _repository = new();

        private TargetPointerController _targetPointerController;

        public QuestViewController(QuestViewFactory factory) =>
            _factory = factory;

        public void Initialize()
        {
            _factory.CreateBoard();
            _targetPointerController = _factory
                .CreateTargetPointer()
                .GetComponentInChildren<TargetPointerController>();
        }

        public async UniTask ShowQuest(QuestConfig questConfig, Transform targetForPointer = null)
        {
            if (targetForPointer != null)
                _targetPointerController.SetTarget(targetForPointer, questConfig.QuestDescription);
            QuestUIElement questUIElement = await _factory.Get(questConfig.QuestDescription, questConfig.ID);
            _repository.Add(questUIElement);
        }

        public async UniTask RemoveQuest(QuestConfig questConfig)
        {
            QuestUIElement uIElement = _repository.GetUIElement(questConfig.ID);

            if (questConfig.Type == QuestType.WithTarget)
                _targetPointerController.SetTarget(null, null);

            await _factory.Remove(uIElement);
            _repository.Remove(uIElement);
        }

        public void DisableTargetPointer() =>
            _targetPointerController.DeactivateTargetPointer();

        public void EnableTargetPointer() =>
            _targetPointerController.ActivateTargetPointer();
    }
}