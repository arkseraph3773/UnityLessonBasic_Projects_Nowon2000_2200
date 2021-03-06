using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorHandler : MonoBehaviour
{
    public static bool controllable = true; 

    private GraphicRaycaster _graphicRaycaster; //ui 레이캐스팅 하는 컴포넌트
    private PointerEventData _pointerEventData; // 마우스 이벤트 데이터
    private EventSystem _eventSystem; // 이벤트를 처리하는 객체

    private void Awake()
    {
        _graphicRaycaster = GetComponent<GraphicRaycaster>();
        _eventSystem = GetComponent<EventSystem>();

    }

    private void Update()
    {
        if (controllable)
        {
            // 마우스 왼쪽버튼
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // 발생할 이벤트에 대한 마우스 이벤트 데이터
                _pointerEventData = new PointerEventData(_eventSystem); // 현재 이벤트에서 마우스 이벤트 데이터만 따로 생성
                _pointerEventData.position = Input.mousePosition; // 마우스 입력 들어왔으니 현재 마우스 위치를 마우스 이벤트 데이터 위치로

                List<RaycastResult> results = new List<RaycastResult>(); // 레이캐스트 대상들
                _graphicRaycaster.Raycast(_pointerEventData, results); // UI 레이캐스트

                // UI 캐스트됨
                if (results.Count > 0)
                    ShowCursor(); // 마우스 보여줘
                                  // 안됨
                else
                    HideCursor(); // 마우스 숨겨줘
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowCursor();
        }
    }

    

    public static void HideCursor()
    {
        if (Cursor.visible)
            Cursor.visible = false;

        if (Cursor.lockState != CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.Locked;

        if (Player.instance != null)
            Player.CMDState = CMDState.Ready;

    }

    public static void ShowCursor()
    {
        if (Cursor.visible == false)
            Cursor.visible = true;

        if (Cursor.lockState != CursorLockMode.Confined)
            Cursor.lockState = CursorLockMode.Confined;

        if (Player.instance != null)
            Player.CMDState = CMDState.Busy;
    }

}
