using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationManager : MonoBehaviour
{
    public GameObject[] stage1_Puzzle;
    public GameObject[] stage2_Puzzle;
    public GameObject[] stage3_Puzzle;

    public GameObject[] stage1_clue;
    public GameObject[] stage2_clue;
    public GameObject[] stage3_clue;

    public GameObject[] stage1_prop;
    public GameObject[] stage2_prop;
    public GameObject[] stage3_prop;

    public GameObject[] stage1_OrignalNode;
    public GameObject[] stage2_OriginalNode;
    public GameObject[] stage3_OriginalNode;

    public GameObject[] stage1_EndNode;
    public GameObject[] stage2_EndNode;
    public GameObject[] stage3_EndNode;

    public static int stage1_puzzle_count= 0;
    public static int stage2_puzzle_count = 0;

    void Start()
    {
        stage1_activation();
    }

    void Update()
    {
        //if stage 1 is done, activate stage 2 objects
       if (stage1_puzzle_count == 3 && TriggerManager.Stage1_ProgressCheck)
        {
            stage2_activation();
        }
    }

    //stage 2 objects 
    void stage2_activation()
    {
        stage1_Puzzle[0].GetComponent<BoxCollider>().enabled = false;
        stage1_Puzzle[1].GetComponent<BoxCollider>().enabled = false;
        stage1_Puzzle[2].GetComponent<BoxCollider>().enabled = false;

        stage2_Puzzle[0].GetComponent<BoxCollider>().enabled = true;
        stage2_Puzzle[1].GetComponent<BoxCollider>().enabled = true;
        stage2_Puzzle[2].GetComponent<BoxCollider>().enabled = true;

        stage1_clue[0].GetComponent<BoxCollider>().enabled = false;
        stage1_clue[1].GetComponent<BoxCollider>().enabled = false;
        stage1_clue[2].GetComponent<BoxCollider>().enabled = false;

        stage2_clue[0].GetComponent<BoxCollider>().enabled = true;
        stage2_clue[1].GetComponent<BoxCollider>().enabled = true;
        stage2_clue[2].GetComponent<BoxCollider>().enabled = true;

        stage1_prop[0].GetComponent<BoxCollider>().enabled = false;
        stage1_prop[1].GetComponent<BoxCollider>().enabled = false;

        stage2_prop[0].GetComponent<BoxCollider>().enabled = true;
        stage2_prop[1].GetComponent<BoxCollider>().enabled = true;
    }

    //stage 1 objects
    void stage1_activation()
    {
        stage1_Puzzle[0].GetComponent<BoxCollider>().enabled = true;
        stage1_Puzzle[1].GetComponent<BoxCollider>().enabled = true;
        stage1_Puzzle[2].GetComponent<BoxCollider>().enabled = true;

        stage2_Puzzle[0].GetComponent<BoxCollider>().enabled = false;
        stage2_Puzzle[1].GetComponent<BoxCollider>().enabled = false;
        stage2_Puzzle[2].GetComponent<BoxCollider>().enabled = false;

        stage1_clue[0].GetComponent<BoxCollider>().enabled = true;
        stage1_clue[1].GetComponent<BoxCollider>().enabled = true;
        stage1_clue[2].GetComponent<BoxCollider>().enabled = true;

        stage2_clue[0].GetComponent<BoxCollider>().enabled = false;
        stage2_clue[1].GetComponent<BoxCollider>().enabled = false;
        stage2_clue[2].GetComponent<BoxCollider>().enabled = false;

        stage1_prop[0].GetComponent<BoxCollider>().enabled = true;
        stage1_prop[1].GetComponent<BoxCollider>().enabled = true;

        stage2_prop[0].GetComponent<BoxCollider>().enabled = false;
        stage2_prop[1].GetComponent<BoxCollider>().enabled = false;
    }
}
