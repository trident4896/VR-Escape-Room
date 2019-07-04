using UnityEngine;

internal interface IHave_Selection_Response
{
    void SelectObject(Transform selection);

    void DeselectObject(Transform selection);
}
