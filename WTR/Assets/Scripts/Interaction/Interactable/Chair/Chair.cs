using UnityEngine;

public class Chair : MonoBehaviour
{

    public bool isTake = false;
    public bool onDoor;

    private PlayerInteraction _playerInteraction;
    public Rigidbody rb;

    private void Start(){
        if(GameObject.Find("Mark") != null) _playerInteraction = GameObject.Find("Mark").GetComponentInChildren<PlayerInteraction>();
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {

        if (isTake)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                isTake = false;
                GetComponent<MeshCollider>().enabled = true;
                GetComponent<MeshCollider>().isTrigger = false;
                rb.useGravity = true;
                rb.AddForce(transform.forward * 5, ForceMode.Impulse);

                _playerInteraction.selectedObject = null;

                GetComponent<AudioSource>().Play();
            }
        }
    }
}
