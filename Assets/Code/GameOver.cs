using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    List<Text> textList = new List<Text>(); 
        private void Awake()
    {

        textList.AddRange(GetComponentsInChildren<Text>());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("UI");
        }
    }
}
