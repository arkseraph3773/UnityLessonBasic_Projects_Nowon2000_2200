using UnityEngine;

[CreateAssetMenu(fileName = "New TowerInfo", menuName = "Tower/Create New TowerInfo")]
public class TowerInfo : ScriptableObject
{
    public TowerType type;
    public int lever; //Ÿ������
    public int price; //����
}

public enum TowerType
{
    MachineGun,
    Missile,
    Laser,
}