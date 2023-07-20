using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Monster2 : MonoBehaviour
{
    public float multiplier; // 곱하는 수
    public Transform hudPos;
    public TextMeshPro text;
    float GetRandomMultiplier()
    {
        float[] multipliers = { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };
        int randomIndex = Random.Range(0, multipliers.Length);
        return multipliers[randomIndex];
    }
    void Start()
    {
        text = hudPos.GetComponentInChildren<TextMeshPro>(); // 수정된 코드
        multiplier = GetRandomMultiplier();

        UpdateText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(multiplier);
            GameManager.Instance.hud.ChangeScore2(multiplier); // 먼저 점수를 변경합니다.
            this.gameObject.SetActive(false); // 몬스터를 비활성화합니다.
        }
    }

    // 이 메서드는 게임 오브젝트가 다시 활성화될 때마다 호출됩니다.
    private void OnEnable()
    {
        multiplier = GetRandomMultiplier();
        UpdateText();
    }

    void UpdateText()
    {
        if (text != null)
        {
            string symbol = "÷";
            int divisor = Mathf.RoundToInt(1 / multiplier); // 나누는 수 계산
            string formattedText = symbol + divisor.ToString();
            text.text = formattedText;
            text.transform.position = hudPos.position;
        }
    }
}

