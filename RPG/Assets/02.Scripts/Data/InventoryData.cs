using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인벤토리 안에 아이템 이름과 데이터 
/// </summary>

public class InventoryData
{
    public List<InventoryItemData> items; // 소지한 아이템의 데이터 리스트
    public List<EquipmentItemData> equipItems; // 장착중인 아이템들

    /// <summary>
    /// 리스트 초기화
    /// </summary>
    public InventoryData() 
    {
        items = new List<InventoryItemData>();
        equipItems = new List<EquipmentItemData>();
    }

    /// <summary>
    /// 아이템 데이터 세팅
    /// </summary>
    /// <param name="type"> 타입 (장비, 소비, 기타) </param>
    /// <param name="itemName"> 아이템 이름 </param>
    /// <param name="num"> 보유 갯수 </param>
    /// <param name="slotID"> 해당 아이템이 존재하는 인벤토리 슬롯 </param>
    public void SetItemData(Item item, int num, int slotID)
    {
        InventoryItemData oldData = items.Find(x => x.type == item.type && x.slotID == slotID); // 이미 해당 슬롯에 아이템이 존재하는지

        // 이미 해당 슬롯에 아이템 데이터가 존재하면 삭제
        if (oldData != null) 
        {
            items.Remove(oldData);
        }

        // 인자들로 아이템 데이터 추가
        items.Add(new InventoryItemData() // 아이템 추가
        {
            key = item.key,
            type = item.type,
            itemName = item.name,
            num = num,
            slotID = slotID,
        });
    }

    /// <summary>
    /// 아이템 데이터 삭제
    /// </summary>
    /// <param name="type"> 타입 (장비, 소비, 기타) </param>
    /// <param name="itemName"> 아이템 이름 </param>
    /// <param name="slotID"> 해당 아이템이 존재하는 슬롯 </param>
    public void RemoveItemData(ItemType type, string itemName, int slotID) // 아이템 삭제 함수
    {
        // 해당 슬롯에 아이템이 존재하면 삭제
        InventoryItemData oldData = items.Find(x => x.type == type && x.itemName == itemName && x.slotID == slotID);
        if (oldData != null)
        {
            items.Remove(oldData);
        }
    }

    /// <summary>
    /// 장비하고 있는 아이템 제이터 세팅 함수
    /// </summary>
    /// <param name="type"> 장비 종류 </param>
    /// <param name="itemName"> 장비 아이템 이름 </param>
    public void SetEquipmentItemData(Item item, EquipmentType type)
    {
        EquipmentItemData tmpData = equipItems.Find(x => x.type == type);
        if (tmpData != null)
        {
            tmpData.type = type;
            tmpData.itemName = item.name;
        }
        else
        {
            tmpData = new EquipmentItemData()
            {
                key = item.key,
                type = type,
                itemName = item.name
            };
            equipItems.Add(tmpData);
        }
    }

    /// <summary>
    /// 장비아이템을 장착해제 했을때 호출해서 기존에 장착 데이터를 삭제함
    /// </summary>
    /// <param name="type"> 장비 종류 </param>
    public void RemoveEquipmentItemData(EquipmentType type)
    {
        EquipmentItemData tmpData = equipItems.Find(x => x.type == type);

        if (tmpData != null)
            equipItems.Remove(tmpData);
    }
}




