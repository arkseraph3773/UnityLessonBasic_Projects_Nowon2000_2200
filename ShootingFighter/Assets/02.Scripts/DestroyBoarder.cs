using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoarder : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // OnCollisionEvent는 
        //Rigidbody와 Collider또는
        //Collider 와 Rigidbody
        if (collision.collider == null) return;
        // != null 충돌체가 있을때 == null 충돌체가 있을때
        // 충돌 데이터의 충돌체가 가지고 있는 collider컴포넌트가 존재하는지 체크
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
