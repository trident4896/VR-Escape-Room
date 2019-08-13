using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    [SerializeField] private string itemCollide;
    public static int progressCounter = 0;

    public void OnTriggerEnter(Collider other)
    {
        itemCollide = null;
        itemCollide = other.transform.gameObject.name;

        Debug.Log(itemCollide);

        if((this.transform.gameObject.name == "EN Puzzle 01" && itemCollide == "Puzzle 01") || (this.transform.gameObject.name == "EN Puzzle 02" && itemCollide == "Puzzle 02") || (this.transform.gameObject.name == "EN Special Puzzle 01" && other.transform.gameObject.name == "Special Puzzle 01"))
        {
            other.enabled = false;
            progressCounter += 1;
        }

        Debug.Log(progressCounter);
    }

}
