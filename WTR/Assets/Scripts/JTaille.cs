using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JTaille : MonoBehaviour
{
    public float xSize = 1.1f;
    public float ySize = 1.1f;
    public float zSize = 1.1f;
    void Start()
    {
        this.transform.localScale = new Vector3(xSize, ySize, zSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
