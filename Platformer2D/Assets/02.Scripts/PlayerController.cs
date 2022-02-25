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
    Vector2 move; // direction vector (���⺤��), ���⼭�� ũ�Ⱑ 1�� �Ѿ�� �����

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
    public RunState runState;
    public AttackState attackState;
    // Detectors
    PlayerGroundDetector groundDetector;

    // animation
    Animator animator;
    float animationTimeElapsed;
    float attackTime;
    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        groundDetector = GetComponent<PlayerGroundDetector>();
        animator = GetComponentInChildren<Animator>();

        attackTime = GetAnimationTime("Attack");
    }
    float GetAnimationTime(string name)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == name)
            {
                time = ac.animationClips[i].length;
                break;
            }
        }
        return time;
    }
    void Update()
    {
        // Ű���� �¿� �Է� �޾Ƽ� ���� ������Ʈ�� �¿�� �����̴� ���
        float h = Input.GetAxis("Horizontal"); //�¿��̵�
        if (h < 0)
        {
            direction = -1;
        }
        else if (h > 0)
        {
            direction = 1;
        }

        move.x = h;

        if (groundDetector.isGrounded && jumpState == JumpState.Idle && attackState == AttackState.Idle)
        {
            if (Mathf.Abs(h) > 0.1f) // �����Է��� ������ 0���� ũ��
            {
                if (playerState != PlayerState.Run) // �÷��̾���°� �޸��� ���� ������
                {
                    playerState = PlayerState.Run; // �÷��̾���� �޸���� �ٲ�
                    runState = RunState.PrepareToRun; // �޸��� ���� �޸��� �غ�� �ٲ�
                }
            }
            else // �����Է��� 0�̸�
            {
                h = 0; // �ٷ� ���߰� �Ϸ���
                if(playerState != PlayerState.Idle) // �÷��̾���°� Idle�� �ƴϸ� 
                {
                    playerState = PlayerState.Idle; // �÷��̾� ���¸� Idle��
                    animator.Play("Idle");
                }

            }
        }
        
        //rb.velocity = new Vector2(h * moveSpeed, 0); // ���ɾ����� 
        //Rigidbody.velocity �� �������� �ֱ⸶�� �����Ұ�� 
        //���������� ������ ����ų ���ɼ��� �����Ƿ�
        //�ֱ��Լ����� velocity�� �ƴ϶� position�� �����ϴ� ������� �����δ�
        //velocity�� ���� �����ϴ� ����
        //�����ϴ� ���� ���� ��쿡 ���������� �ӵ��� �ٲ��� �Ҷ�
        //�Ǵ� Ư�� ���ۿ��� �ٸ� �������� �Ѿ�� ���� �ӵ��� �缳�� �ؾ��Ҷ� ��������
        //float d = rb.velocity.x; // ������Ʈ �Լ����� �ᵵ ��������
        if (playerState != PlayerState.Jump && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            playerState = PlayerState.Jump;
            jumpState = JumpState.PrepareToJump;
        }

        if (playerState != PlayerState.Attack && Input.GetKeyDown(KeyCode.C))
        {
            playerState = PlayerState.Attack;
            attackState = AttackState.PrepareToAttack;
        }
        UpdatePlayerState();
    }
    private void FixedUpdate()
    {
        FixedUpdateMovement();
    }
    void FixedUpdateMovement()
    {
        Vector2 velocity = new Vector2(move.x * moveSpeed, move.y);
        rb.position += velocity * Time.fixedDeltaTime;
    }
    void UpdatePlayerState()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Run:
                UpdateRunState();
                break;
            case PlayerState.Jump:
                UpdateJumpState();
                break;
            case PlayerState.Attack:
                UpdateAttackState();
                break;
            default:
                break;
        }
    }
    void UpdateRunState()
    {
        switch (runState)
        {
            case RunState.PrepareToRun:
                animator.Play("Run");
                runState = RunState.Running;
                break;
            case RunState.Running:
                break;
        }
    }
    void UpdateJumpState()
    {
        switch (jumpState)
        {
            case JumpState.PrepareToJump:
                // todo -> changeAnimation //velocity = ������Ƽ
                //animator.SetTrigger("JumpTrigger");
                animator.Play("Jump"); // �ش���·� �Ѿ
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
                    //animator.SetTrigger("IdleTrigger");
                    animator.Play("Idle");
                }
                break;
        }  
    }
    void UpdateAttackState()
    {
        switch (attackState)
        {
            case AttackState.PrepareToAttack:
                animator.Play("Attack");
                attackState = AttackState.Attacking;
                break;
            case AttackState.Attacking:
                if (animationTimeElapsed > attackTime)
                {
                    attackState = AttackState.Attacked;
                }
                animationTimeElapsed += Time.deltaTime;
                break;
            case AttackState.Attacked:
                playerState = PlayerState.Idle;
                attackState = AttackState.Idle;
                animator.Play("Idle");
                break;
        }
    }
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Attack,
    }
    public enum JumpState
    {
        Idle,
        PrepareToJump, //������ �ʿ��� �Ķ���� ����, �ִϸ��̼� ��ȯ ��
        Jumping, //���� ���������� �����ϴ� �ܰ�
        InFlight, //���� ���������� ������ ���߿� ĳ���Ͱ� ���ִ� ����
    }
    public enum RunState
    { 
        Idle,
        PrepareToRun,
        Running,
    }
    public enum AttackState
    {
        Idle,
        PrepareToAttack,
        Attacking,
        Attacked,
    }
}
// raycast ���� ���� ���� ������Ʈ���� �����Ҽ� �ִ� ���
// boxcast �ڽ� ������ ���� ������Ʈ���� �����Ҽ� �ִ� ���
// overlap Ư�� ���� ���� ������Ʈ�� ������ �����Ҽ� �ִ� ���