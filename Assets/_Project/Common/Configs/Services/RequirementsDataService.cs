namespace Project.Common.Configs
{
    public class RequirementsDataService
    {
        private readonly RequirementsData _requirementsData;

        public RequirementsDataService(RequirementsData requirementsData) =>
            _requirementsData = requirementsData;
        
        public RequirementsConfig GetRequirementsConfig(int dayNumber)
        {
            if (dayNumber < _requirementsData.RequirementsConfigs.Length)
                return _requirementsData.RequirementsConfigs[dayNumber];
            else
                return _requirementsData.RequirementsConfigs[UnityEngine.Random.Range(0, _requirementsData.RequirementsConfigs.Length)];
        }
    }
}
