using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameOver : MonoBehaviour
{
    private ScroeData sd;
    List<Text> textList = new List<Text>(); //나중에 순위를 보관하기 위하여
        private void Awake()
    {
        sd = new ScroeData();
        textList.AddRange(GetComponentsInChildren<Text>());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("재시작 On");
            SceneManager.LoadScene("UI");
        }
    }
    void PlayerRank(SortedDictionary<int,int> Rank) 
    {
        
    }

}
