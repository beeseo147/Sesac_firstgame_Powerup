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
        
        anim.SetBool("Touched", true); //아마 죽는 애니메이션 추가 예정
        yield return new WaitForSeconds(0.7f); // 다른 지연 시간으로 변경
        GameManager.Instance.hud.ChangeScore(multiplier); // 다른 방식으로 점수 변경
        this.gameObject.SetActive(false);
    }
}




