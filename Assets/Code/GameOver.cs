using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

public class GameOver : MonoBehaviour
{
    SortedDictionary<int, int> Rank;
    [SerializeField] List<Text> textList = new List<Text>(); // 나중에 순위를 보관하기 위하여

    private void Awake()
    {
        AudioManager.Instance.musicSource.Stop();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("재시작 On");
            SceneManager.LoadScene("UI");
        }
        printRank();
    }
    void printRank()
    {
        SetRank(GameClient.Instance.finalplayersRank);
        int index = Rank.Count-1;
        foreach (var kvp in Rank)
        {
            // textList의 크기가 인덱스보다 작으면 루프 종료
            if (index >= textList.Count)
            {
                break;
            }

            // sortedRank의 각 항목을 Text로 변환하여 textList에 할당합니다.
            textList[index].text = kvp.Key + "플레이어 " + kvp.Value + "등 ";
            index--;
        }
    }
    void SetRank(SortedDictionary<int, int> End)
    {

        Rank =End;
    }
}

