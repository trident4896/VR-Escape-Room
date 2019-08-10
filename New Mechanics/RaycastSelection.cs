using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastSelection : MonoBehaviour
{
    [HideInInspector] public GameObject nextObject;
    [HideInInspector] public GameObject currentObject;
    [HideInInspector] public GameObject previousObject;
    [HideInInspector] public GameObject lastPreviousObject;
    [HideInInspector] public GameObject moreLastPreviousObject;
    [HideInInspector] public GameObject veryLastPreviousObject;
    [HideInInspector] public GameObject absoluteLastPreviousObject;
    [HideInInspector] public GameObject extremePreviousObject;
    [HideInInspector] public GameObject superPreviousObject;

    public GameObject lastObject;

    private bool isFading = false;

    public static float cursorFillAmount;
    public static float cursorFillAmountOther;

    public float fillDelayTimer = 2.0f;
    public float fillDelayTimerOther = 0.5f;

    public string currentObjectName;
    public string currentNodeName;
    public Transform objectInHand;
    public Transform nodeLocation;

    private ProgressionCheck stageActivation;
    public GameObject stageActivationObject;

    [SerializeField] private Camera camera;
    [SerializeField] private ColorFade lastObjectHit;

    public void Awake()
    {
        stageActivation = stageActivationObject.GetComponent<ProgressionCheck>();
        stageActivation.ActivateOneStageDeactivateTheRest(0);

        if (!camera)
        {
            camera = Camera.main;
        }
    }

    public void Update()
    {

        RaycastHit hit;
        Ray ray;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(Camera.main.transform.position, forward, Color.red);

        //if player is looking
        if (Physics.Raycast(ray, out hit))
        {

            var selection = hit.transform;
            if (selection.CompareTag("Puzzle") || selection.CompareTag("Clue") || selection.CompareTag("Selectable"))
            {
                // if hitting same object do nothing
                if (lastObjectHit && hit.transform == lastObjectHit.transform) return;

                // if looking on different object start fade out previous if exists
                if (lastObjectHit != null)
                {
                    lastObjectHit.FadeOut();
                }

                // start fade in current
                var currentObject = hit.transform.GetComponent<ColorFade>();
                
                if (currentObject != null)
                {
                    currentObject.FadeIn();
                }

                lastObjectHit = currentObject;

                // update previous hit
                var buttonSelection = hit.transform;
                if (buttonSelection.CompareTag("Yes") || buttonSelection.CompareTag("No"))
                {
                    if (buttonSelection.CompareTag("Yes"))
                    {
                        if (cursorFillAmountOther >= 1)
                        {
                            ButtonSpawn buttonDespawn = GetComponent<ButtonSpawn>();
                            buttonDespawn.ButtonDespawn();

                            PickAndDrop pickUp = GetComponent<PickAndDrop>();
                            objectInHand = currentObject.transform;
                            pickUp.PickObject(objectInHand);

                            cursorFillAmountOther = 0f;
                            fillDelayTimerOther = 0.5f;
                        }
                    }

                    if (buttonSelection.CompareTag("No"))
                    {
                        if (cursorFillAmountOther >= 1)
                        {
                            ButtonSpawn buttonDespawn = GetComponent<ButtonSpawn>();
                            buttonDespawn.ButtonDespawn();

                            cursorFillAmountOther = 0f;
                            fillDelayTimerOther = 0.5f;
                        }
                    }
                }

                var nodeSelection = hit.transform;
                if (nodeSelection.CompareTag("PuzzleNode") || nodeSelection.CompareTag("ClueNode") || nodeSelection.CompareTag("PropNode"))
                {
                    currentNodeName = nodeSelection.name;
                    nodeLocation = nodeSelection.transform;

                    fillDelayTimerOther -= Time.deltaTime;

                    if (fillDelayTimerOther <= 0f)
                    {
                        FillCursor fillingCursorNode = GetComponent<FillCursor>();
                        //fillingCursorNode.cursorToFill("Other");
                    }

                    if (cursorFillAmountOther >= 1)
                    {

                        PickAndDrop dropObject = GetComponent<PickAndDrop>();
                        dropObject.PutDownObject(objectInHand, nodeLocation);

                        cursorFillAmountOther = 0f;
                        fillDelayTimerOther = 0.5f;
                    }

                }
            }
            //if the player is looking other objects
            else
            {
                valueReset();
            }
        }//if player is not looking
        else
        {
            valueReset();
        }
          
    }

    public void CursorReadyToFill()
    {
        fillDelayTimer -= Time.deltaTime;

        if (fillDelayTimer <= 0f)
        {
            FillCursor fillingCursor = GetComponent<FillCursor>();
            //fillingCursor.cursorToFill("Items");
        }

        if (cursorFillAmount >= 1)
        {
            fillDelayTimer = 2.0f;
            cursorFillAmount = 0f;

            ButtonSpawn buttonSpawn = GetComponent<ButtonSpawn>();
            buttonSpawn.ButtonSpawning();
        }

    }

    public void valueReset()
    {
        fillDelayTimer = 2.0f;
        fillDelayTimerOther = 0.5f;

        FillCursor fillingCursor3 = GetComponent<FillCursor>();
        //fillingCursor3.cursorToFill("NotLooking");

        // if looking at nothing start fadeout previous hit if exists
        if (lastObjectHit != null)
        {
            lastObjectHit.FadeOut();
        }
        // reset previous hit
        lastObjectHit = null;
    }
}