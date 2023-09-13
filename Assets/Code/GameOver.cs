using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameOver : MonoBehaviour
{
    SortedDictionary<int, int> Rank;
    List<Text> textList = new List<Text>(); // 나중에 순위를 보관하기 위하여

    private void Awake()
    {
        Rank = new SortedDictionary<int, int>
        {
            { GameClient.Instance.GetPlayerNumber(), GameClient.Instance.GetScore() }
        };
        AudioManager.Instance.musicSource.Stop();
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

    void PlayerRank(SortedDictionary<int, int> Rank)
    {
        var sortedRank = Rank.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        Rank.Clear();
        foreach (var kvp in sortedRank)
        {
            Rank.Add(kvp.Key, kvp.Value);
        }
    }

    void printRank()
    {
        foreach (var text in textList)
        {
            text.text = "";
        }

        // Sort the Rank dictionary by values in ascending order
        var sortedRank = Rank.OrderBy(pair => pair.Value).ToList();

        // Iterate through the sortedRank and update the textList with player ranks
        for (int i = 0; i < sortedRank.Count; i++)
        {
            int playerNumber = sortedRank[i].Key;
            int playerScore = sortedRank[i].Value;

            // Create the rank text
            string rankText = $"{i + 1}등 = {playerNumber} : {playerScore} 점";

            // Update the corresponding text element in textList if it exists
            if (i < textList.Count)
            {
                textList[i].text = rankText;
            }
            else
            {
                Debug.LogWarning("Not enough text elements for displaying all ranks.");
                break;
            }
        }
    }
}

