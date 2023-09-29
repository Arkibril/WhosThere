using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public int trapIndex = 0;
    public int NumberOfTrap;

    public List<GameObject> trap;
    public float currentTime;
    public float timeSpawn;


    void Start()
    {
        currentTime = timeSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;


        if (currentTime <= 0)
        {
            trapIndex = Random.Range(0, NumberOfTrap);

            GameObject obj = trap[trapIndex];
            currentTime = 30f;

            obj.GetComponent<HesHere>().enabled = true;
            this.GetComponent<MonsterScript>().enabled = false;

        }
    }

}
