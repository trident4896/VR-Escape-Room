using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFade : MonoBehaviour
{

    [SerializeField] private Renderer renderer;

    [SerializeField] private Color startColor;
    [SerializeField] private Color EndColor;

    // fade duration for a complete 0-100 fade
    // fading will be shorter if only fading parts
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] [Range(0f, 1f)] private float currentFade;

    public float fillTimer;

    public Image playerCursor;

    [SerializeField] private float totalfillTime = 3f;

    public static float passFillAmount;

    private void Awake()
    {
        playerCursor.enabled = false;
        

        if (!renderer)
        {
            renderer = GetComponent<Renderer>();
            startColor = renderer.material.color;
        }
    }

    public void FadeIn()
    {
        // avoid concurrent routines
        StopAllCoroutines();

        StartCoroutine(FadeTowards(1, 1));
    }

    public void FadeOut()
    {
        // avoid concurrent routines
        StopAllCoroutines();

        StartCoroutine(FadeTowards(0, 0));
    }

    private IEnumerator FadeTowards(float targetFade, float targetFillAmount)
    {
        while (!Mathf.Approximately(currentFade, targetFade) || !Mathf.Approximately(playerCursor.fillAmount, targetFillAmount))
        {
            // increase or decrease the currentFade according to the fade speed
            if (currentFade < targetFade)
            {
                currentFade += Time.deltaTime / fadeDuration;
            }
            else
            {
                currentFade -= Time.deltaTime / fadeDuration;
            }

            if(playerCursor.fillAmount < 1 || playerCursor.fillAmount > 0)
            {
                playerCursor.enabled = true;
            }

            if (playerCursor.fillAmount < targetFillAmount)
            {
                fillTimer += Time.deltaTime;
            }
            else
            {
                fillTimer -= Time.deltaTime;
            }

            // if you like you could even add some ease-in and ease-out here
            //var lerpFactor = Mathf.SmoothStep(0, 1, currentFade);

            renderer.material.color = Color.Lerp(startColor, EndColor, currentFade /*or lerpFactor for eased fading*/);

            playerCursor.fillAmount = fillTimer / totalfillTime;
            passFillAmount = playerCursor.fillAmount;

            if (playerCursor.fillAmount >= 1 || playerCursor.fillAmount <= 0)
            {
                playerCursor.enabled = false;
            }

            // let this frame be rendered and continue from this point
            // in the next frame
            yield return null;
        }
    }

    /*public void FillUp()
    {
        CursorFilling(1);
    }

    public void FillDown()
    {
        CursorFilling(0);
    }


    public void CursorFilling(float targetFillAmount)
    {

        while (!Mathf.Approximately(currentFillAmount, targetFillAmount))
        {
            playerCursor.enabled = true;

            if (currentFillAmount < targetFillAmount)
            {
                fillTimer += Time.deltaTime;
            }
            else
            {
                fillTimer -= Time.deltaTime;
            }

            currentFillAmount = fillTimer / totalfillTime;
            passFillAmount = currentFillAmount;

            if (currentFillAmount >= 1 || currentFillAmount <= 0)
            {
                playerCursor.enabled = false;
            }
        }
    }*/
}
