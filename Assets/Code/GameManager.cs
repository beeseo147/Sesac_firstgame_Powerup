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

    private void Awake()
    {
        InitializeValues();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    public void InitializeValues()
    {
        pool = GameObject.Find("PoolManager").GetComponent<PoolManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        hud = GameObject.Find("HUD").GetComponent<HUD>();

        // ���⿡ �ٸ� scene ��ȯ �� ȣ���ؾ� �ϴ� �ڵ� �߰�
    }
}