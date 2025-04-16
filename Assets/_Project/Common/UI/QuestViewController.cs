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

        public void ShowQuest(QuestConfig questConfig, Transform targetForPointer = null)
        {
            _repository.Add(_factory.Get(questConfig.QuestDescription, questConfig.ID));
            if (targetForPointer != null)
                _targetPointerController.SetTarget(targetForPointer, questConfig.QuestDescription);
        }

        public void RemoveQuest(QuestConfig questConfig)
        {
            QuestUIElement uIElement = _repository.GetUIElement(questConfig.ID);

            if (questConfig.Type == QuestType.WithTarget)
                _targetPointerController.SetTarget(null, null);

            _factory.Remove(uIElement);
            _repository.Remove(uIElement);
        }
    }
}