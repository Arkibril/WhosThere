using System.Collections;
using UnityEngine;

public class DesactivationWithTime : MonoBehaviour
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
        Object.SetActive(false);
    }
}
