using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;


public class OutlineSelectionResponse : MonoBehaviour, IHave_Selection_Response
{
    [SerializeField] public static float timeLeft = 5f;

    public void SelectObject(Transform selection)
    {

        var outline = selection.GetComponent<Outline>();

        if (outline != null)
        {
            timeLeft -= Time.deltaTime;
            Debug.Log(timeLeft);

            if (timeLeft <= 0)
            {
                DrawOutline(selection);
            }
        }
    }

    public void DrawOutline (Transform selection)
    {
        var outline = selection.GetComponent<Outline>();

        outline.OutlineWidth = 10;
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