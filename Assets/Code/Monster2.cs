using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monster2 : Enemy
{
    public void OnEnable()
    {
        anim.SetBool("Touched", false);
        Tag = "÷";
        multiplier = (Random.Range(1, 6)/10.0f);
        UpdateText();
    }
   
    protected override IEnumerator ActivateItem()
    {
        yield return new WaitForSeconds(0.7f); // 원하는 지연 시간 설정
        GameManager.Instance.hud.ChangeScore2(multiplier); // 먼저 점수를 변경합니다.
        Destroy(this.gameObject); // 몬스터를 비활성화합니다.
    }
}

