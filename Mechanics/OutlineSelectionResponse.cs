using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;


public class OutlineSelectionResponse : MonoBehaviour, IHave_Selection_Response
{
    [SerializeField] public static float timeLeft = 2f;

    public void SelectObject(Transform selection)
    {

        var outline = selection.GetComponent<Outline>();
        
        if (outline != null)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                DrawOutline(selection); 
            }
        }
    }

    public void DrawOutline (Transform selection)
    {
        var outline = selection.GetComponent<Outline>();

        if (Raycast_Tag_Selector.pickup_count == true)
        {
            outline.OutlineWidth = 10;
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
        }
    }

     public void DeselectObject(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();

        if (outline != null)
        {
            outline.OutlineWidth = 0;
        }
        
    }

}