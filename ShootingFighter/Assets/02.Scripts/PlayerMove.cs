using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Transform tr;
    [SerializeField] private float speed;

    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;
        Vector3 deltaMove = dir * speed * Time.deltaTime; // 이동 변화량
        tr.Translate(deltaMove);
    }
}
