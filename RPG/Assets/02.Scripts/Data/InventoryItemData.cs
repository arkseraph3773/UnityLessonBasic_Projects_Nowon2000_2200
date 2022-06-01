using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ��ü ��ü�� �����ϸ� �ʹ� ũ�⶧���� �����͸� �ּ�ȭ�ؼ� �����ϱ� ���� ���� ���� ������ ������ ������ Ŭ����
/// </summary>
[System.Serializable] // json���� ������ ����ȭ/������ȭ �Ҽ��ְ� �ϱ����� �Ӽ�
public class InventoryItemData
{
    public string key;
    public ItemType type; // (���, �Һ�, ��Ÿ)
    public string itemName; // �̸�
    public int num; // ���� ����
    public int slotID; // �ش� �������� �����ϴ� ���Թ�ȣ
}
