using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    Transform tr;
    // public 접근제한자를 사용하면 Inspector창에 노출이 된다.
    // 만약에 다른 클래스로부터의 접근은 제한하면서 Inspector창에 노출시키고 싶으면 [SerializeField] 속성을 사용한다.
    [SerializeField] private float speed = 1; //[SerializeField]private 
    public float rotateSpeed;
    // Inspector창의 값이 초기화 값보다 우선순위다.

    void Start()
    {
        //transform 에 접근해서 좌표에 대한 데이터를 변경시켜도 되지만 굳이 transform 멤버변수 tr을 선언해서
        //transform component를 대입한 후에 사용하는 이유는
        //캐시 메모리 문제때문 (캐시 : 임시로 연산이 용이하도록 생성한 메모리)
        //transform을 사용하면 이 멤버변수를 호출할때 마다 gameobject에 접근해서 getComponent로 transform 성분을 가져옴
        //하지만 transform 멤버 변수tr에다가 한번 넣어놓고 사용하면
        //tr 은 사용할때마다 처음에 넣어줬던 transform component에 바로 접근하기 때문에
        //동시에 아주아주 많은 게임오브젝트들의 transform 컴포넌트를 써야하면 그때는 퍼포먼스에서 차이가 난다.

        //tr = this.gameObject.transform; // 게임오브젝트의 멤버변수 transform을 대입한다( 멤버변수 transform에 뭐가 들어있는지 내부적으로 알수없다
        //tr = this.gameObject.GetComponent<Transform>(); // 이 클래스를 포함하는 게임 오브젝트에게서 Transform 컴포넌트를 가져온다
        /*tr = this.gameObject.GetComponent<Transform>();*/
        tr = gameObject.GetComponent<Transform>(); //보편적으로 많이 사용함 //게임오브젝트에게서 트랜스폼 컴포넌트를 가져온다
        /*tr = GetComponent<Transform>();
        tr = gameObject.transform;
        tr = GetComponent("Transform") as Transform;
        tr = transform; //여기까지 tr 불러오는법*/
    }
    // Update 는 매프레임마다 호출되는 함수
    void Update()
    {
        // position
        // ======================================================================
        // 1프레임당 z 축 1전진
        // 만약에 컴퓨터 사양이 달라서 하나는 60FPS, 다른 하나는 30FPS
        // -> 1초에 하나는 60 만큼 전진하고 다른 하나는 30만큼 전진하게됨
        //tr.position += new Vector3(0, 0, 1);
        // Time.deltaTime 직전프레임과 현재 프레임 사이 걸린 시간
        // 즉, Time.deltaTime을 곱해주면 기기성능에 관계없이 초당 같은 변화량을 가질수있다
        //tr.position += new Vector3(0, 0, speed) * Time.deltaTime;
        // Physics 관련 데이터를 처리할때 자주 사용함
        //tr.position += new Vector3(0, 0, 1) * Time.fixedDeltaTime; 
        // 오브젝트 이동
        float h = Input.GetAxis("Horizontal"); // 수직
        float v = Input.GetAxis("Vertical"); // 수평

        Debug.Log("h = " + h); 
        Debug.Log($"v = {v}"); // 키보드 토글 방향키 설정
        // Z axis forward, backward
        // X axis left, right
        // Y axis up, down
        //tr.position += new Vector3(h, 0, v) * speed * Time.deltaTime;
        //Translate는 함수에다가 변경 요청하는것

        // 아래처럼 쓰면 동시에 여러축으로 움직였을때 방향벡터의 크기가 1이 넘어가서 방향에 따라 속도가 일정하지 않다.
        /*Vector3 movePos = new Vector3(h, 0, v) * speed * Time.deltaTime;
        tr.Translate(movePos);*/

        // Vector 방향과 크기를 모두 가지는 성질
        // 특히 Vector 크기가 1인 벡터를 단위벡터(Unit Vector)
        // 움직이고 싶은 방향에 대한 단위벡터 * 속도로 물체를 움직임
        Vector3 dir = new Vector3(h, 0, v).normalized;
        Vector3 moveVec = dir * speed * Time.deltaTime;
        //tr.Translate(moveVec);

        //tr.Translate(moveVec, Space.Self); // local 좌표계 기준이동
        tr.Translate(moveVec, Space.World); // global(world) 좌표계 기준이동

        //오브젝트 회전(Rotation)
        // =============================================================================
        //tr.Rotate(new Vector3(0f, 30f, 0f)); //Y축으로 30 radian 만큼 회전하라. Degree 0~360 까지 나타내는 단위, RAfian 0~2 pi 단위
        //마우스 입력
        float r = Input.GetAxis("Mouse X");
        Vector3 rotateVec = Vector3.up * rotateSpeed * r * Time.deltaTime;
        tr.Rotate(rotateVec);
        
    }
    // FixedUpdate는 고정 프레임마다 호출되는 함수    
}
