using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //..변수
    [Header("# Enemy")]
    [SerializeField] GameObject[] prefabs;
    //..풀 담당을 하는 리스트들
    public List<GameObject>[] pools;
    //TODO : Pool에 담긴 배열값을 어떻게 관리할것인가?
    public List<int> enemies; //멀티용
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
        //새로운 게임 오브젝트 생성

        GameObject newObject = Instantiate(prefabs[index], transform);
        newObject.SetActive(true);
        pools[index].Add(newObject);
        enemies.Add(index);
        return newObject;
    }
}