using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFade : MonoBehaviour
{

    [SerializeField] private bool saveColorOnce = false;
    [SerializeField] private Transform thisObject;

    [SerializeField] private Color startColor;
    [SerializeField] private Color EndColor;

    [SerializeField] private float lerpFadeTime = 0f;
    private float fadeDuration = 2f;

    private void Start()
    {
        startColor = this.transform.GetComponent<Renderer>().material.color;
        thisObject = this.transform;
    }

    public void FadeCheck(bool fadingStatus)
    {
        if(fadingStatus == true)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }

    }

    private void FadeIn()
    { 
            lerpFadeTime += Time.deltaTime / fadeDuration;

            thisObject.GetComponent<Renderer>().material.color = Color.Lerp(startColor, EndColor, lerpFadeTime);

            if(lerpFadeTime >= 1f)
            {
                lerpFadeTime = 1f;
            }
    }

    private void FadeOut()
    {
            lerpFadeTime -= Time.deltaTime / fadeDuration;

            thisObject.GetComponent<Renderer>().material.color = Color.Lerp(startColor, EndColor, lerpFadeTime);

            if (lerpFadeTime <= 0f)
            {
                lerpFadeTime = 0f;
            }
    }
}
