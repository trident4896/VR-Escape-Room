using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillCursor : MonoBehaviour
{
    public float fillTimer;
    
    public Image playerCursor;

    [SerializeField] private float totalfillTime = 8f;

    [SerializeField] [Range(0f, 1f)] private float currentFillAmount;
    public static float passFillAmount;

    private void Awake()
    {
        playerCursor.enabled = false;
        currentFillAmount = playerCursor.fillAmount;
    }

    public void FillUp()
    {
        StartCoroutine(CursorFilling(1));
    }

    public void FillDown()
    {
        StartCoroutine(CursorFilling(0));
    }

    public IEnumerator CursorFilling(float targetFillAmount)
    {
        yield return new WaitForSeconds(0.5f);

        while (Mathf.Approximately(currentFillAmount, targetFillAmount))
        {
            playerCursor.enabled = true;

            if(currentFillAmount < targetFillAmount)
            {
                fillTimer += Time.deltaTime;
            }
            else
            {
                fillTimer -= Time.deltaTime;
            }

            currentFillAmount = fillTimer / totalfillTime;
            passFillAmount = currentFillAmount;

            if (currentFillAmount >= 1 || currentFillAmount <= 0 )
            {
                playerCursor.enabled = false;
            }
        }
    }

}
