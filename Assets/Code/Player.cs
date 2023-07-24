using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public float speed; // �÷��̾��� �̵� �ӵ�
    public Vector2 inputVec; // �÷��̾��� �Է� ����
    public float inputDelay = 0.5f; // �Է� ���� �ð� (�ʿ信 ���� ������ �� �ֽ��ϴ�)

    private bool isMoving = false; // �÷��̾ �̵� ������ ����
    private float nextInputTime; // ���� �Է� �ð�
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

    IEnumerator Move()
    {
        isMoving = true;
        Vector2 startPos = rigid.position;
        Vector2 inputVecNormalized = inputVec.normalized; // Normalize the input vector to ensure consistent movement speed

        
        float targetX = Mathf.Clamp(startPos.x + 4 * inputVecNormalized.x, minX, maxX);
        float targetY = Mathf.Clamp(startPos.y + 4 * inputVecNormalized.y, minY, maxY);
        Vector2 endPos = new Vector2(Mathf.Round(targetX), Mathf.Round(targetY));

        // Check if the player is already at the desired end position
        if (endPos == startPos)
        {
            isMoving = false; // Cancel the movement
            yield break; // Exit the coroutine early
        }

        float t = 0; // ���� �̵� �ð�
        while (t < 1) // �̵��� �Ϸ���� ���� ���
        {
            t += Time.deltaTime * speed; // ���� �̵� �ð��� ������ŵ�ϴ�
            rigid.position = Vector2.Lerp(startPos, endPos, t); // �÷��̾��� ��ġ�� �̵��մϴ�
            yield return null; // ���� �������� ��ٸ��ϴ�
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