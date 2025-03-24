using UnityEngine;
using Zenject;
using Project.Common.Configs;
using System;

namespace Project.Common.Core
{
    public class ChairController : MonoBehaviour, IIntertableObject
    {
        public string Name => _dataService.GetObjectConfig(ObjectType.Chair).Name;

        private ObjectsDataService _dataService;

        [Inject] private void Construct(ObjectsDataService objectsDataService) =>
            _dataService = objectsDataService;

        public void Interact()
        {
            Debug.Log("Interact!");
        }
    }

    public class PlayerRayCasterController : MonoBehaviour
    {
        private PlayerRayCasterModel _model;
        private PlayerRayCastData _data;

        [Inject] private void Construct(PlayerRayCastData data) =>
            _data = data;

        public void Initialized(PlayerRayCasterModel model) =>
            _model = model;

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(_data.RayStartPosition);

            if (Physics.Raycast(ray, _data.Distance, _data.RayCastIgnoreLayerMask))
                return;

            if (Physics.Raycast(ray, out RaycastHit hit, _data.Distance, _data.InteractableObjectLayerMask))
                _model.ChangeCurrentObject(hit.transform.gameObject);
            else
                _model.ChangeCurrentObject(null);
        }
    }

    public class PlayerRayCasterModel : IObjectChangedEvent<GameObject>
    {
        public event Action<GameObject> OnCurrentObjectChanged;
        public GameObject CurrentGameObject { get; private set; }

        public void ChangeCurrentObject(GameObject gameObject)
        {
            if (gameObject != CurrentGameObject)
            {
                CurrentGameObject = gameObject;
                OnCurrentObjectChanged?.Invoke(gameObject);
            }
            else 
                CurrentGameObject = gameObject;
        }
    }
}


