using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePortal : MonoBehaviour
{
    public GameObject[] uiGroup;
    public GameObject serverObj;

    public void Enter()
    {
        serverObj.SetActive(false);
        for(int i =0; i < uiGroup.Length; i++)
            uiGroup[i].SetActive(true);

        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        // 공개방
        buttonEvent.Women();
        for (int i = 0; i < buttonEvent.Women_Men.Length; i++)
            buttonEvent.Women_Men[i].SetActive(true);
        if(networkManager.adminCheck)
            buttonEvent.PublicCreateRoomPrevious();
        else if(!networkManager.adminCheck)
            buttonEvent.PublicInputRoomPrevious();
        buttonEvent.RoomCreate_Input.SetActive(false);
        //buttonEvent.LobbyPanel_NextButton.SetActive(true);
        buttonEvent.LobbyPanel_PreviousButton.SetActive(false);
        buttonEvent.publicItemList.SetActive(true);
        buttonEvent.publicplayerBackGround.SetActive(true);
        buttonEvent.publicItemBackGround.SetActive(true);
        buttonEvent.publicNextButton.SetActive(false);
        buttonEvent.publicPreviousButton.SetActive(false);
        buttonEvent.pCount = 0;
        buttonEvent.publicRoomCount = 3;
        StartCoroutine(networkManager.pItem());
    }

    public void CreateEnter()
    {
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        networkManager.CreateTeam();
        StartCoroutine(networkManager.cItem());

        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();
        buttonEvent.TeamCreate_Women();
        for (int i = 0; i < buttonEvent.TeamCreate_Women_Men.Length; i++)
            buttonEvent.TeamCreate_Women_Men[i].SetActive(true);
        buttonEvent.TeamCreate_PreviousButton.SetActive(false);
        buttonEvent.CreateplayerBackGround.SetActive(true);
        buttonEvent.TeamCreateNext.SetActive(false);
        buttonEvent.TeamCreatePrevious.SetActive(false);
        buttonEvent.CreateItemList.SetActive(true);
        buttonEvent.CreateItemBackGround.SetActive(true);
        
        buttonEvent.count = 0;
    }

    public void InputEnter()
    {
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        networkManager.InputTeam();
        StartCoroutine(networkManager.iItem());

        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();
        buttonEvent.TeamInput_Women();
        for (int i = 0; i < buttonEvent.TeamInput_Women_Men.Length; i++)
            buttonEvent.TeamInput_Women_Men[i].SetActive(true);
        buttonEvent.TeamInput_PreviousButton.SetActive(false);
        buttonEvent.TeamInputNext.SetActive(false);
        buttonEvent.TeamInputPrevious.SetActive(false);
        buttonEvent.playerBackGround.SetActive(true);

        buttonEvent.iCount = 0;
}


    public void Exit()
    {
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        for (int i = 0; i < uiGroup.Length; i++)
            uiGroup[i].SetActive(false);
        serverObj.SetActive(true);

        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();

        // 공개방
        buttonEvent.Women();
        for (int i = 0; i < buttonEvent.Women_Men.Length; i++)
            buttonEvent.Women_Men[i].SetActive(true);
        buttonEvent.PublicCreateRoomPrevious();
        buttonEvent.RoomCreate_Input.SetActive(false);
        //buttonEvent.LobbyPanel_NextButton.SetActive(true);
        buttonEvent.LobbyPanel_PreviousButton.SetActive(false);

        // 팀방 입장
        buttonEvent.TeamInput_Women();
        for (int i = 0; i < buttonEvent.TeamInput_Women_Men.Length; i++)
            buttonEvent.TeamInput_Women_Men[i].SetActive(true);
        buttonEvent.TeamInput_PreviousButton.SetActive(false);
        buttonEvent.TeamInputNext.SetActive(false);
        buttonEvent.TeamInputPrevious.SetActive(false);
        buttonEvent.playerBackGround.SetActive(true);

        buttonEvent.TeamRoomCreate.SetActive(false);

        networkManager.curPage = 0;
        networkManager.endPage = 0;
        networkManager.pageCount = 1;
    }
}