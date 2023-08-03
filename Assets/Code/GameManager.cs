using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GameManager Class
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager pool;
    public HUD hud;
    public Player player;

    private void Awake()
    {
        AudioManager.Instance.PlayMusic("GameTitle1");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}