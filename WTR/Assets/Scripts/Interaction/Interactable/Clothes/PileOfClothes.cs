using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOfClothes : MonoBehaviour
{

    public bool isTake;
    public bool isWashed;
    public bool isDryed;

    public Material mat;

    public Rigidbody rb;
    private PlayerInteraction _playerInteraction;

    public bool done;
    private Color wantedColor;

    private void Start()
    {
        mat.color = Color.white;
        rb = GetComponent<Rigidbody>();
        _playerInteraction = GameObject.Find("Mark").GetComponent<PlayerInteraction>();
        wantedColor = new Color(0.5792452f, 0.885943f, 1, 1);
    }
    // Update is called once per frame
    void Update()
    {
        if (isWashed && mat.color != wantedColor) mat.color = wantedColor;
        if (isDryed && mat.color != Color.white) mat.color = Color.white;

        if (isTake)
        {
            GetComponent<BoxCollider>().enabled = false;

            if (Input.GetKeyDown(KeyCode.J))
            {
                isTake = false;
                GetComponent<BoxCollider>().enabled = true;
                rb.useGravity = true;
                rb.AddForce(transform.forward * 10, ForceMode.Impulse);

                if (_playerInteraction.selectedObject = null) _playerInteraction.selectedObject = null;
            }
        }

        if(isDryed && isWashed && !done)
        {
            done = true;
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
