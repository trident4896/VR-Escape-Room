using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    private Transform Current_Selection;

    public static float ray_DelayTimer = 2f;

    private IRayProvider _RayProvider;
    private ISelector _selector;
    private IHave_Selection_Response _selectionresponse;

    private void Awake()
    {
        _RayProvider = GetComponent<IRayProvider>();
        _selector = GetComponent<ISelector>();
        _selectionresponse = GetComponent<IHave_Selection_Response>();
    }

    private void Update()
    {
        //Deselect
            Deselection();

        //Generate and Check Ray
         _selector.Check_Ray(_RayProvider.Create_Ray());
         Current_Selection = _selector.GetSelection();
        
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(Camera.main.transform.position, forward, Color.red);

        //Select
        Selection();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void Selection()
    {
        if (Current_Selection != null)
        {
            _selectionresponse.SelectObject(Current_Selection);       
        }
    }

    public void Deselection()
    {
        if (Current_Selection != null)
        {
            _selectionresponse.DeselectObject(Current_Selection);
        }
    }
}
