using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

public class GameOver : MonoBehaviour
{
    SortedDictionary<int, int> Rank;
    [SerializeField] List<Text> textList = new List<Text>(); // ���߿� ������ �����ϱ� ���Ͽ�

    private void Awake()
    {
        AudioManager.Instance.musicSource.Stop();
        Rank = new SortedDictionary<int, int>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("����� On");
            SceneManager.LoadScene("UI");
        }
        printRank();
    }
    void printRank()
    {
        if (GameClient.Instance.GetPlayer())
        {
            int playerID = 1; // Replace this with the actual player ID or a unique identifier.
            int playerScore = (int)(GameManager.Instance.hud.getscore());

            // Check if the player ID already exists in Rank
            if (!Rank.ContainsKey(playerID))
            {
                Rank.Add(playerID, playerScore);
            }
        }
        else
        {
            SetRank(GameClient.Instance.finalplayersRank);
        }

        int index = 0;
        foreach (var kvp in Rank)
        {
            // textList�� ũ�Ⱑ �ε������� ������ ���� ����
            if (index >= textList.Count)
            {
                break;
            }

            // sortedRank�� �� �׸��� Text�� ��ȯ�Ͽ� textList�� �Ҵ��մϴ�.
            if (GameClient.Instance.GetPlayer())
            {
                textList[index].text = kvp.Key + "�� " + kvp.Value + "�� ";
            }
            else
            textList[index].text = kvp.Key + "�÷��̾� " + kvp.Value + "�� ";

            index++;
        }
    }

    void SetRank(SortedDictionary<int, int> End)
    {

        Rank =End;
    }
}

