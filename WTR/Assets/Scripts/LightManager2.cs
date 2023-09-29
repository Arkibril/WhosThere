using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class LightManager2 : MonoBehaviour
{
    private Light[] lights; // Tableau pour stocker toutes les lumières de la scène
    public bool Off;
    public AudioSource Switch;
    public Volume Volume;

    private bool allLightsOff = false; // Variable pour suivre l'état des lumières

    private void Start()
    {

        Volume = FindObjectOfType<Volume>();
        // Collecte toutes les lumières de la scène au démarrage
        lights = FindObjectsOfType<Light>();
        if (this.gameObject.name == "untitled(Clone)" && Off == false)
        {
            ToggleLights();
        }
    }

    private void Update()
    {
        // Désactive toutes les lumières de la scène lorsqu'un certain bouton est enfoncé
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ToggleLights();
        //}
    }

    private void ToggleLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = !light.enabled; // Active ou désactive chaque lumière
        }

        allLightsOff = !allLightsOff; // Met à jour l'état global des lumières
        Switch.Play();

        // Ajuste l'exposition en fonction de l'état des lumières
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
