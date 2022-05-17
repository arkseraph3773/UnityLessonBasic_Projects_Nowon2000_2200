using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인벤토리 데이터를 읽고/쓰고/저장하고/지우는 클래스
/// </summary>

public class InventoryDataManager
{
    #region 싱글톤
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
    public static string dirPath; // 인벤토리 데이터를 저장할 디렉토리 경로

    /// <summary>
    /// 디렉토리 경로 초기화
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
            throw new System.Exception("인벤토리 데이터 불러오기 실패");
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
