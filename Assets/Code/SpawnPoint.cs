using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isOccupied;     // ���� ����Ʈ�� ��ü�� �ִ��� ����
    public bool isInCooldown;   // ���� ����Ʈ�� ��ٿ� ������ ����
    private int objectsInside = 0;  // ���� ����Ʈ�� �ִ� ��ü�� ����
    private float cooldownTimer = 0.0f;  // ��ٿ� Ÿ�̸�

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*objectsInside++;    // ���� ����Ʈ �ȿ� ���� ��ü�� ������ ����
        UpdateOccupiedStatus();  // ��ü ������ ���� ���� ����Ʈ�� ���¸�*/
        gameObject.SetActive(true); // Activate the game object before calling the coroutine
        objectsInside++;
        UpdateOccupiedStatus();

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        /*objectsInside--;    // ���� ����Ʈ���� ���� ��ü�� ������ ����
        UpdateOccupiedStatus();  // ��ü ������ ���� ���� ����Ʈ�� ���¸� ������Ʈ*/
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true); // Activate the game object before calling the coroutine
        }
        objectsInside--;
        UpdateOccupiedStatus();
    }

    void UpdateOccupiedStatus()
    {
        if (objectsInside > 0)
        {
            isOccupied = true;  // ���� ����Ʈ �ȿ� ��ü�� �ִ� ��� isOccupied�� true�� ����
        }
        else
        {
            if (isOccupied == true) // ������ ��ü�� �־��ٸ�
            {
                StartCoroutine(Cooldown()); // ��ٿ� �ڷ�ƾ ����
            }
            isOccupied = false; // ���� ����Ʈ �ȿ� ��ü�� ���� ��� isOccupied�� false�� ����
        }
    }

    IEnumerator Cooldown()
    {
        isInCooldown = true;
        float cooldownDuration = 0.7f; // ��Ÿ�� 0.7��

        while (cooldownTimer < cooldownDuration)
        {
            cooldownTimer += Time.deltaTime;
            yield return null;
        }

        isInCooldown = false;
        cooldownTimer = 0.0f; // ��Ÿ�� �ʱ�ȭ
    }
}



