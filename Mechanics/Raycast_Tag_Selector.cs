using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Raycast_Tag_Selector : MonoBehaviour, ISelector
{
    [SerializeField] public string Selectable_Tag = "Selectable";
    public Transform _selection;

    public Image cursor;
    public float Total_Time = 5f;
    bool VR_Status;
    public float VR_Timer;

    public void Check_Ray(Ray ray)
    {
        this._selection = null;
        if (Physics.Raycast(ray, out var hit))
        {
            VR_On();

            var selection = hit.transform;
            if (selection.CompareTag(this.Selectable_Tag))
            {
                this._selection = selection;
            }

            if (VR_Status)
            {
                VR_Timer += Time.deltaTime;
                cursor.fillAmount = VR_Timer / Total_Time;
            }
        }
        else
        {
            OutlineSelectionResponse.timeLeft = 5f;
            VR_Off();
        }
    }

    public void VR_On()
    {
        VR_Status = true;
    }

    public void VR_Off()
    {
        VR_Status = false;
        VR_Timer = 0;
        cursor.fillAmount = 0;
    }

    public Transform GetSelection()
    {
        return this._selection;
    }

}
