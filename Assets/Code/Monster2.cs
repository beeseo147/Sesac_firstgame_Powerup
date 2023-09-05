using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monster2 : Enemy
{
    public void OnEnable()
    {
        anim.SetBool("Touched", false);
        Tag = "��";
        Point = (Random.Range(1, 6)/10.0f);
        UpdateText();
    }
   
    protected override IEnumerator ActivateItem()
    {
        anim.SetBool("Touched", true);
        AudioManager.Instance.PlaySFX("Monster");
        yield return new WaitForSeconds(0.7f); // ���ϴ� ���� �ð� ����
        GameManager.Instance.hud.MultiplicationAndDivision(Point); // ���� ������ �����մϴ�.
        Destroy(this.gameObject); // ���͸� ��Ȱ��ȭ�մϴ�.
    }
}

