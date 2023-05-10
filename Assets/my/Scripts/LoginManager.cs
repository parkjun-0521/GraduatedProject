using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Linq;
using StarterAssets;

public class LoginManager : MonoBehaviour
{
    // 기본 변수 선언 
    [Header("LoginPanel")]
    public InputField IDInputField;             // 아이디 입력 inputField 
    public InputField PWInputField;             // 비밀번호 입력 inputField 
    public GameObject LoginPanelObj;            // 로그인 Panel 오브젝트 
    [Header("CreateAccountPanel")]
    public InputField NewIDInputField;          // 계정 생성 아이디 입력 inputField 
    public InputField NewPWInputField;          // 계정 생성 비밀번호 입력 inputField
    public InputField NickNameInputField;       // 계정 생성 닉네임 입력 inputField 
    public GameObject CreateAccountPanelObj;    // 계정 생성 Panel 오브젝트 

    // 관리자 
    [Header("Admin")]
    public GameObject ConnectServerObj;         // 관리자 서버 접속 오브젝트

    // 사용자
    [Header("User")]
    public GameObject UserRoomServerObj;        // 사용자 서버 접속 오브젝트 

    // 공용 Panel
    [Header("Public")]
    public GameObject Server;                   // 닉네임 + 방 이름 InputField ( 팀 방입장에서 InputField 사용할 예정 )
    public GameObject LobbyPanelObj;            // 서버 접속시 띄울 로비 페이지 ( 관리자 : 방 생성, 사용자 : 방 참가 ) 
    public GameObject RoomPanelObj;             // 로비에서 방 접속시 띄울 방 오브젝트 ( 채팅 + 맵을 띄워줌 )
    public GameObject createTeam;
    public GameObject inputTeam;
    private Transform tr;

    [Header("Lobby")]
    public GameObject LobbyRoom;
    public GameObject LobbyMainPlayer;
    public GameObject publicPortal;
    public GameObject CreateTeamPortal;
    public GameObject InputTeamPortal;
    
    // DB관련 변수 
    [Header("Database")]
    UnityWebRequest wwwData;                    // 데이터베이스로 정보를 관리하기 위해 선언한 변수
                                                // 
    [Header("Url")]
    public string LoginUrl;                     // 백서버와 통신할 로그인 URL
    public string CreateUrl;                    // 백서버와 통신할 계정생성 URL
    // Use this for initialization

    public GameObject loginErrorMessage;
    public RectTransform rectLogin;
    //--------------------------------------------- URL 초기화 ---------------------------------------------//
    void Start()
    {
        // 버튼을 눌렀을 때 이동할 URL (인텔리제이에서 jsp 파일을 실행시켰을 때의 URL을 입력)
        // 로그인 URL
        LoginUrl = "http://223.131.75.181:1356//Metaverse_war_exploded/Login.jsp";
        // 계정생성 URL 
        CreateUrl = "http://223.131.75.181:1356/Metaverse_war_exploded/NewUserCreate.jsp";

        rectLogin = loginErrorMessage.GetComponent<RectTransform>();

    }

    //--------------------------------------------- 로그인 버튼 ---------------------------------------------// 
    public void LoginButton()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        // 내가 입력한 아이디 비밀번호를 console에출력 
        Debug.Log(IDInputField.text);
        Debug.Log(PWInputField.text);

        // 빈 WWWForm 객체를 만듬 
        // 웹 과의 통신을 위한 form 객체 선언 
        WWWForm form = new WWWForm();
        // WWWform에 id, pw 필드를 생성하고 
        // 내가 inputField에 입력한 아이디, 비밀번호를 추가한다.
        form.AddField("id", IDInputField.text);
        form.AddField("pw", PWInputField.text);

        // Post 방식으로 로그인URL에 WWWform필드 값을 wwwData 변수에 저장  
        wwwData = UnityWebRequest.Post(LoginUrl, form);
        // wwwData의 값을 웹으로 전송 후 전달 받은 값을 리턴 
        yield return wwwData.SendWebRequest();              

        // 백(웹 쪽을 말함)에서 전송한 데이터를 문자열 형태로 전송
        // 백쪽에서도 문자열 형태로 데이터를 전송해 주어야 함 
        string str = wwwData.downloadHandler.text;
        // 받아온 문자열의 앞뒤 공백을 다 지움
        // jsp로 받아오니깐 위에 줄바꿈 3줄이 생겨서 데이터가 제대로 받아지지 않는 문제가 발생, 따라서 문자열의 공백을 지움으로써 오류 해결 
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        // 백쪽 데이터에 공백이 잘 지워져서 출력이 되는지 확인 
        Debug.Log(re);

