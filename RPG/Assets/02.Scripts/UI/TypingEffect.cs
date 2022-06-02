using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using System.Text;

public class TypingEffect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text typingText;
    [SerializeField] private float typingDelay = 0.1f;
    [SerializeField] private List<GameObject> gameObjectsForToggle; // 타이핑 이펙트 끝나면 활성 / 비활성화 시킬 게임 오브젝트 리스트
    private string originText;
    //private byte[] originBytes; 

    private Coroutine coroutine = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowOriginText();
    }

    public void ShowOriginText()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        typingText.text = originText;
        ActiveGameObjects();
    }

   

    private void OnEnable()
    {
        originText = typingText.text;
        //originBytes = Encoding.Unicode.GetBytes(originText);

        DeactiveGameObjects();

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(E_TypingEffect());
    }

    IEnumerator E_TypingEffect()
    {
        string tmpText = originText;

        for (int i = 0; i < tmpText.Length; i++)
        {
            typingText.text = tmpText.Substring(0, i + 1); // +1 안할거면 (int i = 0; i <= tmpText.Length; i++)
            yield return new WaitForSeconds(typingDelay);
        }

        ActiveGameObjects();
        coroutine = null;
    }

    private void ActiveGameObjects()
    {
        foreach (GameObject go in gameObjectsForToggle)
        {
            go.SetActive(true);
        }
    }
    private void DeactiveGameObjects()
    {
        foreach (GameObject go in gameObjectsForToggle)
        {
            go.SetActive(false);
        }
    }
}
