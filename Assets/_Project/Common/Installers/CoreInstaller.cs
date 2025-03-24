using UnityEngine;
using Zenject;
using Project.Common.UI;

namespace Project.Common.Installers
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private TextView _interactiveObjectsTextView;
    
        public override void InstallBindings()
        {
            Container.Bind<InteractiveObjectsTextController>().AsSingle().WithArguments(_interactiveObjectsTextView);
        }
    }
}