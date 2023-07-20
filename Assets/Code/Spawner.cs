using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;  // 스폰 포인트 배열

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();  // 하위 오브젝트에서 SpawnPoint 컴포넌트를 찾아 배열에 할당
    }

    void Update()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (!spawnPoint.isOccupied && !spawnPoint.isInCooldown)
            {
                Debug.Log(spawnPoint);
                Spawn(spawnPoint);  // 스폰 포인트가 비어있고 쿨다운 중이 아닌 경우 스폰 함수 호출
                spawnPoint.isOccupied = true;  // 스폰 포인트를 점유 상태로 설정
            }
        }
    }

    void Spawn(SpawnPoint spawnPoint)
    {
        GameObject enemy = null;
        float randomValue = Random.value;
        Debug.Log(randomValue);
        if (randomValue < 0.2f)  // 20% 확률
        {
            enemy = GameManager.Instance.pool.Get(0);
        }
        else if (randomValue < 0.5f)  // 30% 확률
        {
            enemy = GameManager.Instance.pool.Get(1);
        }
        else if (randomValue < 0.9f)  // 40% 확률
        {
            enemy = GameManager.Instance.pool.Get(2);
        }
        else  // 10% 확률
        {
            enemy = GameManager.Instance.pool.Get(3);
        }

        if (enemy != null)
        {
            enemy.transform.position = spawnPoint.transform.position;
        }

        // enemy를 이용한 후속 처리

    }
}

