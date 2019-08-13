using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillCursor : MonoBehaviour
{
    public float fillTimer;
    public float totalfillTime;
    public Image playerCursor;

    public void Start()
    {
        playerCursor.enabled = false;
    }

    public void cursorToFill(string objectLooking)
    {
        if (objectLooking == "Items")
        {
            fillCursor(3.0f);
        }
        else if (objectLooking == "Other")
        {
            fillCursor(0.8f);
        }
        else if (objectLooking == "NotLooking")
        {
            playerCursor.enabled = false;
            fillTimer = 0f;
            playerCursor.fillAmount = 0f;
        }        
    }

    private void fillCursor(float totalFillTimer)
    {
            playerCursor.enabled = true;
            fillTimer += Time.deltaTime;
            playerCursor.fillAmount = fillTimer / totalFillTimer;

            RaycastSelection.cursorFillAmount = playerCursor.fillAmount;

            if (playerCursor.fillAmount >= 1)
            {
                playerCursor.enabled = false;
                playerCursor.fillAmount = 0f;
            }

    }

}
