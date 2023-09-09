using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //..����
    [Header("# Monster")]
    [SerializeField] GameObject[] prefabs;
    //..Ǯ ����� �ϴ� ����Ʈ��
    public List<GameObject>[] pools;
    public List<int>[] enemies;
    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        enemies = new List<int>[prefabs.Length];
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>(); // �� ��Ҹ� List<GameObject>�� �ν��Ͻ��� �ʱ�ȭ
            enemies[index] = new List<int>();
        }
    }

    public GameObject Get(int index)
    {
    // ���ο� ���� ������Ʈ ����
    GameObject newObject = Instantiate(prefabs[index], transform);
    newObject.SetActive(true);
    pools[index].Add(newObject);
    enemies[index].Add(index);
    return newObject;
    }
}