        // 백에서의 값으로 로그인 여부를 판단 
        // 관리자, 사용자, 로그인 실패를 판단 
        if (re == "1") {
            Debug.Log("로그인 성공");
            // 로그인 성공시 ( 관리자 계정이다 ) 
            // 로그인 Panel은 비활성화, 관리자 서버 연결 Panel은 활성화 
            LoginPanelObj.SetActive(false);
           // ConnectServerObj.SetActive(true);
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            networkManager.Connect();
            networkManager.LoginlodingPanel.SetActive(true);
        }
        else if (re == "0") {
            Debug.Log("로그인 성공");
            // 로그인 성공시 ( 사용자 계정이다 ) 
            // 로그인 Panel은 비활성화, 사용자 서버 연결 Panel은 활성화 
            LoginPanelObj.SetActive(false);
            //UserRoomServerObj.SetActive(true);
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            networkManager.Connect();
            networkManager.LoginlodingPanel.SetActive(true);
        }
        else {
            rectLogin.anchoredPosition = new Vector2(0,0);
            Debug.Log("로그인 실패");
        }
        // 메모리 누수 방지를 위한 메모리 관리 
        wwwData.Dispose();
    }

    //--------------------------------------------- 로그인 화면의 계정생성 버튼 ---------------------------------------------// 
    public void OpenCreateAccountButton()
    {
        // 로그인 Panel 비활성화, 계정생성 Panel 활성화 
        LoginPanelObj.SetActive(false);
        CreateAccountPanelObj.SetActive(true);
    }
    
    //--------------------------------------------- 계정생성 페이지의 계정생성 버튼 ---------------------------------------------// 
    public void CreateAccountButton()
    {
        StartCoroutine(CreateUser());
    }

    // 로그인과 유사한 방식으로 웹과 통신한다  
    IEnumerator CreateUser()
    {
        WWWForm form = new WWWForm();
        // WWWform에 id, pw, nick 필드를 생성하고 
        // 내가 inputField에 입력한 아이디, 비밀번호, 닉네임을 추가한다.
        form.AddField("id", NewIDInputField.text);
        form.AddField("pwd", NewPWInputField.text);
        form.AddField("nick", NickNameInputField.text);

        // Post 방식으로 로그인URL에 WWWform필드 값을 wwwData 변수에 저장  
        wwwData = UnityWebRequest.Post(CreateUrl, form);
        yield return wwwData.SendWebRequest();
        Debug.Log(wwwData.downloadHandler.text);

        // 백쪽의 데이터를 받아서 문자열 형태로 저장하고 
        // 공백을 지움으로써 유니티에서 사용할 수 있는 데이터로 가공 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        Debug.Log(re);

        // 계정 생성이 완료가 되면 로그인 페이지로 돌아가는 함수 
        Back();

        // 메모리 누수를 방지 하기 위한 메모리 관리 
        wwwData.Dispose();
    }
    // 계정 생성이 완료가 되면 로그인 페이지로 돌아가는 함수 
    public void Back()
    {
        // 로그인 Panel 활성화, 계정생성 Panel 비활성화 
        LoginPanelObj.SetActive(true);
        CreateAccountPanelObj.SetActive(false);
    }

    //--------------------------------------------- 서버접속 후 Room에서 로비로 이동하는 버튼 ---------------------------------------------// 
    public void BackLobby()
    {
        // Room은 비활성화, Lobby 활성화 
        RoomPanelObj.SetActive(false);
        createTeam.SetActive(false);
        inputTeam.SetActive(false);
        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();
        buttonEvent.Women();
        for (int i = 0; i < buttonEvent.Women_Men.Length; i++)
            buttonEvent.Women_Men[i].SetActive(true);
        buttonEvent.PublicCreateRoomPrevious();
        buttonEvent.RoomCreate_Input.SetActive(false);
        //buttonEvent.LobbyPanel_NextButton.SetActive(true);
        buttonEvent.LobbyPanel_PreviousButton.SetActive(false);

        buttonEvent.TeamInput_Women();
        for (int i = 0; i < buttonEvent.TeamInput_Women_Men.Length; i++)
            buttonEvent.TeamInput_Women_Men[i].SetActive(true);
        for (int i = 0; i < buttonEvent.TeamInput_NextButton.Length; i++)
            buttonEvent.TeamInput_NextButton[i].SetActive(true);
        buttonEvent.TeamInput_PreviousButton.SetActive(false);
        buttonEvent.TeamInputNext.SetActive(false);
        buttonEvent.TeamInputPrevious.SetActive(false);
        buttonEvent.playerBackGround.SetActive(true);

        ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        thirdPersonController.MoveSpeed = 50f;
        thirdPersonController.SprintSpeed = 80f;
    }
}
