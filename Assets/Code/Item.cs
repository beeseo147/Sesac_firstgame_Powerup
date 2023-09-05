using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : Enemy
{
    protected override IEnumerator ActivateItem()
    {
        anim.SetBool("Touched", true);
        AudioManager.Instance.PlaySFX("Item");
        yield return new WaitForSeconds(0.7f); // ���ϴ� ���� �ð� ����
        GameManager.Instance.hud.MultiplicationAndDivision(Point); // ���� ������ �����մϴ�.
        Destroy(this.gameObject);
    }
    public void OnEnable()
    {
        anim.SetBool("Touched", false);
        Tag = "+";
        Point = Random.Range(1, 20);
        UpdateText();
    }
}
