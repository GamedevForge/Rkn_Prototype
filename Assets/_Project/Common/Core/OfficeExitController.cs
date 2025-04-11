namespace Project.Common.Core
{
    public class OfficeExitController : BusController
    {
        public override string Name => GetObjectConfig(ObjectType.Door).Name;
    }
}