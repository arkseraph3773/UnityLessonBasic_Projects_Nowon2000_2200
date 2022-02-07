using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // 프로퍼티 ( Property )
    // set 함수, get함수를 따로 정의하지 않아도 되는 편리함을 위해 탄생
    private float m_HP; // 변수이름 앞에 _ 혹은 m_ 혹은 m 이 붙으면 멤버 변수 (특히 private) 지칭
    public float HP
    {
        set 
        {
            m_HP = value;
            int HPint = (int)m_HP;
            HPText.text = HPint.ToString();
            HPSlider.value = m_HP / HPMax;
        }
        get
        {
            return m_HP;
        }
    }
    //public float HP;
    [SerializeField] private float HPInit;
    [SerializeField] private float HPMax;
    [SerializeField] private Text HPText;
    [SerializeField] private Slider HPSlider;

    private float m_score;
    public float Score
    {
        set
        {
            m_score = value;
            int scoreInt = (int)m_score;
            scoreText.text = scoreInt.ToString();
            GameManager.instance.CheckScoreAndMoveStage(scoreInt);
        }
        get
        {
            return m_score;
        }
    }
    [SerializeField] private Text scoreText;
    private void Awake()
    {
        HP = HPInit;
    }
}
