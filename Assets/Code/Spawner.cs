using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;  // ���� ����Ʈ �迭

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();  // ���� ������Ʈ���� SpawnPoint ������Ʈ�� ã�� �迭�� �Ҵ�
    }

    void Update()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (!spawnPoint.isOccupied && !spawnPoint.isInCooldown)
            {
                Debug.Log(spawnPoint);
                Spawn(spawnPoint);  // ���� ����Ʈ�� ����ְ� ��ٿ� ���� �ƴ� ��� ���� �Լ� ȣ��
                spawnPoint.isOccupied = true;  // ���� ����Ʈ�� ���� ���·� ����
            }
        }
    }

    void Spawn(SpawnPoint spawnPoint)
    {
        GameObject enemy = null;
        float randomValue = Random.value;
        Debug.Log(randomValue);
        if (randomValue < 0.2f)  // 20% Ȯ��
        {
            enemy = GameManager.Instance.pool.Get(0);
        }
        else if (randomValue < 0.5f)  // 30% Ȯ��
        {
            enemy = GameManager.Instance.pool.Get(1);
        }
        else if (randomValue < 0.9f)  // 40% Ȯ��
        {
            enemy = GameManager.Instance.pool.Get(2);
        }
        else  // 10% Ȯ��
        {
            enemy = GameManager.Instance.pool.Get(3);
        }

        if (enemy != null)
        {
            enemy.transform.position = spawnPoint.transform.position;
        }

        // enemy�� �̿��� �ļ� ó��

    }
}
