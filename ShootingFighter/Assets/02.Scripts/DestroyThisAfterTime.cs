using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �� ��ũ��Ʈ�� ����Ʈ �����տ� �־ ���
public class DestroyThisAfterTime : MonoBehaviour
{
    [SerializeField] float destroyDelay;

    private void OnEnable()
    {
        Destroy(gameObject, destroyDelay);
    }
}
