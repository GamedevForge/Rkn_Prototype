using UnityEngine;

namespace Project.Common.UI
{
    public class DayReceiver : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_InputField _inputField;
        
        public int DayNumber { get; private set; } = 1;

        public void ChangeDayNumber()
        {
            if (int.TryParse(_inputField.text, out int result))
                DayNumber = result;
        }
    }
}