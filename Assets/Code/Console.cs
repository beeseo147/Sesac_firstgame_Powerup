using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class Console : MonoBehaviour
{
    public Text[] text;
    void Awake()
    {
        text = GetComponentsInChildren<Text>();
    }
    public void set(SortedDictionary<int, bool> playersReady)
    {
        string T;
        int i = 0;
        foreach (var layer in playersReady)
        {
            if (layer.Value)
            {
                T = "준비";
            }
            else
            {
                T = "대기";
            }

            text[i].text = layer.Key.ToString() + "Player 가 " + T + " 상태입니다"; // layer.Key로 수정
            i++;
        }
    }


    private void Update()
    {

    }

}
