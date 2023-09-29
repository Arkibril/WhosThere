using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ProximityEffects : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxDistance = 10f;
    private Volume volume;
    private Vignette vignette;
    private Exposure exposure;
    private ChromaticAberration chromaticAberration;

    private void Start()
    {
        volume = Object.FindObjectOfType<Volume>();

        if (volume != null && volume.profile.TryGet(out vignette))
        {
            vignette.active = true;
        }
        if (volume != null && volume.profile.TryGet(out exposure))
        {
            exposure.active = true;
        }
        if (volume != null && volume.profile.TryGet(out chromaticAberration))
        {
            chromaticAberration.active = true;
        }

        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.position);
        float normalizedDistance = Mathf.Clamp01(distance / maxDistance);

        if (vignette != null)
        {
            vignette.intensity.value = 0.8f - normalizedDistance;
        }
        if (exposure != null)
        {
            exposure.compensation.value -= normalizedDistance * 0.02f;
        }
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = 2f - normalizedDistance;
        }
    }
}
