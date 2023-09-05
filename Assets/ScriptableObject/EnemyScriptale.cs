using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyScriptale : ScriptableObject
{
    [SerializeField] EnemyType enemyType;
    [SerializeField] Sprite EnemySprite;
    [SerializeField] string description;

    public enum EnemyType
    {
       PlusItem,
       MultipleItem,
       MinusItem,
       DivideItem,
       AttackItem
    }

    public EnemyType GetAccessoryType()
    {
        return enemyType;
    }

    public Sprite GetSprite()
    {
        return EnemySprite;
    }

    public string GetDescription()
    {
        return description;
    }
}
