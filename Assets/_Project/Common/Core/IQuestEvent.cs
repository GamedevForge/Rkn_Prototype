using System;

namespace Project.Common.Core
{
    public interface IQuestEvent<T>
    {
        event Action OnEvent;
        T MarkerTarget {  get; }
    }
}