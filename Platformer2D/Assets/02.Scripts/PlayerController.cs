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

    PlayerState playerState;

    // Detectors
    PlayerGroundDetector groundDetector;

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
        rb.position += new Vector2(h * moveSpeed * Time.deltaTime, 0);

        if (playerState != PlayerState.Jump && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            playerState = PlayerState.Jump;
        }
        UpdatePlayerState();
        Debug.Log(playerState);
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
                if (groundDetector.isGrounded) //그라운드 체크 해서 idle 상태로 만드는 if문
                {
                    playerState = PlayerState.Idle;
                }
                break;
            default:
                break;
        }
    }

    enum PlayerState
    {
        Idle,
        Run,
        Jump,
    }
}
// raycast 선을 쏴서 닿은 오브젝트들을 참조할수 있는 기능
// boxcast 박스 범위에 닿은 오브젝트들을 참조할수 있는 기능
// overlap 특정 범위 내에 오브젝트가 있으면 참조할수 있는 기능