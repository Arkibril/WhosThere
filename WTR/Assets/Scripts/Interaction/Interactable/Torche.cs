using UnityEngine;

public class Torche : MonoBehaviour
{

    public bool isTake;
    [SerializeField]
    private bool isOn;
    public Rigidbody rb;

    private PlayerInteraction _playerInteraction;
    private int inputTime;
    private AudioSource audioSource;

    private void Start()
    {
        GetComponentInChildren<Light>().enabled = false;
        _playerInteraction = GameObject.Find("Mark").GetComponentInChildren<PlayerInteraction>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (isTake)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                isTake = false;
                GetComponent<BoxCollider>().enabled = true;
                rb.useGravity = true;
                rb.AddForce(transform.forward * 10, ForceMode.Impulse);

                _playerInteraction.selectedObject = null;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                inputTime += 1;
                audioSource.Play();
            }

            if (inputTime == 0) isOn = false;
            if (inputTime == 1) isOn = true;
            if (inputTime == 2) inputTime = 0;
        }

        if (isOn)
        {
            GetComponentInChildren<Light>().enabled = true;
        }
        else
        {
            GetComponentInChildren<Light>().enabled = false;
        }
    }

}
