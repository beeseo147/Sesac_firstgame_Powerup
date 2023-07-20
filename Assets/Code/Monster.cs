using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monster : MonoBehaviour
{
    public float multiplier; // ���ϴ� ��
    public Transform hudPos;
    public TextMeshPro text;

    void Start()
    {
        text = hudPos.GetComponentInChildren<TextMeshPro>(); // ������ �ڵ�
        multiplier = Random.Range(-1, -20);
        UpdateText();
    }

    IEnumerator ActivateItem()
    {
        yield return new WaitForSeconds(0.7f); // ���ϴ� ���� �ð� ����

        GameManager.Instance.hud.ChangeScore(multiplier); // ���� ������ �����մϴ�.
        this.gameObject.SetActive(false); // ���͸� ��Ȱ��ȭ�մϴ�.
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ActivateItem());
        }
    }

    // �� �޼���� ���� ������Ʈ�� �ٽ� Ȱ��ȭ�� ������ ȣ��˴ϴ�.
    private void OnEnable()
    {
        multiplier = Random.Range(-1, -20);
        UpdateText();
    }

    void UpdateText()
    {
        if (text != null)
        {
            text.text = multiplier.ToString();
            text.transform.position = hudPos.position;
        }
    }
}

