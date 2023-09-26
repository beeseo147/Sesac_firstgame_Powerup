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
                T = "�غ�";
            }
            else
            {
                T = "���";
            }

            text[i].text = layer.Key.ToString() + "Player �� " + T + " �����Դϴ�"; // layer.Key�� ����
            i++;
        }
    }


    private void Update()
    {

    }

}
