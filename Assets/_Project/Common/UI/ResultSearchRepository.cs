using System.Collections.Generic;

namespace Project.Common.UI
{
    public class ResultSearchRepository
    {
        public IEnumerable<SearchButton> Objects => _list;

        private readonly List<SearchButton> _list = new();

        public void Add(SearchButton searchButton) =>
            _list.Add(searchButton);

        public void Remove(SearchButton searchButton) =>
            _list.Remove(searchButton);

        public void EnableAllSearchButtons()
        {
            foreach (var button in _list)
                button.gameObject.SetActive(true);
        }

        public void DisableAllSearchButtons()
        {
            foreach (var button in _list)
                button.gameObject.SetActive(false);
        }

        public void Clear() => _list.Clear();
    }
}