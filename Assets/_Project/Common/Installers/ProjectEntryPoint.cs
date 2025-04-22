using Project.Common.Core;
using UnityEngine;
using Zenject;

namespace Project.Common.Installers
{
    public class ProjectEntryPoint : IInitializable
    {
        private readonly Core.IFactory<GameObject, WidescreenData> _widesreenFactory;
        private readonly WidescreenData _widescreenData;

        public ProjectEntryPoint(
            Core.IFactory<GameObject, WidescreenData> widesreenFactory,
            WidescreenData widescreenData)
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