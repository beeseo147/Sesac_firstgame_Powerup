using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    List<Text> textList = new List<Text>(); //나중에 순위를 보관하기 위하여
        private void Awake()
    {
        textList.AddRange(GetComponentsInChildren<Text>());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("r키는 눌림");
            SceneManager.LoadScene("UI");
        }
    }
}
