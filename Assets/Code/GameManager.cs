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
    public AudioSource bgm1;
    

    void Awake()
    {
        Instance = this; // Assign the current instance of GameManager to the static Instance variable.
        bgm1 = GetComponent<AudioSource>();
    }
}