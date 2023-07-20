using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    TextMeshPro text;
    public float damage;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
    }
}
