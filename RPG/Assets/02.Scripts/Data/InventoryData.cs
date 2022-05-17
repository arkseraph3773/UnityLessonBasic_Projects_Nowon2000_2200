using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인벤토리 안에 아이템 이름과 데이터 
/// </summary>

public class InventoryData
{
    public List<InventoryItemData> items; // 소지한 아이템의 데이터 리스트

    /// <summary>
    /// 리스트 초기화
    /// </summary>
    public InventoryData() 
    {
        items = new List<InventoryItemData>();
    }

    /// <summary>
    /// 아이템 데이터 세팅
    /// </summary>
    /// <param name="type"> 타입 (장비, 소비, 기타) </param>
    /// <param name="itemName"> 아이템 이름 </param>
    /// <param name="num"> 보유 갯수 </param>
    /// <param name="slotID"> 해당 아이템이 존재하는 인벤토리 슬롯 </param>
    public void SetItemData(ItemType type, string itemName, int num, int slotID)
    {
        InventoryItemData oldData = items.Find(x => x.type == type && x.slotID == slotID); // 이미 해당 슬롯에 아이템이 존재하는지

        // 이미 해당 슬롯에 아이템 데이터가 존재하면 삭제
        if (oldData != null) 
        {
            items.Remove(oldData);
        }

        // 인자들로 아이템 데이터 추가
        items.Add(new InventoryItemData() // 아이템 추가
        {
            type = type,
            itemName = itemName,
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
}

/// <summary>
/// 아이템 객체 자체를 저장하면 너무 크기때문에 데이터를 최소화해서 저장하기 위해 새로 만든 아이템 데이터 관리용 클래스
/// </summary>
[System.Serializable] // json포맷 등으로 직렬화/역직렬화 할수있게 하기위한 속성
public class InventoryItemData
{
    public ItemType type; // (장비, 소비, 기타)
    public string itemName; // 이름
    public int num; // 보유 갯수
    public int slotID; // 해당 아이템이 존재하는 슬롯번호
}


