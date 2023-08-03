using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //..변수
    public GameObject[] prefabs;
    //..풀 담당을 하는 리스트들
    public List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>(); // 각 요소를 List<GameObject>의 인스턴스로 초기화
        }
    }

    public GameObject Get(int index)
    {
    // 새로운 게임 오브젝트 생성
    GameObject newObject = Instantiate(prefabs[index], transform);
    newObject.SetActive(true);
    pools[index].Add(newObject);
    return newObject;
    }
}
