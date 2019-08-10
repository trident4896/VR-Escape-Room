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
            fillCursor();
        }
        else if (objectLooking == "Other")
        {
            fillCursorOther();
        }
        else if (objectLooking == "NotLooking")
        {
            playerCursor.enabled = false;
            fillTimer = 0f;
            playerCursor.fillAmount = 0f;
        }        
    }

    private void fillCursor()
    {
            playerCursor.enabled = true;
            totalfillTime = 3.0f;
            fillTimer += Time.deltaTime;
            playerCursor.fillAmount = fillTimer / totalfillTime;

            RaycastSelection.cursorFillAmount = playerCursor.fillAmount;

            if (playerCursor.fillAmount >= 1)
            {
                playerCursor.enabled = false;
                playerCursor.fillAmount = 0f;
            }

    }

    private void fillCursorOther()
    {
            playerCursor.enabled = true;
            totalfillTime = 0.8f;
            fillTimer += Time.deltaTime;
            playerCursor.fillAmount = fillTimer / totalfillTime;

            RaycastSelection.cursorFillAmountOther = playerCursor.fillAmount;

            if (playerCursor.fillAmount >= 1)
            {
                playerCursor.enabled = false;
                playerCursor.fillAmount = 0f;
        }
        
    }

}
