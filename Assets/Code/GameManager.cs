using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GameManager Class
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# In game")]
    public PoolManager pool;
    public HUD hud;
    public Player player;
    bool SoloPlayer = false;
    private void Awake()
    {
        AudioManager.Instance.PlayMusic("GameTitle1");
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public bool getPlayer()
    {
        return SoloPlayer;
    }
    public void setPlayer(bool solo)
    {
         SoloPlayer = solo;
    }
}