using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 이 스크립트를 이펙트 프리팹에 넣어서 사용
public class DestroyThisAfterTime : MonoBehaviour
{
    [SerializeField] float destroyDelay;

    private void OnEnable()
    {
        Destroy(gameObject, destroyDelay);
    }
}
