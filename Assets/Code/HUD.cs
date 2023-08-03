using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HUD : MonoBehaviour
{
    public Text timerText;
    public TextMeshProUGUI scoreText;
    private float time = 20.0f;
    private float score = 1;
    public bool isplay = false;
    void Start()
    {
        timerText = GetComponentInChildren<Text>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    public void ChangeScore(float value) // +/-���� ���
    {
        score += value;
    }

    public void ChangeScore2(float value) // ����/������ ����
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
            // 0 ������ ���� ���� ���� ó��
        }
    }

    public void IsPause()
    {
        isplay = !isplay;
        if (isplay)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

    }
    void Update()
    {
        if (time > 0)
            time -= Time.deltaTime;
        else
        {
            time = 0;
            GameManager.Instance.player.isdead = true;
            SceneManager.LoadScene("GameOver");

        }
            timerText.text = time.ToString("F1");
            scoreText.text = score.ToString() + " point";
            
    }
}
