using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("# Monster Status")]
    public float Point; // ���ϰų� ���ϴ� ��
    protected string Tag;
    [Header("# Monster set")]
    public Transform hudPos;
    public TextMeshPro text;
    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
        text = hudPos.GetComponentInChildren<TextMeshPro>();
        UpdateText();
    }
    public void UpdateText()
    {
        if (text != null)
        {
            if (0 < Point && 1 > Point)
            {
                int divisor = Mathf.RoundToInt(1 / Point); // ������ �� ���
                text.text = Tag + divisor.ToString();
            }
            else
            text.text = Tag + Point.ToString();
            
            text.transform.position = hudPos.position;
        }
    }
    
    protected virtual IEnumerator ActivateItem()
    {
        // ���� ActivateItem ������ �������̵��� �� �ֵ��� virtual�� ����
        anim.SetBool("Touched", true);
        AudioManager.Instance.PlaySFX("Monster");
        yield return new WaitForSeconds(0.7f); // ���ϴ� ���� �ð� ����

        GameManager.Instance.hud.AdditionAndSubtraction(Point);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� OnTriggerEnter2D ������ �������̵��� �� �ֵ��� virtual�� ����
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ActivateItem());
        }
    }

}
