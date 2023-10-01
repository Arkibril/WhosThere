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
    private Renderer[] affectedRenderers; // Tableau pour stocker les rendereurs affectés par ce script
    public Light flashlight; // Référence à la lampe de poche
    private bool allLightsOff = false; // Variable pour suivre l'état des lumières

    private void Start()
    {
        flashlight = GameObject.FindWithTag("Light").GetComponent<Light>();
        Volume = FindObjectOfType<Volume>();
        // Collecte toutes les lumières de la scène au démarrage
        lights = FindObjectsOfType<Light>();
        affectedRenderers = FindObjectsOfType<Renderer>();

        if (this.gameObject.name == "untitled(Clone)" && Off == false)
        {
            ToggleLights();
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ToggleLights();
        //}
    }

    private void ToggleLights()
    {
        foreach (Light light in lights)
        {
            if (light == flashlight)
            {
                continue; 
            }

            light.enabled = !light.enabled; // Active ou désactive chaque lumière
        }

        allLightsOff = !allLightsOff; // Met à jour l'état global des lumières
        Switch.Play();

        // Ajuste l'exposition en fonction de l'état des lumières
        if (allLightsOff)
        {
            foreach (Renderer renderer in affectedRenderers)
            {
                Material[] materials = renderer.materials;
                foreach (Material material in materials)
                {
                    material.SetColor("_EmissionColor", Color.black); // Désactive l'émission lumineuse
                }
            }
        }
        else
        {
            foreach (Renderer renderer in affectedRenderers)
            {
                Material[] materials = renderer.materials;
                foreach (Material material in materials)
                {
                    //material.SetColor("_EmissionColor", originalEmissionColor);
                }
            }
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
