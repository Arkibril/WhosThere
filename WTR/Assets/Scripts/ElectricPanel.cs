using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPanel : MonoBehaviour
{
    private Light[] lights; // Tableau pour stocker toutes les lumi�res de la sc�ne
    private bool lightsOff;

    private void Start()
    {
        FindLights(); // Recherche les lumi�res de la sc�ne
        ToggleLights();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // V�rifie si le joueur regarde un objet avec le tag "PaneauElectrique"
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
        lights = FindObjectsOfType<Light>(); // Collecte toutes les lumi�res de la sc�ne
    }

    private void ToggleLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = !light.enabled; // Active ou d�sactive chaque lumi�re
        }
    }
}