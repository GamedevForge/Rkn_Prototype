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
            _text.text = "Day " + _dayHandler.DayNumber;
        }
    }
}