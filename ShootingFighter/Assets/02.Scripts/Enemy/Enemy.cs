using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float m_HP; // 변수이름 앞에 _ 혹은 m_ 혹은 m 이 붙으면 멤버 변수 (특히 private) 지칭
    public float HP
    {
        set
        {
            m_HP = value;
            int HPint = (int)m_HP;
            HPSlider.value = m_HP / HPMax;
            if(m_HP <= 0)
            {
                DestroyByPlayerWeapon();
            }
        }
        get
        {
            return m_HP;
        }
    }
    [SerializeField] float HPInit;
    [SerializeField] float HPMax;
    [SerializeField] Slider HPSlider;
    [SerializeField] private float score;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private GameObject destroyEffect;
    Transform tr;
    Vector3 dir;
    Vector3 deltaMove;
    [SerializeField] private int AIPercent;
    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
    }
    private void Start()
    {
        HP = HPInit;
        SetTarget_RandomlyToPlayer(AIPercent);
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        deltaMove = dir * speed * Time.deltaTime;
        tr.Translate(deltaMove, Space.World);
    }
    //적을 랜덤으로 유저한테 가게 하는 함수(random 사용)
    private void SetTarget_RandomlyToPlayer(int percent)
    {
        int tmpRandomValue = Random.Range(0, 100);
        if (percent > tmpRandomValue)
        {
            GameObject target = GameObject.Find("Player");
            if(target == null)
            {
                dir = Vector3.back;
            }
            else
            {
                dir = (target.transform.position - tr.position).normalized;
            }
        }
        else
        {
            dir = Vector3.back;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.HP -= damage;
            /*int HPint = (int)player.HP; // 형변환
            player.HPText.text = HPInt.ToString();
            player.HPSlider.value = player.HP / player.HPMax;*/ //쓰지마
            // 파괴이펙트
            GameObject effectGO = Instantiate(destroyEffect);
            effectGO.transform.position = tr.position;
            Destroy(this.gameObject);
        }

        /*if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))
        {
            DestroyByPlayerWeapon();
        }*/
        
    }
    public void DestroyByPlayerWeapon()
    {
        // todo -> 파괴 이펙트
        GameObject effectGO = Instantiate(destroyEffect);
        effectGO.transform.position = tr.position;

        GameObject playerGO = GameObject.Find("Player");
        playerGO.GetComponent<Player>().Score += score;
        //Destroy(effectGO, 3f); //이펙트 사라지게 함 //안쓰고 프리팹에 DestroyThisAfterTime 넣어서 사용해도 됨

        //Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }
}
