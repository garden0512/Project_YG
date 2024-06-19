using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ForceMode2D;

public class CharacterMoveControl : MonoBehaviour
{
    public float MaxSpeed;
    public float JumpSpeed;
    private Rigidbody2D rb;
    private bool isJumping;
    public bool IsMoving { get; private set; }
    public Vector3 MoveDirection { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        // 캐릭터가 움직이고 있는지 여부를 설정
        IsMoving = Mathf.Abs(move) > 0;
        // 이동 방향 설정
        MoveDirection = new Vector3(move, 0, 0);

        //점프
        if(Input.GetButtonDown("Jump") && isJumping == false/*후에 애니메이션을 넣으면 && !anim.GetBool("isJumping") 같은 방식으로 넣어서 지금 점프상태인지 아닌지 확인해주기*/ )
        {
            rb.AddForce(Vector2.up*JumpSpeed, ForceMode2D.Impulse);
            isJumping = true;
        }

        if(Input.GetButtonUp("Horizontal"))//정지속도
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0.5f, rb.velocity.y);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");//키 눌러서 움직이기
        rb.AddForce(Vector2.right*h, ForceMode2D.Impulse);

        if(rb.velocity.x > MaxSpeed)//오른쪽 최대속도
        {
            rb.velocity = new Vector2(MaxSpeed, rb.velocity.y);
        }
        else if(rb.velocity.x < MaxSpeed*(-1))//왼쪽 최대속도
        {
            rb.velocity = new Vector2(MaxSpeed*(-1), rb.velocity.y);
        }

        //착지판단
        if(rb.velocity.y < 0)
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0,1,0));

            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if(rayHit.collider != null)
            {
                Debug.Log(rayHit.collider.name);
                isJumping = false;
            }
        }
    }
}
