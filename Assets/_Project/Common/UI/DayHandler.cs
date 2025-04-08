using System;

namespace Project.Common.UI
{
    public class DayHandler
    {
        public event Action<int> OnDayNumberChanged;
        
        public int DayNumber { get; private set; }

        public DayHandler(int day)
        {
            DayNumber = day;
        }

        public void SetDayCount(int dayNumber)
        {
            DayNumber = dayNumber;
            OnDayNumberChanged?.Invoke(DayNumber);
        }
    }
}