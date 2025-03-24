using System;

namespace Project.Common.Core
{
    public interface IObjectChangedEvent<T>
    {
        event Action<T> OnCurrentObjectChanged;
    }
}


