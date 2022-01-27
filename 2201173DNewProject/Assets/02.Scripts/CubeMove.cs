using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    Transform tr;
    // public ���������ڸ� ����ϸ� Inspectorâ�� ������ �ȴ�.
    // ���࿡ �ٸ� Ŭ�����κ����� ������ �����ϸ鼭 Inspectorâ�� �����Ű�� ������ [SerializeField] �Ӽ��� ����Ѵ�.
    [SerializeField] private float speed = 1; //[SerializeField]private 
    public float rotateSpeed;
    // Inspectorâ�� ���� �ʱ�ȭ ������ �켱������.

    void Start()
    {
        //transform �� �����ؼ� ��ǥ�� ���� �����͸� ������ѵ� ������ ���� transform ������� tr�� �����ؼ�
        //transform component�� ������ �Ŀ� ����ϴ� ������
        //ĳ�� �޸� �������� (ĳ�� : �ӽ÷� ������ �����ϵ��� ������ �޸�)
        //transform�� ����ϸ� �� ��������� ȣ���Ҷ� ���� gameobject�� �����ؼ� getComponent�� transform ������ ������
        //������ transform ��� ����tr���ٰ� �ѹ� �־���� ����ϸ�
        //tr �� ����Ҷ����� ó���� �־���� transform component�� �ٷ� �����ϱ� ������
        //���ÿ� ���־��� ���� ���ӿ�����Ʈ���� transform ������Ʈ�� ����ϸ� �׶��� �����ս����� ���̰� ����.

        //tr = this.gameObject.transform; // ���ӿ�����Ʈ�� ������� transform�� �����Ѵ�( ������� transform�� ���� ����ִ��� ���������� �˼�����
        //tr = this.gameObject.GetComponent<Transform>(); // �� Ŭ������ �����ϴ� ���� ������Ʈ���Լ� Transform ������Ʈ�� �����´�
        /*tr = this.gameObject.GetComponent<Transform>();*/
        tr = gameObject.GetComponent<Transform>(); //���������� ���� ����� //���ӿ�����Ʈ���Լ� Ʈ������ ������Ʈ�� �����´�
        /*tr = GetComponent<Transform>();
        tr = gameObject.transform;
        tr = GetComponent("Transform") as Transform;
        tr = transform; //������� tr �ҷ����¹�*/
    }
    // Update �� �������Ӹ��� ȣ��Ǵ� �Լ�
    void Update()
    {
        // position
        // ======================================================================
        // 1�����Ӵ� z �� 1����
        // ���࿡ ��ǻ�� ����� �޶� �ϳ��� 60FPS, �ٸ� �ϳ��� 30FPS
        // -> 1�ʿ� �ϳ��� 60 ��ŭ �����ϰ� �ٸ� �ϳ��� 30��ŭ �����ϰԵ�
        //tr.position += new Vector3(0, 0, 1);
        // Time.deltaTime ���������Ӱ� ���� ������ ���� �ɸ� �ð�
        // ��, Time.deltaTime�� �����ָ� ��⼺�ɿ� ������� �ʴ� ���� ��ȭ���� �������ִ�
        //tr.position += new Vector3(0, 0, speed) * Time.deltaTime;
        // Physics ���� �����͸� ó���Ҷ� ���� �����
        //tr.position += new Vector3(0, 0, 1) * Time.fixedDeltaTime; 
        // ������Ʈ �̵�
        float h = Input.GetAxis("Horizontal"); // ����
        float v = Input.GetAxis("Vertical"); // ����

        Debug.Log("h = " + h); 
        Debug.Log($"v = {v}"); // Ű���� ��� ����Ű ����
        // Z axis forward, backward
        // X axis left, right
        // Y axis up, down
        //tr.position += new Vector3(h, 0, v) * speed * Time.deltaTime;
        //Translate�� �Լ����ٰ� ���� ��û�ϴ°�

        // �Ʒ�ó�� ���� ���ÿ� ���������� ���������� ���⺤���� ũ�Ⱑ 1�� �Ѿ�� ���⿡ ���� �ӵ��� �������� �ʴ�.
        /*Vector3 movePos = new Vector3(h, 0, v) * speed * Time.deltaTime;
        tr.Translate(movePos);*/

        // Vector ����� ũ�⸦ ��� ������ ����
        // Ư�� Vector ũ�Ⱑ 1�� ���͸� ��������(Unit Vector)
        // �����̰� ���� ���⿡ ���� �������� * �ӵ��� ��ü�� ������
        Vector3 dir = new Vector3(h, 0, v).normalized;
        Vector3 moveVec = dir * speed * Time.deltaTime;
        //tr.Translate(moveVec);

        //tr.Translate(moveVec, Space.Self); // local ��ǥ�� �����̵�
        tr.Translate(moveVec, Space.World); // global(world) ��ǥ�� �����̵�

        //������Ʈ ȸ��(Rotation)
        // =============================================================================
        //tr.Rotate(new Vector3(0f, 30f, 0f)); //Y������ 30 radian ��ŭ ȸ���϶�. Degree 0~360 ���� ��Ÿ���� ����, RAfian 0~2 pi ����
        //���콺 �Է�
        float r = Input.GetAxis("Mouse X");
        Vector3 rotateVec = Vector3.up * rotateSpeed * r * Time.deltaTime;
        tr.Rotate(rotateVec);
        
    }
    // FixedUpdate�� ���� �����Ӹ��� ȣ��Ǵ� �Լ�    
}
