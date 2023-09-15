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
        int index = Rank.Count-1;
        foreach (var kvp in Rank)
        {
            // textList�� ũ�Ⱑ �ε������� ������ ���� ����
            if (index >= textList.Count)
            {
                break;
            }

            // sortedRank�� �� �׸��� Text�� ��ȯ�Ͽ� textList�� �Ҵ��մϴ�.
            textList[index].text = kvp.Value + "�÷��̾� " + kvp.Key + "��";
            index--;
        }
    }



    void SetRank(SortedDictionary<int, int> End)
    {

        Rank =End;
    }
}

