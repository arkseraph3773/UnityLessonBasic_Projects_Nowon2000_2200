using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public void MoveSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void MoveSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
