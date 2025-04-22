using UnityEngine;
using Zenject;

namespace Project.Common.UI
{
    public class DayCountCanvas : MonoBehaviour
    {
        private CanvasRepository _canvasRepository;

        [Inject] private void Construct(CanvasRepository canvasRepository) =>
            _canvasRepository = canvasRepository;

        private void Awake() =>
            _canvasRepository.Add(gameObject);

        private void OnDestroy() =>
            _canvasRepository.Remove(gameObject);
    }
}