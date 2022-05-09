using UnityEngine;
public class ItemController_Spend : ItemController, IUseable
{
    public virtual void Use()
    {
        InventoryView.instance.GetItemView(ItemType.Spend).Remove(item, 1);
    }
    public override void PickUp(Player player)
    {
        if (coroutine == null)
        {
            // to do -> 인벤토리에 아이템 추가
            int remain = InventoryView.instance.GetItemView(item.type).AddItem(item, num, Use);
            Debug.Log($"플레이어가 아이템 {item.name} {num - remain} 개 획득했습니다");

            if (remain <= 0)
                coroutine = StartCoroutine(E_PickUpEffect(player));

        }
    }
}


