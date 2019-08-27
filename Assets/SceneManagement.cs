using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadLevel (int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }

}
