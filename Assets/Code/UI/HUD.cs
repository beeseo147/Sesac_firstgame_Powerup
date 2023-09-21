using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HUD : MonoBehaviour
{
    [SerializeField] Text timerText;
    [SerializeField] Button PauseButton;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float time=20;
    [SerializeField] float score = 1;
    [SerializeField] bool isplay = false;
    void Start()
    {
        timerText = GetComponentInChildren<Text>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        PauseButton = GetComponentInChildren<Button>();
        if (!GameClient.Instance.GetPlayer())
        {
            //PauseButton.interactable = false;
            PauseButton.gameObject.SetActive(false);
        }
    }
    public void AdditionAndSubtraction(float value) {
        // +/-점수 계산
        score += value;
    }

    public void MultiplicationAndDivision(float value) // 곱셈/나눗셈 연산
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
    public float gettime()
    {
        return time;
    }
    public float getscore()
    {
        return score;
    }
    public void IsPause()
    {
        isplay = !isplay;
        if (isplay)
        {
            Time.timeScale = 0f;
            AudioManager.Instance.musicSource.Pause();
        }
        else
        {
            Time.timeScale = 1f;
            AudioManager.Instance.musicSource.UnPause();
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

    }
    void Update()
    {
        if (time > 0)
            time -= Time.deltaTime;
        else
        {
            GameOver();
        }

        timerText.text = time.ToString("F1");
        scoreText.text = score.ToString() + " point";
            
    }
    public void GameOver()
    {
        time = 0;
        GameClient.Instance.SetScore((int)score);
        GameManager.Instance.player.isdead = true;
        SceneManager.LoadScene("GameOver");
        isplay = false;
    }
    public void SetTime(long T)
    {
        time = T;
    }
}
