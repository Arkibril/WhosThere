using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Hide : MonoBehaviour
{
    public GameObject Text;
    public GameObject LitCam;
    public GameObject Player;

    public bool SousLeLit;
    public bool SeCacher;
    void Start()
    {
        Text = GameObject.FindWithTag("InteractionText");
        Player = GameObject.FindWithTag("PlayerHeadTag");
    }

    void Update()
    {
        if(SeCacher == true && Input.GetKeyDown(KeyCode.E))
        {
            LitCam.SetActive(true);
            SousLeLit = true;
        }

        if(SousLeLit == true)
        {
            Player.SetActive(false);
            Text.GetComponent<Text>().text = "Appuez sur 'Espace' pour sortir ";

        }

        if (SousLeLit == true && Input.GetKeyDown(KeyCode.Space))
        {
            SousLeLit = false;
            Player.SetActive(true);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Mark")
        {
            Text.GetComponent<Text>().text = "Appuez sur 'E' pour vous cacher ";
            SeCacher = true;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Mark")
        {
            Text.GetComponent<Text>().text = " ";

        }
    }
}
