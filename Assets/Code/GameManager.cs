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

    void Awake()
    {
        Instance = this; // Assign the current instance of GameManager to the static Instance variable.
    }
}