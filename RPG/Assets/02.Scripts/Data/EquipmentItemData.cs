[System.Serializable] // json포맷 등으로 직렬화/역직렬화 할수있게 하기위한 속성
public class EquipmentItemData
{
    public string key;
    public EquipmentType type; // 장비 아이템 타입
    public string itemName; // 장비 아이템 이름
}
