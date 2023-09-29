using UnityEngine;

public class BreadPlate : MonoBehaviour
{
    [Header("Toast Positions")]
    public GameObject ToastPosition1;
    public GameObject ToastPosition2;
    public GameObject ToastPosition3;
    public GameObject ToastPosition4;

    [Header("Toast GameObject")]
    public GameObject Toast1;
    public GameObject Toast2;
    public GameObject Toast3;
    public GameObject Toast4;

    public int numberOfToast;

    public bool done;

    private void Update()
    {
        if (Toast1 != null && Toast2 != null && Toast3 != null && Toast4 != null && !done) done = true;
    }

}
