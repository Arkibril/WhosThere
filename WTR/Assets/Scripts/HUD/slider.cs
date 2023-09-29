using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    private Camera player_Camera;
    public TMPro.TextMeshProUGUI valueText;
    private AudioSource[] audioSource;
    private GameObject markGameObject;

    private void Start()
    {
        markGameObject = GameObject.Find("Mark");
        player_Camera = markGameObject.GetComponentInChildren<Camera>();
        audioSource = GameObject.FindObjectsOfType<AudioSource>();
        
        for (int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i].volume = 0.5f;
            audioSource[i].spatialBlend = 1;
        }
    }
    private void Update()
    {
        valueText.text = GetComponent<Slider>().value.ToString("F0");
    }

    public void FOV()
    {
        player_Camera.fieldOfView = GetComponent<Slider>().value;
    }

    public void GeneralVolume()
    {
        var musicVolume = GameObject.Find("Slider volume de la musique").GetComponent<Slider>();
        var effectVolume = GameObject.Find("Slider Volume des effets").GetComponent<Slider>();
        
        for (int i = 0; i < audioSource.Length; i++)
        {
            if ( !audioSource[i].CompareTag("musicSound"))
            {
                audioSource[i].volume = (GetComponent<Slider>().value / 100);
                musicVolume.value = GetComponent<Slider>().value;
                effectVolume.value = GetComponent<Slider>().value;
            }        
        }
    }

    public void MusicVolume()
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            if (audioSource[i].CompareTag("musicSound"))
            {
                audioSource[i].volume = (GetComponent<Slider>().value / 100);
            }
        }
    }

    public void EffectVolume()
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            if (audioSource[i].CompareTag("effectSound"))
            {
                audioSource[i].volume = (GetComponent<Slider>().value / 100);
            }
        }
    }

}
