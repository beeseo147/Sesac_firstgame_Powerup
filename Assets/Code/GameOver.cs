using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameOver : MonoBehaviour
{
    private ScroeData sd;
    SortedDictionary<int, int> Rank;
    List<Text> textList = new List<Text>(); //���߿� ������ �����ϱ� ���Ͽ�
        private void Awake()
    {
        Rank = new SortedDictionary<int, int>();
        Rank.Add((int)GameManager.Instance.hud.getscore(), GameManager.Instance.player.GetPlayerNumber());
        AudioManager.Instance.musicSource.Stop();
        sd = new ScroeData();
        textList.AddRange(GetComponentsInChildren<Text>());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("����� On");
            SceneManager.LoadScene("UI");
        }
    }
    void PlayerRank(SortedDictionary<int,int> Rank) 
    {
    }

}
