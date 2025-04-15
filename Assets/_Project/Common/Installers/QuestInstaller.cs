using UnityEngine;
using Zenject;
using Project.Common.Core.Quest;

namespace Project.Common.Installers
{
    public class QuestInstaller : MonoInstaller
    {
        [SerializeField] private QuestEvent[] _questEvents; 
        
        public override void InstallBindings()
        {
            Container.Bind<QuestEvent[]>().FromInstance(_questEvents).AsSingle();
        }
    }
}