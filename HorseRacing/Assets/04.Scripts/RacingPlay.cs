using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacingPlay : MonoBehaviour
{
    #region �̱�������
    // singleton(�̱���)����
    static public RacingPlay instance; // RacingPlay��ü�� �����Ҽ� �ְ� �ν��Ͻ�

    private void Awake()
    {
        if(instance == null) instance = this;
    }
    #endregion

    private List<PlayerMove> list_PlayerMove = new List<PlayerMove>();
    private List<Transform> list_FinishdePlayer = new List<Transform>();
    [SerializeField] private List<Transform> list_WinPlatform = new List<Transform>();
    private int totalPlayerNum;
    private int grade;
    [SerializeField] Transform goal;
    [SerializeField] Text grade1PlayerNameText;
    private void Update()
    {
        CheckPlayerReachedToGoalAndStopMove();
    }
    public void Register(PlayerMove playerMove)
    {
        list_PlayerMove.Add(playerMove);
        totalPlayerNum++;
        Debug.Log($"{playerMove.gameObject.name} (��)�� ��� �Ϸ� �Ǿ����ϴ�, ���� �� ��ϼ� : {list_PlayerMove.Count}");
    }
    public void StartRacing()
    {
        foreach(PlayerMove playerMove in list_PlayerMove)
        {
            playerMove.doMove = true;
        }
    }
    private void CheckPlayerReachedToGoalAndStopMove()
    {
        PlayerMove tmpFinishedPlayerMove = null;
        //�÷��̾ ��ǥ������ �����ߴ��� üũ
        foreach (PlayerMove playerMove in list_PlayerMove)
        {
            if(playerMove.transform.position.z > goal.position.z)
            {
                tmpFinishedPlayerMove = playerMove;
                break;
            }
        }
        // �÷��̾ ��ǥ������ ����������
        if(tmpFinishedPlayerMove != null)
        {
            tmpFinishedPlayerMove.doMove = false;
            list_FinishdePlayer.Add(tmpFinishedPlayerMove.transform);
            list_PlayerMove.Remove(tmpFinishedPlayerMove);
        }
        // ���ְ� �����ٸ� (��� �÷��̾ ��ǥ�� ���������)
        if(list_FinishdePlayer.Count == totalPlayerNum)
        {
            // 1,2,3���� �ܻ����� ��ġ��Ű��, ī�޶�� �ܻ��� ���ߵ��� �Ѵ�.
            for (int i = 0; i < list_WinPlatform.Count; i++)
            {
                list_FinishdePlayer[i].position = list_WinPlatform[i].position;
            }
            CameraHandler.instance.MoveToPlatform();
            // 1�� ģ�� �̸� �ؽ�Ʈ ������Ʈ
            grade1PlayerNameText.text = list_FinishdePlayer[0].name;
            /*grade1PlayerNameText.enabled = true;*/
            grade1PlayerNameText.gameObject.SetActive(true);
        }
    }
    public Transform GetPlayerTransform(int index)
    {
        /*Transform tmpPlayerTransform = null;
        if (index < list_PlayerMove.Count)
        {
            Transform tmpPlayerTransform = list_PlayerMove[index].transform;
            return tmpPlayerTransform;
        }
        else
        {
            return null; // �ƹ��͵� ���°��� ��ȯ�Ѵ�
        }*/

        // �Լ��� ��ȯ�� ���������� ����� �ʱ�ȭ�� �Լ��� ���� ��ܿ� �Ѵ�.
        Transform tmpPlayerTransform = null; 
        
        // �Լ����� : ���꿡 ���� ��ȯ�� ���������� ���� �����Ѵ�.
        if (index < list_PlayerMove.Count)
        {
            tmpPlayerTransform = list_PlayerMove[index].transform;
        }
        // �Լ��� ���� �ϴܿ��� ��ȯ�� ���������� ��ȯ�Ѵ�.
        return tmpPlayerTransform;
    }
    public Transform Get1GradePlayer()
    {
        Transform leader = list_PlayerMove[0].gameObject.GetComponent<Transform>();
        float prevDistance = list_PlayerMove[0].distance;
        foreach(PlayerMove playerMove in list_PlayerMove)
        {
            if(playerMove.distance > prevDistance)
            {
                prevDistance = playerMove.distance;
                leader = playerMove.gameObject.GetComponent<Transform>();
            }
        }
        return leader;
    }

    public int GetTotalPlayerNumber()
    {
        return list_PlayerMove.Count;
    }
}