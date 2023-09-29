using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingChangeValue : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;

    public AutoExposure autoExposure;

    public float ExposurFloat;

    void Start()
    {
        postProcessVolume.profile.TryGetSettings(out autoExposure);      
    }

    // Update is called once per frame
    void Update()
    {
        autoExposure.minLuminance.value = ExposurFloat;
    }
}
