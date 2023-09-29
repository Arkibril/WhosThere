using UnityEngine;

public class Toast : MonoBehaviour
{
    private PlayerInteraction _playerInteraction;
    private MeshRenderer _meshRenderer;
    private MeshCollider _meshCollider;
    
    public bool isTake;
    public bool toasted;

    private void Start()
    {
        _playerInteraction = GameObject.Find("Mark").GetComponentInChildren<PlayerInteraction>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshCollider = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if (isTake)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                isTake = false;
                _meshCollider.enabled = true;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);

                _playerInteraction.selectedObject = null;
            }
        }

        _meshRenderer.material.color = toasted ? new Color(0.5f, 0.3704358f, 0.1245283f) : Color.white;
    }
}
