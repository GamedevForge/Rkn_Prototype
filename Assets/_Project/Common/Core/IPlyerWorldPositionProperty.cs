using System;
using UnityEngine;

namespace Project.Common.Core
{
    public interface IPlyerWorldPositionProperty
    {
        event Action OnTransformDisable;
        Vector3 PlayerPosition { get; }
        bool CurrentPlayerTransformIsNotNull { get; }
    }
}