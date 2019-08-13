using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    public Transform hand;
    public bool isPickUp = false;

    public void PickObject(Transform theItem)
    {
        if (isPickUp == false)
        {
            theItem.transform.parent = hand.transform;
            theItem.transform.localPosition = hand.transform.localPosition;
            
            isPickUp = true;
        }
    }

    public void PutDownObject(Transform theItem, Transform nodeLocation)
    {
        if (isPickUp == true)
        {
            theItem.transform.parent = null;
            theItem.transform.position = nodeLocation.transform.position;
            isPickUp = false;
        }
    }
}
