using UnityEngine;

public class MouseScreenRayProvider : MonoBehaviour, IRayProvider
{
    public Ray Create_Ray()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}