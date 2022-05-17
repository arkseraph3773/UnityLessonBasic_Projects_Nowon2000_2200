using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �κ��丮 �����͸� �а�/����/�����ϰ�/����� Ŭ����
/// </summary>

public class InventoryDataManager
{
    #region �̱���
    private static InventoryDataManager _instance;
    public static InventoryDataManager instance
    {
        get
        {
            if (_instance == null)
                _instance = new InventoryDataManager();
            return _instance;
        }
    }
    #endregion
    public static string dirPath; // �κ��丮 �����͸� ������ ���丮 ���

    /// <summary>
    /// ���丮 ��� �ʱ�ȭ
    /// </summary>
    public InventoryDataManager()
    {
        dirPath = $"{Application.persistentDataPath}/InventoryData";
    }


    private static InventoryData _data;
    public static InventoryData data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
            SaveData();
        }
    }

    public static InventoryData CreateData(string nickName)
    {

        if (System.IO.Directory.Exists(dirPath) == false)
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }

        data = new InventoryData();

        string jsonPath = dirPath + $"/Inventory_{nickName}.json";
        string jsonData = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(jsonPath, jsonData);

        return data;
    }

    public static InventoryData LoadData(string nickName)
    {
        string jsonPath = dirPath + $"/Inventory_{nickName}.json";
        if (System.IO.File.Exists(jsonPath))
        {
            string jsonData = System.IO.File.ReadAllText(jsonPath);
            data = JsonUtility.FromJson<InventoryData>(jsonData);
            return data;
        }
        else
        {
            throw new System.Exception("�κ��丮 ������ �ҷ����� ����");
        }
    }

    public static InventoryData[] GetAllDatas()
    {
        string[] jsonPaths = System.IO.Directory.GetFiles(dirPath);
        InventoryData[] inventoryDatas = new InventoryData[jsonPaths.Length];

        for (int i = 0; i < jsonPaths.Length; i++)
        {
            string jsonData = System.IO.File.ReadAllText(jsonPaths[i]);
            inventoryDatas[i] = JsonUtility.FromJson<InventoryData>(jsonData);
        }
        return inventoryDatas;
    }

    public static void SaveData()
    {
        if (data == null)
            return;

        string jsonPath = dirPath + $"/Inventory_{PlayerDataManager.data.nickName}.json";
        string jsonData = JsonUtility.ToJson(_data);
        System.IO.File.WriteAllText(jsonPath, jsonData);
    }

    public static bool RemoveData(string nickName)
    {
        string jsonPath = dirPath + $"/Inventory_{nickName}.json";
        if (System.IO.File.Exists(jsonPath))
        {
            System.IO.File.Delete(jsonPath);
            return true;
        }
        return false;
    }
}
