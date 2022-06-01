using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 객체 자체를 저장하면 너무 크기때문에 데이터를 최소화해서 저장하기 위해 새로 만든 아이템 데이터 관리용 클래스
/// </summary>
[System.Serializable] // json포맷 등으로 직렬화/역직렬화 할수있게 하기위한 속성
public class InventoryItemData
{
    public string key;
    public ItemType type; // (장비, 소비, 기타)
    public string itemName; // 이름
    public int num; // 보유 갯수
    public int slotID; // 해당 아이템이 존재하는 슬롯번호
}
