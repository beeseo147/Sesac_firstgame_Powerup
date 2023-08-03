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
}
