using Project.Common.Core;
using UnityEngine;
using Zenject;

namespace Project.Common.Installers
{
    public class ProjectEntryPoint : IInitializable
    {
        private readonly Core.IFactory<GameObject, BaseFactoryData> _widesreenFactory;
        private readonly BaseFactoryData _widescreenData;

        public ProjectEntryPoint(
            Core.IFactory<GameObject, BaseFactoryData> widesreenFactory,
            BaseFactoryData widescreenData)
        {
            _widesreenFactory = widesreenFactory;
            _widescreenData = widescreenData;
        }

        public void Initialize()
        {
            _widesreenFactory.Create(_widescreenData);
        }
    }
}