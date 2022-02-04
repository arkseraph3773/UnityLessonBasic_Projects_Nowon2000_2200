using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoarder : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // OnCollisionEvent�� 
        //Rigidbody�� Collider�Ǵ�
        //Collider �� Rigidbody
        if (collision.collider == null) return;
        // != null �浹ü�� ������ == null �浹ü�� ������
        // �浹 �������� �浹ü�� ������ �ִ� collider������Ʈ�� �����ϴ��� üũ
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
