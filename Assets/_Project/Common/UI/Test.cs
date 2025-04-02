using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image Image => GetComponent<Image>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        Image.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Image.color = Color.yellow;
    }
}
