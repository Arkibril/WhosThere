using UnityEngine;

public class DesactivateObject : MonoBehaviour
{
    public GameObject Object;
    void Start()
    {
        Object.SetActive(true); 
    }
}
