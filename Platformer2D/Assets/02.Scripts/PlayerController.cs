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
        // Ű���� �¿� �Է� �޾Ƽ� ���� ������Ʈ�� �¿�� �����̴� ���
        float h = Input.GetAxis("Horizontal"); //�¿��̵�
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
                if (groundDetector.isGrounded) //�׶��� üũ �ؼ� idle ���·� ����� if��
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
// raycast ���� ���� ���� ������Ʈ���� �����Ҽ� �ִ� ���
// boxcast �ڽ� ������ ���� ������Ʈ���� �����Ҽ� �ִ� ���
// overlap Ư�� ���� ���� ������Ʈ�� ������ �����Ҽ� �ִ� ���