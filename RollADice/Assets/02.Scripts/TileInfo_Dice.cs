using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_Dice : TileInfo
{
    public override void TileEvent()
    {
        base.TileEvent();
        DicePlayManager.instance.diceNum += 1; //++나 += 1 사용 ++ 사용하면 여러번 접근으로 인한 메모리 사용 급증
    }
}
