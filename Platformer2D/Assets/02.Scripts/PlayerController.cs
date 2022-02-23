using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform tr;
    Rigidbody2D rb;
    BoxCollider2D col;
    public float moveSpeed;
    public float jumpForce;

    int _direction;
    int direction
    {
        set
        {
            _direction = value;
            if (_direction < 0)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            else if (_direction > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
        }
        get
        {
            return _direction;
        }
    }

    // States
    public PlayerState playerState;
    public JumpState jumpState;

    // Detectors
    PlayerGroundDetector groundDetector;

    // animation
    float animationTimeElapsed;
    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        groundDetector = GetComponent<PlayerGroundDetector>();
    }
    void Update()
    {
        // 키보드 좌우 입력 받아서 게임 오브젝트를 좌우로 움직이는 기능
        float h = Input.GetAxis("Horizontal"); //좌우이동
        if (h < 0)
        {
            direction = -1;
        }
        else if (h > 0)
        {
            direction = 1;
        }

        rb.position += new Vector2(h * moveSpeed * Time.deltaTime, 0);
        //rb.velocity = new Vector2(h * moveSpeed, 0); // 일케쓰지마 
        //Rigidbody.velocity 를 물리연산 주기마다 실행할경우 
        //비정상적인 동작을 일으킬 가능성이 있으므로
        //주기함수에서 velocity가 아니라 position을 변경하는 방식으로 움직인다
        //velocity를 직접 수정하는 경우는
        //점프하는 순간 등의 경우에 순간적으로 속도가 바뀌어야 할때
        //또는 특정 동작에서 다른 동작으로 넘어가는 순간 속도를 재설정 해야할때 직접수정
        //float d = rb.velocity.x; // 업데이트 함수에서 써도 문제없음
        if (playerState != PlayerState.Jump && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            playerState = PlayerState.Jump;
            jumpState = JumpState.PrepareToJump;
        }
        UpdatePlayerState();
    }

    void UpdatePlayerState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Run:
                break;
            case PlayerState.Jump:
                UpdateJumpState();
                break;
            default:
                break;
        }
    }
    void UpdateJumpState()
    {
        switch (jumpState)
        {
            case JumpState.PrepareToJump:
                // todo -> changeAnimation //velocity = 프로퍼티
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jumpState = JumpState.Jumping;
                break;
            case JumpState.Jumping:
                if(groundDetector.isGrounded == false)
                {
                    jumpState = JumpState.InFlight;
                }
                break;
            case JumpState.InFlight:
                if (groundDetector.isGrounded)
                {
                    playerState = PlayerState.Idle;
                    jumpState = JumpState.Idle;
                }
                break;
        }
    }
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
    }
    public enum JumpState
    {
        Idle,
        PrepareToJump, //점프에 필요한 파라미터 세팅, 애니메이션 전환 등
        Jumping, //점프 물리연산을 시작하는 단계
        InFlight, //점프 물리연산이 끝나고 공중에 캐릭터가 떠있는 상태
    }
}
// raycast 선을 쏴서 닿은 오브젝트들을 참조할수 있는 기능
// boxcast 박스 범위에 닿은 오브젝트들을 참조할수 있는 기능
// overlap 특정 범위 내에 오브젝트가 있으면 참조할수 있는 기능