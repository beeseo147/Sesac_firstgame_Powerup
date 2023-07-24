using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isOccupied;     // 스폰 포인트에 물체가 있는지 여부
    public bool isInCooldown;   // 스폰 포인트가 쿨다운 중인지 여부
    private int objectsInside = 0;  // 스폰 포인트에 있는 물체의 개수
    private float cooldownTimer = 0.0f;  // 쿨다운 타이머

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*objectsInside++;    // 스폰 포인트 안에 들어온 물체의 개수를 증가
        UpdateOccupiedStatus();  // 물체 개수에 따라 스폰 포인트의 상태를*/
        gameObject.SetActive(true); // Activate the game object before calling the coroutine
        objectsInside++;
        UpdateOccupiedStatus();

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        /*objectsInside--;    // 스폰 포인트에서 나간 물체의 개수를 감소
        UpdateOccupiedStatus();  // 물체 개수에 따라 스폰 포인트의 상태를 업데이트*/
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
            isOccupied = true;  // 스폰 포인트 안에 물체가 있는 경우 isOccupied를 true로 설정
        }
        else
        {
            if (isOccupied == true) // 이전에 물체가 있었다면
            {
                StartCoroutine(Cooldown()); // 쿨다운 코루틴 실행
            }
            isOccupied = false; // 스폰 포인트 안에 물체가 없는 경우 isOccupied를 false로 설정
        }
    }

    IEnumerator Cooldown()
    {
        isInCooldown = true;
        float cooldownDuration = 0.7f; // 쿨타임 0.7초

        while (cooldownTimer < cooldownDuration)
        {
            cooldownTimer += Time.deltaTime;
            yield return null;
        }

        isInCooldown = false;
        cooldownTimer = 0.0f; // 쿨타임 초기화
    }
}



