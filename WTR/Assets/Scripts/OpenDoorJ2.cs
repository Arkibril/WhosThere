using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpenDoorJ2 : MonoBehaviour
{
    public RaycastHit hit;
    public GameObject Door;
    public GameObject Chair;
    public MeshDestroy meshDestroy;
    public Door door;
    public AudioSource BreakSound;

    private void Update()
    {
        // Lancer le raycast depuis la position du bot dans la direction souhaitée
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            meshDestroy = hit.collider.GetComponentInChildren<MeshDestroy>();

            // Vérifier si le raycast a touché un objet avec le tag "Door"
            if (hit.collider.CompareTag("MainDoor"))
            {
                Door = hit.collider.gameObject;
                // Ouvrir la porte
                door = hit.collider.GetComponent<Door>();
                if (door.isOpen == false && door.chairOnDoor == null)
                {
                    hit.collider.GetComponent<Animation>().Play("Door_open");
                    door.isOpen = true;
                }
                else if (door.isOpen == false && door.chairOnDoor != null)
                {
                    meshDestroy.DestroyMesh();
                    StartCoroutine(timeStop());
                    Destroy(door.chairOnDoor.gameObject);
                    door.isOpen = true;
                    door.chairOnDoor = null;
                
                }
            }
        }
    }

    IEnumerator timeStop()
    {
        BreakSound.Play();
        this.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(Door.transform.GetChild(0).gameObject);
        this.GetComponent<NavMeshAgent>().enabled = true;
    }
}
