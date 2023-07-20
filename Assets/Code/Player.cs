using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public float speed; // 플레이어의 이동 속도
    public Vector2 inputVec; // 플레이어의 입력 방향
    public float inputDelay = 0.5f; // 입력 무시 시간 (필요에 따라 변경할 수 있습니다)

    private bool isMoving = false; // 플레이어가 이동 중인지 여부
    private float nextInputTime; // 다음 입력 시간

    SpriteRenderer spriter; // 플레이어의 스프라이트 렌더러
    Rigidbody2D rigid; // 플레이어의 Rigidbody2D
    Animator anim; // 플레이어의 애니메이터
    int count = 0;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving && Time.time > nextInputTime && inputVec != Vector2.zero) // 입력 무시 시간 이후이고, 플레이어가 이동 중이 아니며, 입력이 있는 경우
        {
            StartCoroutine(Move()); // Move() 코루틴을 시작합니다
            nextInputTime = Time.time + inputDelay; // 다음 입력 시간은 현재 시간 + 입력 무시 시간입니다
        }
    }

    IEnumerator Move()
    {
        isMoving = true; // 플레이어가 이동 중으로 설정합니다

        Vector2 startPos = rigid.position; // 시작 위치를 저장합니다
        Vector2 endPos;
        if (inputVec.x > 0)
        {
            endPos = new Vector2(Mathf.Round(startPos.x + 4 * inputVec.x), startPos.y); // 이동할 위치를 계산합니다
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
        
        float t = 0; // 현재 이동 시간
        while (t < 1) // 이동이 완료되지 않은 경우
        {
            t += Time.deltaTime * speed; // 현재 이동 시간을 증가시킵니다
            rigid.position = Vector2.Lerp(startPos, endPos, t); // 플레이어의 위치를 이동합니다
            yield return null; // 다음 프레임을 기다립니다
        }

        isMoving = false; // 플레이어가 이동 중으로 설정합니다
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); // 입력을 저장합니다
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude); // 애니메이터의 "Speed" 속성에 입력의 크기를 설정합니다

        if (inputVec.x != 0) // 입력의 x 성분이 0이 아닌 경우
        {
            spriter.flipX = inputVec.x < 0; // 스프라이트를 뒤집습니다
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster") // 충돌한 오브젝트의 태그가 "Monster"인 경우
        {
            anim.SetTrigger("Attack"); // 애니메이터의 "Attack" 트리거를 실행합니다
        }
    }
}