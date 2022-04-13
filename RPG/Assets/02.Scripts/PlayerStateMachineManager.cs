using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineManager : MonoBehaviour
{
    public float turnSpeed = 1f;

    Vector3 direction;
    Vector3 move;
    Coroutine turnCoroutine = null;
    Animator animator;
    Rigidbody rb;
    Transform tr;

    Vector3 targetAngle;
    bool isMove;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        targetAngle = tr.eulerAngles;
    }

    private void Update()
    {
        bool tmpMove = false;
        Vector3 tmpDir = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // 1. 만약에 플레이어 Y축 각도가 0이 아니면 0이 될때까지 회점
            // 2. 앞으로 전진
            tmpDir = tr.forward;
            targetAngle = Vector3.zero;
            tmpMove = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // 1. 만약에 플레이어 Y축 각도 180 아니면 180 될때까지 회전
            // 2. 뒤로 전진
            tmpDir = -tr.forward;
            targetAngle = new Vector3(0, 180, 0);
            tmpMove = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // 1. 만약에 플레이어 Y축 각도 270 아니면 270 될때까지 회전
            // 2. 왼쪽으로 전진
            tmpDir = (tmpDir + tr.right).normalized;
            targetAngle = new Vector3(0, 270, 0);
            tmpMove = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // 1. 만약에 플레이어 Y축 각도 90 아니면 90 될때까지 회전
            // 2. 오른쪽으로 전진
            tmpDir = (tmpDir + tr.right).normalized;
            targetAngle = new Vector3(0, 90, 0);
            tmpMove = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 점프하기
        }

        /*targetAngle /= 90;
        targetAngle += tr.eulerAngles;*/

        isMove = tmpMove;


    }
    private void FixedUpdate()
    {
        if (isMove)
        {
            targetAngle *= turnSpeed * Time.deltaTime;
            targetAngle += tr.eulerAngles;
            tr.eulerAngles = new Vector3(tr.eulerAngles.x,
                                        Mathf.Lerp(tr.eulerAngles.y, targetAngle.y, 5f),
                                        tr.eulerAngles.z);
        }
    }
}

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Jump,
    Fall,
}
    
