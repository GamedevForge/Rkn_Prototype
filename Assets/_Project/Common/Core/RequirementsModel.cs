using Zenject;
using Project.Common.Configs;
using Project.Common.UI;

namespace Project.Common.Core
{
    public class RequirementsModel
    {
        private RequirementsDataService _dataService;
        private DayHandler _dayHandler;

        public RequirementsConfig CurrentConfig => _dataService.GetRequirementsConfig(_dayHandler.DayNumber);

        [Inject] private void Construct(
            RequirementsDataService dataService, 
            DayHandler dayHandler)
        {
            _dataService = dataService;
            _dayHandler = dayHandler;
        }
    }
}


