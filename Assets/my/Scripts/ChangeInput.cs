using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ChangeInput : MonoBehaviour {
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;

    //----------------�α���ȭ�鿡�� �� ����� �����Ͽ� ���̵� ��й�ȣ�� ������ �̵��ϵ��� �����ϴ� ���� 
    public void Start()
    {
        system = EventSystem.current;
        // ó���� ���̵� Input Field�� �����ϵ��� �Ѵ�.
        firstInput.Select();
    }


    void Update()
    {
        // ���� ����Ʈ�� ���� ���� ��� ���� ���콺 Ŀ���� �̵� 
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift)) {
            // Tab + LeftShift�� ���� Selectable ��ü�� ����
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null) {
                next.Select();
            }
        }
        // ���� ���� ��� �Ʒ������� Ŀ�� �̵� 
        // ���̻� ������ ���� ������ ó������ Ŀ���� �̵���Ų��. 
        else if (Input.GetKeyDown(KeyCode.Tab)) {
            // Tab�� �Ʒ��� Selectable ��ü�� ����
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null) {
                next.Select();
            }
            else {
                Start();
            }
        }
        // ����Ű�� ������ �α��� ��ư�� �������� ���� 
        else if (Input.GetKeyDown(KeyCode.Return)) {
            // ����Ű�� ġ�� �α��� (����) ��ư�� Ŭ��
            submitButton.onClick.Invoke();
            Debug.Log("Button pressed!");
        }
    }
}
