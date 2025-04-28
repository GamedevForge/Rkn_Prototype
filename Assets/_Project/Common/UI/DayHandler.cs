using Project.Common.Core;
using Project.Common.Core.SaveLoadSystem;
using System;
using Zenject;

namespace Project.Common.UI
{
    public class DayHandler : IInitializable
    {
        private readonly IProperty<PlayerSaveData> _playerSaveData;
        
        public event Action<int> OnDayNumberChanged;
        
        public int DayNumber { get; private set; }

        public DayHandler(int day, IProperty<PlayerSaveData> platerSaveData) =>
            _playerSaveData = platerSaveData;

        public void Initialize() =>
            DayNumber = _playerSaveData.Property.Day;

        public void SetDayCount(int dayNumber)
        {  
            DayNumber = dayNumber;
            _playerSaveData.Property.Day = DayNumber;
            OnDayNumberChanged?.Invoke(DayNumber);
        }
    }
}