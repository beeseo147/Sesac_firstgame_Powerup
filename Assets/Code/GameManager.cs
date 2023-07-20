using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// GameManager Class
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager pool;
    public HUD hud;


    void Awake()
    {
        Instance = this; // Assign the current instance of GameManager to the static Instance variable.
    }

    void Update() // Method to update the score text.
    {

    }
}