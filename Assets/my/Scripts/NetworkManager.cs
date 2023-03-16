using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// Photon을 사용하기 위함
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor;
using System.Linq;
using Photon.Pun.Demo.Cockpit;
// 자체적으로 Photon에 규칙을 주기 위함 
using Hashtable = ExitGames.Client.Photon.Hashtable;
using StarterAssets;
using Vuplex.WebView;

// 포톤 서버를 활용한 멀티 시스템 구현 
public class NetworkManager : MonoBehaviourPunCallbacks,IPunInstantiateMagicCallback
{
    // User의 데이터를 가져오기 위해 DB와 통신 
    [Header("Url")]
    public string UserData;                 // 사용자의 데이터 URL ( 닉네임과 관리자 여부를 받아온다 ) 
    public string LogoutURL;                // 사용자 로그아웃 URL ( 로그아웃 시 login_state = 0 으로 만듬 ) 
    public string LoginUrl;                 // 사용자 로그인 URL ( 원래는 로그인 시 login_state = 1 로 변경 하였는데 기술적인 문제로 보류 ) 
    public string TeamData;                 // 팀정보 URL ( 팀테이블에서 팀의 이름을 받아온다 ) 
    public string UserTeamData;             // 사용자 팀전체 정보 URL ( 사용자의 팀 이름을 받아온다 ) 
    public string AllTeamData;              // 모든 팀의 정보 URL ( 팀에 속한 모든 사용자의 데이터를 가져옴 ) 
    public string ChatLogData;              // 채팅 로그를 넘기는 URL 
    UnityWebRequest wwwData;                // 요청한 값을 받아서 저장하는 변수 
    UnityWebRequest wwwData_2;              // 요청한 값을 받아서 저장하는 변수 

    [Header("Text")]
    public Text NickNameText = null;        // 닉네임을 받아오기위한 Text
    public Text StatusText;                 // 현재 계정의 접속 상태를 확인하기 위한 Text

    // 공용 Panel
    [Header("Public")]
    public GameObject Server;               // 닉네임, 상태, 인원, 로그아웃 관련 UI 
    public GameObject LoginPanelObj;        // 로그인 UI Panel 오브젝트 
    public GameObject LobbyPanelObj;        // 로비 UI Panel 오브젝트 

    // 관리자 
    [Header("Admin")]
    public GameObject ConnectServerObj;     // 관리자 서버 접속 오브젝트 ( 사용자와 구별하기 위해 만듬 ) 

    // 사용자
    [Header("User")]
    public GameObject UserConnectServerObj; // 사용자 서버 접속 오브젝트 ( 관리자와 구별하기 위해 만듬 ) 

    [Header("AdminCheck")]
    public bool adminCheck = false;

    // 버튼 
    [Header("CreateRoomButton")]
    public GameObject[] createRoom;          // 관리자가 생성하는 공개방 A 
    public GameObject[] Create_Next_Previous;

    [Header("InputRoomButton")]
    public GameObject[] inputRoom;           // 사용자가 입장하는 공개방 A 
    public GameObject[] Input_Next_Previous;

    [Header("CreateTeamRoom")]
    public GameObject createTeam;           // 팀장이 만들 방 UI Panel
    public GameObject inputTeam;            // 팀원이 입장할 방 UI Panel
    public GameObject[] createTeamRoom;     // 방을 만들 버튼 배열 ( 한 페이지에 최대 10개만 띄움 ) 
    public GameObject[] inputTeamRoom;      // 방에 들어갈 버튼 배열 ( 한 페이지에 최대 10개만 띄움 ) 

    [Header("Test Map")]
    public GameObject map;                  // 맵 프리팹 ( 각 맵에 이름만 지정 해주면 됨 ) 
    public GameObject player;               // 플레이어 프리팹 ( 각 캐릭터에 이름만 지정 해주면 됨 ) 
    public GameObject web;                  // 
    public GameObject keybo;                  // 
    public string playername;               // 버튼을 눌렀을 때 그 버튼의 Name 값을 저잫하기 위한 변수 ( 캐릭터 이름을 저장 ) 
    public string mapname;                  // 버튼을 눌렀을 때 그 버튼의 Namw 값을 저장하기 위한 변수 ( 맵 이름을 저장 ) 

    [Header("Lobby")]
    public GameObject Lobby;
    public GameObject LobbyMainPlayer;      // 로비의 Player 오브젝트 
    //public GameObject webView;              // 로비의 webView 오브젝트 


    [Header("-----실시간 채팅 시스템 구현-----")]
    [Header("LobbyPanel")]
    public GameObject LobbyPanel;           // 로비 Panel
    public Text WelcomeText;                // 닉네임 + 환영합니다 Text
    public Text LobbyInfoText;              // 현재 로비의 인원수 Text
    public Button[] CellBtn;                // 방을 찾기 위한 n개의 방 버튼 
    public Button PreviousBtn;              // 방 리스트 앞으로 가기 버튼 
    public Button NextBtn;                  // 방 리스트 뒤로 가기 버튼 

    [Header("RoomPanel")]
    public GameObject ChatSystem;           // 채팅 UI 오브젝트 
    public GameObject RoomPanel;            // 채팅 + 맵이 있는 Room 오브젝트 
    public Text ListText;                   // 현재 접속해 있는 유저의 이름 표시 
    public Text RoomInfoText;               // 방이름, 현재 인원, 최대 인원 표시 
    public Text[] ChatText;                 // 채팅창의 줄을 몇줄로 지정할 지 TextField의 개수를 지정 
    public InputField ChatInput;            // 채팅을 입력할 InputField 
    public bool chatCheck = false;

    [Header("ETC")]
    public PhotonView PV;                   // 현재 자기 자신의 PhotonView를 넣음 

    List<RoomInfo> myList = new List<RoomInfo>();       // 방 리스트 갱신을 위한 List 타입 변수 
    int currentPage = 1, maxPage, multiple;             // 방 리스트 넘기기 버튼 관련 변수 ( 현재 페이지, 최대 페이지, 몇페지까지 있는지 ) 

    // 팀방 관련 방 제한 풀기 위한 변수
    public int curPage = 0;                 // 현재 페이지 저장 변수
    public int endPage = 0;                 // 마지막 페이지 저장 변수 
    public int pageCount = 1;               // 페이지 넘어갈 때의 변수 

    public bool UserCheck = false;

    public static NetworkManager Instance;

    public GameObject lodingPanel;

    public bool checkRoom = false;
    //--------------------------------------------- 빌드 화면 크기 ---------------------------------------------// 
    void Awake()
    {
        // 빌드했을 때 화면 비율 
        Screen.SetResolution(362, 530, false);
    }

