using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComponent : MonoBehaviour
{
    public GameObject[] arrayEnableObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arrayEnableObj[0].GetComponent<BoxCollider>().enabled = true;
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            arrayEnableObj[0].GetComponent<BoxCollider>().enabled = false;
        }

    }
}
