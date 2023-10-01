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
    private Renderer[] affectedRenderers; // Tableau pour stocker les rendereurs affect�s par ce script
    public Light flashlight; // R�f�rence � la lampe de poche
    private bool allLightsOff = false; // Variable pour suivre l'�tat des lumi�res

    private void Start()
    {
        flashlight = GameObject.FindWithTag("Light").GetComponent<Light>();
        Volume = FindObjectOfType<Volume>();
        // Collecte toutes les lumi�res de la sc�ne au d�marrage
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

            light.enabled = !light.enabled; // Active ou d�sactive chaque lumi�re
        }

        allLightsOff = !allLightsOff; // Met � jour l'�tat global des lumi�res
        Switch.Play();

        // Ajuste l'exposition en fonction de l'�tat des lumi�res
        if (allLightsOff)
        {
            foreach (Renderer renderer in affectedRenderers)
            {
                Material[] materials = renderer.materials;
                foreach (Material material in materials)
                {
                    material.SetColor("_EmissionColor", Color.black); // D�sactive l'�mission lumineuse
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
