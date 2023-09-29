using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.HighDefinition;
using TMPro;
using UnityEngine.Rendering;

public class DropDown : MonoBehaviour
{
    public Button[] content;

    public GameObject template;
    public TextMeshProUGUI label;

    public Options[] options;
    public HDAdditionalCameraData HDCdata;
    public Camera cameraPlayer;

    private Volume volume;
    public int value;

    private Animator arrowAnim;
    public bool clicked = false;
    public int state;
    [Header("Sprite")]
    public Sprite dropDown;
    public Sprite dropDown_Locked;
    public Sprite parametre;
    public Sprite parametre_Locked;
    public Image resolution;
    

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        arrowAnim = GetComponent<Animator>();
        label.text = options[0].name;
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
        cameraPlayer = GameObject.Find("Mark").GetComponentInChildren<Camera>();
        HDCdata = cameraPlayer.GetComponent<HDAdditionalCameraData>();
        HDCdata.antialiasing = HDAdditionalCameraData.AntialiasingMode.FastApproximateAntialiasing;
        volume = GameObject.FindObjectOfType<Volume>();

        int contentLenght = content.Length;

        for (int i = 0; i < contentLenght; i++)
        {
            content[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i].name;
            if (options[i].activated) 
            {
                label.text = options[i].name;
                value = i;
            }
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        arrowAnim.SetInteger("State", state);

        if (state == 0) template.SetActive(false);
        if (state == 1) template.SetActive(true);

        switch (gameObject.name)
        {
            case "DropDown_resolution":
                if (Screen.fullScreenMode == FullScreenMode.Windowed)
                {
                    GetComponent<Button>().interactable = true;
                    GetComponent<Image>().sprite = dropDown;
                    resolution.sprite = parametre;
                }
                else 
                {
                    GetComponent<Button>().interactable = false;
                    GetComponent<Image>().sprite = dropDown_Locked;
                    resolution.sprite = parametre_Locked;
                    label.text = "1920 x 1080";
                }
                break;
            case "DropDown_Anticren�lage":
                if(gameObject.name == "DropDown_Anticren�lage")
                {
                    if (value == 0) label.fontSize = 30f;
                    if (value == 1) label.fontSize = 22.5f;
                    if (value == 2) label.fontSize = 16f;
                    if (value == 3) label.fontSize = 30f;
                }
                break;
        }





    }

    public void OnClick()
    {
        StartCoroutine(WaitForFalse());
        arrowAnim.SetTrigger("Pressed");

        if (state == 0)
        {
            state = 1;
        }
        else if (state == 1)
        {
            state = 0;
        }


        IEnumerator WaitForFalse()
        {
            yield return new WaitForSeconds(0.1f);
            clicked = false;
        }
    }

    public void Activation(int bouton)
    {
        for(int i = 0; i < content.Length; i++)
        {
            options[i].activated = false;
        }
        StartCoroutine(WaitForTrue());
        state = 0;
        label.text = options[bouton].name;
        value = bouton;

        IEnumerator WaitForTrue()
        {
            yield return new WaitForSeconds(0.1f);
            options[bouton].activated = true;
        }
    }

