using System;

namespace Project.Common.Core
{
    public class PlayerState : ISitAndStandUpEvents
    {
        public event Action OnSit;
        public event Action OnStandUp;

        public bool IsSitting { get; private set; } = false;

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
    }
}


