using System.Collections.Generic;

namespace Project.Common.UI
{
    public class QuestUIElementsRepository
    {
        private readonly List<QuestUIElement> _elements;

        public void Add(QuestUIElement uIElement) =>
            _elements.Add(uIElement);

        public void Remove(QuestUIElement uIElement) =>
            _elements.Remove(uIElement);
        
        public QuestUIElement GetUIElement(string id)
        {
            foreach (var element in _elements)
            {
                if (id == element.CurrentID)
                    return element;
            }
            return null;
        }
    }
}