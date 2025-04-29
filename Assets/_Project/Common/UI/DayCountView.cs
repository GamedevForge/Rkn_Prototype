using UnityEngine;
using Zenject;

namespace Project.Common.UI
{
    public class DayCountView : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text _text;

        private DayHandler _dayHandler;

        [Inject] private void Construct(DayHandler dayHandler)
        {
            _dayHandler = dayHandler;
            SetNewState();
            _dayHandler.OnDayNumberChanged += SetNewState;
        }

        private void OnDestroy()
        {
            _dayHandler.OnDayNumberChanged -= SetNewState;
        }

        private void SetNewState() =>
            _text.text = "Day " + _dayHandler.DayNumber;
    }
}