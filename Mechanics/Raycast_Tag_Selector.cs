using UnityEngine;

public class Raycast_Tag_Selector : MonoBehaviour, ISelector
{
    [SerializeField] public string Selectable_Tag = "Selectable";
    public Transform _selection;

    public void Check_Ray(Ray ray)
    {
        this._selection = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(this.Selectable_Tag))
            {
                this._selection = selection;
            }
        }
    }

    public Transform GetSelection()
    {
        return this._selection;
    }

}
