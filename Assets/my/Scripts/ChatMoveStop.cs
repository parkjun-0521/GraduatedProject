using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMoveStop : MonoBehaviour
{
    public InputField textField;

    // ä���� ġ�� ���� �� �̵��� �����ϴ� ���� 
    // ���� ���콺�� ä��â�� ������ ���� �̵��� ���� ���Ͽ���. 
    private void Start()
    {
        // TextField�� ã�Ƽ� ������ �Ҵ��մϴ�.
        textField.onEndEdit.AddListener(OnTextFieldClick);
    }

    // ä��â�� Ȱ��ȭ �Ǿ��� �� 
    public void OnTextFieldClick(string text)
    {
        ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        thirdPersonController.MoveSpeed = 0.0f;     // �̵��ӵ��� 0
        thirdPersonController.SprintSpeed = 0.0f;   // �޸��� �ӵ� 0
        thirdPersonController.turnStop = true;      // ĳ������ ȸ���� ���� 
        thirdPersonController.EnterCheck = false;   // ä�� �Է½� ���͸� ������ �Ǵµ� �̰��� �����ϱ� ���� ������ false�� ���� 

        SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
        smoothFollow.turnOff = true;                // ī�޶��� ȸ���� ���� 
    }
}
