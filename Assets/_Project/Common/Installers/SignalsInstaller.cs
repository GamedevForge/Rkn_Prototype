using Project.Common.Core;
using UnityEngine;
using Zenject;

namespace Project.Common.Installers
{
    public class SignalsInstaller : MonoInstaller
    {
        [SerializeField] private DialogSignalsReceiverList _npcReceiverArray;
        
        public override void InstallBindings()
        {
            Container.Bind<DialogSignalsReceiverList>().FromInstance(_npcReceiverArray).AsSingle();

            Container.BindSignal<DialogSignal>()
                .ToMethod<DialogSignalsReceiverList>(x => x.TriggerAllMethods).FromResolve();

        }
    }
}