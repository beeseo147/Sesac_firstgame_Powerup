using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isOccupied;     // ���� ����Ʈ�� ��ü�� �ִ��� ����
    public bool isInCooldown;   // ���� ����Ʈ�� ��ٿ� ������ ����
    private int objectsInside = 0;  // ���� ����Ʈ�� �ִ� ��ü�� ����

    void OnTriggerEnter2D(Collider2D collision)
    {
        objectsInside++;    // ���� ����Ʈ �ȿ� ���� ��ü�� ������ ����
        UpdateOccupiedStatus();  // ��ü ������ ���� ���� ����Ʈ�� ���¸� ������Ʈ
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        objectsInside--;    // ���� ����Ʈ���� ���� ��ü�� ������ ����
        UpdateOccupiedStatus();  // ��ü ������ ���� ���� ����Ʈ�� ���¸� ������Ʈ
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
        isInCooldown = true;  // ��ٿ� ���·� ����
        yield return new WaitForSeconds(0.7f);  // 2���� ��ٿ� �Ⱓ ���
        isInCooldown = false;  // ��ٿ��� �������Ƿ� ���� ����
    }
}




