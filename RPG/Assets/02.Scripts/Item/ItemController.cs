using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;
    public int num = 1;

    [Header("Floating Effect")]
    public bool doFloatingEffect;
    public float floatingSpeed;
    public float floatingHeight;

    [Header("Dropping Effect")]
    public float popForce = 1f;
    public float rotateSpeed = 1f;

    

    [Header("Kinematics")]
    private Rigidbody rb;
    private BoxCollider col;

    public LayerMask groundLayer;
    private Transform rendererTransform;
    private Vector3 rendererOffset;
    private float elapsedFixedTime;
    private Vector3 eulerAngleOrigin;
    private Coroutine coroutine = null; 
    //=========================================================================
    //***************************Public Methods *******************************
    //=========================================================================

    public void PickUp(Player player)
    {
        if (coroutine == null)
        {
            // to do -> 인벤토리에 아이템 추가
            coroutine = StartCoroutine(E_PickUpEffect(player));
        }
    }

    //=========================================================================
    //***************************Private Methods ******************************
    //=========================================================================

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        rendererTransform = transform.Find("Renderer");
        rendererOffset = rendererTransform.localPosition;
        eulerAngleOrigin = rendererTransform.eulerAngles;
    }

    private void OnEnable()
    {
        elapsedFixedTime = 0;
        StartCoroutine(E_ShowEffect());
    }

    private void FixedUpdate()
    {
        if (doFloatingEffect)
        {
            Floating();
        }
    }

    private void Floating()
    {
        rendererTransform.localPosition = rendererOffset + 
                                          new Vector3(0f,
                                                      floatingHeight * Mathf.Sin(floatingSpeed * elapsedFixedTime),
                                                      0f);
        elapsedFixedTime += Time.fixedDeltaTime;
    }

    IEnumerator E_ShowEffect()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * popForce, ForceMode.Impulse);

        while (doFloatingEffect == false)
        {
            Collider[] grounds = Physics.OverlapSphere(rb.position - new Vector3(0f, col.size.y / 2, 0f), 0.3f, groundLayer);

            if(grounds.Length > 0)
            {
                doFloatingEffect = true;
                rb.velocity = Vector3.zero;
                break;
            }

            rendererTransform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0f, rotateSpeed * Time.deltaTime));
            yield return null;
        }
        rendererTransform.eulerAngles = eulerAngleOrigin;
    }

    IEnumerator E_PickUpEffect(Player player)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * popForce, ForceMode.Impulse);
        yield return new WaitForEndOfFrame();

        rb.useGravity = false;
        bool isReachedToPlayer = false;

        MeshRenderer meshRenderer = rendererTransform.GetComponent<MeshRenderer>();
        float fadeAlpha = 1f;
        Color fadeColor = meshRenderer.material.color;

        float pickUpTimer = 1f;

        while (pickUpTimer > 0 && isReachedToPlayer == false)
        {
            // 아이템과 플레이어 사이 거리
            float distance = (Vector3.Distance(player.transform.position, rb.position));
            // 아이템이 플레이어에게 도달함
            if (distance < 0.1f)
            {
                isReachedToPlayer = true;
            }

            // 아이템 -> 플레이어로 날아갈 속도 벡터
            Vector3 moveVec = player.transform.position - rb.position;

            // 플레이어에게 날아갈 속도
            rb.position += moveVec * Time.deltaTime;

            // 색변경
            fadeAlpha -= Time.deltaTime;
            fadeColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeAlpha);
            meshRenderer.material.color = fadeColor;
            
            pickUpTimer -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        
    }
}
