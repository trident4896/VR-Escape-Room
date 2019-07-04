using UnityEngine;

internal class HighlightSelectionResponse : MonoBehaviour, IHave_Selection_Response
{
    [SerializeField] public Material Highlight_Material;
    [SerializeField] public Material Default_Material;

    //Select
    public void SelectObject(Transform selection)
    {
        var Selection_Renderer = selection.GetComponent<Renderer>();
        if (Selection_Renderer != null)
        {
            Selection_Renderer.material = this.Highlight_Material;
        }
    }

    //Deslect
    public void DeselectObject(Transform selection)
    {
        var Selection_Renderer = selection.GetComponent<Renderer>();
        if (Selection_Renderer != null)
        {
            Selection_Renderer.material = this.Default_Material;
        }
    }
}