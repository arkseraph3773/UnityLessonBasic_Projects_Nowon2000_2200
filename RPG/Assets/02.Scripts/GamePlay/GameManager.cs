using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GamaState state;

    public static void Next()
    {
        state++;
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Next();
    }

    private void Update()
    {
        switch (state)
        {
            case GamaState.Idle:
                break;
            case GamaState.StartLooadAssets:
                if (PlayerDataManager.instance != null)
                {
                    Next();
                }
                break;
            case GamaState.WaitForAssetsLoaded:
                Next();
                break;
            case GamaState.SelectCharacter:
                SceneMover.MoveScene("CharacterSelection");
                Next();
                break;
            case GamaState.WaitForCharacterSelected:
                break;
            case GamaState.StartStage:
                break;
            case GamaState.OnStage:
                break;
            default:
                break;
        }
    }
}

public enum GamaState
{
    Idle,
    StartLooadAssets,
    WaitForAssetsLoaded,
    SelectCharacter,
    WaitForCharacterSelected,
    StartStage,
    OnStage,
}
