using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monster : Enemy
{
    public void OnEnable()
    {
        anim.SetBool("Touched", false);
        Tag = "";
        multiplier = Random.Range(-1, -20);
        UpdateText();
    }

    protected override IEnumerator ActivateItem()
    {
        
        anim.SetBool("Touched", true); //�Ƹ� �״� �ִϸ��̼� �߰� ����
        yield return new WaitForSeconds(0.7f); // �ٸ� ���� �ð����� ����
        GameManager.Instance.hud.ChangeScore(multiplier); // �ٸ� ������� ���� ����
        this.gameObject.SetActive(false);
    }
}