    //--------------------------------------------- 백 접근 URL ---------------------------------------------//  
    void Start()
    {
        // 버튼을 눌렀을 때 이동할 URL 
        // (인텔리제이에서 jsp 파일을 실행시켰을 때의 URL을 입력)
        // 포트포워딩을 이용하여 외부에서도 접근할 수 있도록 함 
        LoginUrl = "http://223.131.75.181:1356/Metaverse_war_exploded/Login.jsp";
        UserData = "http://223.131.75.181:1356/Metaverse_war_exploded/UserData.jsp";
        LogoutURL = "http://223.131.75.181:1356/Metaverse_war_exploded/Logout.jsp";

        // 팀 데이터 가져오기 위한 URL
        TeamData = "http://223.131.75.181:1356/Metaverse_war_exploded/TeamData.jsp";
        UserTeamData = "http://223.131.75.181:1356/Metaverse_war_exploded/UserTeamData.jsp";
        AllTeamData = "http://223.131.75.181:1356/Metaverse_war_exploded/TeamAllData.jsp";

        // 채팅로그 가져오기
        ChatLogData = "http://223.131.75.181:1356/Metaverse_war_exploded/ChatLog.jsp";
    }

    //--------------------------------------------- 사용자의 상태, 로비의 인원, 채팅창 관련 로직 ---------------------------------------------// 
    void Update() 
    {
        // 상태를 나타내는 text
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();

        // Lobby에 몇명이 있는지 확인하는 text
        // 전체Player 중에서 Room에 들어가 있는 사람은 제외 하고 로비에 몇명이 있는지 확인 
        LobbyInfoText.text = "<color=#0000ff>" + (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms)  + "로비 / " + PhotonNetwork.CountOfPlayers + "접속" + "</color>";

        // Enter를 눌렀을 때 채팅창에 입력할 수 있게 하기 위한 로직 
        // Enter를 누르고 방에 입장 하였을 때 캐릭터를 멈추고 채팅을 칠 수 있게 함 
        if (Input.GetButtonDown("ChatStart") && RoomPanel.activeSelf == true) {
            Debug.Log("enter 누름");
            // Player 태그의 오브젝트의 속도를 줄이고 회전을 막음 
            ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            thirdPersonController.MoveSpeed = 0.0f;
            thirdPersonController.SprintSpeed = 0.0f;
            thirdPersonController.turnStop = true;
            thirdPersonController.JumpCheck = true;

            SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
            smoothFollow.turnOff = true;

            chatCheck = true;
            // 채팅창 UI를 활성화 
            ChatSystem.SetActive(true);
            // 채팅창의 InputField를 활성화 하여 바로 채팅을 칠 수 있도록 함 
            ChatInput.ActivateInputField();
            // 보내는 함수 
            Send();
        }
        // 다시 움직이고 싶으면 방에서 마우스를 클릭하면 움직일 수 있도록 함 
        else if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && RoomPanel.activeSelf == true && chatCheck) {
            // 속도를 50 주고 회전을 풀음 
            ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            thirdPersonController.MoveSpeed = 50.0f;
            thirdPersonController.SprintSpeed = 80.0f;
            thirdPersonController.turnStop = false;
            thirdPersonController.JumpCheck = false;

            SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
            smoothFollow.turnOff = false;

            chatCheck = false;
        }
    }

    //--------------------------------------------- 서버 접속 함수  ---------------------------------------------//  
    public void Connect()
    {
        // 네트워크에 접속이 되었을 때 
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // 서버 접속이 성공적으로 되었을 때 
        print("서버접속완료");
        NickNameText.text = null;
        // 사용자의 데이터를 가져온다 
        StartCoroutine(UserNick());
        Screen.SetResolution(1920, 1080, true);

        lodingPanel.SetActive(true);
        // 로비 캐릭터의 위치를 0, 3, 0으로 초기화
        LobbyMainPlayer.transform.position = new Vector3(0f, 1f, -9f);

        // 성공적으로 접속이 될 시 바로 로비로 이동 
        PhotonNetwork.JoinLobby();
    }

    //--------------------------------------------- Lobby 입장 함수  ---------------------------------------------// 
    public void JoinLobby()
    {
        // 로비 참가 
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("로비접속완료");
       
        // 방 생성과 동일하게 계정의 닉네임을 가져온다 
        //StartCoroutine(UserNick());
        RoomPanel.SetActive(false);             // Room Panel 비활성화 
        Lobby.SetActive(true);                  // 로비 Floor 활성화 
        //webView.SetActive(true);


        // player 오브젝트를 찾아서 그 안의 Start 함수를 실행 
        // 카메라가 방에서 나왔을 시 로비 캐릭터를 잡게 만들기 위해서 Start 함수를 실행 하는 것 이다. 
        LobbyPlayer lobbyPlayer = GameObject.Find("Female1").GetComponent<LobbyPlayer>();
        lobbyPlayer.Start();

        SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
        smoothFollow.roteta();

        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();
        buttonEvent.OptionClose();
        buttonEvent.SoundOptionClose();


        // 방에서 나가고 로비로 돌아왔을때 
        // 리스트를 초기화를 해줘야 버그가 발생하지 않는다. 
        // 이렇게 해야 중간에 빈방이 생기지 않는다. 
        myList.Clear();
    }


    public void OnPhotonInstantiate(PhotonMessageInfo info)    
    {
        //if (info.photonView.gameObject.name==("WebViewPrefab")) {
        //    WebViewRPC.Instance._Prefab = web;
        //    WebViewRPC.Instance.Initalize();
        //}
       
    }

    //--------------------------------------------- NickName를 가져오는 코루틴 ---------------------------------------------// 
    IEnumerator UserNick()
    {
        // 외부 스크립트를 가져오기위해 Find를 사용 
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        // ID가 키값이므로 ID의 값만 가져온다. 
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(UserData, form);
        yield return wwwData.SendWebRequest();

        // 반환값을 저장하고 공백을 제거하여 문자열을 저장 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        // 받아온 문자열을 ,를 기준으로 나누어 배열로 저장 
        string[] reSplit = re.Split(',');
        // 배열에 문자열이 잘 들어갔는지 확인 
        // 결과 [0] : 닉네임 [1] : 1 또는 0 ( 1은 admin 0은 user ) 
        for (int i = 0; i < reSplit.Length; i++)
            Debug.Log(reSplit[i]);

        // 닉네임을 받아와서 변수에 저장 
        NickNameText.text = reSplit[0];
        PhotonNetwork.LocalPlayer.NickName = reSplit[0];
        

        // 로비 Panel에서 접속시 생성되는 문구 
        WelcomeText.text = "<color=#0000ff>" + PhotonNetwork.LocalPlayer.NickName + "</color>" + "님 환영합니다";
        

        // 받아온 값이 admin 계정일 때 
        // 관리자와 사용자의 화면을 구별하여 각각 버튼 활성화의 값을 다르게 줌 
        // 공개방 생성 버튼만 만듬 ( 추후 관리자도 공개방에 입장하기 버튼을 활성화 할 예정 ) 
        if (reSplit[1] == "1") {
            adminCheck = true;
            ConnectServerObj.SetActive(false);          // 관리자 서버 연결 화면 비활성화 
            //Server.SetActive(true);                     // 닉네임을 가지고 있는 Panel 활성화 
            createTeam.SetActive(false);                // 팀방 만들기 버튼 비활성화 
            inputTeam.SetActive(false);                 // 팀방 입장하기 버튼 비활성화 
            for (int i = 0; i < createRoom.Length; i++) {
                inputRoom[i].SetActive(false);
                createRoom[i].SetActive(true);
            }
            for (int i = 0; i < Create_Next_Previous.Length; i++) {
                Create_Next_Previous[i].SetActive(true);
                Input_Next_Previous[i].SetActive(false);
            }
        }
        // 받아온 값이 user 계정일 때 
        // 공개방 입장 버튼만 만듬 
        else if (reSplit[1] == "0" ) {
            adminCheck = false;
            UserConnectServerObj.SetActive(false);      // 사용자 서버 연결 화면 비활성화       
            //Server.SetActive(true);                     // 닉네임을 가지고 있는 Panel 활성화 
            createTeam.SetActive(false);                // 팀방 만들기 버튼 비활성화 
            inputTeam.SetActive(false);                 // 팀방 입장하기 버튼 비활성화 
            for (int i = 0; i < createRoom.Length; i++) {
                inputRoom[i].SetActive(true);
                createRoom[i].SetActive(false);
            }
            for (int i = 0; i < Create_Next_Previous.Length; i++) {
                Create_Next_Previous[i].SetActive(false);
                Input_Next_Previous[i].SetActive(true);
            }
        }
      
        // 메모리 누수 방지를 위해 
        wwwData.Dispose();
    }

    //--------------------------------------------- 서버 연결을 끊을 때 함수 ---------------------------------------------// 
    public void Disconnect()
    {
        // 서버를 끊을 때 ( 로그아웃이 될 때 )
        StartCoroutine(Logout());                       

        LoginPanelObj.SetActive(true);                  // 로그인 Panel 활성화 
        ConnectServerObj.SetActive(false);              // 관리자 서버 연결 Panel 비활성화 
        UserConnectServerObj.SetActive(false);          // 사용자 서버 연결 Panel 비활성화
        LobbyPanelObj.SetActive(false);                 // 로비 Panel 비활성화 
        Server.SetActive(false);                        // 닉네임 Panel 비활성화 
        Lobby.SetActive(false);
        //webView.SetActive(false);
        NickNameText.text = null;                       // 닉네임창 초기화 
        UserCheck = false;

        Screen.SetResolution(362, 530, false);
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        loginManager.IDInputField.text = "";
        loginManager.PWInputField.text = "";
        // 실제로 포톤서버의 네트워크를 끊기 
        PhotonNetwork.Disconnect();                 
    }

    public override void OnDisconnected( DisconnectCause cause )
    {
        // Disconnect() 함수가 실행될 때 
        print("연결끊김");
        // 로비 Panel과 방 Panel을 비활성화 
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
    }

    //--------------------------------------------- Logout 값을 가져오는 코루틴 ---------------------------------------------// 
    // 로그인 상태를 0으로 만들어 주는 LogOut 백서버를 불러옴 
    IEnumerator Logout()
    {
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(LogoutURL, form);
        yield return wwwData.SendWebRequest();
        
        // 버튼을 끄기 위함 코루틴 ( 팀방 생성하기, 팀방 입장하기 버튼 ) 
        StartCoroutine(TeamButtonOff());     
    }
    IEnumerator TeamButtonOff()
    {
        //------------------------------------- 팀방 만들기 버튼 끄기 -------------------------------------//
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(TeamData, form);
        yield return wwwData.SendWebRequest();

        // 반환값을 저장하고 공백을 제거하여 문자열을 저장 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        // 마지막 ,도 가져오게 되면 \n 값도 추가가 되기때문에 마지막 ,는 지워준다
        re = re.TrimEnd(',');
        Debug.Log(re);
        string[] reSplit = re.Split(',');

        // 배열을 넘어가는 에러를 잡기위한 로직 
        // 생성된 버튼의 개수가 10개보다 많으면 전체 10개의 버튼을 비활성화 
        if(reSplit.Length > 10) {
            for (int i = 0; i < 10; i++) 
                createTeamRoom[i].SetActive(false);
        }
        // 생성된 버튼의 개수가 10개 보다 적으면 생성된 만큼만 비활성화 
        else {
            for (int i = 0; i < reSplit.Length; i++) 
                createTeamRoom[i].SetActive(false);
        }

        //--------------------------------------------- 팀방 입장하기 버튼 끄기 ---------------------------------------------// 
        wwwData_2 = UnityWebRequest.Post(UserTeamData, form);
        yield return wwwData_2.SendWebRequest();

        // 반환값을 저장하고 공백을 제거하여 문자열을 저장 
        string str_2 = wwwData.downloadHandler.text;
        string re_2 = string.Concat(str_2.Where(x => !char.IsWhiteSpace(x)));
        // 마지막 ,도 가져오게 되면 \n 값도 추가가 되기때문에 마지막 ,는 지워준다
        re_2 = re_2.TrimEnd(',');
        Debug.Log(re_2);
        string[] reSplit_2 = re_2.Split(',');

        // 배열을 넘어가는 에러를 잡기위한 로직 
        // 생성된 버튼의 개수가 10개보다 많으면 전체 10개의 버튼을 비활성화
        if (reSplit_2.Length > 10) {
            for (int i = 0; i < 10; i++) 
                inputTeamRoom[i].SetActive(false);
        }
        // 생성된 버튼의 개수가 10개 보다 적으면 생성된 만큼만 비활성화 
        else {
            for (int i = 0; i < reSplit_2.Length; i++)
                inputTeamRoom[i].SetActive(false);
        }
        wwwData.Dispose();
    }

    //--------------------------------------------- 팀방 만들기 버튼의 제한을 없애기 위한 함수 ---------------------------------------------// 
    // ◀버튼 -2 , ▶버튼 -1
    public void TeamListClick(int num)
    {
        // 이전 버튼을 눌렀을 때 
        if (num == -2) {
            // 전체적으로 버튼을 다 비활성화 
            for (int i = 0; i < 10; i++)
                createTeamRoom[i].SetActive(false);
            // 이후 다시 활성화
            CreateTeam();
            // 현재 페이지가 0보다 크면 다음 페이지로 넘어갔다는 것 
            // 따라서 이전으로 돌아오게 하기 위해서 Count를 줄이고 배열의 값을 10 감소
            // 버튼을 클릭시 reSplit의 배열 값을 10 감소 시킴 
            if (curPage > 0) {
                pageCount--;
                curPage -= 10;
            }
            // 현재 페이지가 0보다 작거나 같으면 페이지를 넘긴 적이 없다는 것이므로 
            // 값을 0으로 초기화 하여 배열을 초과하는 에러를 막는다. 
            else if (curPage <= 0)
                curPage = 0;
        }
        // 다음 버튼을 눌렀을 때 
        else if (num == -1) {
            // 전체 버튼을 비활성화 후 다시 활성화 
            for (int i = 0; i < 10; i++)
                createTeamRoom[i].SetActive(false);
            CreateTeam();
            // 현재 페이지가 0보다 크거나 같고 현재페이지 + 10 한 값이 마지막 페이지 값보다 작으면 다음 페이지로 넘길 수 있게 함 
            // 현재 페이지는 0 부터 시작하고 마지막 페이지는 기본 값이 10부터 시작하여 10만큼 값이 차이가 난다 
            // 현재 페이지가 마지막 페이지 인것을 확인하려면 10을 더해줘서 그 값이 같은지 확인 하여야 한다
            // 버튼을 클릭시 reSplit의 배열 값을 10 증가 시킴 
            if (curPage >= 0 && (curPage + 10) < endPage) {
                pageCount++;
                curPage += 10;
            }
            // 마지막 페이지라면 현재 페이지에 마지막페이지 -10 값을 대입 
            // 차이가 10만큼 나기 때문에 -10 값을 넣어주어 배열을 초과하는 에러를 막는다.
            else if (curPage + 10 >= endPage)
                curPage = endPage - 10;
        }
    }

    //--------------------------------------------- 팀방 생성 함수 ---------------------------------------------// 
    public void CreateTeam()
    {
        LobbyPanel.SetActive(false);        // 로비 Panel 비활성화 
        createTeam.SetActive(true);         // 팀방 생성 UI 활성화 
        inputTeam.SetActive(false);         // 팀방 입장하기 UI 비활성화 
        Server.SetActive(false);            // server에 있는 UI 비활성화 
        StartCoroutine(cTeam());            // 팀방 생성 관련 코루틴 
    }
    
    IEnumerator cTeam()
    {
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        // ID가 키값이므로 ID의 값만 가져온다. 
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(TeamData, form);
        yield return wwwData.SendWebRequest();

        // 반환값을 저장하고 공백을 제거하여 문자열을 저장 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x))); 
        // 마지막 ,도 가져오게 되면 \n 값도 추가가 되기때문에 마지막 ,는 지워준다
        re = re.TrimEnd(',');
        Debug.Log(re);
        // 받아온 문자열을 ,를 기준으로 나누어 배열로 저장
        
        string[] reSplit = re.Split(',');

        for (int i = 0; i < reSplit.Length; i += 2)
            Debug.Log(reSplit[i]);

        // 팀이 있을 때만 버튼을 활성화 하기 위한 if문 
        if (re != "") {
            // 각 버튼에 알맞는 팀 이름 넣기 
            for (int i = 0; i < reSplit.Length; i++) {
                // 팀이 10개가 넘을 때 
                if (reSplit.Length > 10) {
                    // 페이지 Count만큼 ( 몫만큼 페이지를 만들고 나머지는 else문에서 만듬 ) 
                    // 예로 팀이 33개가 있다면 pageCount는 3까지만 올라간다.
                    // 따라서 3페이지 까지는 총 10개의 버튼을 활성화 한다. ( 이론상 총 30개의 버튼을 활성화 하게 됨 ) 
                    // 하지만 페이지는 총 4개이기 때문에 4번째 페이지는 
                    // else 문에서 처리를 하게 된다. 
                    // else 문은 당연하게 나머지로 처리하여 버튼을 활성화 한다. 33을 10으로 나눈 나머지가 3이기 때문에 3개의 버튼을 활성화함 
                    // 따라서 최대 팀의 개수에 맞게 방의 버튼들이 활성화가 된다. 
                    if (pageCount <= (reSplit.Length / 10)) {
                        for (int j = 0; j < 10; j++) {
                            // Room + 숫자 방의 오브젝트의 ButtonValues스크립트에 접근 
                            // 각 버튼의 text에 가져온 팀 이름을 대입 
                            // Name 변수에 팀 이름을 대입 ( Name 변수는 ButtonValues스크립트에 있음 ) 
                            createTeamRoom[j].SetActive(true);
                            ButtonValues buttonValues = GameObject.Find("Room" + j).GetComponent<ButtonValues>();
                            createTeamRoom[j].transform.GetChild(0).GetComponent<Text>().text = reSplit[j + curPage];
                            buttonValues.Name = reSplit[j + curPage];
                        }
                    }
                    else {
                        for (int j = 0; j < reSplit.Length % 10; j++) {
                            // Room + 숫자 방의 오브젝트의 ButtonValues스크립트에 접근 
                            // 각 버튼의 text에 가져온 팀 이름을 대입 
                            // Name 변수에 팀 이름을 대입 ( Name 변수는 ButtonValues스크립트에 있음 ) 
                            createTeamRoom[j].SetActive(true);
                            ButtonValues buttonValues = GameObject.Find("Room" + j).GetComponent<ButtonValues>();
                            createTeamRoom[j].transform.GetChild(0).GetComponent<Text>().text = reSplit[j + curPage];
                            buttonValues.Name = reSplit[j + curPage];
                        }
                    }
                }
                // 팀이 10개가 넘지 않을 때 
                // reSplit의 개수 만큼 버튼을 활성화 
                else {
                    createTeamRoom[i].SetActive(true);
                    // Room + 숫자 방의 오브젝트의 ButtonValues스크립트에 접근 
                    // 각 버튼의 text에 가져온 팀 이름을 대입 
                    // Name 변수에 팀 이름을 대입 ( Name 변수는 ButtonValues스크립트에 있음 ) 
                    ButtonValues buttonValues = GameObject.Find("Room" + i).GetComponent<ButtonValues>();
                    createTeamRoom[i].transform.GetChild(0).GetComponent<Text>().text = reSplit[i + curPage];
                    buttonValues.Name = reSplit[i + curPage];
                }
                // 최대 페이지를 구해서 저장하는 변수 
                // 최대 페이지는 팀의 전체 개수를 10으로 나눈 나머기가 0이면 10,20 .. 처럼 딱 떨어지기 때문에 몫에 10을 곱하여 최대 버튼의 개수를 구하고 
                // 0이 아니라면 1페이지가 더 많은것이기 때문에 + 1을 하고 10을 곱하여 최대 버튼의 개수를 구한다. 
                // 이 값은 삼항연산자를 사용하여 값을 저장 
                endPage = (reSplit.Length % 10 == 0) ? (reSplit.Length / 10) * 10 : (reSplit.Length / 10 + 1) * 10;
            }
        }
        
        // 메모리 누수 방지 
        wwwData.Dispose();
    }
    public void CreateTeamRoom()
    {
        // 버튼에 알맞는 팀 방 생성 
        // 버튼을 클릭시 그 해당 버튼의 이벤트를 저장 
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        // 버튼의 value 값을 index에 저장 
        int index = clickObj.GetComponent<ButtonValues>().value;
        // 버튼의 Name 값을 TeamName에 저장 
        string TeamName = clickObj.GetComponent<ButtonValues>().Name;
        Debug.Log(index + TeamName);

        // 버튼의 최대 개수가 10개 index는 0 ~ 9까지 존재 
        // 버튼을 눌렀을 때 그 버튼의 value 값과 i 값이 같은 것의 방을 만듬 
        // 캐릭터와 맵을 선택하지 않으면 오류 메시지 
        for (int i =0; i < 10; i++) {
            if (index == i) {
                if (mapname != "" && playername != "") {
                    //ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
                    //thirdPersonController.CameraObj();
                    PhotonNetwork.CreateRoom(TeamName, new RoomOptions { MaxPlayers = 8 });
                }
                else if (mapname=="")
                    Debug.Log("맵을 선택해주세요.");
                else if (playername=="")
                    Debug.Log("캐릭터를 선택해 주세요");
            }
        }
    }

    //--------------------------------------------- 공개방 생성 함수 ---------------------------------------------// 
    // 공개방의 이름을 A, B, C 로 선언 
    string[] room = {"A", "B", "C", "D"};    
    public void CreateRoomA() 
    {
        // A방 생성, 방의 인원은 최대 21명 ( 관리자 한명 무조건 포함 ) 
        // 만약 방의 이름이 없다면 랜덤숫자로 + Room으로 방이름이 정해지고 아니면 내가 입력한 방이름으로       
        // 캐릭터와 맵을 선택하지 않으면 오류 메시지 
        if (mapname != "" && playername != "") {
            PhotonNetwork.CreateRoom(room[0] == "" ? "Room" + Random.Range(0, 100) : room[0], new RoomOptions { MaxPlayers = 20 });
        }
        else if (playername == "")
            Debug.Log("캐릭터를 선택해 주세요");
    }

    public void CreateRoomB()
    {
        // B방 생성, 방의 인원은 최대 21명 
        // 만약 방의 이름이 없다면 랜덤숫자로 + Room으로 방이름이 정해지고 아니면 내가 입력한 방이름으로
        // 캐릭터와 맵을 선택하지 않으면 오류 메시지 
        if (mapname != "" && playername != "")
            PhotonNetwork.CreateRoom(room[1] == "" ? "Room" + Random.Range(0, 100) : room[1], new RoomOptions { MaxPlayers = 20 });
        else if (playername == "")
            Debug.Log("캐릭터를 선택해 주세요");
    }

    public void CreateRoomC()
    {
        // C방 생성, 방의 인원은 최대 21명 
        // 만약 방의 이름이 없다면 랜덤숫자로 + Room으로 방이름이 정해지고 아니면 내가 입력한 방이름으로 
        // 캐릭터와 맵을 선택하지 않으면 오류 메시지 
        if (mapname != "" && playername != "")
            PhotonNetwork.CreateRoom(room[2] == "" ? "Room" + Random.Range(0, 100) : room[2], new RoomOptions { MaxPlayers = 20 });
        else if (playername == "")
            Debug.Log("캐릭터를 선택해 주세요");
    }

    public void CreateRoomD()
    {
        // C방 생성, 방의 인원은 최대 21명 
        // 만약 방의 이름이 없다면 랜덤숫자로 + Room으로 방이름이 정해지고 아니면 내가 입력한 방이름으로 
        // 캐릭터와 맵을 선택하지 않으면 오류 메시지 
        if (mapname != "" && playername != "")
            PhotonNetwork.CreateRoom(room[3] == "" ? "Room" + Random.Range(0, 100) : room[3], new RoomOptions { MaxPlayers = 20 });
        else if (playername == "")
            Debug.Log("캐릭터를 선택해 주세요");
    }

    //--------------------------------------------- 팀방 입장하기 버튼의 제한을 없애기 위한 함수 ---------------------------------------------// 
    // ◀버튼 -2 , ▶버튼 -1
    // 팀방 만들기 버튼의 제한을 없애기 위한 함수와 원리가 동일하다.
    public void TeamInputListClick(int num)
    {
        // 이전 버튼을 눌렀을 떄 
        if (num == -2) {
            for (int i = 0; i < 10; i++)
                inputTeamRoom[i].SetActive(false);
            InputTeam();
            if (curPage > 0) {
                pageCount--;
                curPage -= 10;
            }
            else if (curPage <= 0)
                curPage = 0;
        }
        // 다음 버튼을 눌렀을 때 
        else if (num == -1) {
            for (int i = 0; i < 10; i++)
                inputTeamRoom[i].SetActive(false);
            InputTeam();
            if (curPage >= 0 && (curPage + 10) < endPage) {
                pageCount++;
                curPage += 10;
            }
            else if (curPage + 10 >= endPage)
                curPage = endPage - 10;
        }
    }

    //--------------------------------------------- 팀방 입장하기 함수 ---------------------------------------------// 
    public void InputTeam()
    {
        LobbyPanel.SetActive(false);        // 로비 UI 및 오브젝트 비활성화 
        createTeam.SetActive(false);        // 팀방 생성하기 UI 비활성화 
        inputTeam.SetActive(true);          // 팀방 입장하기 UI 활성화 
        Server.SetActive(false);            // server관련 UI 비활성화 
        StartCoroutine(iTeam());            // 팀방 입장하기 코루틴 
    }
    IEnumerator iTeam()
    {
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        // ID가 키값이므로 ID의 값만 가져온다. 
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(UserTeamData, form);
        yield return wwwData.SendWebRequest();

        // 반환값을 저장하고 공백을 제거하여 문자열을 저장 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        // 마지막 ,도 가져오게 되면 \n 값도 추가가 되기때문에 마지막 ,는 지워준다
        re = re.TrimEnd(',');
        Debug.Log(re);
        // 받아온 문자열을 ,를 기준으로 나누어 배열로 저장 
        string[] reSplit = re.Split(',');

        // 원리는 /**팀방 생성 함수**/ 의 코루틴과 동일한 방식이다. 
        if (re != "") {
            // 각 버튼에 알맞는 팀 이름 넣기 
            for (int i = 0; i < reSplit.Length; i++) {
                // 팀으로 속해있는 방이 10개가 넘을 때 
                if (reSplit.Length > 10) {
                    // 페이지 Count만큼 ( 몫만큼 페이지를 만들고 나머지는 else문에서 만듬 ) 
                    // 예로 팀으로속해있는방이 33개가 있다면 pageCount는 3까지만 올라간다.
                    // 따라서 3페이지 까지는 총 10개의 버튼을 활성화 한다. ( 이론상 총 30개의 버튼을 활성화 하게 됨 ) 
                    // 하지만 페이지는 총 4개이기 때문에 4번째 페이지는 
                    // else 문에서 처리를 하게 된다. 
                    // else 문은 당연하게 나머지로 처리하여 버튼을 활성화 한다. 33을 10으로 나눈 나머지가 3이기 때문에 3개의 버튼을 활성화함 
                    // 따라서 최대 팀의 개수에 맞게 방의 버튼들이 활성화가 된다. 
                    if (pageCount <= (reSplit.Length / 10)) {
                        for (int j = 0; j < 10; j++) {
                            inputTeamRoom[j].SetActive(true);
                            ButtonValues buttonValues = GameObject.Find("InputRoom" + j).GetComponent<ButtonValues>();
                            inputTeamRoom[j].transform.GetChild(0).GetComponent<Text>().text = reSplit[j + curPage];
                            buttonValues.Name = reSplit[j + curPage];
                        }
                    }
                    // 나머지를 활용하여 가장 마지막 페이지의 버튼들을 활성화 
                    else {
                        for (int j = 0; j < reSplit.Length % 10; j++) {
                            inputTeamRoom[j].SetActive(true);
                            ButtonValues buttonValues = GameObject.Find("InputRoom" + j).GetComponent<ButtonValues>();
                            inputTeamRoom[j].transform.GetChild(0).GetComponent<Text>().text = reSplit[j + curPage];
                            buttonValues.Name = reSplit[j + curPage];
                        }
                    }
                }
                // 팀으로 속해있는 방이 10개가 넘지 않을 때 
                else {
                    inputTeamRoom[i].SetActive(true);
                    ButtonValues buttonValues = GameObject.Find("InputRoom" + i).GetComponent<ButtonValues>();
                    inputTeamRoom[i].transform.GetChild(0).GetComponent<Text>().text = reSplit[i + curPage];
                    buttonValues.Name = reSplit[i + curPage];
                }
                // 최대 페이지를 구해서 저장하는 변수 
                // 최대 페이지는 팀의 전체 개수를 10으로 나눈 나머기가 0이면 10,20 .. 처럼 딱 떨어지기 때문에 몫에 10을 곱하여 최대 버튼의 개수를 구하고 
                // 0이 아니라면 1페이지가 더 많은것이기 때문에 + 1을 하고 10을 곱하여 최대 버튼의 개수를 구한다. 
                // 이 값은 삼항연산자를 사용하여 값을 저장 
                endPage = (reSplit.Length % 10 == 0) ? (reSplit.Length / 10) * 10 : (reSplit.Length / 10 + 1) * 10;
            }
        }

        // 메모리 누수 방지 
        wwwData.Dispose();
    }

    public void InputTeamRoom()
    {
        // 버튼의 클릭이벤트를가져옴 
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        // 클릭한 버튼의 value값과 Name값을 가져와서 저장함 
        int index = clickObj.GetComponent<ButtonValues>().value;
        string TeamName = clickObj.GetComponent<ButtonValues>().Name;
        Debug.Log(index + TeamName);

        // 클릭한 버튼의 value 값과 i 값이 같으면 그 버튼의 Name 값을 가져와 그 방으로 입장 
        // 방 입장시 맵은 선택하지 않고 캐릭터만 선택하기 때문에 캐릭터를 선택하지 않고 입장하면 에러 발생 
        for (int i = 0; i < 10; i++) {
            if (index == i) {
                if (playername != "")
                    PhotonNetwork.JoinRoom(TeamName);
                else
                    Debug.Log("캐릭터를 선택해 주세요");
            }
        }
    }

    //--------------------------------------------- 공개방 입장하기 함수 ---------------------------------------------// 
    public void JoinRoomA()
    {
        // A방 입장
        // 캐릭터를 선택하지 않을 때 오류 메시지 
        if (playername != "")
            PhotonNetwork.JoinRoom(room[0]);
        else
            Debug.Log("캐릭터를 선택해 주세요");
    }

    public void JoinRoomB()
    {
        // B방 입장
        // 캐릭터를 선택하지 않을 때 오류 메시지 
        if (playername != "")
            PhotonNetwork.JoinRoom(room[1]);
        else
            Debug.Log("캐릭터를 선택해 주세요");
    }

    public void JoinRoomC()
    {
        // C방 입장
        // 캐릭터를 선택하지 않을 때 오류 메시지 
        if (playername != "")
            PhotonNetwork.JoinRoom(room[2]);
        else
            Debug.Log("캐릭터를 선택해 주세요");
    }
    public void JoinRoomD()
    {
        // C방 입장
        // 캐릭터를 선택하지 않을 때 오류 메시지 
        if (playername != "")
            PhotonNetwork.JoinRoom(room[3]);
        else
            Debug.Log("캐릭터를 선택해 주세요");
    }

    //--------------------------------------------- 사용할지 말지 정하지 못한 함수 ( 입장하면서 방을 생성, 랜덤 방 입장 함수 ) 사용하지 않을 꺼 같음 ---------------------------------------------// 
    public void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(room[0], new RoomOptions { MaxPlayers = 20 }, null);
    }

    // 랜덤방 입장 기능  
    public void JoinRandomRoom() 
    { 
        PhotonNetwork.JoinRandomRoom(); 
    }

    //--------------------------------------------- 방 떠나기 함수 ---------------------------------------------// 
    public void LeaveRoom()
    {
        // 모든 팀의 정보를 가져와 방장이 나가면 방이 파괴되도록 만듬 
        lodingPanel.SetActive(true);
        checkRoom = false;

        UserCheck = false;
        StartCoroutine(AllTeam());

        // 방을 떠나면 Map도 비활성화 해준다. 
        // 프리팹을 파괴하여 비활성화를 한다 
        if (PV.IsMine) {
            PhotonNetwork.Destroy(web);
            PhotonNetwork.Destroy(keybo);
            PhotonNetwork.Destroy(map);
            PhotonNetwork.Destroy(player);
        }
    }

    IEnumerator AllTeam()
    {
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        // ID가 키값이므로 ID의 값만 가져온다. 
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(AllTeamData, form);
        yield return wwwData.SendWebRequest();

        // 반환값을 저장하고 공백을 제거하여 문자열을 저장 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        // 마지막 ,도 가져오게 되면 \n 값도 추가가 되기때문에 마지막 ,는 지워준다
        re = re.TrimEnd(',');
        Debug.Log(re);
        // 받아온 문자열을 ,를 기준으로 나누어 배열로 저장 
        string[] reSplit = re.Split(',');


        wwwData_2 = UnityWebRequest.Post(UserData, form);
        yield return wwwData_2.SendWebRequest();
        // 반환값을 저장하고 공백을 제거하여 문자열을 저장 
        string str_2 = wwwData_2.downloadHandler.text;
        string re_2 = string.Concat(str_2.Where(x => !char.IsWhiteSpace(x)));
        // 받아온 문자열을 ,를 기준으로 나누어 배열로 저장 
        string[] reSplit_2 = re_2.Split(',');
        // 배열에 문자열이 잘 들어갔는지 확인 
        // 결과 [0] : 닉네임 [1] : 1 또는 0 ( 1은 admin 0은 user ) 
        for (int i = 0; i < reSplit_2.Length; i++)
            Debug.Log(reSplit_2[i]);


        // 팀방과 공개방에서 방을 떠날 때 
        for (int i = 0; i < reSplit.Length; i += 2) {
            // 팀방에서 떠날때 그 팀의 팀장일 경우 
            if(PhotonNetwork.CurrentRoom.Name == reSplit[i] && reSplit[i + 1]=="1") {
                // hashtable를 사용하여 커스텀 프로퍼티를 만든다. 
                ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
                // isKicked 이라는 변수에 true 값을 추가 
                hashtable.Add("isKicked", true);
                Debug.Log(reSplit[i]);
                for (int j = 0; j < PhotonNetwork.PlayerList.Length; j++) {
                    // 방 인원 전체에게 isKicked에 true 값을 부여 
                    PhotonNetwork.PlayerList[j].SetCustomProperties(hashtable);
                    Debug.Log(PhotonNetwork.PlayerList[j]);
                }
                // 팀장은 방을 떠나기 
                PhotonNetwork.LeaveRoom();
            }
            // 팀방에서 떠날 때 그 팀의 팀원일 경우 
            // 그냥 Leave Room 만 동작하면 된다.
            else if (PhotonNetwork.CurrentRoom.Name == reSplit[i] && reSplit[i + 1]=="0") {
                if (PhotonNetwork.InRoom)
                    PhotonNetwork.LeaveRoom();
                else if (PhotonNetwork.InLobby)
                    yield return 0;
            }
            // 공개방에서 떠날때 그 공개방의 관리자일 경우 
            else if ((PhotonNetwork.CurrentRoom.Name == "A" || PhotonNetwork.CurrentRoom.Name == "B" || PhotonNetwork.CurrentRoom.Name == "C") && reSplit_2[1] == "1") {
                // hashtable를 사용하여 커스텀 프로퍼티를 만든다. 
                ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
                // isKicked 이라는 변수에 true 값을 추가 
                hashtable.Add("isKicked", true);
                for (int j = 0; j < PhotonNetwork.PlayerList.Length; j++) {
                    // 방 인원 전체에게 isKicked에 true 값을 부여 
                    PhotonNetwork.PlayerList[j].SetCustomProperties(hashtable);
                    Debug.Log(PhotonNetwork.PlayerList[j]);
                }
                // 관리자는 방을 떠나기
                PhotonNetwork.LeaveRoom();
            }
            // 공개방에서 떠날 때 그 공개방의 사용자일 경우 
            // 그냥 Leave Room 만 동작하면 된다. 
            else if((PhotonNetwork.CurrentRoom.Name == "A" || PhotonNetwork.CurrentRoom.Name == "B" || PhotonNetwork.CurrentRoom.Name == "C") && reSplit_2[1] == "0") {
                if (PhotonNetwork.InRoom)
                    PhotonNetwork.LeaveRoom();
                else if (PhotonNetwork.InLobby)
                    yield return 0;
            }
        }

        // 메모리 누수 방지 
        wwwData.Dispose();
    }

    //--------------------------------------------- 팀장 또는 관리자가 방을 떠날때 각 유저들에서 부여한 프로퍼티 삭제후 방 내보내기 ---------------------------------------------// 
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == PhotonNetwork.LocalPlayer) {
            // isKicked property가 존재할경우
            if (changedProps["isKicked"] != null) {
                // isKicked가 true인 경우
                if ((bool)changedProps["isKicked"]) {
                    // isKicked 프로퍼티를 삭제하고 방을 떠난다. 
                    string[] removeProperties = new string[1];
                    removeProperties[0] = "isKicked";
                    PhotonNetwork.RemovePlayerCustomProperties(removeProperties);

                    if (PhotonNetwork.InRoom)
                        PhotonNetwork.LeaveRoom();
                    else if (PhotonNetwork.InLobby)
                        return;
                }
            }
        }
    }

    //--------------------------------------------- 방 생성이 완료되었을 때 함수 ---------------------------------------------// 
    public override void OnCreatedRoom()
    {
        print("방만들기완료");
        Server.SetActive(false);                // server 관련 UI 비활성화 
        LobbyPanel.SetActive(false);            // 로비 UI 비활성화 
        createTeam.SetActive(false);            // 팀방 생성 UI 비활성화 
        inputTeam.SetActive(false);             // 팀방 입장 UI 비활성화 
        Lobby.SetActive(false);                 // 로비 비활성화 
        UserCheck = true;

        // 맵 관련 프리팹 생성 
        // mapname는 맵 버튼을 누를 때 발생하는 함수에서 값이 들어감 ( MapValue() 함수 ) 
        map = PhotonNetwork.Instantiate(mapname, new Vector3(0,0,20), Quaternion.identity);
        Quaternion rot = Quaternion.Euler(0, 270, 0);
        web = PhotonNetwork.Instantiate("WebViewPrefab", new Vector3(4.4f, -4f, -0.073f), rot);
        keybo = PhotonNetwork.Instantiate("Keyboard", new Vector3(4f, -5f, -0.073f), rot);
        OnWebviewCreate();

    }

    public void OnWebviewCreate()
    {
        WebViewRPC.Instance._Prefab = web;
        WebViewRPC.Instance.Initalize();
    }
    //--------------------------------------------- 맵 생성할때 맵의 이름을 저장하는 함수 ---------------------------------------------// 
    public void MapValue()
    {
        // 클릭시 발생하는 이벤트를 저장하는 변수 선언 후 Name를 저장 
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        string TeamName = clickObj.GetComponent<ButtonValues>().Name;

        // 맵 버튼이 가지고 있는 맵의 이름을 mapname 변수에 저장
        // 버튼만 만들면 맵이 몇개든 프리팹으로 가져올 수 있다.
        mapname = TeamName;
        Debug.Log(mapname);
    }


    //--------------------------------------------- 방 입장이 완료되었을 때 함수 ---------------------------------------------// 
    public override void OnJoinedRoom()
    {
        lodingPanel.SetActive(true);
        print("방참가완료");
        Server.SetActive(false);                // server UI 비활성화
        LobbyPanel.SetActive(false);            // 로비 Panel 비활성화 
        createTeam.SetActive(false);            // 팀방 생성 UI 비활성화 
        inputTeam.SetActive(false);             // 팀방 입장 UI 비활성화 
        Lobby.SetActive(false);                 // 로비 비활성화 

        checkRoom = true;
        //RoomPanel.SetActive(true);              // 방 Panel 활

        // 프리팹 생성 및 플레이어 동기화 
        // 캐릭터 이름을 받아서 캐릭터 생성 
        SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
        smoothFollow.roteta();
        player = PhotonNetwork.Instantiate(playername, Vector3.zero, Quaternion.identity);

        // 현재 방의 정보를 text로 표시 
        // 최대 몇명까지 현재 몇명이 있는지
        RoomRenewal();

        ChatInput.text = "";                // 채팅 입력 창을 초기화 

        // 모든 채팅기록을 초기화 
        for (int i = 0; i < ChatText.Length; i++) 
            ChatText[i].text = "";

    }

    //--------------------------------------------- 캐릭터를 선택할 때 캐릭터의 이름을 저장하는 함수 ---------------------------------------------// 
    public void PlayerNameValue()
    {
        // 클릭시 발생하는 이벤트를 저장하는 변수 선언 후 Name를 저장 
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        string TeamName = clickObj.GetComponent<ButtonValues>().Name;

        // 캐릭터 버튼이 가지고 있는 캐릭터의 이름을 playername 변수에 저장
        // 버튼만 만들면 캐릭터가 몇개든 프리팹으로 가져올 수 있다.
        playername = TeamName;
        Debug.Log(playername);
    }

    //--------------------------------------------- 방 만들기가 실패하였을 때 ---------------------------------------------// 
    public override void OnCreateRoomFailed( short returnCode, string message )
    {
        print("방만들기실패");
    }
    //--------------------------------------------- 방이 만들어져 있지 않아 방 참가에 실패하였을 때 ---------------------------------------------// 
    public override void OnJoinRoomFailed( short returnCode, string message )
    {
        print("방참가실패");
    }

    //--------------------------------------------- 사용하지 않는 기능의 오류... ---------------------------------------------// 
    public override void OnJoinRandomFailed( short returnCode, string message )
    {
        print("방랜덤참가실패");

    }
    //----------------------------------------------------------------------------------------------------------------------// 

    [ContextMenu("정보")]
    // 현재 방의 정보 
    void Info()
    {
        if (PhotonNetwork.InRoom) {
            print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            print("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else {
            print("접속한 인원 수 : " + PhotonNetwork.CountOfPlayers);
            print("방 개수 : " + PhotonNetwork.CountOfRooms);
            print("모든 방에 있는 인원 수 : " + PhotonNetwork.CountOfPlayersInRooms);
            print("로비에 있는지? : " + PhotonNetwork.InLobby);
            print("연결됐는지? : " + PhotonNetwork.IsConnected);
        }
    }


    //--------------------------------------------- 방리스트 갱신 함수 ---------------------------------------------// 
    #region 방리스트 갱신
    // ◀버튼 -2 , ▶버튼 -1 , 셀 숫자
    public void MyListClick( int num )
    {
        if (num == -2)
            --currentPage;
        else if (num == -1)
            ++currentPage;
        else {
            if (myList[multiple + num].Name == "A") {
                PhotonNetwork.JoinRoom(myList[multiple + num].Name);               
            }
            else if (myList[multiple + num].Name == "B") {
                PhotonNetwork.JoinRoom(myList[multiple + num].Name);
            }
            else if (myList[multiple + num].Name == "C") {
                PhotonNetwork.JoinRoom(myList[multiple + num].Name);
            }
        }

        MyListRenewal();
    }

    void MyListRenewal()
    {
        // 최대페이지
        // 만약 방이 9개라면 페이지는 3개가 만들어져야 한다.
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // 이전, 다음버튼
        // 처음 페이지이면 이전 버튼이 비활성화 
        // 마지막 페이지면 다음 버튼이 비활성화 
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // 페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++) {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < myList.Count) ? (myList[multiple + i].PlayerCount - 1) + "/" + (myList[multiple + i].MaxPlayers - 1) : "";
        }
    }

    // 룸 리스트를 다음 페이지로 이동 시키고 그 정보를 가져옴 
    // 리스트가 갱신이 됨 ( 누군가 나가면 방이 생성되고, 나가면 방이 없어지는 ) 
    public override void OnRoomListUpdate( List<RoomInfo> roomList )
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++) {
            if (!roomList[i].RemovedFromList) {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }

    //------------------------------------------- 채팅 시스템 -------------------------------------------//
    public void ChatEnd()
    {
        ChatSystem.SetActive(false);
    }

    public override void OnPlayerEnteredRoom( Player newPlayer )
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "님이 참가하셨습니다</color>");
    }

    public override void OnPlayerLeftRoom( Player otherPlayer )
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
    }

    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");

        // Room안에서 현재방 이름, 인원수, 최대인원수를 표시 
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + (PhotonNetwork.CurrentRoom.PlayerCount) + "명 / " + (PhotonNetwork.CurrentRoom.MaxPlayers) + "최대";
    }
    #endregion


    #region 채팅
    public void Send()
    {
        // PV.RPC 사용법 
        // PV.RPC ( RPC 함수 이름, RpcTarget. 보낼 사람, 매개변수( 값을 넘겨줌 ) )
        // 보낼 사람을 All로 할 시 방에 있는 모든인원이 볼 수 있음 
        if (!(ChatInput.text == "")) {
            PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text);
            StartCoroutine(ChatLog());
        }
        ChatInput.text = "";
    }
    IEnumerator ChatLog()
    {
        WWWForm form = new WWWForm();
        // WWWform에 id, pw, nick 필드를 생성하고 
        // 내가 inputField에 입력한 아이디, 비밀번호, 닉네임을 추가한다.
        form.AddField("chat_log", ChatInput.text);
        form.AddField("team_name", PhotonNetwork.CurrentRoom.Name);
        form.AddField("nick", PhotonNetwork.NickName);
        Debug.Log(ChatInput.text + PhotonNetwork.CurrentRoom.Name + PhotonNetwork.NickName);
        

        // Post 방식으로 로그인URL에 WWWform필드 값을 wwwData 변수에 저장  
        wwwData = UnityWebRequest.Post(ChatLogData, form);
        yield return wwwData.SendWebRequest();

        // 백쪽의 데이터를 받아서 문자열 형태로 저장하고 
        // 공백을 지움으로써 유니티에서 사용할 수 있는 데이터로 가공 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        Debug.Log(re);

        // 메모리 누수를 방지 하기 위한 메모리 관리 
        wwwData.Dispose();
    }


    // RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
    // 매우 중요한 함수 RPC
    [PunRPC] 
    void ChatRPC( string msg )
    {
        bool isInput = false;
        
        for (int i = 0; i < ChatText.Length; i++)
            // 채팅의 text중에서 비어있는 구간을 찾는다.
            // 비어있는 구간이 있으면 매개변수 값을 넘겨준다 
            if (ChatText[i].text == "") {
                isInput = true;             // 채팅 즉 text가 들어갔다면 true로 확인 
                ChatText[i].text = msg;
                break;
            }

        // 만약 비어있는 공간이 없어서 text가 들어가지 못하였다면 
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatText.Length; i++) 
                ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion
}