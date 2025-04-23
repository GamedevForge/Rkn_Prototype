using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Project.Common.Core
{
    public abstract class BaseSignalReceiver<TData> : SerializedMonoBehaviour
    {
        [field: SerializeField] protected string SignalIDs { get; private set; }
        [field: SerializeField] protected ISignalMethod<TData> Signal { get; private set; }

        public abstract void SignalMethod(TData data);
    }
}