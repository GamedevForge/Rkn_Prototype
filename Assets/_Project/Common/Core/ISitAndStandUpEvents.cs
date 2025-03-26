using System;

namespace Project.Common.Core
{
    public interface ISitAndStandUpEvents
    {
        event Action OnSit;
        event Action OnStandUp;
        event Action OnLookedEnable;
        event Action OnLookedDisable;
        event Action OnSitDownAtComputer;
        event Action OnStandUpAtComputer;
    }
}


