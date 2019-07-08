using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GvrButton : MonoBehaviour
{
    public Image cursor;
    public UnityEvent GVR_Click;
    public float Total_Time = 2f;
    bool GVR_Status;
    public float GVR_Timer;

    public Renderer test_object;

    Color[] colors = new Color[] { Color.white, Color.red, Color.green, Color.blue };
    private int currentColor, length;
    // Use this for initialization

    void Start()
    {
        test_object = GetComponent<Renderer>();

        currentColor = 0; //White
        length = colors.Length;
        test_object.material.color = colors[currentColor];
    }

    // Update is called once per frame
    void Update()
    {
        if (GVR_Status)
        {
            GVR_Timer += Time.deltaTime;
            cursor.fillAmount = GVR_Timer / Total_Time;
        }

        if (GVR_Timer > Total_Time)
        {
            GVR_Click.Invoke();
        }
    }

    public void GVR_On()
    {
        GVR_Status = true;
    }

    public void GVR_Off()
    {
        GVR_Status = false;
        GVR_Timer = 0;
        cursor.fillAmount = 0;
    }

    public void Change_Color()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                currentColor = (currentColor + 1) % length;
                test_object.material.color = colors[currentColor];
            }
    }
}
