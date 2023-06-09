using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMoveStop : MonoBehaviour
{
    public InputField textField;

    private void Start()
    {
        // TextField�� ã�Ƽ� ������ �Ҵ��մϴ�.
        textField.onEndEdit.AddListener(OnTextFieldClick);
    }

    public void OnTextFieldClick(string text)
    {
        ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        thirdPersonController.MoveSpeed = 0.0f;
        thirdPersonController.SprintSpeed = 0.0f;
        thirdPersonController.turnStop = true;
        thirdPersonController.EnterCheck = false;

        SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
        smoothFollow.turnOff = true;
    }
}
