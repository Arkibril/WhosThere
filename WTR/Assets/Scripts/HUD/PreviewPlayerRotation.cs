using UnityEngine;
using UnityEngine.EventSystems;

public class PreviewPlayerRotation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private GameObject player;
    private bool isHold;

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        isHold = true;
    }

    void IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        isHold = false;
    }

    private void Start(){
        player = GameObject.Find("Mark");
    }

    private void Update(){
        if (isHold){
            float mouseY = Input.GetAxis("Mouse X");

            player.transform.Rotate(Vector3.down, mouseY * 100 * Time.deltaTime);
        }
    }

}
