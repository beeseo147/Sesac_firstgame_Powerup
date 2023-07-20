using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float multiplier; // ���ϴ� ��
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

        yield return new WaitForSeconds(0.7f); // ���ϴ� ���� �ð� ����

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


    // �� �޼���� ���� ������Ʈ�� �ٽ� Ȱ��ȭ�� ������ ȣ��˴ϴ�.
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
