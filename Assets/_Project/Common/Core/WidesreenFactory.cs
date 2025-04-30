using UnityEngine;
using Project.Common.UI;

namespace Project.Common.Core
{
    public class WidesreenFactory : 
        IFactory<GameObject, WidescreenData>, 
        IProperty<WidescreenAnimation, GameObject>
    {
        private readonly CanvasRepository _canvasRepository;
        private readonly Zenject.IInstantiator _instantiator;

        public WidescreenAnimation Property { get; private set; }
        public GameObject Property1 { get; private set; }

        public WidesreenFactory(
            CanvasRepository canvasRepository, 
            Zenject.IInstantiator instantiator)
        {
            _canvasRepository = canvasRepository;
            _instantiator = instantiator;
        }

        public GameObject Create(WidescreenData data)
        {
            Property1 = _instantiator.InstantiatePrefab(data.Prefab);
            WidescreenComponentsAndData widescreenComponentsAndData = Property1.GetComponent<WidescreenComponentsAndData>();
            Property = new WidescreenAnimation(
                widescreenComponentsAndData.TopImageTransform, 
                widescreenComponentsAndData.BottomImageTransform, 
                widescreenComponentsAndData.Duration);

            _canvasRepository.Add(Property1);
            Property1.transform.SetParent(null);
            GameObject.DontDestroyOnLoad(Property1);

            Property.Initialize();

            return Property1;
        }
    }
}