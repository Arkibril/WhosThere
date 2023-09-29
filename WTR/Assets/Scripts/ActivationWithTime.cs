using System.Collections;
using UnityEngine;

public class ActivationWithTime : MonoBehaviour
{
    public float Time;
    public GameObject Object;
    void Start()
    {
        StartCoroutine(TimeActivation());
    }

    IEnumerator TimeActivation() 
    {
        yield return new WaitForSeconds(Time);
        Object.SetActive(true);
    }
}
