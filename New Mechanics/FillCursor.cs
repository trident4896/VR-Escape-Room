using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillCursor : MonoBehaviour
{
    public float fillTimer;
    public float totalfillTime;
    public Image playerCursor;

    private ButtonSpawn buttonReset;
    private RaycastSelection checkSameObject;

    public void Start()
    {
        checkSameObject = GetComponent<RaycastSelection>();
        buttonReset = GetComponent<ButtonSpawn>();

        playerCursor.enabled = false;
    }

    public void Update()
    {
        if (buttonReset.isButtonSpawn == true)
        {
            if (checkSameObject.isSameObject == true)
            {
                fillTimer = 0f;
            }
        }
    }

    public void cursorToFill(int objectLooking)
    {
        if (objectLooking == 1)
        {
            fillCursor(1.5f);
        }
        else if (objectLooking == 2)
        {
            fillCursor(0.5f);
        }
        else if (objectLooking == 0)
        {
            playerCursor.enabled = false;
            fillTimer = 0f;
            playerCursor.fillAmount = 0f;
        }        
    }

    private void fillCursor(float totalFillTimer)
    {
            buttonReset = GetComponent<ButtonSpawn>();

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
