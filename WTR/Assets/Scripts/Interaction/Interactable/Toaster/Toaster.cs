using System.Collections;
using UnityEngine;

public class Toaster : MonoBehaviour
{

    public bool isOn;
    public bool timerTextPause;

    public Material mat;
    public GameObject timerCanvas;
    public TMPro.TextMeshProUGUI timertext;
    public TMPro.TextMeshProUGUI[] numberText;

    public float timeValue = 150;
    public float holdTime;
    public bool wait;

    public UnityEngine.UI.Image[] V_Filler;

    public GameObject ToastPosition1;
    public GameObject ToastPosition2;
    public GameObject Toast1;
    public GameObject Toast2;

    public BreadPlate _breadPlate;

    public GameObject instructionCanvas;
    public int GUIActivate = 0;
    public GameObject[] instructionType;
    private Levier _levier;

    private void Start()
    {
        instructionType[0].SetActive(true);
        instructionType[1].SetActive(false);
        _levier = GameObject.FindObjectOfType<Levier>();
    }
    void Update()
    {
        GetComponent<Animator>().SetBool("isOn", isOn);

        if (_levier.isOn){

            for(int i = 0; i < numberText.Length; i++) 
            {
                numberText[i].text = _breadPlate.numberOfToast.ToString() + "/ 4";
            }      

            if (Toast1 != null && !Toast1.GetComponent<Toast>().toasted)
            {
                Toast1.transform.SetPositionAndRotation(ToastPosition1.transform.position, ToastPosition1.transform.rotation);
            }
            if (Toast2 != null && !Toast2.GetComponent<Toast>().toasted)
            {
                Toast2.transform.SetPositionAndRotation(ToastPosition2.transform.position, ToastPosition1.transform.rotation);
            }

            if (Toast1 != null && Toast1.GetComponent<Toast>().isTake) Toast1 = null;
            if (Toast2 != null && Toast2.GetComponent<Toast>().isTake) Toast2 = null;
            
            if (isOn)
            {
                mat.color = Color.red;
                mat.SetColor("_EmissionColor", Color.red);
                timerCanvas.SetActive(true);
                CountDown();
            }
            else
            {
                mat.color = Color.green;
                mat.SetColor("_EmissionColor", Color.green);
                timeValue = 150;
            }

            if (timeValue == 0) 
            {
                timerCanvas.SetActive(false);
                isOn = false;
                Toast1.GetComponent<MeshCollider>().enabled = true;
                Toast1.GetComponent<MeshCollider>().isTrigger = true;
                Toast2.GetComponent<MeshCollider>().enabled = true;
                Toast2.GetComponent<MeshCollider>().isTrigger = true;
                GetComponent<AudioSource>().Play();

                StartCoroutine(WaitForToasted());
            } 

            TutoInput();

            if(GUIActivate == 0)
            {
                instructionType[0].SetActive(true);
                instructionType[1].SetActive(false);
            }
            else
            {
                instructionType[0].SetActive(false);
                instructionType[1].SetActive(true);
            }
            
        }
    }

    IEnumerator WaitForToasted()
    {
        yield return new WaitForSeconds(1.5f);
        Toast1.GetComponent<Toast>().toasted = true;
        Toast2.GetComponent<Toast>().toasted = true;
    }

    void CountDown()
    {
        if (timeValue > 0) timeValue -= Time.deltaTime;
        else timeValue = 0;

        DisplayTime();
    }

    void DisplayTime()
    {
        if (timeValue < 0) timeValue = 0;

        float minutes = Mathf.FloorToInt(timeValue / 60);
        float seconds = Mathf.FloorToInt(timeValue % 60);

        if (!timerTextPause) 
        {
            timertext.fontSize = 0.1f;
            timertext.text = minutes.ToString() + ":" + seconds.ToString();
            if (seconds >= 0 && seconds <= 9) timertext.text = minutes.ToString() + ":" + "0" + seconds.ToString();
        } 
    }

    void TutoInput()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            wait = false;
            holdTime = 0f;
            if (GUIActivate == 0)
            {
                V_Filler[0].fillAmount = 0;
            }
            else if (GUIActivate == 1)
            {
                V_Filler[1].fillAmount = 0;
            }
        }

        if (Input.GetKey(KeyCode.V) && !wait)
        {
            holdTime += Time.deltaTime;
            if (GUIActivate == 0)
            {
                V_Filler[0].fillAmount = holdTime;
            }
            else if (GUIActivate == 1)
            {
                V_Filler[1].fillAmount = holdTime;
            }

            if (holdTime >= 1)
            {

                if (GUIActivate == 0)
                {
                    GUIActivate = 1;
                    wait = true;
                    holdTime = 0f;
                    V_Filler[0].fillAmount = 0;
                }
                else if (GUIActivate == 1)
                {
                    GUIActivate = 0;
                    wait = true;
                    holdTime = 0f;
                    V_Filler[1].fillAmount = 0;
                }
            }

        }
    }
}
