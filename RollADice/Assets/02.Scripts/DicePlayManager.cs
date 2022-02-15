using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePlayManager : MonoBehaviour
{
    static public DicePlayManager instance;
    private int currentTileIndex;
    //private int previousTileIndex;
    private int _diceNum;
    public int diceNum // Awake 함수와 RollADice에서 이중초기화하는것을 방지하기 위해서 사용
    {
        set
        {
            _diceNum = value;
            diceNumText.text = _diceNum.ToString();
        }
        get
        {
            return _diceNum;
        }
    }
    private int _goldenDiceNum;
    public int goldenDiceNum
    {
        set
        {
            _goldenDiceNum = value;
            goldenDiceNumText.text = _goldenDiceNum.ToString();
        }
        get
        {
            return _goldenDiceNum;
        }
    }
    public int diceNumInit;
    public int goldenDiceNumInit;
    public List<Transform> list_MapTile; //타일 정보

    public Text diceValueText;
    public Text diceNumText;
    public Text goldenDiceNumText;
    private void Awake()
    {
        instance = this; //if(instance == null) instance = this;사용은 두종류의 매니저가 있을때
        diceNum = diceNumInit;
        goldenDiceNum = goldenDiceNumInit;
    }
    public void RollADice()
    {
        if (diceNum < 1) return;

        diceNum--;
        int diceValue = Random.Range(1, 7);
        diceValueText.text = diceValue.ToString(); // string 을 int형변환
        MovePlayer(diceValue);
    }
    public void RollAGoldenDice(int diceValue)
    {
        if(goldenDiceNum < 1) return;

        goldenDiceNum--;
        MovePlayer(diceValue);
    }
    // 해당 눈금만큼 플레이어를 이동
    private void MovePlayer(int diceValue)
    {
        currentTileIndex += diceValue;
        if (currentTileIndex >= list_MapTile.Count)
        {
            currentTileIndex -= (list_MapTile.Count);
        }
        Vector3 target = GetTilePosition(currentTileIndex);
        Player.instance.Move(target);
        list_MapTile[currentTileIndex].GetComponent<TileInfo>().TileEvent();
    }
    private Vector3 GetTilePosition(int tileIndex)
    {
        Vector3 pos = list_MapTile[tileIndex].position;
        return pos;
    }
}
