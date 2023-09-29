using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPanel : MonoBehaviour
{
    private Light[] lights; // Tableau pour stocker toutes les lumières de la scène
    private bool lightsOff;

    private void Start()
    {
        FindLights(); // Recherche les lumières de la scène
        ToggleLights();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Vérifie si le joueur regarde un objet avec le tag "PaneauElectrique"
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider.CompareTag("PaneauElectrique"))
                {
                    ToggleLights();
                }
            }
        }
    }

    private void FindLights()
    {
        lights = FindObjectsOfType<Light>(); // Collecte toutes les lumières de la scène
    }

    private void ToggleLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = !light.enabled; // Active ou désactive chaque lumière
        }
    }
}