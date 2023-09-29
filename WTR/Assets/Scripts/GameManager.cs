using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public float TimeToSurvive;

    public GameObject player;
    public GameObject Screamer;
    public GameObject World;
    public GameObject EndAnim;

    void Start()
    {
        //TimeToSurvive = 10f;
        player = GameObject.FindGameObjectWithTag("PlayerHeadTag");
        Screamer = GameObject.FindGameObjectWithTag("Screamer");
        World = GameObject.FindGameObjectWithTag("Map");
    }

    // Update is called once per frame
    void Update()
    {
        TimeToSurvive -= 1 * Time.deltaTime;

        if(TimeToSurvive <= 0)
        {
            Destroy(player);
            Destroy(World);
            Destroy(Screamer);
            EndAnim.SetActive(true);
        }
    }
}
