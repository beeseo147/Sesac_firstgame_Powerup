using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : Enemy
{
    public void OnEnable()
    {
        anim.SetBool("Touched", false);
        Tag = "+";
        multiplier = Random.Range(1, 20);
        UpdateText();
    }

    protected override IEnumerator ActivateItem()
    {
        
        anim.SetBool("Touched", true);
        yield return new WaitForSeconds(0.7f); // 원하는 지연 시간 설정
        GameManager.Instance.hud.ChangeScore(multiplier);
        this.gameObject.SetActive(false);
    }
}
