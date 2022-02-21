using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform tr;
    Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    public Transform groundDetectPoint;
    public float groundMinDistance;

    public bool isJumping;
    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // 키보드 좌우 입력 받아서 게임 오브젝트를 좌우로 움직이는 기능
        float h = Input.GetAxis("Horizontal"); //좌우이동
        rb.position += new Vector2(h * moveSpeed * Time.deltaTime, 0);

        // Jump
        if (isJumping == false && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        Vector2 origin = groundDetectPoint.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundMinDistance);
        Collider2D hitCol = hit.collider;
        if (hitCol != null)
        {
            if (hitCol.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                isJumping = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundDetectPoint.position, new Vector3(groundDetectPoint.position.x, groundDetectPoint.position.y - groundMinDistance, groundDetectPoint.position.z));
    }
}
// raycast 선을 쏴서 닿은 오브젝트들을 참조할수 있는 기능
// boxcast 박스 범위에 닿은 오브젝트들을 참조할수 있는 기능
// overlap 특정 범위 내에 오브젝트가 있으면 참조할수 있는 기능