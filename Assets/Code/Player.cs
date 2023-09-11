using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float speed; // Player Moving Speed
    public Vector2 inputVec; // Player Way
    public float inputDelay = 0.5f; // 입력 무시 시간 (필요에 따라 변경할 수 있습니다)
    public bool isdead = false;
    private bool isMoving = false; // 플레이어가 이동 중인지 여부
    private float nextInputTime; // ���� �Է� �ð�
    private int PlayerNumber;
    float maxX = 4.0f; // Set the maximum allowed X position
    float minX = -4.0f; // Set the minimum allowed X position
    float maxY = 3.0f; // Set the maximum allowed Y position
    float minY = -5.0f; // Set the minimum allowed Y position

    SpriteRenderer spriter; // �÷��̾��� ��������Ʈ ������
    Rigidbody2D rigid; // �÷��̾��� Rigidbody2D
    Animator anim; // �÷��̾��� �ִϸ�����
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (!isMoving && Time.time > nextInputTime && inputVec != Vector2.zero) // �Է� ���� �ð� �����̰�, �÷��̾ �̵� ���� �ƴϸ�, �Է��� �ִ� ���
        {
            StartCoroutine(Move()); // Move() �ڷ�ƾ�� �����մϴ�
            nextInputTime = Time.time + inputDelay; // ���� �Է� �ð��� ���� �ð� + �Է� ���� �ð��Դϴ�

        }
    }
    public void Move(int playernumber,int key, List<int> enemies) {

    }
    IEnumerator Move()
    {
        isMoving = true;
        float speed = 4.0f; // �̵� �ӵ�

        Vector2 targetPos = rigid.position;

        if (inputVec.x > 0.5f)
        {
            targetPos.x += 4;
        }
        else if (-0.7f < inputVec.x && inputVec.x < 0.71f && inputVec.y > 0.5f)
        {
            targetPos.y += 4;
        }
        else if (-0.7f < inputVec.x && inputVec.x < 0.71f && inputVec.y < 0.5f)
        {
            targetPos.y -= 4;
        }
        else
        {
            targetPos.x -= 4;
        }
        //�ִ�Ÿ��� ������
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        float t = 0; // ���� �̵� �ð�
        while (t < 1) // �̵��� �Ϸ���� ���� ���
        {
            t += (Time.deltaTime * speed) * 0.3f;
            rigid.position = Vector2.Lerp(rigid.position, targetPos, t);
            yield return null;
        }

        isMoving = false; // �÷��̾ �̵� ������ �����մϴ�
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); // �Է��� �����մϴ�
    }
    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude); // �ִϸ������� "Speed" �Ӽ��� �Է��� ũ�⸦ �����մϴ�

        if (inputVec.x != 0) // �Է��� x ������ 0�� �ƴ� ���
        {
            spriter.flipX = inputVec.x < 0; // ��������Ʈ�� �������ϴ�
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster") // �浹�� ������Ʈ�� �±װ� "Monster"�� ���
        {
            anim.SetTrigger("Attack"); // �ִϸ������� "Attack" Ʈ���Ÿ� �����մϴ�
        }
    }
}