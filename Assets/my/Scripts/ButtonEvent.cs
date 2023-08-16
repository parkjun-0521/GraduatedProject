using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class ButtonEvent : MonoBehaviourPunCallbacks {
    //================================== 공개방 포탈에 입장했을 때 발생하는 Button 이벤트 ==================================//
    [Header("--------공개방--------")]
    public GameObject RoomCreate_Input;                 // 방을 만들기 위한 전체적인 오브젝트
    public GameObject[] LobbyPanel_Player;              // 캐릭터 선택 배열 변수 
    public GameObject[] LobbyPanel_NextButton;          // 캐릭터 선택 후 다음으로 이동하기 위한 변수 선언 
    public GameObject LobbyPanel_PreviousButton;        // 이전으로 이동하기 위한 변수 

    public GameObject[] Women_Men;                      // 남여 캐릭터를 선택하기 위한 변수 

    public GameObject[] publicCreateRoom;               // 방 만들기 맵      ( 관리자 )
    public GameObject[] publicInputRoom;                // 방 입장하기 맵    ( 사용자 )

    public GameObject publicplayerBackGround;           // 캐릭터 선택 UI 뒤배경 
    public GameObject publicItemBackGround;             // 아바타 선택 UI 뒤배경 

    public GameObject publicItemList;                   // 아바타 전체를 저장하고 있는 UI 오브젝트 
    public GameObject publicNextButton;                 // 다음으로 이동 할 수 있는 버튼 오브젝트    
    public GameObject publicPreviousButton;             // 이전으로 이동 할 수 있는 버튼 오브젝트

    public int pCount = 0;                              // 현재 페이지를 확인할 Flag 변수 
    public int publicRoomCount = 3;                     // 맵을 선택할 때 봄, 여름, 가을, 겨울 순 등장할 수 있도록 지정하기 위한 변수 

    //================================== 팀방 입장 포탈에 들어갔을 때 발생하는 Button 이벤트 ==================================//
    [Header("--------팀방 입장--------")]
    public GameObject[] TeamInput_Player;               // 팀방 입장의 캐릭터 선택을 위한 배열 변수 
    public GameObject[] TeamInput_NextButton;           // 캐릭터 선택 후 다음으로 넘어갈 수 있는 버튼 
    public GameObject TeamInput_PreviousButton;         // 이전으로 돌아갈 수 있는 버튼 오브젝트 

    public GameObject[] TeamInput_Women_Men;            // 남여 캐릭터를 바꾸기 위한 버튼 오브젝트 

    public GameObject playerBackGround;                 // 캐릭터를 선택할 때 뒤배경
    public GameObject InputplayerTeaminput;             // 방을 들어갈 때 UI 뒤배경 
    public GameObject InputItemBackGround;              // 아바타를 선택할 때 UI 뒤 배경 


    public GameObject TeamInputNext;                    // 다음으로 이동할 수 있는 버튼 오브젝트 
    public GameObject TeamInputPrevious;                // 이전으로 돌아갈 수 있는 버튼 오브젝트 

    public GameObject TeamInputRoom;                    // 팀 방을 선택하기 위한 변수 
    public GameObject InputItemList;                    // 아바타를 가지고 있는 오브젝트 변수 
    public GameObject inputNextButton;                  // 다음으로 넘어가기 버튼 오브젝트  
    public int iCount = 0;                              // 몇번째 페이지인지 구분하기 위한 변수 

    //================================== 팀방 생성 포탈에 들어갔을 때 발생하는 Button 이벤트 ==================================//
    [Header("--------팀방 생성--------")]
    public GameObject[] TeamCreate_Player;              // 팀방 생성의 캐릭터 선택을 위한 배열 변수 
    public GameObject[] TeamCreate_NextButton;          // 캐릭터를 선택하면 바로 넘어갈 수 있도록 하기 위한 변수 선언 
    public GameObject TeamCreate_PreviousButton;        // 팀방 생성에서 이전으로 돌아가기 위한 버튼 오브젝트 

    public GameObject[] TeamCreate_Women_Men;           // 남여 캐릭터를 바꾸기 위한 버튼 오브젝트 
        
    public GameObject[] TeamCreateMap;                  // 팀방에만 존재하는 4개의 맵을 저장한 배열 
    public GameObject TeamRoomCreate;                   // 맵 선택에 관련된 UI 전체를 가지고 있는 오브젝트 

    public GameObject CreateplayerBackGround;           // 캐릭터 선택할 때 뒤배경
    public GameObject CreateItemBackGround;             // 캐릭터를 선택할 때 뒤배경 
    public GameObject CreateItemSelectBackGround;       // 아바타을 선택할 때 뒤배경 
    public GameObject CreateRoomBackGround;             // 맵을 선택할 때 뒤배경 

    public GameObject CreateItemList;                   // 아바타 체크박스의 전체 UI를 저장하고 있는 오브젝트 
    public GameObject CreateTeamNextSelect;             // UI에서 다음으로 넘어가는 버튼 오브젝트 
    public GameObject CreateTeamPreSelect;              // UI에서 이전으로 돌아가는 버튼 오브젝트 

    public GameObject TeamCreateRoom;                   // 팀방 10개의 버튼을 가지고 있는 오브젝트 
    public GameObject TeamCreateNext;                   // 팀방이 10개가 넘었을 때 다음으로 넘겨서 확인할 수 있는 버튼 오브젝트 
    public GameObject TeamCreatePrevious;               // 이전으로 넘겨서 이전것을 확인할 수 있는 버튼 오브젝트 

    public int count = 0;                               // 다음 버튼을 눌렀을 때 몇번째 페이지 인지 확인하기 위한 변수 

    public int roomCount = 0;                           // 맵을 선택할 때 몇번째 맵인지 확인 하기 위한 변수 

    //================================== 옵션창을 열었을 때 발생하는 Button 이벤트 ==================================//
    [Header("--------옵션 창--------")]
    public GameObject option;                   // 옵션 UI 오브젝트 
    public GameObject soundPanel;               // 사운드 조절 관련 UI 오브젝트 
    bool soundOptionCheck = false;              // 사운드 옵션 창 관련 Flag 변수 

    public GameObject Lobby;                    // 로비 오브젝트 
    public GameObject roomPanel;                // Room에서만 있는 UI Panel 오브젝트 
    bool optionCheck = false;                   // 옵션 창 관련 Flag 변수 

    //================================== 옵션 창에서 사운드 관련 탭의 Button 이벤트 ==================================//
    public Button Mic;                          // 마이크 버튼 오브젝트 
    public Button Speker;                       // 스피커 버튼 오브젝트 
    public Recorder NetworkVoiceManager;        // 보이스 네트워크 매니저 선언 ( 사운드 조절을 위해서는 필요 ) 
    public PunVoiceClient punVoiceClient;       // photon 보이스 관련 변수 선언 ( 스피커 차단을 위해서 필요 ) 

    int micCount = 0;                           // 마이크가 꺼져있는지 확인하기 위한 Flag 변수 
    public Image curImage;                      // 기존에 존재하는 이미지
    public Sprite changeSprite;                 // 바뀌어질 이미지
    public Sprite changeCurSprite;              // 바뀐 현재 이미지 ( 이미지를 바꾸고 현재 이미지를 저장 ) 

    int spekerCount = 0;                        // 스피커가 꺼져있는지 확인하기 위한 Flag 변수 
    public Image curSpekerImage;                // 기존에 존재하는 이미지
    public Sprite changeSpekerSprite;           // 바뀌어질 이미지
    public Sprite changeCurSpekerSprite;        // 바뀐 현재 이미지 ( 이미지를 바꾸고 현재 이미지를 저장 ) 

    //================================== 그림판 관련 변수 선언 ==================================//
    [Header("--------그림판--------")]
    public GameObject DrawPanel;
    public GameObject mainCamera;

    //=====================================================================//
    public GameObject preimg;
    public GameObject[] preava;
    public Camera precam;
    NetworkManager netManager;

    //=============================== 로그인 창에 존재하는 버튼의 이벤트 ======================================//
    // 홈페이지로 이동하기 버튼을 눌렀을 때 발생하는 이벤트 
    public void WithRium_Move()
    {
        Application.OpenURL("http://withrium.com/"); // 링크 걸 사이트 주소
    }

    // 회원가입 버튼을 눌렀을 때 발생하는 이벤트 
    public void WithRium_UserCreate()
    {
        Application.OpenURL("http://withrium.com/signup"); // 링크 걸 사이트 주소
    }

    //=============================== 공개방 입장 포탈에서 할 수 있는 Button 이벤트 ===============================//
    public void LobbyPanel_Select_Next()
    {
        // 캐릭터 선택 후 일어나는 이벤트 

        // 캐릭터를 선택했을 때 캐릭터 버튼을 다 비활성화 
        // 남여 선택 버튼도 비활성화 
        for(int i = 0; i < LobbyPanel_Player.Length; i++) 
            LobbyPanel_Player[i].SetActive(false);
        for (int i = 0; i < Women_Men.Length; i++)
            Women_Men[i].SetActive(false);
        for(int i =0;i< LobbyPanel_NextButton.Length; i++)    
            LobbyPanel_NextButton[i].SetActive(false);
        
        LobbyPanel_PreviousButton.SetActive(true);              // 아바타 선택에서 캐릭터 선택으로 돌아갈 수 이전 버튼 활성화 
        publicplayerBackGround.SetActive(false);                // 캐릭터 선택창의 뒤배경 비활성화 
        publicNextButton.SetActive(true);                       // 아바타 선택 후 맵선택으로 이동할 수 있는 다음 버튼 활성화 
        publicPreviousButton.SetActive(true);                   // 캐릭터 선택으로 돌아갈 수 있는 버튼 활성화 
        pCount++;                                               // 다음 페이지로 이동하였기 때문에 flag 변수를 1 증가 ( 현재 1페이지라는 것을 알림 ) 
    }

    public void LobbyPanel_Next_Button()
    {
        // 두번째 뒤로가기 버튼을 눌렀을 때 ( 아바타 선택을 끝냈을 때 ) 

        publicRoomCount = 0;                                        // 맵 선택 창으로 가면 초기에 무조건 처음 맵으로 초기화 하기 위해서 맵 변수 초기화 
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();           // LoginManager 오브젝트를 찾아서 가져온다.  
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();   // NetworkManager 오브젝트를 찾아서 가져온다. 

        for (int i = 0; i < networkManager.itemName.Length; i++) 
            Debug.Log(networkManager.itemName[i]);                  // 아이템을 가져왔는지 확인 ( 디버그 )                
            
        publicItemBackGround.SetActive(false);                      // 아바타 선택 뒤배경 비활성화 
        publicItemList.SetActive(false);                            // 아바타 체크 박스 비활성화 
        publicNextButton.SetActive(false);                          // 다음 버튼 비활성화 ( 맵을 선택하면 자동으로 맵으로 이동되기 때문에 다음 버튼이 필요가 없음 ) 

        RoomCreate_Input.SetActive(true);                           // 맵 선택 활성화 

        // 관리자일 때와 사용자일 때 나눠서 서로 나오는 UI 창이 다름 
        // 1 일 때는 관리자 ( 따라서 맵을 생성하는 버튼이 등장함 ) 
        // 0 일 때는 사용자 ( 따라서 맵을 입장하는 버튼이 등장함 ) 
        if (loginManager.managerIndex == 1) {
            for (int i = 0; i < publicCreateRoom.Length; i++)
                publicCreateRoom[i].SetActive(false);
            publicCreateRoom[publicRoomCount].SetActive(true);
        }
        else if(loginManager.managerIndex == 0) {
            for (int i = 0; i < publicInputRoom.Length; i++)
                publicInputRoom[i].SetActive(false);
            publicInputRoom[publicRoomCount].SetActive(true);
        }

        pCount++;           // 다음 버튼을 눌러서 이동하였기 때문에 flag 변수 변동 ( flag 변수 == 2 )
    }

    public void LobbyPanel_Select_Previous()
    {
        // 이전 버튼을 눌렀을 동작하는 이벤트 

        // 아바타 창에서 캐릭터 선택창으로 돌아갈 때
        if (pCount == 1) {
            RoomCreate_Input.SetActive(false);          // 맵 버튼 비활성화
            LobbyPanel_PreviousButton.SetActive(false); // 이전 버튼 비활성화 
            publicPreviousButton.SetActive(false);      // 이전 버튼 비활성화 
            publicNextButton.SetActive(false);          // 다음 버튼 비활성화 

            Women();                                    // 기본으로 여자 캐릭터가 나오도록 함 
            for (int i = 0; i < Women_Men.Length; i++)
                Women_Men[i].SetActive(true);           // 남여 선택 버튼 활성화 
            publicplayerBackGround.SetActive(true);     // 캐릭터 선택 뒤배경 활성화 

            pCount--;                                   // 페이지 Flag 변수 감소 ( 현재 Flag == 0 ) 
        }
        // 맵 선택 창에서 아바타 선택 창으로 돌아갈 때 
        else if(pCount == 2) {
            publicItemBackGround.SetActive(true);       // 아바타 선택 뒤배경 활성화 
            publicItemList.SetActive(true);             // 아바타 체크 박스 활성화 
            publicNextButton.SetActive(true);           // 다음 버튼 활성화 

            RoomCreate_Input.SetActive(false);          // 캐릭터 선택 버튼 비활성화

            pCount--;                                   // 페이지 Flag 변수 감소 ( 현재 Flag == 1 )     
        }
    }
    public void Women()
    {
        // 여자 캐릭터 버튼 활성화 
        for (int i = 0; i < 3; i++) {
            LobbyPanel_Player[i].SetActive(true);
            LobbyPanel_Player[i+3].SetActive(false);
        }
    }
    public void Men()
    {
        // 남자 캐릭터 버튼 활성화 
        for (int i = 0; i < 3; i++) {
            LobbyPanel_Player[i].SetActive(false);
            LobbyPanel_Player[i + 3].SetActive(true);
        }
    }

    // 맵 선택 ( 기본적으로 맵은 봄, 여름, 가을, 겨울이 있다. 이것을 순서대로 나오도록 한다. ) 
    // 봄에서 이전 버튼을 누르면 겨울이 나오고 겨울에서 다음 버튼을 누르면 봄이 나온다. 
    public void PublicCreateRoomNext()
    {
        // 관리자일 때 맵 생성 버튼이 만들어지도록 함 
        if (publicRoomCount == 0) {             // 봄에서 여름 
            publicRoomCount++;
            MapRotation(publicRoomCount);
        }
        else if (publicRoomCount == 1) {        // 여름에서 가을 
            publicRoomCount++;
            MapRotation(publicRoomCount);
        }
        else if (publicRoomCount == 2) {        // 가을에서 겨울 
            publicRoomCount++;
            MapRotation(publicRoomCount);
        }
        else if (publicRoomCount == 3) {        // 겨울에서 봄
            publicRoomCount = 0;                // 봄으로 가기 위해 맵 변수 초기화 
            MapRotation(publicRoomCount);
        }

        for (int i = 0; i < publicInputRoom.Length; i++)
            publicInputRoom[i].SetActive(false);
    }
    public void PublicCreateRoomPrevious()
    {
        // 관리자일 때 맵 생성 버튼이 만들어지도록 함 
        if (publicRoomCount == 0) {             // 봄에서 겨울  
            publicRoomCount = 3;                // 겨울 맵으로 그기 위해 맵 변수 초기화 
            MapRotation(publicRoomCount);
        }   
        else if (publicRoomCount == 1) {        // 여름에서 봄 
            publicRoomCount--;
            MapRotation(publicRoomCount);
        }
        else if (publicRoomCount == 2) {        // 가을에서 여름 
            publicRoomCount--;
            MapRotation(publicRoomCount);
        }
        else if (publicRoomCount == 3) {        // 겨울에서 가을 
            publicRoomCount--;
            MapRotation(publicRoomCount);
        }

        for (int i = 0; i < publicInputRoom.Length; i++)
            publicInputRoom[i].SetActive(false);
    }
    // 맵 버튼을 껐다 키면서 버튼을 보여준다. 
    public void MapRotation(int flag)
    {
        // 맵 버튼을 전체적으로 끔 
        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(false);

         // 이후 맵 Flag 변수에 맞는 맵 버튼을 활성화 시킴 
        publicCreateRoom[flag].SetActive(true);
    }

    // 맵 입장 ( 기본적으로 맵은 봄, 여름, 가을, 겨울이 있다. 이것을 순서대로 나오도록 한다. ) 
    // 봄에서 이전 버튼을 누르면 겨울이 나오고 겨울에서 다음 버튼을 누르면 봄이 나온다. 
    public void PublicInputRoomNext()
    {
        // 사용자일 때 맵 입장 버튼이 만들어지도록 함 
        if (publicRoomCount == 0) {             // 봄에서 여름  
            publicRoomCount++;
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 1) {        // 여름에서 가을 
            publicRoomCount++;
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 2) {        // 가을에서 겨울 
            publicRoomCount++;
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 3) {        // 겨울에서 봄
            publicRoomCount = 0;                // 봄으로 가기 위해 맵 변수 초기화 
            MapRotationPlayer(publicRoomCount);
        }

        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(false);
    }

    public void PublicInputRoomPrevious()
    {
        if (publicRoomCount == 0) {             // 봄에서 겨울  
            publicRoomCount = 3;                // 겨울 맵으로 그기 위해 맵 변수 초기화 
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 1) {        // 여름에서 봄 
            publicRoomCount--;
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 2) {        // 가을에서 여름 
            publicRoomCount--;
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 3) {        // 겨울에서 가을 
            publicRoomCount--;
            MapRotationPlayer(publicRoomCount);
        }

        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(false);
    }

    public void MapRotationPlayer(int flag)
    {
        // 맵 버튼을 전체적으로 끔 
        for (int i = 0; i < publicInputRoom.Length; i++)
            publicInputRoom[i].SetActive(false);

        // 이후 맵 Flag 변수에 맞는 맵 버튼을 활성화 시킴 
        publicInputRoom[publicRoomCount].SetActive(true);
    }
    //=============================== 팀방 입장 포탈에서 할 수 있는 Button 이벤트 ===============================//
    public void TeamInput_Select_Next()
    {
        // 팀방 입장에서 다음버튼을 눌렀을때 
        for (int i = 0; i < TeamInput_Player.Length; i++)
            TeamInput_Player[i].SetActive(false);                   // 캐릭터 선택 버튼 비활성화
        for (int i = 0; i < TeamInput_Women_Men.Length; i++)
            TeamInput_Women_Men[i].SetActive(false);                // 남여 선택 버튼 비활성화 
        for (int i = 0; i < TeamInput_NextButton.Length; i++) 
            TeamInput_NextButton[i].SetActive(false);               // 다음 버튼 비활성화 ( 캐릭터에 있는 다음 버튼을 비활성화 ) 
        playerBackGround.SetActive(false);                          // 캐릭터 선택 뒤 배경 비활성화 

        TeamInput_PreviousButton.SetActive(true);                   // 이전 버튼 활성화 
        inputNextButton.SetActive(true);                            // 다음 버튼 활성화 ( 아바타 선택후 맵 선택으로 넘어가는 다음 버튼 ) 

        iCount++;                                                   // 페이지를 확인하기 위한 Flag 변수 ( flag == 1 ) 
    }

    public void TeamInput_Select_Next_Button()
    {
        // 아바타 선택 이후 다음 버튼을 눌렀을 때 
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();           // NetworkManager 스크립트가 있는 오브젝트를 가져온다. 
        for (int i = 0; i < networkManager.itemName.Length; i++) {
            Debug.Log(networkManager.itemName[i]);                  // 아바타 리스트을 제대로 가져왔는지 확인 ( 디버그 )
        }
        InputItemList.SetActive(false);                             // 아바타 체크박스 비활성화 
        InputItemBackGround.SetActive(false);                       // 아바타 선택 뒤 배경 비활성화 
        TeamInputPrevious.SetActive(true);                          // 방 선택 이전 버튼 활성화 
        TeamInputNext.SetActive(true);                              // 방 선택 다음 버튼 활성화 

        InputplayerTeaminput.SetActive(true);                       // 방 입장 UI 활성화 
        inputNextButton.SetActive(false);                           // 다음 버튼 비활성화 
        iCount++;                                                   // 페이지를 확인하기 위한 Flag 변수 ( flag == 2 ) 
    }

    public void TeamInput_Select_Previous()
    {
        // 이전 버튼을 눌렀을 때 발생하는 이벤트 
        // 아바타 선택창에서 이전 버튼을 눌렀을 때 
        if (iCount == 1) {
            TeamInput_Women();
            for (int i = 0; i < TeamInput_Women_Men.Length; i++)
                TeamInput_Women_Men[i].SetActive(true);             // 남여 선택 버튼 활성화 

            TeamInput_PreviousButton.SetActive(false);              // 이전 버튼 비활성화 
            TeamInputPrevious.SetActive(false);                     // 이전 버튼 비활성화
            TeamInputNext.SetActive(false);                         // 다음 버튼 비활성화 
            inputNextButton.SetActive(false);                       // 다음 버튼 비활성화
            playerBackGround.SetActive(true);                       // 캐릭터 선택 뒤 배경 활성화 
            iCount--;                                               // 페이지 확인을 위한 Flag 변수 ( flag == 0 됨 ) 
        }
        // 맵 선택에서 이전 버튼을 눌렀을 때 
        else if(iCount == 2) {                          
            InputItemList.SetActive(true);                          // 아바타 체크박스 활성화 
            InputItemBackGround.SetActive(true);                    // 아바타 뒤 배경 활성화 
            inputNextButton.SetActive(true);                        // 다음 버튼 활성화 
            InputplayerTeaminput.SetActive(false);                  // 방입장 UI 비활성화 
            TeamInputPrevious.SetActive(false);                     // 방 선택 이전 버튼 비활성화 
            TeamInputNext.SetActive(false);                         // 방 선택 다음 버튼 비활성화 
            iCount--;                                               // 페이지 확인을 위한 Flag 변수 ( flag == 1 됨 )        
        }
    }

    public void TeamInput_Women()
    {
        // 여자 캐릭터 버튼만 등장하도록 함 
        for (int i = 0; i < 3; i++) {
            TeamInput_Player[i].SetActive(true);
            TeamInput_Player[i + 3].SetActive(false);
        }
    }
    public void TeamInput_Men()
    {
        // 남자 캐릭터 버튼만 등장하도록 함 
        for (int i = 0; i < 3; i++) {
            TeamInput_Player[i].SetActive(false);
            TeamInput_Player[i + 3].SetActive(true);
        }
    }
    //========================================================================//
    public void TeamCreate_Select_Next()
    {
        if (count == 0) {
            for (int i = 0; i < TeamCreate_Player.Length; i++)
            {
                TeamCreate_Player[i].SetActive(false);
            }
            for (int i = 0; i < TeamCreate_Women_Men.Length; i++)
                TeamCreate_Women_Men[i].SetActive(false);
            preimg.SetActive(true);
            TeamRoomCreate.SetActive(false);
            TeamCreateRoomPrevious();
            TeamCreate_PreviousButton.SetActive(true);
            CreateplayerBackGround.SetActive(true);
            CreateItemBackGround.SetActive(false);
            CreateTeamNextSelect.SetActive(true);
            CreateTeamPreSelect.SetActive(true);
            count++;
        }
        else if(count == 1) {
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            for (int i = 0; i < networkManager.itemName.Length; i++) {
                Debug.Log(networkManager.itemName[i]);
            }
            CreateplayerBackGround.SetActive(false);
            TeamRoomCreate.SetActive(true);
            CreateItemList.SetActive(false);
            CreateTeamNextSelect.SetActive(false);
            CreateTeamPreSelect.SetActive(true);
            CreateItemSelectBackGround.SetActive(true);
            preimg.SetActive(false);
            count++;
            for (int i = 0; i < TeamCreateMap.Length; i++)
                TeamCreateMap[i].SetActive(false);
            TeamCreateMap[0].SetActive(true);
            roomCount = 0;
        }
        else if(count == 2) {
            CreateItemSelectBackGround.SetActive(false);
            for(int i =0; i<TeamCreate_NextButton.Length; i++)
                TeamCreate_NextButton[i].SetActive(false);
            TeamRoomCreate.SetActive(false);
            TeamCreateNext.SetActive(true);
            TeamCreatePrevious.SetActive(true);
            CreateTeamPreSelect.SetActive(true);
            count++;
            CreateRoomBackGround.SetActive(true);
        }
    }

    public void TeamCreate_Select_Previous()
    {
        
        if (count == 1) {
            TeamCreate_Women();
            for (int i = 0; i < TeamCreate_Women_Men.Length; i++)
                TeamCreate_Women_Men[i].SetActive(true);
            TeamRoomCreate.SetActive(false);
            TeamCreate_PreviousButton.SetActive(false);
            CreateItemBackGround.SetActive(true);
            CreateTeamNextSelect.SetActive(false);
            CreateTeamPreSelect.SetActive(false);
            preimg.SetActive(false);
            count--;
        }
        else if (count == 2) {
            TeamRoomCreate.SetActive(false);
            CreateItemList.SetActive(true);
            CreateTeamNextSelect.SetActive(true);
            CreateplayerBackGround.SetActive(true);
            CreateItemSelectBackGround.SetActive(false);
            preimg.SetActive(true);
            count--;
        }
        else if (count == 3) {
            CreateItemSelectBackGround.SetActive(true);
            for (int i = 0; i < TeamCreate_NextButton.Length; i++)
                TeamCreate_NextButton[i].SetActive(false);
            TeamRoomCreate.SetActive(true);
            TeamCreateNext.SetActive(false);
            TeamCreatePrevious.SetActive(false);
            CreateRoomBackGround.SetActive(false);
            for (int i = 0; i < TeamCreateMap.Length; i++)
                TeamCreateMap[i].SetActive(false);
            TeamCreateMap[0].SetActive(true);
            roomCount = 0;
            preimg.SetActive(false);
            count--;
        }
        
    }
    public void TeamCreate_Women()
    {
        for (int i = 0; i < 3; i++) {
            TeamCreate_Player[i].SetActive(true);
            TeamCreate_Player[i + 3].SetActive(false);
        }
    }
    public void TeamCreate_Men()
    {
        for (int i = 0; i < 3; i++) {
            TeamCreate_Player[i].SetActive(false);
            TeamCreate_Player[i + 3].SetActive(true);
        }
    }

    public void TeamCreateRoomNext()
    {
        if (roomCount == 0) {
            roomCount++;
            for (int i = 0; i < TeamCreateMap.Length; i++)
                TeamCreateMap[i].SetActive(false);
            TeamCreateMap[roomCount].SetActive(true);
        }
        else if (roomCount == 1) {
            roomCount++;
            for (int i = 0; i < TeamCreateMap.Length; i++)
                TeamCreateMap[i].SetActive(false);
            TeamCreateMap[roomCount].SetActive(true);
        }
        else if (roomCount == 2) {
            roomCount++;
            for (int i = 0; i < TeamCreateMap.Length; i++)
                TeamCreateMap[i].SetActive(false);
            TeamCreateMap[roomCount].SetActive(true);
        }
        else if (roomCount == 3) {
            roomCount=0;
            for (int i = 0; i < TeamCreateMap.Length; i++)
                TeamCreateMap[i].SetActive(false);
            TeamCreateMap[roomCount].SetActive(true);
        }
    }

    public void TeamCreateRoomPrevious()
    {
        if (roomCount == 0) {
            roomCount = 3;
            for (int i = 0; i < TeamCreateMap.Length; i++)
                TeamCreateMap[i].SetActive(false);
            TeamCreateMap[roomCount].SetActive(true);
        }
        else if (roomCount == 1) {
            roomCount--;
            for (int i = 0; i < TeamCreateMap.Length; i++)
                TeamCreateMap[i].SetActive(false);
            TeamCreateMap[roomCount].SetActive(true);
        }
        else if (roomCount == 2) {
            roomCount--;
            for (int i = 0; i < TeamCreateMap.Length; i++)
                TeamCreateMap[i].SetActive(false);
            TeamCreateMap[roomCount].SetActive(true);
        }
        else if (roomCount == 3) {
            roomCount--;
            for (int i = 0; i < TeamCreateMap.Length; i++)
                TeamCreateMap[i].SetActive(false);
            TeamCreateMap[roomCount].SetActive(true);
        }

    }

    //-----------------------------------------------------------------------------------//
    public void female1_preview()
    {
        precam.transform.localPosition = new Vector3(-980.6f, -538.5f, 19.5f);
        preimg.SetActive(true);
    }

    public void female2_preview()
    {
        precam.transform.localPosition = new Vector3(-980.6f, -538.5f, 15.5f);
        preimg.SetActive(true);
    }

    public void female3_preview()
    {
        precam.transform.localPosition = new Vector3(-980.6f, -538.5f, 11.5f);
        preimg.SetActive(true);
    }

    public void man1_preview()
    {
        precam.transform.localPosition = new Vector3(-980.6f, -538.5f, 7.5f);
        preimg.SetActive(true);
    }

    public void man2_preview()
    {
        precam.transform.localPosition = new Vector3(-980.6f, -538.5f, 3.5f);
        preimg.SetActive(true);
    }

    public void man3_preview()
    {
        precam.transform.localPosition = new Vector3(-980.6f, -538.5f, -1.5f);
        preimg.SetActive(true);
    }

    //-----------------------------------------------------------------------------------//

    public void OptionClose()
    {
        if (Lobby.activeSelf == true) {
            ThirdPersonController thirdPersonController = GameObject.Find("Female1").GetComponent<ThirdPersonController>();
            SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
            option.SetActive(false);
            optionCheck = false;
            thirdPersonController.MoveSpeed = 50f;
            thirdPersonController.SprintSpeed = 80f;
            thirdPersonController.turnStop = false;
            smoothFollow.turnOff = false;
        }
        else {
            ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
            option.SetActive(false);
            optionCheck = false;
            thirdPersonController.MoveSpeed = 50f;
            thirdPersonController.SprintSpeed = 80f;
            thirdPersonController.turnStop = false;
            smoothFollow.turnOff = false;
        }
    }

    public void SoundOption()
    {
        soundPanel.SetActive(true);
        soundOptionCheck = true;
        MusicControl musicControl = GameObject.FindGameObjectWithTag("MusicSource").GetComponent<MusicControl>();
        musicControl.audioSlider = GameObject.Find("BackGorundSoundSlide").GetComponent<Slider>();;
    }
    public void SoundOptionClose()
    {
        soundPanel.SetActive(false);
        soundOptionCheck = false;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Lobby.activeSelf == true && !optionCheck) {
            OptionOpen();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && roomPanel.activeSelf == true && Lobby.activeSelf == false && !optionCheck) {
            OptionOpen();
        }
        else if(optionCheck && Input.GetKeyDown(KeyCode.Escape) && Lobby.activeSelf == true) {
            ThirdPersonController thirdPersonController = GameObject.Find("Female1").GetComponent<ThirdPersonController>();
            SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
            if (soundOptionCheck) {
                SoundOptionClose();
            }
            else {
                option.SetActive(false);
                optionCheck = false;
                thirdPersonController.MoveSpeed = 50f;
                thirdPersonController.SprintSpeed = 80f;
                thirdPersonController.turnStop = false;
                smoothFollow.turnOff = false;
            }
        }
        else if (optionCheck && Input.GetKeyDown(KeyCode.Escape) && roomPanel.activeSelf == true && Lobby.activeSelf == false) {
            ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
            if (soundOptionCheck) {
                SoundOptionClose();
            }
            else {
                option.SetActive(false);
                optionCheck = false;
                thirdPersonController.MoveSpeed = 50f;
                thirdPersonController.SprintSpeed = 80f;
                thirdPersonController.turnStop = false;
                smoothFollow.turnOff = false;
            }
        }
    }

    public void OptionOpen()
    {
        ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
        option.SetActive(true);
        optionCheck = true;
        thirdPersonController.MoveSpeed = 0f;
        thirdPersonController.SprintSpeed = 0f;
        thirdPersonController.turnStop = true;
        smoothFollow.turnOff = true;
    }
    public void Micblock()
    {
        if (micCount == 0) {
            NetworkVoiceManager.TransmitEnabled = false;
            micCount++;
            curImage.sprite = changeSprite;
            
        }
        else if (micCount == 1){
            NetworkVoiceManager.TransmitEnabled = true;
            micCount = 0;
            curImage.sprite = changeCurSprite;

            punVoiceClient.GetComponent<PunVoiceClient>().enabled = true;
            spekerCount = 0;
            curSpekerImage.sprite = changeCurSpekerSprite;
        }
        else {
            NetworkVoiceManager.TransmitEnabled = true;
            micCount = 0;
            curImage.sprite = changeCurSprite;

            punVoiceClient.GetComponent<PunVoiceClient>().enabled = true;
            spekerCount = 0;
            curSpekerImage.sprite = changeCurSpekerSprite;
        }
    }

    public void SpekerBlock()
    {
        if (spekerCount == 0) {
            punVoiceClient.GetComponent<PunVoiceClient>().enabled = false;
            spekerCount++;
            curSpekerImage.sprite = changeSpekerSprite;

            NetworkVoiceManager.TransmitEnabled = false;
            micCount++;
            curImage.sprite = changeSprite;
        }
        else if (spekerCount == 1) {
            punVoiceClient.GetComponent<PunVoiceClient>().enabled = true;
            spekerCount = 0;
            curSpekerImage.sprite = changeCurSpekerSprite;

            NetworkVoiceManager.TransmitEnabled = true;
            micCount = 0;
            curImage.sprite = changeCurSprite;
        }
    }

    //=============================== 그림판 관련 함수 ===============================//
    public void SetDrawPanel()
    {
        DrawPanel.SetActive(true);
        mainCamera.SetActive(false);
        roomPanel.SetActive(false);
    }

    public void ExitDrawPanel()
    {
        DrawPanel.SetActive(false);
        mainCamera.SetActive(true);
        roomPanel.SetActive(true);
    }
    //=============================== 로그인 실패시 에러 문구 출력 ===============================//

    public void LoginErrorMessageClose()
    {
        // 로그인 실패시 UI를 표기해준다. 
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        loginManager.rectLogin.anchoredPosition = new Vector2(400, 400);
    }
}
