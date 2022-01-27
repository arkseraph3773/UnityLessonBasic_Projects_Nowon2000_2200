using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Transform tr;
    public float moveSpeed;
    public float rotateSpeed;

    void Start()
    {
        tr = this.gameObject.GetComponent<Transform>();
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;
        Vector3 moveVec = dir * moveSpeed * Time.deltaTime;
        tr.Translate(moveVec);

        float r = Input.GetAxis("Mouse X");
        Vector3 rotateVec = new Vector3(0, r, 0) * rotateSpeed * Time.deltaTime;
        tr.Rotate(rotateVec);

    }
}
