using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class LightManager2 : MonoBehaviour
{
    private Light[] lights; // Tableau pour stocker toutes les lumi�res de la sc�ne
    public bool Off;
    public AudioSource Switch;
    public Volume Volume;

    private bool allLightsOff = false; // Variable pour suivre l'�tat des lumi�res

    private void Start()
    {

        Volume = FindObjectOfType<Volume>();
        // Collecte toutes les lumi�res de la sc�ne au d�marrage
        lights = FindObjectsOfType<Light>();
        if (this.gameObject.name == "untitled(Clone)" && Off == false)
        {
            ToggleLights();
        }
    }

    private void Update()
    {
        // D�sactive toutes les lumi�res de la sc�ne lorsqu'un certain bouton est enfonc�
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ToggleLights();
        //}
    }

    private void ToggleLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = !light.enabled; // Active ou d�sactive chaque lumi�re
        }

        allLightsOff = !allLightsOff; // Met � jour l'�tat global des lumi�res
        Switch.Play();

        // Ajuste l'exposition en fonction de l'�tat des lumi�res
        if (allLightsOff)
        {
            //Volume.expos
        }
        else
        {
            //colorAdjustments.postExposure.Override(normalExposure);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            ToggleLights();
        }
    }
}
