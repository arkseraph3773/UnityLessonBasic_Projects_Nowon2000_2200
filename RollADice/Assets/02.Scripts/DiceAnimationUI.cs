using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DiceAnimationUI : MonoBehaviour
{
    public Image diceAnimationImage;
    public float diceAnimationTime;

    private List<Sprite> list_DiceImage = new List<Sprite>();
    private void Start()
    {
        LoadDiceImages();
    }
    private void LoadDiceImages() // 스프라이트 불러오기
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("DiceImages");
        list_DiceImage = sprites.ToList();

        foreach (var item in list_DiceImage)
        {
            Debug.Log(item.name);
        }
    }
}
