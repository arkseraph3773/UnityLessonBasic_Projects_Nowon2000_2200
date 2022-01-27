using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Transform tr;
    [HideInInspector] public float distance; //[HideInInspector] : 인스펙터창에서 숨기기
    public Vector3 dir;
    public float minSpeed;
    public float maxSpeed;
    public bool doMove;
    void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        RacingPlay.instance.Register(this);
        // instance의 초기화는 Awake 함수에서 instance에 접근하는 구문은 Start 함수에서 
        // 일반적으로 수행한다.
    }

    void Update()
    {
        if(doMove)
        {
            Move();
        }
    }
    private void Move()
    {
        float moveSpeed = Random.Range(minSpeed, maxSpeed);
        Vector3 moveVec = dir * moveSpeed * Time.deltaTime;
        tr.Translate(moveVec);
        distance += moveVec.magnitude;
    }
}
