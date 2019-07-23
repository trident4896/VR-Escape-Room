using UnityEngine;

public interface IHave_Selection_Response
{
    void SelectObject(Transform selection);

    void DeselectObject(Transform selection);

    void DrawOutline(Transform selection);
}
