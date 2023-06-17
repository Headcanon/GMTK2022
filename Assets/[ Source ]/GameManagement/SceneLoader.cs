using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame() 
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadSelection()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
