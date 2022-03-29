using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;

    private void Awake()
    {
        instance = this;
        
    }

    public static IEnumerator ActiveBuff(Enemy enemy, Buff buff)
    {
        buff.OnActive(enemy); //버프 발동

        bool doBuff = true;

        float timeMark = Time.time;
        while(doBuff && 
              Time.time - timeMark < buff.duration &&
              enemy != null) //Time.time게임시간 - timeMark발동시간 < buff.duration버프지속시간
        {
            doBuff = buff.OnDuration(enemy);
            yield return null; //해당 반복문이 프레임당 한번 실행되게 하기위함.
        }

        if (enemy != null)
        {
            buff.OnDeactive(enemy);
        }

        
    }
}
