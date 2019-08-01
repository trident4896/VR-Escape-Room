using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;


public class OutlineSelectionResponse : MonoBehaviour, IHave_Selection_Response
{
    [SerializeField] public static float timeLeft = 2f;
    [SerializeField] public static float timeDelay = 2f;

    public int redCol;
    public int blueCol;
    public int greenCol;
    public bool FlashingIn = true;
    public bool startedFlashing = false;
    private bool isCoroutineStarted = false;

    public Color oriColor;

    public void SelectObject(Transform selection)
    {

        /*var outline = selection.GetComponent<Outline>();
        
        if (outline != null)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                DrawOutline(selection); 
            }
        }*/

        //Assign the original color of the gameobject to a variable
        oriColor = selection.GetComponent<Renderer>().material.color;


        //check whether if the player is already holding an object, if no, proceed to highlight the object
        if(Raycast_Tag_Selector.pickup_count == true) { 

            //Get the color of the object and assign a new color to it
            Color temp_color = selection.GetComponent<Renderer>().material.color;
            temp_color = new Color32((byte)redCol, (byte)blueCol, (byte)blueCol, 255);
            selection.GetComponent<Renderer>().material.color = temp_color;

            //check if the object is flashing
            if (startedFlashing == false)
            {
                //the object is now allowed to flash
                startedFlashing = true;

                //call the coroutine will only call once in the update
                if (!isCoroutineStarted)
                {
                        StartCoroutine(FlashObject());
                }
            }

        }

    }

    public void DrawOutline (Transform selection)
    {
        /*var outline = selection.GetComponent<Outline>();

        if (Raycast_Tag_Selector.pickup_count == true)
        {
          
            /*outline.OutlineWidth = 10;
            if (selection.CompareTag("Selectable"))
            {
                outline.OutlineColor = Color.yellow;
            }
            else if (selection.CompareTag("Puzzle"))
            {
                outline.OutlineColor = Color.green;
            }
            else if (selection.CompareTag("Clue"))
            {
                outline.OutlineColor = Color.blue;
            }
            else if (selection.CompareTag("Yes") || selection.CompareTag("No"))
            {
                outline.OutlineColor = Color.red;
            }
        }*/
    }

    public void DeselectObject(Transform selection)
    {
        /*var outline = selection.GetComponent<Outline>();

        if (outline != null)
        {
            outline.OutlineWidth = 0;
        }*/

        //reset the object back to original state
        startedFlashing = false;
        StopCoroutine(FlashObject());
        selection.GetComponent<Renderer>().material.color = oriColor;
    }

    IEnumerator FlashObject()
    {
        //coroutine only call once in update
        isCoroutineStarted = true;

        
        while(Raycast_Tag_Selector.pickup_count == true)
        {
            //flashing coroutine started
            yield return new WaitForSeconds(0.05f);

            //create a loop of color changing to create a flashing effect as long as player is looking at the object

            //if the object is allowed to flash
            if(FlashingIn == true)
            {
                if(blueCol <= 30)
                {
                    FlashingIn = false;
                }
                else
                {
                    blueCol -= 25;
                    greenCol -= 1;
                }
            }

            //if the is not allowed to flash
            if (FlashingIn == false)
            {
                if (blueCol >= 250)
                {
                    FlashingIn = true;
                }
                else
                {
                    blueCol += 25;
                    greenCol += 1;
                }
                    
            }
        }

        //reset the coroutine
        isCoroutineStarted = false;
    }
}