using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePortal : MonoBehaviour
{
    public GameObject[] uiGroup;        // 포탈 종류 저장 변수 
    public GameObject serverObj;        // 서버의 기본 UI 

    customizing custom;

    // 포탈에 닿았을 때 
    public void Enter()
    {
        serverObj.SetActive(false);                 // 기본 서버의 UI를 비활성화 
        for(int i =0; i < uiGroup.Length; i++)
            uiGroup[i].SetActive(true);             // 맵을 들어갔을 때 UI를 활성화 

        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();                   // ButtonEvent 스크립트가 달려있는 오브젝트를 찾는다. 
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();       // NetworkManager 스크립트가 달려있는 오브젝트를 찾는다. 

        // 공개방
        buttonEvent.Women();                                            // 포탈에 들어갔을 때 여자 캐릭터가 보여질 수 있도록 지정 
        for (int i = 0; i < buttonEvent.Women_Men.Length; i++)
            buttonEvent.Women_Men[i].SetActive(true);                   // 남여 캐릭터를 변경할 수 있는 버튼을 활성화 
        if(networkManager.adminCheck)
            buttonEvent.PublicCreateRoomPrevious();                     // 포탈에 들어간 사람이 관리자일 경우 공개방 입장 버튼을 비활성화 
        else if(!networkManager.adminCheck)
            buttonEvent.PublicInputRoomPrevious();                      // 포탈에 들어간 사람이 사용자일 경우 공개방 생성 버튼을 비활성화 

        buttonEvent.RoomCreate_Input.SetActive(false);                  // 캐릭터 선택 오브젝트를 비활성화 
        buttonEvent.LobbyPanel_PreviousButton.SetActive(false);         // 캐릭터 선택후 이전 버튼 비활성화 
        buttonEvent.publicPreviousButton.SetActive(false);              // 이전 버튼 비활성화 
        buttonEvent.publicNextButton.SetActive(false);                  // 다음 버튼 비활성화 
        buttonEvent.pCount = 0;                                         // 포탈을 들어갔을 때 페이지 Flag 변수 초기화 
        buttonEvent.publicRoomCount = 3;                                // 포탈입장시 맵 선택 Falg 변수 초기화 
        buttonEvent.publicItemList.SetActive(true);                     // 아바타 리스트 활성화 
        buttonEvent.publicplayerBackGround.SetActive(true);             // 캐릭터 선택 뒤 배경 활성화 
        buttonEvent.publicItemBackGround.SetActive(true);               // 아바타 선택 뒤 배경 활성화 

        StartCoroutine(networkManager.pItem());                         // 아바타를 가져오는 코루틴 실행                    
    }

    // 팀 방 생성 포탈에 들어갔을 때 
    public void CreateEnter()
    {
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();       // NetworkManager 스크립트가 달려있는 오브젝트를 찾는다. 
        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();                   // ButtonEvent 스크립트가 달려있는 오브젝트를 찾는다

        networkManager.CreateTeam();                                    // 팀방 생성 UI를 띄우기 위해 함수 불러오기 
        StartCoroutine(networkManager.cItem());                         // 아바타를 가져오는 코루틴 실행 

        buttonEvent.TeamCreate_Women();                                 // 입장 시 여자 캐릭터 선택 창으로 초기화 
        for (int i = 0; i < buttonEvent.TeamCreate_Women_Men.Length; i++)
            buttonEvent.TeamCreate_Women_Men[i].SetActive(true);        // 남여 선택 버튼 활성화 
        buttonEvent.CreateplayerBackGround.SetActive(true);             // 캐릭터 선택 뒤 배경 활성화 
        buttonEvent.CreateItemList.SetActive(true);                     // 아바타 리스트 활성화 
        buttonEvent.CreateItemBackGround.SetActive(true);               // 아이템 선택 뒤 배경 활성화 
        buttonEvent.CreateTeamNextSelect.SetActive(false);
        buttonEvent.TeamCreate_PreviousButton.SetActive(false);         // 이전 버튼 비활성화 
        buttonEvent.TeamCreatePrevious.SetActive(false);                // 이전 버튼 비활성화 
        buttonEvent.TeamCreateNext.SetActive(false);                    // 다음 버튼 비활성화 
        
        buttonEvent.count = 0;                                          // 팀방 생성 페이지 Flag 변수 초기화 
    }

    // 팀방 입장 포탈에 들어갔을 때 
    public void InputEnter()
    {
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();       // NetworkManager 스크립트가 달려있는 오브젝트를 찾는다. 
        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();                   // ButtonEvent 스크립트가 달려있는 오브젝트를 찾는다

        networkManager.InputTeam();                                     // 팀방 입장 UI를 띄위기 위해 함수를 불러옴     
        StartCoroutine(networkManager.iItem());                         // 아바타를 가져오는 코루틴 실행 

        buttonEvent.TeamInput_Women();                                  // 입장 시 여자 캐릭터 선택 창으로 초기화 
        for (int i = 0; i < buttonEvent.TeamInput_Women_Men.Length; i++)
            buttonEvent.TeamInput_Women_Men[i].SetActive(true);         // 남여 선택 버튼 활성화 
        buttonEvent.TeamInput_PreviousButton.SetActive(false);          // 이전 버튼 비활성화 
        buttonEvent.TeamInputPrevious.SetActive(false);                 // 이전 버튼 비활성화 
        buttonEvent.TeamInputNext.SetActive(false);                     // 다음 버튼 비활성화 
        buttonEvent.playerBackGround.SetActive(true);                   // 캐릭터 선택 뒤 배경 활성화 
        buttonEvent.InputItemList.SetActive(true);                      // 아바타 리스트 활성화   
        buttonEvent.InputItemBackGround.SetActive(true);                // 아바타 선택 뒤 배경 비활성화 

        buttonEvent.iCount = 0;                                         // 팀방 입장 페이지 Flag 변수 초기화 
}

    // 포탈에서 나갔을 때 발생하는 이벤트 
    public void Exit()
    {
        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();
        custom = GameObject.Find("Customizing").GetComponent<customizing>();

        for (int i = 0; i < uiGroup.Length; i++)
            uiGroup[i].SetActive(false);                            // 포탈을 들어갔을 때 열린 UI를 비활성화 
        serverObj.SetActive(true);                                  // 기본적인 로비의 UI를 활성화 
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


        //NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();       // NetworkManager 스크립트가 달려있는 오브젝트를 찾는다. 
        //ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();                   // ButtonEvent 스크립트가 달려있는 오브젝트를 찾는다
        // 공개방
        //buttonEvent.Women();                                        
        //for (int i = 0; i < buttonEvent.Women_Men.Length; i++)
        //    buttonEvent.Women_Men[i].SetActive(true);
        //buttonEvent.PublicCreateRoomPrevious();
        //buttonEvent.RoomCreate_Input.SetActive(false);
        ////buttonEvent.LobbyPanel_NextButton.SetActive(true);
        //buttonEvent.LobbyPanel_PreviousButton.SetActive(false);

        // 팀방 입장
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

        //// 버튼 안누르고 그냥 꺼질 때
        //buttonEvent.preimg.SetActive(false);
    }
}