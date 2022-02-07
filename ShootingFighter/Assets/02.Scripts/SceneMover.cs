using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    static public SceneMover instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion
    [SerializeField] private GameObject gameOverUI;
    public void MoveSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void MoveSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void MoveNextScene()
    {
        int currntSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currntSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currntSceneIndex + 1);
        }
        else
        {
            Instantiate(gameOverUI);
        }
    }
}
