using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float speed; // �÷��̾��� �̵� �ӵ�
    public Vector2 inputVec; // �÷��̾��� �Է� ����
    public float inputDelay = 0.5f; // �Է� ���� �ð� (�ʿ信 ���� ������ �� �ֽ��ϴ�)
    public bool isdaed = false;
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
        //Vector2 startPos = rigid.position;
        //Vector2 inputVecNormalized = inputVec.normalized;



        //float targetX = Mathf.Clamp(startPos.x + 4, minX, maxX); //��ǥ�� �ϴ� ��ġ��(�̵��� ��ġ��) �ּҰ����� ������ �ּҰ����� ũ�� max������
        //float targetY = Mathf.Clamp(startPos.y + 4, minY, maxY);


        //Vector2 endPos = new Vector2(Mathf.Round(targetX), Mathf.Round(targetY));

        //if (endPos == startPos)
        //{
        //    isMoving = false; // Cancel the movement
        //    yield break; // Exit the coroutine early
        //}

        //float t = 0; // ���� �̵� �ð�
        //while (t < 1) // �̵��� �Ϸ���� ���� ���
        //{
        //    t += Time.deltaTime * speed; // ���� �̵� �ð��� ������ŵ�ϴ�
        //    rigid.position = Vector2.Lerp(startPos, endPos, t); // �÷��̾��� ��ġ�� �̵��մϴ�
        //    yield return null; // ���� �������� ��ٸ��ϴ�
        //}
        Vector2 inputVecNomalized = inputVec.normalized*4f;

        Debug.Log(inputVecNomalized);
        if (rigid.position.x + inputVecNomalized.x > maxX ||
        rigid.position.y + inputVecNomalized.y > maxY ||
        rigid.position.x + inputVecNomalized.x < minX || 
        rigid.position.y + inputVecNomalized.y  < minY)
        {
            isMoving = false; // Cancel the movement
            yield break; // Exit the coroutine early
        }



        float speed = 4.0f; // �̵� �ӵ�

        Vector2 targetPos = rigid.position;

        if (inputVecNomalized.x > 0)
        {
            targetPos.x += 4;
        }
        else if (inputVecNomalized.x == 0 && inputVecNomalized.y > 0)
        {
            targetPos.y += 4;
        }
        else if (inputVecNomalized.x ==0  && inputVecNomalized.y < 0)
        {
            targetPos.y -= 4;
        }
        else
        {
            targetPos.x -= 4;
        }

        float t = 0; // ���� �̵� �ð�
        while (t < 1) // �̵��� �Ϸ���� ���� ���
        {
            t += Time.deltaTime * speed;
            rigid.position = Vector2.Lerp(rigid.position, targetPos, t);
            yield return null;
        }

        isMoving = false; // �÷��̾ �̵� ������ �����մϴ�
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); // �Է��� �����մϴ�
        Vector2 joystickInput = value.Get<Vector2>();

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