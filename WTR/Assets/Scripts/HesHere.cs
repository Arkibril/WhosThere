using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class HesHere : MonoBehaviour
{

    [Header("Intrusion Type")]
    public bool Spam;
    public bool Stay;

    private bool ItsGood;
    private bool TimeLeftFinish;
    public bool Tuto;

    [Header("Click / Time Number")]
    public int NumberSpam = 20;
    public int StayTime;

    private int NumberTouchEnter;
    public float LeftTime = 15;
    public float CurrentTime;
    public float Y;
    public float Ysave;
    [Header("GameObject")]
    public AudioSource music;
    public AudioSource windows;

    [Header("GameObject")]
    public GameObject Monster;
    public Transform MonsterSpawn;
    public Vector3 spawnOffset;         

    public GameObject Indicator;
    public GameObject tEST;
    private CameraShakeScript _cameraShakeScript;

    float xSize = 8.0f;
    float ySize = 8.0f;
    float zSize = 8.0f;


    void Start()
    {
        Ysave = tEST.transform.position.y + 1;

        if (SceneManager.GetActiveScene().name == "TUTO SCENE")
        {
            Tuto = true;
        }
        else
        {
            Tuto = false;
        }

        Ysave = tEST.transform.position.y + 1;
        GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
        MonsterScript.GetComponent<MonsterScript>().enabled = false;
        CurrentTime = LeftTime;
        _cameraShakeScript = GameObject.FindGameObjectWithTag("CameraHolder").GetComponent<CameraShakeScript>();

    }


    void Update()
    {
        Y = tEST.transform.position.y;
      
        CurrentTime -= 1 * Time.deltaTime;


        if (LeftTime > 0 && Ysave > Y)
        {
            Indicator.SetActive(true);
            LeftTime -= 1 * Time.deltaTime;
            tEST.transform.Translate(Vector3.up * Time.deltaTime / 9.5f);
            windows.Play();

        }

        if (LeftTime <= 0 && Y < Ysave && Tuto == false)
        {
            Indicator.SetActive(false);
            windows.Stop();
            LeftTime = 15f;
            this.GetComponent<HesHere>().enabled = false;
            GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
            MonsterScript.GetComponent<MonsterScript>().enabled = true;
        }
        else if (LeftTime <= 0 && Y < Ysave && Tuto == true)
        {
            Indicator.SetActive(false);
            windows.Stop();
            LeftTime = 15f;
            this.GetComponent<HesHere>().enabled = false;
            GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
            MonsterScript.GetComponent<MonsterScript>().enabled = true;
        }


        if (Y > Ysave && Tuto == false)
        {
            Indicator.SetActive(false);
            windows.Stop();
            Vector3 spawnPosition = MonsterSpawn.position + spawnOffset;
            Quaternion spawnRotation = MonsterSpawn.rotation;

            GameObject spawnedPrefab = Instantiate(Monster, spawnPosition, spawnRotation);

            //LeftTime = 20f;
            GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
            MonsterScript.GetComponent<MonsterScript>().enabled = false;
            this.GetComponent<HesHere>().enabled = false;
        }
        else if (LeftTime < 0 && Y > Ysave && Tuto == false)
        {
            Indicator.SetActive(false);
            Vector3 spawnPosition = MonsterSpawn.position + spawnOffset;
            Quaternion spawnRotation = MonsterSpawn.rotation;

            GameObject spawnedPrefab = Instantiate(Monster, spawnPosition, spawnRotation);

            //LeftTime = 20f;
            GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
            MonsterScript.GetComponent<MonsterScript>().enabled = false;
            this.GetComponent<HesHere>().enabled = false;
        }

        if (Y > Ysave && Tuto == true)
        {
            Indicator.SetActive(false);
            //LeftTime = 20f;
            GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
            MonsterScript.GetComponent<MonsterScript>().enabled = false;
            this.GetComponent<HesHere>().enabled = false;
        }
        else if (LeftTime < 0 && Y > Ysave && Tuto == true)
        {
            Indicator.SetActive(false);
            //LeftTime = 20f;
            GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
            MonsterScript.GetComponent<MonsterScript>().enabled = false;
            this.GetComponent<HesHere>().enabled = false;


          
        }

        if (GameObject.FindGameObjectWithTag("MonsterCheck").GetComponent<MonsterCheck>().HesHere == true)
        {
            this.gameObject.GetComponent<HesHere>().enabled = false;
        }

        tEST.transform.Translate(Vector3.up * Time.deltaTime / 19);

    }

    void OnTriggerStay(Collider other)
    {
        // Vérifie si la position Y de tEST est inférieure ou égale à Ysave
        if (tEST.transform.position.y +1 >= Ysave)
        {
            // Si c'est le cas et que la touche "E" est maintenue enfoncée, faire descendre tEST
            if (Input.GetKey(KeyCode.E))
            {
                tEST.transform.Translate(Vector3.down * Time.deltaTime / 1.5f);
                _cameraShakeScript.CameraShake();
            }
        }
    }

    IEnumerator SpamWaiting()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NumberTouchEnter++;
        }
        yield return new WaitUntil(() => ItsGood == true);
    }

    IEnumerator TimeLeft()
    {
        yield return new WaitForSeconds(1f);
        TimeLeftFinish = true;
    }
}
