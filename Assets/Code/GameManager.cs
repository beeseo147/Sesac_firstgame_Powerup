using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GameManager Class
public class GameManager : Singleton<GameManager>
{
    [Header("# In game")]
    [SerializeField] public PoolManager pool;
    [SerializeField] public HUD hud;
    [SerializeField] public Player player;

    private void Start()
    {
        InitializeValues();
    }

    public void InitializeValues()
    {
        pool = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        hud = GameObject.Find("HUD").GetComponent<HUD>();
    }
}