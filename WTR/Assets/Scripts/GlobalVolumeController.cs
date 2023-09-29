using BeautifyHDRP;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class GlobalVolumeController : MonoBehaviour
{
    private Volume globalVolume;
    public float VignetteValue;
    public float ExposureValue;
    public float InnerRingValue;
    public float OuterRingValue;
    public float BlinkValue;


    void Start()
    {
        globalVolume = FindObjectOfType<Volume>();
    }

    // Fonction pour modifier la propriété d'intensité de l'éclairage volumétrique
    public void SetVolumetricLightingIntensity()
    {

    }

    public void Update()
    {
        globalVolume.profile.TryGet(out Vignette volumeVignette);
        volumeVignette.intensity.value = VignetteValue;

        globalVolume.profile.TryGet(out Exposure volumeExposure);
        volumeExposure.compensation.value = ExposureValue;

        globalVolume.profile.TryGet(out Beautify blink);
        blink.vignettingInnerRing.value = InnerRingValue;
        blink.vignettingOuterRing.value = OuterRingValue;
        blink.vignettingBlink.value = BlinkValue;
    }
}
