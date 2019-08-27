using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainFill : MonoBehaviour
{
    public float mainFillTimer;
    public Image mainPlayerCursor;

    public void Awake()
    {
        mainPlayerCursor.enabled = false;
    }

    public void MainCursorToFill(int mainObjectLooking)
    {
        if (mainObjectLooking == 1)
        {
            MainFillCursor(0.5f);
        }
        else if (mainObjectLooking == 0)
        {
            mainPlayerCursor.enabled = false;
            mainFillTimer = 0f;
            mainPlayerCursor.fillAmount = 0f;
        }
    }

    private void MainFillCursor(float mainTotalFillTimer)
    {
        mainPlayerCursor.enabled = true;
        mainFillTimer += Time.deltaTime;
        mainPlayerCursor.fillAmount = mainFillTimer / mainTotalFillTimer;

        MainSelection.getMainCursorFill = mainPlayerCursor.fillAmount;

        if (mainPlayerCursor.fillAmount >= 1)
        {
            mainPlayerCursor.enabled = false;
            mainPlayerCursor.fillAmount = 0f;
        }
    }
}
