using UnityEngine;

public class HouseEnterAnimation : MonoBehaviour
{
    public GameObject Text;
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public HouseEnterAnimation() 
    {
        if (Input.anyKeyDown)
        {
            anim.Play("StartCam");
            Text.SetActive(false);
        }
    }
}
