using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monster : MonoBehaviour
{
    public float multiplier; // 곱하는 수
    public Transform hudPos;
    public TextMeshPro text;

    void Start()
    {
        text = hudPos.GetComponentInChildren<TextMeshPro>(); // 수정된 코드
        multiplier = Random.Range(-1, -20);
        UpdateText();
    }

    IEnumerator ActivateItem()
    {
        yield return new WaitForSeconds(0.7f); // 원하는 지연 시간 설정

        GameManager.Instance.hud.ChangeScore(multiplier); // 먼저 점수를 변경합니다.
        this.gameObject.SetActive(false); // 몬스터를 비활성화합니다.
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ActivateItem());
        }
    }

    // 이 메서드는 게임 오브젝트가 다시 활성화될 때마다 호출됩니다.
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

