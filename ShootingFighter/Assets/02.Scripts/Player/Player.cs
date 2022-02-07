using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // ������Ƽ ( Property )
    // set �Լ�, get�Լ��� ���� �������� �ʾƵ� �Ǵ� ������ ���� ź��
    private float m_HP; // �����̸� �տ� _ Ȥ�� m_ Ȥ�� m �� ������ ��� ���� (Ư�� private) ��Ī
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