    public void ModeFenetre()
    {
        if (value == 0)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
        }
        else if (value == 1) 
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.SetResolution(1920, 1080, true);
        } 
        else if (value == 2) Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void Resolution()
    {
        if (value == 0) Screen.SetResolution(1920, 1080, false);
        else if (value == 1) Screen.SetResolution(1680, 1050, false);
        else if(value == 2) Screen.SetResolution(1600, 900, false);
        else if(value == 3) Screen.SetResolution(1440, 900, false);
        else if(value == 4) Screen.SetResolution(1440, 1050, false);
        else if(value == 5) Screen.SetResolution(1366, 768, false);
        else if(value == 6) Screen.SetResolution(1360, 768, false);
        else if(value == 7) Screen.SetResolution(1280, 1024, false);
        else if(value == 8) Screen.SetResolution(1280, 1024, false);
        else if(value == 9) Screen.SetResolution(1280, 960, false);
        else if(value == 10) Screen.SetResolution(1280, 800, false);
    }

    public void AntiAliasing()
    {
        HDAdditionalCameraData.AntialiasingMode[] antialiasingModes = new HDAdditionalCameraData.AntialiasingMode[]
        {
            HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing,
            HDAdditionalCameraData.AntialiasingMode.FastApproximateAntialiasing,
            HDAdditionalCameraData.AntialiasingMode.SubpixelMorphologicalAntiAliasing,
            HDAdditionalCameraData.AntialiasingMode.None
        };
        HDCdata.antialiasing = antialiasingModes[value];
    }

    public void FPS()
    {
        if (value == 0) Application.targetFrameRate = 1000;
        else if(value == 1) Application.targetFrameRate = 30;
        else if(value == 2) Application.targetFrameRate = 60;
        else if(value == 3) Application.targetFrameRate = 75;
        else if(value == 4) Application.targetFrameRate = 120;
        else if(value == 5) Application.targetFrameRate = 144;
        else if(value == 6) Application.targetFrameRate = 160;
        else if(value == 7) Application.targetFrameRate = 165;
        else if(value == 8) Application.targetFrameRate = 180;
        else if(value == 9) Application.targetFrameRate = 200;
        else if(value == 10) Application.targetFrameRate = 240;
        else if(value == 11) Application.targetFrameRate = 360;
    }

    private void UpdateDropDownGraphisme(int qualityIndex)
    {
        var dropDownGraphisme = GameObject.Find("Graphisme Menu").GetComponentsInChildren<DropDown>();
        for (int i = 0; i < dropDownGraphisme.Length; i++)
        {
            for (int j = 0; j < dropDownGraphisme[i].options.Length; j++)
            {
                dropDownGraphisme[i].options[j].activated = false;
            }
            dropDownGraphisme[i].options[qualityIndex].activated = true;
            dropDownGraphisme[i].GetComponentInChildren<TextMeshProUGUI>().text = options[qualityIndex].name;
        }
    }

    public void GlobalQuality()
    {
        UpdateDropDownGraphisme(value);
    }

    public void MaterialQuality()
    {
        UpdateDropDownGraphisme(value);
    }

    public void FogQuality()
    {
        Fog fog;
        volume.profile.TryGet<Fog>(out fog);

        if (value == 0) fog.quality.value = 2;
        if (value == 1) fog.quality.value = 1;
        if (value == 2) fog.quality.value = 0;

    }

    public void QualityPostProcess()
    {
        Bloom bloom;
        ContactShadows contactShadows;
        AmbientOcclusion ambientOcclusion;
        MotionBlur motionBlur;
        volume.profile.TryGet<Bloom>(out bloom);
        volume.profile.TryGet<ContactShadows>(out contactShadows);
        volume.profile.TryGet<AmbientOcclusion>(out ambientOcclusion);
        volume.profile.TryGet<MotionBlur>(out motionBlur);

        if (value == 0)
        {
            bloom.quality.value = 2;
            contactShadows.quality.value = 2;
            ambientOcclusion.quality.value = 2;
            motionBlur.quality.value = 2;
        }
        else if (value == 1)
        {
            bloom.quality.value = 1;
            contactShadows.quality.value = 1;
            ambientOcclusion.quality.value = 1;
            motionBlur.quality.value = 1;
        }
        else if (value == 2)
        {
            bloom.quality.value = 0;
            contactShadows.quality.value = 0;
            ambientOcclusion.quality.value = 0;
            motionBlur.quality.value = 0;
        }
    }

    public void TextureQuality()
    {
        if (value == 0) QualitySettings.masterTextureLimit = 0;
        if (value == 1) QualitySettings.masterTextureLimit = 1;
        if (value == 2) QualitySettings.masterTextureLimit = 2;
    }

    public void Shadows()
    {
        if (value == 0) ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.VeryHigh, ShadowProjection.StableFit, 150, 3, 4);
        if (value == 1) ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.High, ShadowProjection.StableFit, 70, 3, 2);
        if (value == 2) ChangeShadows(ShadowmaskMode.DistanceShadowmask, ShadowQuality.All, ShadowResolution.Medium, ShadowProjection.StableFit, 40, 3, 2);
        if (value == 3) ChangeShadows(ShadowmaskMode.Shadowmask, ShadowQuality.HardOnly, ShadowResolution.Low, ShadowProjection.StableFit, 20, 3, 0);
        if (value == 3) ChangeShadows(ShadowmaskMode.Shadowmask, ShadowQuality.Disable, ShadowResolution.Low, ShadowProjection.StableFit, 15, 3, 0);
    }

    private void ChangeShadows(ShadowmaskMode mask, ShadowQuality quality, ShadowResolution res, ShadowProjection projection, float shadowDistance, float shadowNearPlaneOffset, int shadowCascade)
    {
        QualitySettings.shadowmaskMode = mask;
        QualitySettings.shadows = quality;
        QualitySettings.shadowResolution = res;
        QualitySettings.shadowProjection = projection;
        QualitySettings.shadowDistance = shadowDistance;
        QualitySettings.shadowNearPlaneOffset = shadowNearPlaneOffset;
        QualitySettings.shadowCascades = shadowCascade;
    }
}

[System.Serializable]
public class Options
{
    public string name;
    public bool activated = false;
}