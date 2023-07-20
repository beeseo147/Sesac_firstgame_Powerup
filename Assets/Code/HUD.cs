using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text timerText;
    public TextMeshProUGUI scoreText;
    private float time = 10.0f;
    private float score = 1;

    void Start()
    {
        timerText = GetComponentInChildren<Text>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    public void ChangeScore(float value) // Method to change the score.
    {
        score += value;
    }

    public void ChangeScore2(float value)
    {
        if (value >= 1)
        {
            score *= value;
        }
        else if (value > 0 && value < 1)
        {
            float divisor = 1 / value;
            score /= Mathf.RoundToInt(divisor);
            score = Mathf.RoundToInt(score);
        }
        else
        {
            // 0 이하의 값에 대한 예외 처리
        }
    }

    void Update()
    {
        time -= Time.deltaTime;
        timerText.text = time.ToString("F1");

        // Updating the score text
        scoreText.text = score.ToString() + " point";
    }
}
