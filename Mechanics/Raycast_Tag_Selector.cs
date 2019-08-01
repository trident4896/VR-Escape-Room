
using UnityEngine.Events;
using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class Raycast_Tag_Selector : objectClass, ISelector
{
    [HideInInspector] public string Selectable_Tag = "Selectable";
    [HideInInspector] public string Puzzle_Tag = "Puzzle";
    [HideInInspector] public string Clue_Tag = "Clue";
    [HideInInspector] public string Yes_Tag = "Yes";
    [HideInInspector] public string No_Tag = "No";
    [HideInInspector] public string Node_Tag = "Node";
    [HideInInspector] public string PuzzleNode_Tag = "PuzzleNode";
    [HideInInspector] public string ClueNode_Tag = "ClueNode";
    [HideInInspector] public string PropNode_Tag = "PropNode";

    public GameObject button_prefab;
    public Transform Camera_object;
    bool spawn_check = false;
    float spawnDistance = 1.5f;

    public string Current_Object;
    public string Previous_Object;
    public bool save_objectData = true;

    public string Current_tag;
    public string Next_tag;

    public Transform selected_item;
    GameObject nodes;
    Transform itemLocation;
    Transform nodeLocation;
    float dropDistance = 1f;
    public static bool pickup_count = true;

    private bool button_check = false;

    public Transform _selection;
   
    public Image cursor;
    float Total_Time = 2f;
    bool VR_Status;
    float VR_Timer;
    float delay_Timer = 3.5f;
    float select_time = 0.5f;

    public void Start()
    {
        cursor.enabled = false;
        pickup_count = true;
        save_objectData = true;
    }

    public void Check_Ray(Ray ray)
    { 
        Vector3 playerPos = Camera_object.position;
        Vector3 playerDirection = Camera_object.forward;
        Quaternion playerRotation = Camera_object.rotation;
        Vector3 spawnPos = playerPos + (playerDirection * spawnDistance);

        //reset the object that raycast detects
        this._selection = null;

        //if raycast detects object
        if (Physics.Raycast(ray, out var hit))
        {
            if (save_objectData == true)
            {
                Previous_Object = hit.transform.gameObject.name;
                save_objectData = false;
            }
            else
            {
                Current_Object = hit.transform.gameObject.name;
                save_objectData = true;
            }

            if (Current_Object == Previous_Object)
            {
                //activate the status to check which object that ray is pointing to
                VR_On();

                //check which object that ray is pointing to
                var selection = hit.transform;
                if (selection.CompareTag(this.Selectable_Tag) || selection.CompareTag(this.Puzzle_Tag) || selection.CompareTag(this.Clue_Tag) || selection.CompareTag(this.No_Tag) || selection.CompareTag(this.Yes_Tag) || selection.CompareTag(this.PuzzleNode_Tag) || selection.CompareTag(this.ClueNode_Tag) || selection.CompareTag(this.PropNode_Tag))
                {

                    //delay for the highlight to happen
                    OutlineSelectionResponse.timeDelay -= Time.deltaTime;

                    if (OutlineSelectionResponse.timeDelay <= 0)
                    {
                        //the object that ray is pointing to
                        this._selection = selection;
                    }
                }

                //if ray is pointing to three kinds of objects (puzzles, clues and props), an fill cursor will appear
                if (selection.CompareTag(this.Selectable_Tag) || selection.CompareTag(this.Puzzle_Tag) || selection.CompareTag(this.Clue_Tag))
                {
                    if (VR_Status)
                    {
                        //delay for the fill cursor to appear
                        delay_Timer -= Time.deltaTime;

                        //fill cursor begins to fill up after delay finished count down and if player is not holding any object in hand
                        if (delay_Timer <= 0 && pickup_count == true)
                        {
                            //fill cursor slowly fills up
                            VR_Timer += Time.deltaTime;
                            cursor.fillAmount = VR_Timer / Total_Time;

                            //once cursor finished fill up, it will disappear
                            if (cursor.fillAmount == 1)
                            {
                                cursor.enabled = false;
                            }
                        }
                    }
                }

                //if player is holding an object, only three kinds of nodes will have fill cursor to appear
                if (pickup_count == false && ((selection.CompareTag(this.PuzzleNode_Tag) && Current_tag == Puzzle_Tag) || (selection.CompareTag(this.ClueNode_Tag) && Current_tag == Clue_Tag) || (selection.CompareTag(this.PropNode_Tag) && Current_tag == Selectable_Tag)))
                {
                    VR_Timer += Time.deltaTime;
                    cursor.fillAmount = VR_Timer / Total_Time;

                    if (cursor.fillAmount == 1)
                    {
                        cursor.enabled = false;
                    }
                }

                //during selection of yes and no, the fill cursor fills up faster
                if (selection.CompareTag(this.Yes_Tag) || selection.CompareTag(this.No_Tag))
                {
                    VR_Timer += Time.deltaTime;
                    cursor.fillAmount = VR_Timer / select_time;

                    if (cursor.fillAmount == 1)
                    {
                        cursor.enabled = false;
                    }
                }

                //during yes and no, once the cursor fills up, do respective actions
                if (VR_Timer > select_time)
                {
                    //if yes, pick up objects
                    if (selection.CompareTag(this.Yes_Tag))
                    {
                        InMyHands(itemLocation);
                        button_check = false;
                        Destroy(button_prefab);
                    }
                    //if no, do nothing to the object
                    else if (selection.CompareTag(this.No_Tag))
                    {
                        button_check = false;
                        Destroy(button_prefab);
                    }
                }

                //when the player stares at three kinds of objects (puzzles, clues and props), the fill cursor will appears to fill up
                //Once the cursor finished fill up, a yes and no button will appear
                if (VR_Timer > Total_Time)
                {
                    if (selection.CompareTag(this.Puzzle_Tag) || selection.CompareTag(this.Clue_Tag) || selection.CompareTag(this.Selectable_Tag))
                    {
                        if (!spawn_check)
                        {
                            if (!button_check)
                            {
                                if (pickup_count == true)
                                {

                                    var button_asset = Resources.Load("YN_Button") as GameObject;
                                    button_prefab = Instantiate(button_asset, spawnPos, playerRotation);

                                    spawn_check = true;
                                    button_check = true;
                                }
                            }
                        }
                    }

                    //save the location and tag of one of the three kinds of objects (puzzles, clues and props) that player selected
                    if (selection.CompareTag(this.Selectable_Tag) || selection.CompareTag(this.Puzzle_Tag) || selection.CompareTag(this.Clue_Tag))
                    {
                        itemLocation = selection;
                        Current_tag = selection.tag;
                    }

                    //puzzle can only fit into puzzle node
                    if (selection.CompareTag(this.PuzzleNode_Tag) && Current_tag == Puzzle_Tag)
                    {
                        nodeLocation = selection;
                        Next_tag = selection.tag;
                        OnTheNodes(nodeLocation, itemLocation);
                    }

                    //clue can only fit into clue node
                    if (selection.CompareTag(this.ClueNode_Tag) && Current_tag == Clue_Tag)
                    {
                        nodeLocation = selection;
                        Next_tag = selection.tag;
                        OnTheNodes(nodeLocation, itemLocation);
                    }

                    //prop can only fit into prop node
                    if (selection.CompareTag(this.PropNode_Tag) && Current_tag == Selectable_Tag)
                    {
                        nodeLocation = selection;
                        Next_tag = selection.tag;
                        OnTheNodes(nodeLocation, itemLocation);
                    }
                }
            }
            else
            {
                //reset all the status when the player is not looking anymore
                VR_Off();
            }

        }
        else
        {
            VR_Off();
        }
    }

    public void InMyHands(Transform the_Item)
    {
        //if the player is not holding an object in hand, proceed to pickup
        if (pickup_count == true)
        {
            //item pickup and player is not allowed to pickup another object in hand
            the_Item.transform.parent = selected_item.transform;
            the_Item.transform.localPosition = selected_item.transform.localPosition;
            pickup_count = false;
        }

    }

    public void OnTheNodes(Transform nodes_location, Transform the_Item)
    {
        //put down object
        the_Item.transform.parent = null;
        the_Item.transform.position = nodes_location.transform.position;

        //reset the status of button spawn and allow player to be able to pickup again
        spawn_check = true;
        pickup_count = true;
    }

    //when player is looking at object
    public void VR_On()
    {
        VR_Status = true;
        cursor.enabled = true;
    }

    //when player is not looking at object
    public void VR_Off()
    {
        VR_Status = false;
        VR_Timer = 0;
        cursor.fillAmount = 0;
        Total_Time = 2f;
        spawn_check = false;
        delay_Timer = 3.5f;
        select_time = 0.5f;
        cursor.enabled = false;
        OutlineSelectionResponse.timeDelay = 2f;

        //OutlineSelectionResponse.timeLeft = 2f;

    }

    //get the object that player is looking at
    public Transform GetSelection()
    {
        return this._selection;
    }

}
