using UnityEngine;

[CreateAssetMenu(fileName = "New TowerInfo", menuName = "Tower/Create New TowerInfo")]
public class TowerInfo : ScriptableObject
{
    public TowerType type;
    public int lever; //타워레벨
    public int price; //가격
}

public enum TowerType
{
    MachineGun,
    Missile,
    Laser,
}