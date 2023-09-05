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
        Point = Random.Range(-1, -20);
        UpdateText();
    }

}




