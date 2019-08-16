using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
   
    [SerializeField] private GameObject itemCollide;
    [SerializeField] private GameObject nodeCollide;
    public static int progressCounter = 0;

    public ProgressionCheck saveProgressCounter;
    public GameObject saveProgressCounterObject;

    public GameObject targetPuzzle;

    public void Awake()
    {
        saveProgressCounter = saveProgressCounterObject.GetComponent<ProgressionCheck>();
    }

    public void OnTriggerEnter(Collider other)
    {
        itemCollide = null;
        itemCollide = other.transform.gameObject;

        if(GameObject.Equals(targetPuzzle, itemCollide))
        {
            other.enabled = false;
            progressCounter += 1;
        }

        /*if((this.transform.gameObject.name == "EN Puzzle 01" && itemCollide == "Puzzle 01") || (this.transform.gameObject.name == "EN Puzzle 02" && itemCollide == "Puzzle 02") || (this.transform.gameObject.name == "EN Special Puzzle 01" && other.transform.gameObject.name == "Special Puzzle 01"))
        {
            other.enabled = false;
            progressCounter += 1;
        }*/

        saveProgressCounter.saveTheProgressCounter = progressCounter;
    }
}
