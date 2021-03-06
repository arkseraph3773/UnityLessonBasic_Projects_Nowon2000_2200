using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public static bool isReady;

    private static CMDState _CMDState;
    public static CMDState CMDState
    {
        set
        {
            if (instance == null) return;

            _CMDState = value;
            switch (_CMDState)
            {
                case CMDState.Idle:
                    break;
                case CMDState.Ready:
                    instance._machineManager.controllable = true;
                    break;
                case CMDState.Busy:
                    instance._machineManager.controllable = false;
                    break;
                case CMDState.Error:
                    instance._machineManager.controllable = false;
                    break;
                default:
                    break;
            }
        }
        get
        {
            return _CMDState;
        }
    }

    private Stats _stats;
    public Stats stats
    {
        set
        {
            _stats = value;

            if (StatsView.instance.gameObject.activeSelf)
                StatsView.instance.Refresh();
        }
        get
        {
            return _stats;
        }
    }

    private Stats _additionalStats;
    public Stats additionalStats
    {
        set
        {
            _additionalStats = value;

            if (StatsView.instance.gameObject.activeSelf)
                StatsView.instance.Refresh();
        }
        get
        {
            return _additionalStats;
        }
    }




    public float hpMax;
    private float _hp;

    public float hp
    {
        set
        {
            if (value < 0)
            {
                value = 0;
                // do die
            }

            _hp = value;
            stats.HP = (int)_hp;
            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.SetHPBar(_hp / hpMax);
            }
        }
        get
        {
            return _hp;
        }
       
    }

    public float mpMax;
    private float _mp;
    public float mp
    {
        set
        {
            if (value < 0)
            {
                value = 0;
                // do die
            }

            _mp = value;
            stats.MP = (int)_mp;

            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.SetHPBar(_mp / mpMax);
            }
        }
        get
        {
            return _mp;
        }

    }

    private float _exp;
    public float exp
    {
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            _exp = value;
            stats.EXP = (int)_exp;

            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.SetEXPBar(_exp / GetEXPRequired(stats.LV));
            }
        }
        get
        {
            return (_exp);
        }
    }

    private int _lv;
    public int lv
    {
        set
        {
            _lv = value;
            stats.LV = _lv;

            if (PlayerUI.instance != null)
            {
                PlayerUI.instance.SetLVText(_lv.ToString());
            }
        }
        get
        {
            return _lv;
        }
    }

    public Transform headPoint;
    public Transform bodyPoint;
    public Transform leftFootPoint;
    public Transform rightFootPoint;
    public Transform weapon1Point;
    public Transform weapon2Point;
    public Transform ringPoint;
    public Transform necklacePoint;


    [Header("????")]
    public Weapon1 weapon1;
    public Weapon2 weapon2;
    public Body body;
    public Head head;
    public Foot leftFoot;
    public Foot rightFoot;
    public Ring ring;
    public Necklace necklace;

    [Header("??????")]
    [SerializeField] private LayerMask NPCLayer;

    private NPC interactableNPC;

    private PlayerStateMachineManager _machineManager;
    private CharacterController _controller;

    //==========================================================================================
    //******************public *****************************************************************
    //==========================================================================================
    public void SetUp(PlayerData data)
    {
        stats = data.stats;
        hpMax = stats.HPMax;
        mpMax = stats.MPMax;
        hp = stats.HP;
        mp = stats.MP;
        lv = stats.LV;
        exp = stats.EXP;

        additionalStats = new Stats();
        isReady = true;
    }

    public bool Equip(EquipmentType equipmentType, GameObject equipmentPrefab)
    {
        Unequip(equipmentType);
        

        switch (equipmentType)
        {
            case EquipmentType.Head:
                head = Instantiate(equipmentPrefab, headPoint).GetComponent<Head>();
                break;
            case EquipmentType.Body:
                body = Instantiate(equipmentPrefab, headPoint).GetComponent<Body>();
                break;
            case EquipmentType.Foot:
                leftFoot = Instantiate(equipmentPrefab, leftFootPoint).GetComponent<Foot>();
                rightFoot = Instantiate(equipmentPrefab, rightFootPoint).GetComponent<Foot>();
                break;
            case EquipmentType.Weapon1:
                weapon1 = Instantiate(equipmentPrefab, weapon1Point).GetComponent<Weapon1>();
                break;
            case EquipmentType.Weapon2:
                weapon2 = Instantiate(equipmentPrefab, weapon2Point).GetComponent<Weapon2>();
                break;
            case EquipmentType.Ring:
                ring = Instantiate(equipmentPrefab, ringPoint).GetComponent<Ring>();
                break;
            case EquipmentType.Necklace:
                necklace = Instantiate(equipmentPrefab, necklacePoint).GetComponent<Necklace>();
                break;
            default:
                break;
        }

        Equipment component = equipmentPrefab.GetComponent<Equipment>();
        additionalStats += component.additionalStats;

        EquipmentView.instance.SetSlot(equipmentType,
                                      component.controller.item);
        return true;
    }

    public bool Unequip(EquipmentType equipmentType)
    {
        if (CheakIsEquipmentExist(equipmentType))
        {
            GameObject equipment = null;

            
            switch (equipmentType)
            {
                case EquipmentType.Head:
                    equipment = headPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Body:
                    equipment = bodyPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Foot:
                    equipment = leftFootPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Weapon1:
                    equipment = weapon1Point.GetChild(0).gameObject;
                    break;
                case EquipmentType.Weapon2:
                    equipment = weapon2Point.GetChild(0).gameObject;
                    break;
                case EquipmentType.Ring:
                    equipment = ringPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Necklace:
                    equipment = necklacePoint.GetChild(0).gameObject;
                    break;
                default:
                    break;
            }

            Equipment component = equipment.GetComponent<Equipment>();
            additionalStats -= component.additionalStats;

            ItemController_Equipment controller = component.controller;
            int remain = InventoryView.instance.GetItemView(ItemType.Equip).AddItem(controller.item, 1, controller.Use);
            // ???????? ????
            if (remain <= 0)
            {
                Destroy(equipment);
                if (equipmentType == EquipmentType.Foot)
                    Destroy(rightFootPoint.GetChild(0).gameObject);
                return true;
            }
        }
        return false;
    }

    public bool Unequip(EquipmentType equipmentType, InventorySlot slot)
    {
        if (CheakIsEquipmentExist(equipmentType))
        {
            GameObject equipment = null;
            switch (equipmentType)
            {
                case EquipmentType.Head:
                    equipment = headPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Body:
                    equipment = bodyPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Foot:
                    equipment = leftFootPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Weapon1:
                    equipment = weapon1Point.GetChild(0).gameObject;
                    break;
                case EquipmentType.Weapon2:
                    equipment = weapon2Point.GetChild(0).gameObject;
                    break;
                case EquipmentType.Ring:
                    equipment = ringPoint.GetChild(0).gameObject;
                    break;
                case EquipmentType.Necklace:
                    equipment = necklacePoint.GetChild(0).gameObject;
                    break;
                default:
                    break;
            }

            
            ItemController_Equipment controller = equipment.GetComponent<Equipment>().controller;
            int remain = InventoryView.instance.GetItemView(ItemType.Equip).AddItem(controller.item, 1, controller.Use);
            if (slot.isItemExist == false)
            {
                slot.SetUp(controller.item, 1, controller.Use);
                return true;
            }
            
        }
        return false;
    }

    private void Awake()
    {
        instance = this;

        //get equioments
        weapon1 = GetComponentInChildren<Weapon1>();
        _machineManager = GetComponent<PlayerStateMachineManager>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            interactableNPC != null &&
            interactableNPC.CMDState == CMDState.Ready)
        {
            interactableNPC.StartTalk();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit NPCHit;

        if (Physics.Raycast(transform.position + Vector3.up * _controller.height / 2, transform.TransformDirection(Vector3.forward), out NPCHit, 1f, NPCLayer))
        {
            Debug.DrawRay(transform.position + Vector3.up * _controller.height / 2, transform.TransformDirection(Vector3.forward) * NPCHit.distance, Color.green);

            if (interactableNPC == null)
                interactableNPC = NPCHit.collider.GetComponent<NPC>();
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up * _controller.height / 2, transform.TransformDirection(Vector3.forward) * 1f, Color.red);
            if (interactableNPC != null)
                interactableNPC = null;
        }
    }

    private float GetEXPRequired(int level)
    {
        return 1000 + (Mathf.Pow(Mathf.Exp(1), level)) * 10;
    }

    

    private bool CheakIsEquipmentExist(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.Head:
                return headPoint.childCount > 0 ? true : false;
            case EquipmentType.Body:
                return bodyPoint.childCount > 0 ? true : false;
            case EquipmentType.Foot:
                return leftFootPoint.childCount > 0 ? true : false;
            case EquipmentType.Weapon1:
                return weapon1Point.childCount > 0 ? true : false;
            case EquipmentType.Weapon2:
                return weapon2Point.childCount > 0 ? true : false;
            case EquipmentType.Ring:
                return ringPoint.childCount > 0 ? true : false;
            case EquipmentType.Necklace:
                return necklacePoint.childCount > 0 ? true : false;
            default:
                throw new System.Exception("???????? ???? : ?????? ???? ?????? ?????????? ??????");

        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            if (Input.GetKey(KeyCode.Z))
            {
                other.gameObject.GetComponent<ItemController>().PickUp(this);
            }
        }
    }
}
