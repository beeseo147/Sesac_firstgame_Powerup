using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum BTNType
{
    Play,
    Option,
    Quit,
    Sound,
    Back,
    Solo,
    Togeter,
    Ready,
    Exit,
    Start
}
public class UI_Manager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic("MainMenuTitle");
    }
}
