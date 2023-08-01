using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float multiplier; // ���ϰų� ���ϴ� ��
    protected string Tag;
    public Transform hudPos;
    public TextMeshPro text;
    public Animator anim;
    public AudioSource diesound;
    public void Start()
    {
        anim = GetComponent<Animator>();
        text = hudPos.GetComponentInChildren<TextMeshPro>();
        diesound = GetComponentInChildren<AudioSource>();
        UpdateText();
    }
    public void UpdateText()
    {
        if (text != null)
        {
            if (0 < multiplier && 1 > multiplier)
            {
                int divisor = Mathf.RoundToInt(1 / multiplier); // ������ �� ���
                text.text = Tag + divisor.ToString();
            }
            else
            text.text = Tag + multiplier.ToString();
            
            text.transform.position = hudPos.position;
        }
    }
    
    protected virtual IEnumerator ActivateItem()
    {
        // ���� ActivateItem ������ �������̵��� �� �ֵ��� virtual�� ����
        anim.SetBool("Touched", true);
        
        yield return new WaitForSeconds(0.7f); // ���ϴ� ���� �ð� ����

        GameManager.Instance.hud.ChangeScore(multiplier);
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� OnTriggerEnter2D ������ �������̵��� �� �ֵ��� virtual�� ����
        if (collision.gameObject.tag == "Player")
        {
            diesound.Play();
            StartCoroutine(ActivateItem());
        }
    }

}
