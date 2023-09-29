using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Bouton : MonoBehaviour
{
    public bool activate = true;
    public TMPro.TextMeshProUGUI bouttonText;
    [SerializeField] private MotionBlur motionBlur;
    private ChromaticAberration chromaticAberration;
    private FilmGrain filmGrain;
    public Volume volume;
    public Image imageTouch;
    public Sprite[] touch;

    private float globalVolumeSavedValue, musicVolumeSavedValue, effectVolumeSavedValue;
    private static Bouton[] allButtons;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        volume.profile.TryGet<MotionBlur>(out motionBlur);
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
        volume.profile.TryGet<FilmGrain>(out filmGrain);

        if (CompareTag("KeyChanger"))
        {
            allButtons = FindObjectsOfType<Bouton>();
        }
    }

    private void OnGUI()
    {
        if (CompareTag("KeyChanger") && activate)
        {
            imageTouch.sprite = null;
            imageTouch.color = new Color(0, 0, 0, 0);

            if (Event.current.isKey && Input.anyKeyDown)
            {
                string keyName = Event.current.keyCode.ToString().ToLower();

                for (int i = 0; i < touch.Length; i++)
                {
                    if (keyName == touch[i].name)
                    {
                        imageTouch.sprite = touch[i];
                        imageTouch.color = Color.white;
                        break;
                    }
                }

                StartCoroutine(WaitBeforeTrue());
                activate = false;
            }
            else if (Event.current.isMouse)
            {
                string keyName = "mouse " + Event.current.button.ToString().ToLower();

                for (int i = 0; i < touch.Length; i++)
                {
                    if (keyName == touch[i].name)
                    {
                        imageTouch.sprite = touch[i];
                        imageTouch.color = Color.white;
                        
                        break;
                    }
                }

                StartCoroutine(WaitBeforeTrue());
                activate = false;
            }
        }
    }

    IEnumerator WaitBeforeTrue(){
        yield return new WaitForSeconds(0.5f);
        GetComponent<Button>().interactable = true;
    }

    public void BouttonState()
    {
        if (activate && !CompareTag("KeyChanger"))
        {
            activate = false;
            if (bouttonText != null)
                bouttonText.text = "Désactivé";
        }
        else
        {
            activate = true;
            if (bouttonText != null)
                bouttonText.text = "Activé";
        }

        if (CompareTag("KeyChanger"))
        {
            foreach (Bouton button in allButtons)
            {
                if (button != this)
                    button.activate = false;
            }

            GetComponent<Button>().interactable = false;
            activate = true;
        }
    }

    public void VSYNC()
    {
        QualitySettings.vSyncCount = activate ? 1 : 0;
    }

    public void MotionBlur()
    {
        motionBlur.active = activate;
    }

    public void ChromaticAberration()
    {
        chromaticAberration.active = activate;
    }

    public void FilmGrain()
    {
        filmGrain.active = activate;
    }

    public void AvancerEtReculer()
    {
        
    }
}
