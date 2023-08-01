using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float multiplier; // 곱하거나 더하는 수
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
                int divisor = Mathf.RoundToInt(1 / multiplier); // 나누는 수 계산
                text.text = Tag + divisor.ToString();
            }
            else
            text.text = Tag + multiplier.ToString();
            
            text.transform.position = hudPos.position;
        }
    }
    
    protected virtual IEnumerator ActivateItem()
    {
        // 기존 ActivateItem 동작을 오버라이드할 수 있도록 virtual로 선언
        anim.SetBool("Touched", true);
        
        yield return new WaitForSeconds(0.7f); // 원하는 지연 시간 설정

        GameManager.Instance.hud.ChangeScore(multiplier);
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 기존 OnTriggerEnter2D 동작을 오버라이드할 수 있도록 virtual로 선언
        if (collision.gameObject.tag == "Player")
        {
            diesound.Play();
            StartCoroutine(ActivateItem());
        }
    }

}
