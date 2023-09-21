using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class PlayerCount : MonoBehaviour
{
    [SerializeField] Text PlayerCountText;
    void Start()
    {
        PlayerCountText = GetComponentInChildren<Text>();
    }
    private void Update()
    {
        PlayerCountText.text = GameClient.Instance.GetPlayerCount().ToString() + "/ 4" ;
    }
}
