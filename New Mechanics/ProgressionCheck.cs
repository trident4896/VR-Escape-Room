using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionCheck : MonoBehaviour
{
    public static int puzzleCounter = 0;

    [System.Serializable]
    public class Item
    {
        public GameObject[] puzzles;
        public GameObject[] clues;
        public GameObject[] props;

        public GameObject[] puzzleStartNodes;
        public GameObject[] puzzleEndNodes;
        public GameObject[] clueNodes;
        public GameObject[] propNodes;

        public void SetActive(bool activate)
        {
            foreach(var puzzle in puzzles)
            {
                puzzle.GetComponent<BoxCollider>().enabled = activate;

                if (puzzle.GetComponent<BoxCollider>().enabled == true)
                {
                    puzzleCounter += 1;
                }
            }

            foreach (var clue in clues)
            {
                clue.GetComponent<BoxCollider>().enabled = activate;
            }

            foreach(var prop in props)
            {
                prop.GetComponent<BoxCollider>().enabled = activate;
            }

            foreach (var puzzleStartNode in puzzleStartNodes)
            {
                puzzleStartNode.GetComponent<BoxCollider>().enabled = activate;
            }

            foreach (var puzzleEndNode in puzzleEndNodes)
            {
                puzzleEndNode.GetComponent<BoxCollider>().enabled = activate;
            }

            foreach (var clueNode in clueNodes)
            {
                clueNode.GetComponent<BoxCollider>().enabled = activate;
            }

            foreach (var propNode in propNodes)
            {
                propNode.GetComponent<BoxCollider>().enabled = activate;
            }
        }

    }

    public Item[] Stages;
    [HideInInspector]public int saveTheProgressCounter;
    private bool isStageLoad = false;
    public static int stageNum = 0;

    private void Awake()
    {
        ActivateOneStageDeactivateTheRest(stageNum);
    }

    private void Update()
    {
        if (isStageLoad == false)
        {
            StageChecking();
        }
        else if(isStageLoad == true)
        {
            if(saveTheProgressCounter == puzzleCounter)
            {
                isStageLoad = false;
            }
        }
    }

    public void ActivateOneStageDeactivateTheRest(int stageID)
    {
        for(int i = 0; i < Stages.Length; i++)
        {
            Stages[i].SetActive(i == stageID);
        }
    }

    public void ObjectDeactivation(GameObject currentObject)
    {
        currentObject.transform.GetComponent<BoxCollider>().enabled = false;
    }

    public void ObjectActivation(GameObject targetObject)
    {
        targetObject.transform.GetComponent<BoxCollider>().enabled = true;
    }


    public void StageChecking()
    {
        if(saveTheProgressCounter == puzzleCounter)
        {
            stageNum += 1;
            ActivateOneStageDeactivateTheRest(stageNum);
            isStageLoad = true;
        }
    }

}
