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
        multiplier = (Random.Range(1, 6)/10.0f);
        UpdateText();
    }
   
    protected override IEnumerator ActivateItem()
    {
        yield return new WaitForSeconds(0.7f); // ���ϴ� ���� �ð� ����
        GameManager.Instance.hud.ChangeScore2(multiplier); // ���� ������ �����մϴ�.
        Destroy(this.gameObject); // ���͸� ��Ȱ��ȭ�մϴ�.
    }
}

