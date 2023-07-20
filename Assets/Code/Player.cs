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

    SpriteRenderer spriter; // �÷��̾��� ��������Ʈ ������
    Rigidbody2D rigid; // �÷��̾��� Rigidbody2D
    Animator anim; // �÷��̾��� �ִϸ�����
    int count = 0;
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
        isMoving = true; // �÷��̾ �̵� ������ �����մϴ�

        Vector2 startPos = rigid.position; // ���� ��ġ�� �����մϴ�
        Vector2 endPos;
        if (inputVec.x > 0)
        {
            endPos = new Vector2(Mathf.Round(startPos.x + 4 * inputVec.x), startPos.y); // �̵��� ��ġ�� ����մϴ�
            Debug.Log(endPos);
            count++;
            Debug.Log(count);
            
        }
        else
        {
            endPos = new Vector2(startPos.x, Mathf.Round(startPos.y + 4 * inputVec.y));
            Debug.Log(endPos);
            count++;
            Debug.Log(count);

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