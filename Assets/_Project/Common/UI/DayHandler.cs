using Zenject;

namespace Project.Common.UI
{
    public class DayHandler
    { 
        public readonly int DayNumber;

        public DayHandler(
        [InjectOptional]
        int day)
        {
            DayNumber = day;
        }
    }
}