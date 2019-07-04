using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class OutlineSelectionResponse : MonoBehaviour, IHave_Selection_Response
{
 

    public void SelectObject(Transform selection)
    {
        var outline = selection.GetComponent<Outline>();

            if (outline != null)
            {
                    outline.OutlineWidth = 10; 
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