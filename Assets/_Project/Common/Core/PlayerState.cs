using System;

namespace Project.Common.Core
{
    public class PlayerState : ISitAndStandUpEvents
    {
        public event Action OnSit;
        public event Action OnStandUp;
        public event Action OnLookedEnable;
        public event Action OnLookedDisable;
        public event Action OnSitDownAtComputer;
        public event Action OnStandUpAtComputer;

        public bool IsSitting { get; private set; } = false;
        public bool IsProcessing { get; private set; } = false;
        public bool InComputer { get; private set; } = false;

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

        public void DisableLooked() =>
            OnLookedDisable?.Invoke();

        public void EnableProcessing() =>
            IsProcessing = true;

        public void DisableProcessing() => 
            IsProcessing = false;

        public void SitDownAtComputer()
        {
            InComputer = true;
            OnSitDownAtComputer?.Invoke();
        }

        public void StandUpAtComputer()
        {
            InComputer = false;
            OnStandUpAtComputer?.Invoke();
        }
    }
}