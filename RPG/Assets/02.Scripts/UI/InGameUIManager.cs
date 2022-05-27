using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;
    public CMDState CMDState;

    [SerializeField] private GameObject statsView;
    [SerializeField] private GameObject inventoryView;
    [SerializeField] private GameObject equipmentView;

    

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        StartCoroutine(E_Init());

    }

    IEnumerator E_Init()
    {
        // �̱��� �ν��Ͻ�ȭ
        statsView.SetActive(true); 
        inventoryView.SetActive(true);
        equipmentView.SetActive(true);

        yield return new WaitUntil(() => StatsView.instance != null &&
                                         InventoryView.instance != null &&
                                         EquipmentView.instance != null);

        yield return new WaitUntil(() => StatsView.instance.CMDState == CMDState.Ready &&
                                         InventoryView.instance.CMDState == CMDState.Ready &&
                                         EquipmentView.instance.CMDState == CMDState.Ready);

        CMDState = CMDState.Ready;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryView.SetActive(inventoryView.activeSelf == false);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            equipmentView.SetActive(equipmentView.activeSelf == false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            statsView.SetActive(statsView.activeSelf == false);
        }
    }
}
