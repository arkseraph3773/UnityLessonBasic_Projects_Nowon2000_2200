using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemHandler : MonoBehaviour
{
    public static InventoryItemHandler instance;


    public Image _image;

    private InventorySlot _slot;

    private GraphicRaycaster _graphicRaycaster; //ui ����ĳ���� �ϴ� ������Ʈ
    private PointerEventData _pointerEventData; // ���콺 �̺�Ʈ ������
    private EventSystem _eventSystem; // �̺�Ʈ�� ó���ϴ� ��ü


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        gameObject.SetActive(false);
        _graphicRaycaster = transform.parent.GetComponent<GraphicRaycaster>();
        _eventSystem = transform.parent.GetComponent<EventSystem>();
    }

    private void Update()
    {
        // ���콺 ���ʹ�ư
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // �߻��� �̺�Ʈ�� ���� ���콺 �̺�Ʈ ������
            _pointerEventData = new PointerEventData(_eventSystem); // ���� �̺�Ʈ���� ���콺 �̺�Ʈ �����͸� ���� ����
            _pointerEventData.position = Input.mousePosition; // ���콺 �Է� �������� ���� ���콺 ��ġ�� ���콺 �̺�Ʈ ������ ��ġ��

            List<RaycastResult> results = new List<RaycastResult>(); // ����ĳ��Ʈ ����
            _graphicRaycaster.Raycast(_pointerEventData, results); // UI ����ĳ��Ʈ

            // UI ĳ��Ʈ��
            if (results.Count > 0)
            {
                // item Slot �ִ���
                foreach (var result in results)
                {
                    if (result.gameObject.TryGetComponent(out InventorySlot slot))
                    {
                        // ���Թ�ȣ�� ���Կ� �ִ� �������̸� ������ �ƹ��͵� ��������
                        if (_slot.id == slot.id &&
                            _slot.itemName == slot.itemName)
                        {
                            gameObject.SetActive(false);
                        }
                        // ĳ���õ� ���԰� ���� ������ ������
                        else
                        {
                            //InventorySlot tmpSlot = _slot;
                            Item oldItem = ItemAssets.GetItem(_slot.itemName);
                            Item newItem = ItemAssets.GetItem(slot.itemName);
                            _slot.SetUp(newItem, slot.num);
                            slot.SetUp(oldItem, _slot.num);
                            gameObject.SetActive(false);
                        }
                        break;
                    }
                }
            }
        }
    }

   

    private void FixedUpdate()
    {
        transform.position = Input.mousePosition;
    }

    public void SetUp(InventorySlot slot, Sprite icon)
    {
        _slot = slot;
        _image.sprite = icon;
    }
}
