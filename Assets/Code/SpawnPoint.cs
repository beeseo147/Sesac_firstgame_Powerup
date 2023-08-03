using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isOccupied;     // ���� ����Ʈ�� ��ü�� �ִ��� ����
    private int objectsInside = 0;  // ���� ����Ʈ�� �ִ� ��ü�� ����(�Ѹ��� �̻� �������� ���ϰ���)

    void OnTriggerEnter2D(Collider2D collision)
    {
        objectsInside++;
        UpdateOccupiedStatus();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // ���� ����Ʈ���� ���� ��ü�� ������ ����
       // ��ü ������ ���� ���� ����Ʈ�� ���¸� ������Ʈ*/
        objectsInside--;
        UpdateOccupiedStatus();
    }

    void UpdateOccupiedStatus()
    {
        if (objectsInside > 0)
            isOccupied = true;  // ���� ����Ʈ �ȿ� ��ü�� �ִ� ��� isOccupied�� true�� ����
        else
            isOccupied = false; // ���� ����Ʈ �ȿ� ��ü�� ���� ��� isOccupied�� false�� ����
    }
}