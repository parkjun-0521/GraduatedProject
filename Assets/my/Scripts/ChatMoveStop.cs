using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMoveStop : MonoBehaviour
{
    public InputField textField;

    // 채팅을 치고 있을 시 이동을 제한하는 로직 
    // 아직 마우스로 채팅창을 눌렀을 때의 이동은 막지 못하였다. 
    private void Start()
    {
        // TextField를 찾아서 변수에 할당합니다.
        textField.onEndEdit.AddListener(OnTextFieldClick);
    }

    // 채팅창이 활성화 되었을 때 
    public void OnTextFieldClick(string text)
    {
        ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        thirdPersonController.MoveSpeed = 0.0f;     // 이동속도를 0
        thirdPersonController.SprintSpeed = 0.0f;   // 달리기 속도 0
        thirdPersonController.turnStop = true;      // 캐릭터의 회전을 막음 
        thirdPersonController.EnterCheck = false;   // 채팅 입력시 엔터를 누르게 되는데 이것을 구분하기 위한 변수를 false로 변경 

        SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
        smoothFollow.turnOff = true;                // 카메라의 회전을 막음 
    }
}
