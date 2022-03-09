using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameState state;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        DontDestroyOnLoad(instance);
        //DontDestroyOnLoad 들은 
        //기본적으로 다른씬에 있는 객체들에 접근하면 안된다.
        //다른씬에 있는 객체들이 DontDestroyOnLoad Object가 가지고 있는 멤버를
        //참조하기 위해 사용함
                
    }
    private void Update()
    {
        switch (state)
        {
            case GameState.Select:
                break;
            case GameState.Play:
                break;
            case GameState.WaitForFinish:
                break;
            case GameState.Finish:
                break;
            case GameState.Score:
                break;
            default:
                break;
        }
    }
    public void NextState()
    {
        state++;
    }
    public void TryPlay()
    {
        if(SongSelector.Instance.isPlayable)
        {
            MoveSceneTo("Play");
            NextState();
        }
    }
    public void MoveSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
public enum GameState
{
    Select,
    Play,
    WaitForFinish,
    Finish,
    Score,
}
