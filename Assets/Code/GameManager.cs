using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GameManager Class
public class GameManager : Singleton<GameManager>
{
    [Header("# In game")]
    public PoolManager pool;
    public HUD hud;
    public Player player;
    [SerializeField] int PlayerNumber;
    public BtnType type;
    public GameClient client;
    [SerializeField]  bool SoloPlayer = false;
    
    public bool GetPlayer()
    {
        return SoloPlayer;
    }
    public void SetPlayer(bool solo)
    {
         SoloPlayer = solo;
    }
}