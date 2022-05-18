using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour , IPointerDownHandler
{
    public bool isItemExist
    {
        get
        {
            return _num > 0 ? true : false;
        }
    }
    private int _id;
    public int id
    {
        set
        {
            _id = value;
        }
        get
        {
            return _id;
        }
    }
    
    private int _num;
    public int num
    {
        set
        {
            if (_num != value)
            {
                _num = value;

                if (StageManager.state != StageState.SetUpPlayer)
                {
                    InventoryDataManager.data.SetItemData(_item.type, _item.name, _num, id);
                    InventoryDataManager.SaveData();
                }

                if (_num > 1)
                    _numText.text = _num.ToString();
                else if (_num == 1)
                    _numText.text = "";
                else
                {
                    Clear();
                }
            }

            
        }
        get
        {
            return _num;
        }
    }
    
    private Item _item;

    public Item item
    {
        set
        {
            _item = value;
            if (_item != null)
                _image.sprite = _item.icon;
            else
                _image.sprite = null;
        }
        get
        {
            return _item;
        }
    }

    [SerializeField] private Image _image;
    [SerializeField] private Text _numText;

    public delegate void OnUse();
    public OnUse dOnUse;

    public void SetUp(Item _item, int _num, OnUse useEvent)
    {
        if (_item != null)
        {
            dOnUse = useEvent;
            item = _item;
            num = _num;
        }
        else
        {
            Clear();
        }
        
    }

    public void Clear()
    {
        _item = null;
        _num = 0;
        dOnUse = null;
        _numText.text = "";
        _image.sprite = null;

    }

    

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isItemExist)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (InventoryItemHandler.instance.gameObject.activeSelf == false)
                {
                    InventoryItemHandler.instance.SetUp(this, _item.icon);
                    InventoryItemHandler.instance.gameObject.SetActive(true);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (dOnUse != null)
                {
                    dOnUse();
                }
            }
        }
    }
}
