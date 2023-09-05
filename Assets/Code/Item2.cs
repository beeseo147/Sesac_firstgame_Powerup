using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item2 : Enemy
{
    public void OnEnable()
    {
        anim.SetBool("Touched", false);
        Tag = "X";
        Point = Random.Range(1, 6);
        UpdateText();
    }

    protected override IEnumerator ActivateItem()
    {
        anim.SetBool("Touched", true);
        AudioManager.Instance.PlaySFX("Item");
        yield return new WaitForSeconds(0.7f); // 원하는 지연 시간 설정
        GameManager.Instance.hud.MultiplicationAndDivision(Point);
        Destroy(this.gameObject);
    }

}
