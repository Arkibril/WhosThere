using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionTime : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TimeDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimeDestroy()
    {
        yield return new WaitForSeconds(Random.Range(10f, 30f));
        GameObject.FindWithTag("MonsterScript").GetComponent<MonsterScript>().enabled = true;
        GameObject.FindWithTag("HorrorMusic").GetComponent<AudioSource>().Stop();
        Destroy(gameObject);
    }
}
