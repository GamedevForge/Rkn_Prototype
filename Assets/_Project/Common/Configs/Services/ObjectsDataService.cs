namespace Project.Common.Configs
{
    public class ObjectsDataService
    {
        private readonly ObjectsData _objectsData;

        public ObjectsDataService(ObjectsData objectsData) =>
            _objectsData = objectsData;

        public ObjectConfig GetObjectConfig(Core.ObjectType objectType) =>
            _objectsData.Configs[objectType];
    }
}
