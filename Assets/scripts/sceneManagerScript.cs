using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagerScript : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log("clicked");
        SceneManager.LoadScene(sceneName);
    }
}
