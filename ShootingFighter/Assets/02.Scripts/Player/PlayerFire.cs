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
            //------------��� 1, ź�� �ѱ��� �����Ѵ�--------------
            Instantiate(bullet, firePoint);
            firePoint.DetachChildren();

            /*//------------��� 2, ź�� �������Ŀ� �ѱ� ��ġ�� ���� ���´�--------
            //gameobject�� �ν��Ͻ�ȭ
            GameObject tmpBullet = Instantiate(bullet*//*, firePoint*//*);
            // Ŭ������ �ν��Ͻ�ȭ : Ŭ����Ÿ�� �����̸� = new Ŭ����������(�Ϲ����� �ν��Ͻ�ȭ) 
            *//*firePoint.DetachChildren(); //���Ӱ��� ����*//*
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
