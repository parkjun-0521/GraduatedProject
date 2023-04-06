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