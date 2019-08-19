using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    public Transform hand;
    public Transform face;
    public bool isPickUp = false;

    public float itemLerpTimePick = 0f;
    public float itemLerpTimeDrop = 0f;

    private Transform saveTheItem;
    private Transform saveTheNode;

    public float speed = 1f;


    private float distanceToFace;
    private float distanceToHand;
    private float distanceToNode;

    public void Update()
    {
        if (isPickUp == true)
        {
            pickCheck(false);
        }
        else
        {
            pickCheck(true);
        }
    }

    public void PickObject(Transform theItem)
    {
        if (isPickUp == false)
        {
            saveTheItem = theItem;

            itemLerpTimePick = 0f;
            isPickUp = true;
        }
    }

    public void PutDownObject(Transform theItem, Transform nodeLocation)
    {
        if (isPickUp == true)
        {
            saveTheItem = theItem;
            saveTheNode = nodeLocation;

            itemLerpTimeDrop = 0f;
            isPickUp = false;
        }
    }

    public void pickCheck(bool pickStatus)
    {
        if(pickStatus == false)
        {
            LiftObject();
        }
        else
        {
            DropObject();
        }
    }

    public void LiftObject()
    {
        itemLerpTimePick += Time.deltaTime * speed;

        if (saveTheItem.tag == "Puzzle")
        {
            distanceToHand = Vector3.Distance(saveTheItem.position, hand.position);

            float fracDistance1 = itemLerpTimePick / distanceToHand;

            saveTheItem.position = Vector3.Lerp(saveTheItem.position, hand.position, fracDistance1);
        }

        if(saveTheItem.tag == "Clue" || saveTheItem.tag == "Selectable")
        {
            distanceToFace = Vector3.Distance(saveTheItem.position, face.position);

            float fracDistance2 = itemLerpTimePick / distanceToFace;

            saveTheItem.position = Vector3.Lerp(saveTheItem.position, face.position, fracDistance2);
        }

        if (itemLerpTimePick >= 1f)
        {
            itemLerpTimePick = 1f;
        }
    }

    public void DropObject()
    {
        if (saveTheItem != null)
        {
            itemLerpTimeDrop += Time.deltaTime * speed;

            distanceToNode = Vector3.Distance(saveTheItem.position, saveTheNode.position);

            float fracDistance3 = itemLerpTimeDrop / distanceToNode;

            saveTheItem.position = Vector3.Lerp(saveTheItem.position, saveTheNode.position, fracDistance3);   
        }

        if (itemLerpTimeDrop >= 1f)
        {
            itemLerpTimeDrop = 1f;
        }
    }
}
