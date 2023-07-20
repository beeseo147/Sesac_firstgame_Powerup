using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Monster2 : MonoBehaviour
{
    public float multiplier; // ���ϴ� ��
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
        text = hudPos.GetComponentInChildren<TextMeshPro>(); // ������ �ڵ�
        multiplier = GetRandomMultiplier();

        UpdateText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(multiplier);
            GameManager.Instance.hud.ChangeScore2(multiplier); // ���� ������ �����մϴ�.
            this.gameObject.SetActive(false); // ���͸� ��Ȱ��ȭ�մϴ�.
        }
    }

    // �� �޼���� ���� ������Ʈ�� �ٽ� Ȱ��ȭ�� ������ ȣ��˴ϴ�.
    private void OnEnable()
    {
        multiplier = GetRandomMultiplier();
        UpdateText();
    }

    void UpdateText()
    {
        if (text != null)
        {
            string symbol = "��";
            int divisor = Mathf.RoundToInt(1 / multiplier); // ������ �� ���
            string formattedText = symbol + divisor.ToString();
            text.text = formattedText;
            text.transform.position = hudPos.position;
        }
    }
}

