using UnityEngine;
using Zenject;
using Project.Common.Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Zenject.SpaceFighter;

namespace Project.Common.Core
{
    public class DoorController : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private Transform _animationTarget;
        [SerializeField] private float _animationDuration;
        
        private bool _openIsProcessing = false;
        private bool _isOpen = false;

        private ObjectsDataService _dataService;
        private Transform _playerTransform;
        private Quaternion _initialRotation;
        
        public bool CanInteract => _openIsProcessing == false;
        public InteractType InteractType => InteractType.Click;
        public float HoldTime => 0f;
        public string Name => _dataService.GetObjectConfig(ObjectType.AnimatedDoor).Name;

        [Inject] private void Construct(
            ObjectsDataService objectsDataService,
            PlayerComponents playerComponents)
        {
            _dataService = objectsDataService;
            _playerTransform = playerComponents.PlayerTransform;
        }

        private void Awake() =>
            _initialRotation = _animationTarget.rotation;

        public async void Interact()
        {
            _openIsProcessing = true;

            if (_isOpen)
            {
                await PlayCloseAnimationAsync();
                _isOpen = false;
            }
            else
            {
                await PlayOpenAnimationAsync();
                _isOpen = true;
            }

            _openIsProcessing = false;
        }
        
        private async UniTask PlayOpenAnimationAsync()
        {
            Vector3 doorForward = _animationTarget.forward;
            Vector3 playerDirection = _playerTransform.position - transform.position;

            Vector3 cross = Vector3.Cross(doorForward, playerDirection);
            float side = cross.y;

            float direction = (side > 0) ? 1f : -1f;
            await _animationTarget.DORotate(
                _animationTarget.eulerAngles + new Vector3(0f, 90f * direction, 0f), 
                _animationDuration).AsyncWaitForCompletion();
        }

        private async UniTask PlayCloseAnimationAsync() =>
            await _animationTarget.DORotate(_initialRotation.eulerAngles, _animationDuration).AsyncWaitForCompletion();
    }


    public class test : MonoBehaviour
    {
        public float openAngle = 90f; // Угол открытия
        public float smoothSpeed = 2f; // Плавность открытия
        public float detectionRadius = 3f; // Дистанция срабатывания

        private bool isOpen = false;
        private Quaternion initialRotation;
        private Quaternion targetRotation;
        private Transform player;

        void Start()
        {
            initialRotation = transform.rotation;
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= detectionRadius)
            {
                // Определяем, с какой стороны от двери находится игрок
                Vector3 doorForward = transform.forward; // Направление "вперед" для двери
                Vector3 playerDirection = player.position - transform.position;
                float dotProduct = Vector3.Dot(doorForward, playerDirection);

                // Если dotProduct > 0, игрок перед дверью, если < 0 — за дверью
                // Но нам нужно лево/право, поэтому используем Cross и Dot
                Vector3 cross = Vector3.Cross(doorForward, playerDirection);
                float side = cross.y; // Если side > 0 — игрок справа, если < 0 — слева

                float direction = (side > 0) ? -1f : 1f; // Выбираем направление открытия
                targetRotation = initialRotation * Quaternion.Euler(0, openAngle * direction, 0);
                isOpen = true;
            }
            else
            {
                targetRotation = initialRotation;
                isOpen = false;
            }

            // Плавное вращение
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }

        // Визуализация радиуса (для отладки)
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}