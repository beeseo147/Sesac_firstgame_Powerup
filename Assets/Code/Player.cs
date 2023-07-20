using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public float speed;
    public Vector2 inputVec;
    public float inputDelay = 0.5f;  // 입력 무시 시간을 설정합니다. 필요에 따라 변경할 수 있습니다.

    private bool isMoving = false;
    private float nextInputTime;

    SpriteRenderer spriter;
    Rigidbody2D rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isMoving && Time.time > nextInputTime && inputVec != Vector2.zero)
        {
            StartCoroutine(Move());
            nextInputTime = Time.time + inputDelay;
        }
    }
    IEnumerator Move()
    {
        isMoving = true;

        Vector2 startPos = rigid.position;
        Vector2 endPos = new Vector2(Mathf.Round(startPos.x + 4*inputVec.x), Mathf.Round(startPos.y + 4*inputVec.y));

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * speed;
            rigid.position = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        isMoving = false;
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed",inputVec.magnitude);

        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }  

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            anim.SetTrigger("Attack");
        }
    }
}
