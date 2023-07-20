using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float multiplier; // 곱하는 수
    public Transform hudPos;
    public TextMeshPro text;
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        text = hudPos.GetComponentInChildren<TextMeshPro>();
    }

    void Start()
    {
        text = hudPos.GetComponentInChildren<TextMeshPro>();
        multiplier = Random.Range(1, 20);
        UpdateText();
    }

    private IEnumerator ActivateItem()
    {
        anim.SetBool("Touched", true);

        yield return new WaitForSeconds(0.7f); // 원하는 지연 시간 설정

        GameManager.Instance.hud.ChangeScore(multiplier);
        this.gameObject.SetActive(false);
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
        anim.SetBool("Touched", false);
        multiplier = Random.Range(1, 20);
        UpdateText();
    }

    void UpdateText()
    {
        if (text != null)
        {
            text.text = "+" +multiplier.ToString();
            text.transform.position = hudPos.position;
        }
    }
}
