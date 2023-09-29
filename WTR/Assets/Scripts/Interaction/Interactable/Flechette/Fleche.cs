using UnityEngine;

public class Fleche : MonoBehaviour
{
    public int point = 0;

    public Vector3 collisionPoint;
    public bool hurt;

    private void OnCollisionEnter(Collision collision)
    {
        collisionPoint = transform.position;
        HandleCollision();
        hurt = true;
        GetComponent<AudioSource>().Play();
    }

    private void HandleCollision(){
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;

    }
}
