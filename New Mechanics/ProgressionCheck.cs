using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionCheck : MonoBehaviour
{
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

    public void ActivateOneStageDeactivateTheRest(int stageID)
    {
        for(int i = 0; i < Stages.Length; i++)
        {

            Debug.Log(i == stageID);
            Stages[i].SetActive(i == stageID);
        }
       
    }

}
