using System;

namespace Project.Common.Core
{
    public class PlayerState : ISitAndStandUpEvents
    {
        public event Action OnSit;
        public event Action OnStandUp;
        public event Action OnLookedEnable;

        public bool IsSitting { get; private set; } = false;
        public bool IsProcessing { get; private set; } = false;

        public void Sit()
        {
            IsSitting = true;
            OnSit?.Invoke();
        }

        public void StandUp()
        {
            IsSitting = false;
            OnStandUp?.Invoke();
        }

        public void EnableLooked() =>
            OnLookedEnable?.Invoke();

        public void EnableProcessing() =>
            IsProcessing = true;

        public void DisableProcessing() => 
            IsProcessing = false;
    }
}


