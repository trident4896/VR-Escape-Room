using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    private Transform Current_Selection;

    public bool isRunning = false;
   
    private IRayProvider _RayProvider;
    private ISelector _selector;
    private IHave_Selection_Response _selectionresponse;

    private void Awake()
    {
        _RayProvider = GetComponent<IRayProvider>();
        _selector = GetComponent<ISelector>();
        _selectionresponse = GetComponent<IHave_Selection_Response>();
    }

    /*IEnumerator HighlightDelay () { 
        yield return new WaitForSeconds(3f);
        Debug.Log("Check");
    }*/
    
    private void Start()
    {

    }

    private void Update()
    {
            //Deselect
            if (Current_Selection != null)
            {
                _selectionresponse.DeselectObject(Current_Selection);
                //StopCoroutine(HighlightDelay());
             }

            //Generate and Check Ray
            _selector.Check_Ray(_RayProvider.Create_Ray());
            Current_Selection = _selector.GetSelection();

            //Select
            if (Current_Selection != null)
            {
                 _selectionresponse.SelectObject(Current_Selection);
                 //StartCoroutine(HighlightDelay());
        }



        }
}
