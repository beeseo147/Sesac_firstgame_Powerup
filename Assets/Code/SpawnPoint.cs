using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isOccupied;     // 스폰 포인트에 물체가 있는지 여부
    private int objectsInside = 0;  // 스폰 포인트에 있는 물체의 개수(한마리 이상 생성하지 못하게함)

    void OnTriggerEnter2D(Collider2D collision)
    {
        objectsInside++;
        UpdateOccupiedStatus();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // 스폰 포인트에서 나간 물체의 개수를 감소
       // 물체 개수에 따라 스폰 포인트의 상태를 업데이트*/
        objectsInside--;
        UpdateOccupiedStatus();
    }

    void UpdateOccupiedStatus()
    {
        if (objectsInside > 0)
            isOccupied = true;  // 스폰 포인트 안에 물체가 있는 경우 isOccupied를 true로 설정
        else
            isOccupied = false; // 스폰 포인트 안에 물체가 없는 경우 isOccupied를 false로 설정
    }
}