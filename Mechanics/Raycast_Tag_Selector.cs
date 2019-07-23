
using UnityEngine.Events;
using System;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class Raycast_Tag_Selector : objectClass, ISelector
{
    [SerializeField] public string Selectable_Tag = "Selectable";
    [SerializeField] public string Puzzle_Tag = "Puzzle";
    [SerializeField] public string Clue_Tag = "Clue";
    [SerializeField] public string Yes_Tag = "Yes";
    [SerializeField] public string No_Tag = "No";
    [SerializeField] public string Node_Tag = "Node";
    [SerializeField] public string PuzzleNode_Tag = "PuzzleNode";
    [SerializeField] public string ClueNode_Tag = "ClueNode";
    [SerializeField] public string PropNode_Tag = "PropNode";

    public GameObject[] stageOneObj;
    public GameObject[] stageTwoObj;
    public GameObject[] stageThreeObj;

    public objectClass puzzle_info;
    string[] puzzle_nameHolder;
    bool[] puzz_activation_check;
    public BoxCollider puzzCollider1;

    public objectClass clue_info;
    string[] clue_nameHolder;
    bool[] clue_activation_check;
    public BoxCollider clueCollider1;

    public objectClass prop_info;
    string[] prop_nameHolder;
    bool[] prop_activation_check;
    public BoxCollider propCollider1;

    public GameObject puzzle1;
    public GameObject clue1;
    public GameObject prop1;

    public GameObject button_prefab;
    public Transform Camera_object;
    bool spawn_check = false;
    float spawnDistance = 1.5f;

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
    float delay_Timer = 1f;

    public void Start()
    {
        pickup_count = true;

        puzzle_nameHolder = new string[3];
        puzz_activation_check = new bool[3];

        clue_nameHolder = new string[3];
        clue_activation_check = new bool[3];

        prop_nameHolder = new string[3];
        prop_activation_check = new bool[3];

        if ((puzzle_info == null) && (GetComponent<objectClass>() != null))
        {
            puzzle_info = GetComponent<objectClass>();
            puzzle1 = GameObject.FindWithTag("Puzzle");
            puzzle_nameHolder[0] = puzzle_info.myPuzzle1.puzzle_name;
            puzz_activation_check[0] = puzzle_info.myPuzzle1.puzz_activation;

            puzzCollider1 = puzzle1.AddComponent<BoxCollider>();
            puzzCollider1.enabled = true;
        }
        else
        {
            Debug.Log("Missing Puzzle File");
        }

        if ((clue_info == null) && (GetComponent<objectClass>() != null))
        {
            clue_info = GetComponent<objectClass>();
            clue1 = GameObject.FindWithTag("Clue");
            clue_nameHolder[0] = clue_info.myClue1.clue_name;
            clue_activation_check[0] = clue_info.myClue1.clue_activation;

            clueCollider1 = clue1.AddComponent<BoxCollider>();
            clueCollider1.enabled = false;
        }
        else
        {
            Debug.Log("Missing Clue File");
        }

        if ((prop_info == null) && (GetComponent<objectClass>() != null))
        {
            prop_info = GetComponent<objectClass>();
            prop1 = GameObject.FindWithTag("Selectable");
            prop_nameHolder[0] = prop_info.myProp1.prop_name;
            prop_activation_check[0] = clue_info.myProp1.prop_activation;

            propCollider1 = prop1.AddComponent<BoxCollider>();
            propCollider1.enabled = false;
        }
        else
        {
            Debug.Log("Missing Prop File");
        }

    }

    public void Check_Ray(Ray ray)
    { 
        Vector3 playerPos = Camera_object.position;
        Vector3 playerDirection = Camera_object.forward;
        Quaternion playerRotation = Camera_object.rotation;
        Vector3 spawnPos = playerPos + (playerDirection * spawnDistance);

        Check_Collider_Active();

        this._selection = null;

        if (Physics.Raycast(ray, out var hit))
        { 
            VR_On();

            var selection = hit.transform;
            if (selection.CompareTag(this.Selectable_Tag) || selection.CompareTag(this.Puzzle_Tag) || selection.CompareTag(this.Clue_Tag) || selection.CompareTag(this.No_Tag) || selection.CompareTag(this.Yes_Tag) || selection.CompareTag(this.PuzzleNode_Tag) || selection.CompareTag(this.ClueNode_Tag) || selection.CompareTag(this.PropNode_Tag))
            {
                this._selection = selection;
            }

            if (selection.CompareTag(this.Selectable_Tag) || selection.CompareTag(this.Puzzle_Tag) || selection.CompareTag(this.Clue_Tag) || selection.CompareTag(this.No_Tag) || selection.CompareTag(this.Yes_Tag) || selection.CompareTag(this.PuzzleNode_Tag) || selection.CompareTag(this.ClueNode_Tag) || selection.CompareTag(this.PropNode_Tag))
            {
                if (VR_Status)
                {
                    delay_Timer -= Time.deltaTime;
                   
                    if (delay_Timer <= 0 && pickup_count == true)
                    {
                        VR_Timer += Time.deltaTime;
                        cursor.fillAmount = VR_Timer / Total_Time;
                        
                        if(cursor.fillAmount == 1)
                        {
                            cursor.enabled = false;
                        }
                    }
                    else if (pickup_count == false && ((selection.CompareTag(this.PuzzleNode_Tag) && Current_tag == Puzzle_Tag) || (selection.CompareTag(this.ClueNode_Tag) && Current_tag == Clue_Tag) || (selection.CompareTag(this.PropNode_Tag) && Current_tag == Selectable_Tag)))
                    {
                        VR_Timer += Time.deltaTime;
                        cursor.fillAmount = VR_Timer / Total_Time;

                        if (cursor.fillAmount == 1)
                        {
                            cursor.enabled = false;
                        }
                    }
                }
            }

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
                if (selection.CompareTag(this.Selectable_Tag) || selection.CompareTag(this.Puzzle_Tag) || selection.CompareTag(this.Clue_Tag))
                {
                    itemLocation = selection;
                    Current_tag = selection.tag;
                    Debug.Log(Current_tag);
                }

                if (selection.CompareTag(this.Yes_Tag))
                {
                        InMyHands(itemLocation);
                        button_check = false;
                        Destroy(button_prefab);
                }
                else if (selection.CompareTag(this.No_Tag))
                {
                    button_check = false;
                    Destroy(button_prefab);
                }

                if (selection.CompareTag(this.PuzzleNode_Tag) && Current_tag == Puzzle_Tag)
                {
                        nodeLocation = selection;
                        Next_tag = selection.tag;
                        Debug.Log(Next_tag);
                        OnTheNodes(nodeLocation, itemLocation);
                        clueCollider1.enabled = true;
                }

                if (selection.CompareTag(this.ClueNode_Tag) && Current_tag == Clue_Tag)
                {
                    nodeLocation = selection;
                    Next_tag = selection.tag;
                    Debug.Log(Next_tag);
                    OnTheNodes(nodeLocation, itemLocation);
                    propCollider1.enabled = true;
                }

                if (selection.CompareTag(this.PropNode_Tag) && Current_tag == Selectable_Tag)
                {
                    nodeLocation = selection;
                    Next_tag = selection.tag;
                    Debug.Log(Next_tag);
                    OnTheNodes(nodeLocation, itemLocation);
                }
            }
        }
        else
        { 
            VR_Off();   
        }


    }

    public void InMyHands(Transform the_Item)
    {
        if (pickup_count == true)
        {
            the_Item.transform.parent = selected_item.transform;
            the_Item.transform.localPosition = selected_item.transform.localPosition;
            pickup_count = false;
        }

    }

    public void OnTheNodes(Transform nodes_location, Transform the_Item)
    {
        the_Item.transform.parent = null;
        the_Item.transform.position = nodes_location.transform.position;

        spawn_check = true;
        pickup_count = true;

    }

    public void VR_On()
    {
        VR_Status = true;
        cursor.enabled = true;
    }

    public void VR_Off()
    {
        VR_Status = false;
        VR_Timer = 0;
        cursor.fillAmount = 0;
        OutlineSelectionResponse.timeLeft = 2f;
        spawn_check = false;
        delay_Timer = 1f;
        cursor.enabled = true;
    }

    public void Check_Collider_Active()
    {

    }

    public Transform GetSelection()
    {
        return this._selection;
    }

}
