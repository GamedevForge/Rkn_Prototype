using Project.Common.Configs;
using Zenject;

namespace Project.Common.Core
{
    public class OfficeExitController : BusController
    {
        private QuestConfigService _questConfigService;
        
        public override string Name => GetObjectConfig(ObjectType.Door).Name;
        public override bool CanInteract => _questConfigService.GetQuestConfig() == null ||
            _questConfigService.GetQuestConfig().ID == "go_home";

        [Inject] private void GetQuestConfigService(QuestConfigService questConfigService) =>
            _questConfigService = questConfigService;
    }
}