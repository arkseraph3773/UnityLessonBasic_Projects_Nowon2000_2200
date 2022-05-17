using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 이름과 스텟 데이터
/// </summary>
public class PlayerData // 단지 데이터 처리용 클래스이기 때문에 Monobehavior 상속받을 필요없다.
{
    public string nickName;
    public Stats stats;
    
    /// <summary>
    /// 스텟 초기화
    /// todo -> 나중에 스텟을 위한 테이블을 참조하는 형태로 수정해야함
    /// </summary>
    /// <param name="newNickName"> 생성할 캐릭터 이름</param>
    public PlayerData(string newNickName) // Monobehavior를 상속받지 않았기 때문에 생성자 사용가능
    {
        nickName = newNickName;
        stats = new Stats()
        {
            LV = 0,
            EXP = 0,

            STR = 10,
            DEX = 10,
            INT = 10,
            LUK = 10,

            HP = 100,
            HPMax = 100,
            MP = 50,
            MPMax = 50,

            ATK = 15,
            DEF = 10,

            statPoint = 0,

        };
    }

    
}

[System.Serializable] // json포맷 등으로 직렬화/역직렬화 할수있게 하기위한 속성
public class Stats
{
    public int LV;
    public int EXP;

    public int STR;
    public int DEX;
    public int INT;
    public int LUK;

    public int HP;
    public int HPMax;
    public int MP;
    public int MPMax;

    public int ATK;
    public int DEF;

    public int statPoint;

}


/*[System.Serializable]

public struct SavePoint
{
    public int mapIdx;
    public float coordx;
    public float coordy;
}
*/
