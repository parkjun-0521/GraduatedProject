using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ChangeInput : MonoBehaviour {
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;

    //----------------로그인화면에서 탭 기능을 구현하여 아이디 비밀번호를 탭으로 이동하도록 설정하는 로직 
    public void Start()
    {
        system = EventSystem.current;
        // 처음은 아이디 Input Field를 선택하도록 한다.
        firstInput.Select();
    }


    void Update()
    {
        // 왼쪽 쉬프트와 탭을 누를 경우 위로 마우스 커서가 이동 
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift)) {
            // Tab + LeftShift는 위의 Selectable 객체를 선택
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null) {
                next.Select();
            }
        }
        // 탭을 누를 경우 아래쪽으로 커서 이동 
        // 더이상 내려갈 곳이 없으면 처음으로 커서를 이동시킨다. 
        else if (Input.GetKeyDown(KeyCode.Tab)) {
            // Tab은 아래의 Selectable 객체를 선택
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null) {
                next.Select();
            }
            else {
                Start();
            }
        }
        // 앤터키를 누르면 로그인 버튼이 눌리도록 지정 
        else if (Input.GetKeyDown(KeyCode.Return)) {
            // 엔터키를 치면 로그인 (제출) 버튼을 클릭
            submitButton.onClick.Invoke();
            Debug.Log("Button pressed!");
        }
    }
}
