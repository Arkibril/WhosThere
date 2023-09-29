using UnityEngine;
using UnityEngine.EventSystems;

public class PointerVerification : MonoBehaviour, IPointerExitHandler
{
    public DropDown _dropDown;
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        _dropDown.state = 0;
    }
}
