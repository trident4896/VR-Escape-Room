using UnityEngine;

public interface ISelector
{
    void Check_Ray(Ray ray);
    Transform GetSelection();
}