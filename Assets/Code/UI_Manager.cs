using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    GameObject Play;
    private void Awake()
    {
       


    }
    public void OnclickNewGame()
    {
    }
    public void OnclikOption()
    {

    }
    public void OnclickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif

    }
}
