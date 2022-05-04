using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

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

    
    private void Awake()
    {
        instance = this;
        _hp = hpMax;
        _mp = mpMax;
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
