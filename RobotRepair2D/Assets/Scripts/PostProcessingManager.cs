using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    private ColorGrading colourGradingLayer = null;
    private Vignette vignetteLayer = null;

    private PostProcessVolume globalVolume;
    private GameObject volumeObject;
    private bool drugs;

    public float drugSpeed;

    // Start is called before the first frame update
    void Start()
    {
        volumeObject = GameObject.Find("GlobalPostProcessor");
        globalVolume = volumeObject.GetComponent<PostProcessVolume>();

        globalVolume.profile.TryGetSettings(out colourGradingLayer);
        globalVolume.profile.TryGetSettings(out vignetteLayer);

        drugs = false;
        ChillOut();
    }

    // Update is called once per frame
    void Update()
    {
        if (drugs)
            colourGradingLayer.hueShift.value += drugSpeed;
        else
        {
            if (colourGradingLayer.hueShift.value != 0.0f)
                colourGradingLayer.hueShift.value = 0.0f;
        }
    }

    public void DoDrugs(float tripspeed) //Don't
    {
        drugs = true;
        if (!(tripspeed.Equals(0.0f)))
            drugSpeed = tripspeed;
    }

    public void StopDrugs()
    {
        drugs = false;
    }

    public void GetMad()
    {
        vignetteLayer.color.value = new Color(1.0f, 0.2f, 0.2f, 1.0f);
        vignetteLayer.intensity.value = 0.6f;
    }

    public void ChillOut()
    {
        vignetteLayer.color.value = new Color(0.24f, 0.0f, 0.28f, 1.0f);
        vignetteLayer.intensity.value = 0.4f;
    }
}
