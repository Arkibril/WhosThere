using UnityEngine;

public class ChairIndication : MonoBehaviour
{

    public MeshRenderer[] chairIndicationRenderer;

    private void Start()
    {
        chairIndicationRenderer = gameObject.GetComponentsInChildren<MeshRenderer>();
        DesactivateRenderer();
    }

    public void DesactivateRenderer()
    {
        for (int i = 0; i < chairIndicationRenderer.Length; i++) 
        {
            chairIndicationRenderer[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void ActivateRenderer()
    {
        for (int i = 0; i < chairIndicationRenderer.Length; i++)
        {
            chairIndicationRenderer[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
