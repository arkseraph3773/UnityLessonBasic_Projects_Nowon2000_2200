using System;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public bool isGuided = false; //유도미사일
    public Transform targetGuide;
    public float missileSpeed;

    private Vector3 moveVec;
    Transform tr;
    private void Awake()
    {
        tr = transform;
    }
    private void FixedUpdate()
    {
        if(isGuided)
        {
            tr.LookAt(targetGuide);
            moveVec = (targetGuide.position - tr.position).normalized * missileSpeed;
        }
        tr.Translate(moveVec);
    }

    public void SetMoveVector(Vector3 dir)
    {
        moveVec = dir * missileSpeed;
    }
}
