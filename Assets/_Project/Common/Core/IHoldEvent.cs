using System;

namespace Project.Common.Core
{
    public interface IHoldEvent
    {
        event Action<float, bool> OnHold;
    }
}