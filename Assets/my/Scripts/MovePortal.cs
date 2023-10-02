using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePortal : MonoBehaviour
{
    public GameObject[] uiGroup;        // ��Ż ���� ���� ���� 
    public GameObject serverObj;        // ������ �⺻ UI 

    customizing custom;

    // ��Ż�� ����� �� 
    public void Enter()
    {
        serverObj.SetActive(false);                 // �⺻ ������ UI�� ��Ȱ��ȭ 
        for(int i =0; i < uiGroup.Length; i++)
            uiGroup[i].SetActive(true);             // ���� ���� �� UI�� Ȱ��ȭ 

        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();                   // ButtonEvent ��ũ��Ʈ�� �޷��ִ� ������Ʈ�� ã�´�. 
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();       // NetworkManager ��ũ��Ʈ�� �޷��ִ� ������Ʈ�� ã�´�. 

        // ������
        buttonEvent.Women();                                            // ��Ż�� ���� �� ���� ĳ���Ͱ� ������ �� �ֵ��� ���� 
        for (int i = 0; i < buttonEvent.Women_Men.Length; i++)
            buttonEvent.Women_Men[i].SetActive(true);                   // ���� ĳ���͸� ������ �� �ִ� ��ư�� Ȱ��ȭ 
        if(networkManager.adminCheck)
            buttonEvent.PublicCreateRoomPrevious();                     // ��Ż�� �� ����� �������� ��� ������ ���� ��ư�� ��Ȱ��ȭ 
        else if(!networkManager.adminCheck)
            buttonEvent.PublicInputRoomPrevious();                      // ��Ż�� �� ����� ������� ��� ������ ���� ��ư�� ��Ȱ��ȭ 

        buttonEvent.RoomCreate_Input.SetActive(false);                  // ĳ���� ���� ������Ʈ�� ��Ȱ��ȭ 
        buttonEvent.LobbyPanel_PreviousButton.SetActive(false);         // ĳ���� ������ ���� ��ư ��Ȱ��ȭ 
        buttonEvent.publicPreviousButton.SetActive(false);              // ���� ��ư ��Ȱ��ȭ 
        buttonEvent.publicNextButton.SetActive(false);                  // ���� ��ư ��Ȱ��ȭ 
        buttonEvent.pCount = 0;                                         // ��Ż�� ���� �� ������ Flag ���� �ʱ�ȭ 
        buttonEvent.publicRoomCount = 3;                                // ��Ż����� �� ���� Falg ���� �ʱ�ȭ 
        buttonEvent.publicItemList.SetActive(true);                     // �ƹ�Ÿ ����Ʈ Ȱ��ȭ 
        buttonEvent.publicplayerBackGround.SetActive(true);             // ĳ���� ���� �� ��� Ȱ��ȭ 
        buttonEvent.publicItemBackGround.SetActive(true);               // �ƹ�Ÿ ���� �� ��� Ȱ��ȭ 

        StartCoroutine(networkManager.pItem());                         // �ƹ�Ÿ�� �������� �ڷ�ƾ ����                    
    }

    // �� �� ���� ��Ż�� ���� �� 
    public void CreateEnter()
    {
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();       // NetworkManager ��ũ��Ʈ�� �޷��ִ� ������Ʈ�� ã�´�. 
        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();                   // ButtonEvent ��ũ��Ʈ�� �޷��ִ� ������Ʈ�� ã�´�

        networkManager.CreateTeam();                                    // ���� ���� UI�� ���� ���� �Լ� �ҷ����� 
        StartCoroutine(networkManager.cItem());                         // �ƹ�Ÿ�� �������� �ڷ�ƾ ���� 

        buttonEvent.TeamCreate_Women();                                 // ���� �� ���� ĳ���� ���� â���� �ʱ�ȭ 
        for (int i = 0; i < buttonEvent.TeamCreate_Women_Men.Length; i++)
            buttonEvent.TeamCreate_Women_Men[i].SetActive(true);        // ���� ���� ��ư Ȱ��ȭ 
        buttonEvent.CreateplayerBackGround.SetActive(true);             // ĳ���� ���� �� ��� Ȱ��ȭ 
        buttonEvent.CreateItemList.SetActive(true);                     // �ƹ�Ÿ ����Ʈ Ȱ��ȭ 
        buttonEvent.CreateItemBackGround.SetActive(true);               // ������ ���� �� ��� Ȱ��ȭ 
        buttonEvent.CreateTeamNextSelect.SetActive(false);
        buttonEvent.TeamCreate_PreviousButton.SetActive(false);         // ���� ��ư ��Ȱ��ȭ 
        buttonEvent.TeamCreatePrevious.SetActive(false);                // ���� ��ư ��Ȱ��ȭ 
        buttonEvent.TeamCreateNext.SetActive(false);                    // ���� ��ư ��Ȱ��ȭ 
        
        buttonEvent.count = 0;                                          // ���� ���� ������ Flag ���� �ʱ�ȭ 
    }

    // ���� ���� ��Ż�� ���� �� 
    public void InputEnter()
    {
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();       // NetworkManager ��ũ��Ʈ�� �޷��ִ� ������Ʈ�� ã�´�. 
        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();                   // ButtonEvent ��ũ��Ʈ�� �޷��ִ� ������Ʈ�� ã�´�

        networkManager.InputTeam();                                     // ���� ���� UI�� ������ ���� �Լ��� �ҷ���     
        StartCoroutine(networkManager.iItem());                         // �ƹ�Ÿ�� �������� �ڷ�ƾ ���� 

        buttonEvent.TeamInput_Women();                                  // ���� �� ���� ĳ���� ���� â���� �ʱ�ȭ 
        for (int i = 0; i < buttonEvent.TeamInput_Women_Men.Length; i++)
            buttonEvent.TeamInput_Women_Men[i].SetActive(true);         // ���� ���� ��ư Ȱ��ȭ 
        buttonEvent.TeamInput_PreviousButton.SetActive(false);          // ���� ��ư ��Ȱ��ȭ 
        buttonEvent.TeamInputPrevious.SetActive(false);                 // ���� ��ư ��Ȱ��ȭ 
        buttonEvent.TeamInputNext.SetActive(false);                     // ���� ��ư ��Ȱ��ȭ 
        buttonEvent.playerBackGround.SetActive(true);                   // ĳ���� ���� �� ��� Ȱ��ȭ 
        buttonEvent.InputItemList.SetActive(true);                      // �ƹ�Ÿ ����Ʈ Ȱ��ȭ   
        buttonEvent.InputItemBackGround.SetActive(true);                // �ƹ�Ÿ ���� �� ��� ��Ȱ��ȭ 

        buttonEvent.iCount = 0;                                         // ���� ���� ������ Flag ���� �ʱ�ȭ 
}

    // ��Ż���� ������ �� �߻��ϴ� �̺�Ʈ 
    public void Exit()
    {
        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();
        custom = GameObject.Find("Customizing").GetComponent<customizing>();

        for (int i = 0; i < uiGroup.Length; i++)
            uiGroup[i].SetActive(false);                            // ��Ż�� ���� �� ���� UI�� ��Ȱ��ȭ 
        serverObj.SetActive(true);                                  // �⺻���� �κ��� UI�� Ȱ��ȭ 
        buttonEvent.preimg.SetActive(false);
        for(int i=0; i<8; i++)
        {
            custom.fe1ava[i].SetActive(false);
            custom.fe2ava[i].SetActive(false);
            custom.fe3ava[i].SetActive(false);
            custom.m1ava[i].SetActive(false);
            custom.m2ava[i].SetActive(false);
            custom.m3ava[i].SetActive(false);
        }


        //NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();       // NetworkManager ��ũ��Ʈ�� �޷��ִ� ������Ʈ�� ã�´�. 
        //ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();                   // ButtonEvent ��ũ��Ʈ�� �޷��ִ� ������Ʈ�� ã�´�
        // ������
        //buttonEvent.Women();                                        
        //for (int i = 0; i < buttonEvent.Women_Men.Length; i++)
        //    buttonEvent.Women_Men[i].SetActive(true);
        //buttonEvent.PublicCreateRoomPrevious();
        //buttonEvent.RoomCreate_Input.SetActive(false);
        ////buttonEvent.LobbyPanel_NextButton.SetActive(true);
        //buttonEvent.LobbyPanel_PreviousButton.SetActive(false);

        // ���� ����
        //buttonEvent.TeamInput_Women();
        //for (int i = 0; i < buttonEvent.TeamInput_Women_Men.Length; i++)
        //    buttonEvent.TeamInput_Women_Men[i].SetActive(true);
        //buttonEvent.TeamInput_PreviousButton.SetActive(false);
        //buttonEvent.TeamInputNext.SetActive(false);
        //buttonEvent.TeamInputPrevious.SetActive(false);
        //buttonEvent.playerBackGround.SetActive(true);

        //buttonEvent.TeamRoomCreate.SetActive(false);

        //networkManager.curPage = 0;
        //networkManager.endPage = 0;
        //networkManager.pageCount = 1;

        //// ��ư �ȴ����� �׳� ���� ��
        //buttonEvent.preimg.SetActive(false);
    }
}