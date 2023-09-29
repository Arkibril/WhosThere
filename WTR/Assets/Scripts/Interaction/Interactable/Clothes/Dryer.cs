using System.Collections;
using UnityEngine;

public class Dryer : MonoBehaviour
{
    public bool isOn;
    public bool timerTextPause;
    public float timeValue = 120;

    public GameObject Text;
    public TMPro.TextMeshProUGUI timertext;
    public GameObject clothesPos;

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

            if (timeValue <= 0)
            {
                Text.SetActive(false);

                GetComponent<AudioSource>().Stop();

                if (clothes != null)
                {
                    clothes.GetComponent<PileOfClothes>().isDryed = true;
                    clothes.GetComponent<BoxCollider>().enabled = true;
                    clothes.GetComponent<Rigidbody>().useGravity = true;
                    clothes.GetComponentInChildren<MeshRenderer>().enabled = true;
                }
                StartCoroutine(WaitForNull());
            }
        }

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
            timertext.fontSize = 0.2f;
            timertext.text = minutes.ToString() + ":" + seconds.ToString();
            if (seconds >= 0 && seconds <= 9) timertext.text = minutes.ToString() + ":" + "0" + seconds.ToString();
        }
    }

    IEnumerator WaitForNull()
    {
        yield return new WaitForSeconds(0.1f);
        clothes = null;
    }
}
