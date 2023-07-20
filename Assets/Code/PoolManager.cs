using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //..����
    public GameObject[] prefabs;
    //..Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for(int index = 0;index < pools.Length;index++)
        {
            pools[index] = new List<GameObject> ();//��� ������Ʈ Ǯ ����Ʈ �ʱ�ȭ
        }

    }
    public GameObject Get(int index)
    {
        // ��Ȱ��ȭ�� ���� ������Ʈ ã��
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                item.SetActive(true);
                return item;
            }
        }

        // ���ο� ���� ������Ʈ ����
        GameObject newObject = Instantiate(prefabs[index], transform);
        newObject.SetActive(true);
        pools[index].Add(newObject);
        return newObject;
    }

}
