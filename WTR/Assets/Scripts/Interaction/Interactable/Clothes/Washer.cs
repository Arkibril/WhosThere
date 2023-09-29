using System.Collections;
using UnityEngine;

public class Washer : MonoBehaviour
{
    public bool isOn;
    public bool timerTextPause;
    public float timeValue = 120;

    public GameObject Text;
    public TMPro.TextMeshProUGUI timertext;
    public GameObject clothesPos;

    public GameObject[] GUI;
    public bool isGUIActivate = false;
    public float holdTime;
    private bool wait;
    public GameObject instructionCanvas;

    public UnityEngine.UI.Image[] V_Filler;

    public GameObject clothes;

    private Levier _levier;

    private void Start()
    {
        Text.SetActive(false);
        timertext = Text.GetComponent<TMPro.TextMeshProUGUI>();
        _levier = GameObject.FindObjectOfType<Levier>();
    }

    private void Update()
    {
        if (_levier.isOn){
            if (isOn)
            {
                CountDown();
                GetComponent<AudioSource>().Play();
            }

            if (timeValue <= 0 && clothes != null)
            {
                Text.SetActive(false);

                clothes.GetComponent<PileOfClothes>().isWashed = true;
                clothes.GetComponent<BoxCollider>().enabled = true;
                clothes.GetComponent<Rigidbody>().useGravity = true;
                clothes.GetComponentInChildren<MeshRenderer>().enabled = true;

                GetComponent<AudioSource>().Stop();

                StartCoroutine(WaitForNull());
            }

            if (!isGUIActivate)
            {
                GUI[0].SetActive(true);
                GUI[1].SetActive(false);
            }
            else
            {
                GUI[0].SetActive(false);
                GUI[1].SetActive(true);
            }

            TutoInput();
        }  

    }

    void CountDown()
    {
        timeValue = Mathf.Max(timeValue - Time.deltaTime, 0);

        DisplayTime();
    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timeValue / 60);
        float seconds = Mathf.FloorToInt(timeValue % 60);

        if (!timerTextPause)
        {
            timertext.fontSize = 0.2f;
            timertext.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    void TutoInput()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            wait = false;
            holdTime = 0f;
            if (!isGUIActivate)
            {
                V_Filler[0].fillAmount = 0;
            }
            else
            {
                V_Filler[1].fillAmount = 0;
            }
        }

        if (Input.GetKey(KeyCode.V) && !wait)
        {
            holdTime += Time.deltaTime;
            if (!isGUIActivate)
            {
                V_Filler[0].fillAmount = holdTime;
            }
            else
            {
                V_Filler[1].fillAmount = holdTime;
            }

            if (holdTime >= 1)
            {
                isGUIActivate = !isGUIActivate;
                wait = true;
                holdTime = 0f;
                if (!isGUIActivate)
                {
                    V_Filler[0].fillAmount = 0;
                }
                else
                {
                    V_Filler[1].fillAmount = 0;
                }
            }
        }
    }

    IEnumerator WaitForNull()
    {
        yield return new WaitForEndOfFrame();
        clothes = null;
    }
}
