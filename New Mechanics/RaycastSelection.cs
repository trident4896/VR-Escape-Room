using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastSelection : MonoBehaviour
{
    public GameObject nextObject;
    public GameObject currentObject;
    public GameObject previousObject;
    [HideInInspector] public GameObject lastPreviousObject;
    [HideInInspector] public GameObject moreLastPreviousObject;
    [HideInInspector] public GameObject veryLastPreviousObject;
    [HideInInspector] public GameObject absoluteLastPreviousObject;
    [HideInInspector] public GameObject extremePreviousObject;
    [HideInInspector] public GameObject superPreviousObject;

    public GameObject lastObject;

    private bool isFading = false;

    public static float cursorFillAmount;

    public float fillDelayTimer = 2.0f;
    public float fillDelayTimerOther = 0.5f;

    public string currentObjectName;
    public string currentNodeName;
    public Transform objectInHand;
    public Transform nodeLocation;

    private ProgressionCheck stageActivation;
    public GameObject stageActivationObject;

    public void Awake()
    {
        stageActivation = stageActivationObject.GetComponent<ProgressionCheck>();
        stageActivation.ActivateOneStageDeactivateTheRest(0);
    }

    public void Update()
    {

        RaycastHit hit;
        Ray ray;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(Camera.main.transform.position, forward, Color.red);

        if(TriggerCheck.progressCounter == 3)
        {
            stageActivation.ActivateOneStageDeactivateTheRest(1);
        }

        //if player is looking
        if (Physics.Raycast(ray, out hit))
        {

            var selection = hit.transform;
            if (selection.CompareTag("Puzzle") || selection.CompareTag("Clue") || selection.CompareTag("Selectable"))
            {
                nextObject = selection.gameObject;
                //if previous and current object are same
                if (GameObject.Equals(nextObject, currentObject))
                {
                        isFading = true;

                        ColorFade fadingColor = currentObject.GetComponent<ColorFade>();
                        fadingColor.FadeCheck(isFading);

                    currentObjectName = currentObject.name;

                    fillDelayTimer -= Time.deltaTime;

                    if (fillDelayTimer <= 0f)
                    {
                        FillCursor fillingCursor = GetComponent<FillCursor>();
                        fillingCursor.cursorToFill("Items");
                    }

                    if(cursorFillAmount >= 1)
                    {
                        fillDelayTimer = 2.0f;
                        cursorFillAmount = 0f;

                        ButtonSpawn buttonSpawn = GetComponent<ButtonSpawn>();
                        buttonSpawn.ButtonSpawning();
                    }

                    if (previousObject != null)
                    {
                        isFading = false;
                        ColorFade fadingcolor1 = previousObject.GetComponent<ColorFade>();
                        fadingcolor1.FadeCheck(isFading);
                    }

                    if (currentObject == moreLastPreviousObject)
                    {
                        moreLastPreviousObject = null;
                    }

                    if (currentObject == veryLastPreviousObject)
                    {
                        veryLastPreviousObject = null;
                    }

                    if (currentObject == absoluteLastPreviousObject)
                    {
                        absoluteLastPreviousObject = null;
                    }

                    if (currentObject == extremePreviousObject)
                    {
                        extremePreviousObject = null;
                    }

                    if (currentObject == superPreviousObject)
                    {
                        superPreviousObject = null;
                    }

                    if (GameObject.Equals(currentObject, lastPreviousObject))
                    {
                        lastPreviousObject = null;

                    }
                    else {
                        if (lastPreviousObject != null)
                        {
                            isFading = false;
                            ColorFade fadingcolor = lastPreviousObject.GetComponent<ColorFade>();
                            fadingcolor.FadeCheck(isFading);
                        }

                        if (moreLastPreviousObject != null)
                        {
                            isFading = false;
                            ColorFade fadingColor2 = moreLastPreviousObject.GetComponent<ColorFade>();
                            fadingColor2.FadeCheck(isFading);
                        }

                        if (veryLastPreviousObject != null)
                        {
                            isFading = false;
                            ColorFade fadingColor3 = veryLastPreviousObject.GetComponent<ColorFade>();
                            fadingColor3.FadeCheck(isFading);
                        }

                        if (absoluteLastPreviousObject != null)
                        {
                            isFading = false;
                            ColorFade fadingColor4 = absoluteLastPreviousObject.GetComponent<ColorFade>();
                            fadingColor4.FadeCheck(isFading);
                        }

                        if (extremePreviousObject != null)
                        {
                            isFading = false;
                            ColorFade fadingColor5 = extremePreviousObject.GetComponent<ColorFade>();
                            fadingColor5.FadeCheck(isFading);
                        }

                        if (superPreviousObject != null)
                        {
                            isFading = false;
                            ColorFade fadingColor6 = superPreviousObject.GetComponent<ColorFade>();
                            fadingColor6.FadeCheck(isFading);
                        }
                    }
                }
                //if previous and current object are different
                else
                {
                    superPreviousObject = extremePreviousObject;
                    extremePreviousObject = absoluteLastPreviousObject;
                    absoluteLastPreviousObject = veryLastPreviousObject;
                    veryLastPreviousObject = moreLastPreviousObject;
                    moreLastPreviousObject = lastPreviousObject;
                    lastPreviousObject = previousObject;
                    previousObject = currentObject;
                    currentObject = nextObject;

                    fillDelayTimer = 2.0f;

                    FillCursor fillingCursor2 = GetComponent<FillCursor>();
                    fillingCursor2.cursorToFill("NotLooking");
                }


            }
            //if detects other objects than the items
            else
            {
                isFading = false;
                lastObject = nextObject;
                if (lastObject != null)
                {
                    ColorFade fadingColor = lastObject.GetComponent<ColorFade>();
                    fadingColor.FadeCheck(isFading);
                }
            }


            var buttonSelection = hit.transform;
            if (buttonSelection.CompareTag("Yes") || buttonSelection.CompareTag("No"))
            {
                fillDelayTimerOther -= Time.deltaTime;

                if (fillDelayTimerOther <= 0f)
                {
                    FillCursor fillingCursorNode = GetComponent<FillCursor>();
                    fillingCursorNode.cursorToFill("Other");
                }

                if (buttonSelection.CompareTag("Yes"))
                {
                    if (cursorFillAmount >= 1)
                    {
                        ButtonSpawn buttonDespawn = GetComponent<ButtonSpawn>();
                        buttonDespawn.ButtonDespawn();

                        PickAndDrop pickUp = GetComponent<PickAndDrop>();
                        objectInHand = currentObject.transform;
                        pickUp.PickObject(objectInHand);

                        cursorFillAmount = 0f;
                        fillDelayTimerOther = 0.5f;
                    }
                }

                if(buttonSelection.CompareTag("No"))
                {
                    if (cursorFillAmount >= 1)
                    {
                        ButtonSpawn buttonDespawn = GetComponent<ButtonSpawn>();
                        buttonDespawn.ButtonDespawn();

                        cursorFillAmount = 0f;
                        fillDelayTimerOther = 0.5f;
                    }
                }
            }

            var nodeSelection = hit.transform;
            if((nodeSelection.CompareTag("PuzzleNode") && objectInHand?.tag == "Puzzle")|| (nodeSelection.CompareTag("ClueNode") && objectInHand?.tag == "Clue")|| (nodeSelection.CompareTag("PropNode") && objectInHand?.tag == "Selectable"))
            {
                currentNodeName = nodeSelection.name;
                nodeLocation = nodeSelection.transform;

                fillDelayTimerOther -= Time.deltaTime;

                if (fillDelayTimerOther <= 0f)
                {
                    FillCursor fillingCursorNode = GetComponent<FillCursor>();
                    fillingCursorNode.cursorToFill("Other");
                }

                if (cursorFillAmount >= 1)
                {

                    PickAndDrop dropObject = GetComponent<PickAndDrop>();
                    dropObject.PutDownObject(objectInHand, nodeLocation);

                    objectInHand = null;
                    currentObject = null;

                    cursorFillAmount = 0f;
                    fillDelayTimerOther = 0.5f;
                }

            }
        }
        //if the player is not looking 
        else
        {
            fillDelayTimer = 2.0f;
            fillDelayTimerOther = 0.5f;

            FillCursor fillingCursor3 = GetComponent<FillCursor>();
            fillingCursor3.cursorToFill("NotLooking");

            isFading = false;
            lastObject = nextObject;
            if (lastObject != null)
            {
                ColorFade fadingColor = lastObject.GetComponent<ColorFade>();
                fadingColor.FadeCheck(isFading);
            }

            if (moreLastPreviousObject != null)
            {
                isFading = false;
                ColorFade fadingColor2 = moreLastPreviousObject.GetComponent<ColorFade>();
                fadingColor2.FadeCheck(isFading);
            }

            if (veryLastPreviousObject != null)
            {
                isFading = false;
                ColorFade fadingColor3 = veryLastPreviousObject.GetComponent<ColorFade>();
                fadingColor3.FadeCheck(isFading);
            }

            if (absoluteLastPreviousObject != null)
            {
                isFading = false;
                ColorFade fadingColor4 = absoluteLastPreviousObject.GetComponent<ColorFade>();
                fadingColor4.FadeCheck(isFading);
            }

            if (extremePreviousObject != null)
            {
                isFading = false;
                ColorFade fadingColor5 = extremePreviousObject.GetComponent<ColorFade>();
                fadingColor5.FadeCheck(isFading);
            }

            if (superPreviousObject != null)
            {
                isFading = false;
                ColorFade fadingColor6 = superPreviousObject.GetComponent<ColorFade>();
                fadingColor6.FadeCheck(isFading);
            }
        }
    }
}