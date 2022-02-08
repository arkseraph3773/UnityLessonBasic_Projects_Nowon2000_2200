using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bomb;
    [SerializeField] private Transform firePoint;
    private void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            //------------방법 1, 탄을 총구에 생성한다--------------
            Instantiate(bullet, firePoint);
            firePoint.DetachChildren();

            /*//------------방법 2, 탄을 생성한후에 총구 위치에 갖다 놓는다--------
            //gameobject의 인스턴스화
            GameObject tmpBullet = Instantiate(bullet*//*, firePoint*//*);
            // 클래스의 인스턴스화 : 클래스타입 변수이름 = new 클래스생성자(일반적인 인스턴스화) 
            *//*firePoint.DetachChildren(); //종속관계 해제*//*
            tmpBullet.transform.position = firePoint.position;
            tmpBullet.transform.rotation = firePoint.rotation;*/
        }
        if(Input.GetKeyDown("b"))
        {
            Instantiate(bomb, firePoint);
            firePoint.DetachChildren();
        }
    }
}
