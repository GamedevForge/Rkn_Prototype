using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Project.Common.Core
{
    public class NPCAnimatorController : SerializedMonoBehaviour, ISignalMethod<DialogSignal>
    {
        public delegate void AnimationMethod();

        private const string IDLE = "Idle";
        private const string SHAKE_FIT = "ShakeFist";
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Dictionary<string, AnimationMethod> _animations;

        public void Idle() =>
            _animator.SetTrigger(IDLE);

        public void ShakeFit() =>
            _animator.SetTrigger(SHAKE_FIT);

        public void TriggerMethod(DialogSignal data)
        {
            foreach (var key in _animations.Keys)
            {
                if (key == data.SignalID)
                    _animations[key]?.Invoke();
            }
        }
    }
}