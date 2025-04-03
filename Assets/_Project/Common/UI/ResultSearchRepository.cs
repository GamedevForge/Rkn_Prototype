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

        public void Clear() => _list.Clear();
    }
}