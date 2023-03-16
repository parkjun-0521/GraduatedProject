using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// Photon�� ����ϱ� ����
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor;
using System.Linq;
using Photon.Pun.Demo.Cockpit;
// ��ü������ Photon�� ��Ģ�� �ֱ� ���� 
using Hashtable = ExitGames.Client.Photon.Hashtable;
using StarterAssets;
using Vuplex.WebView;

// ���� ������ Ȱ���� ��Ƽ �ý��� ���� 
public class NetworkManager : MonoBehaviourPunCallbacks,IPunInstantiateMagicCallback
{
    // User�� �����͸� �������� ���� DB�� ��� 
    [Header("Url")]
    public string UserData;                 // ������� ������ URL ( �г��Ӱ� ������ ���θ� �޾ƿ´� ) 
    public string LogoutURL;                // ����� �α׾ƿ� URL ( �α׾ƿ� �� login_state = 0 ���� ���� ) 
    public string LoginUrl;                 // ����� �α��� URL ( ������ �α��� �� login_state = 1 �� ���� �Ͽ��µ� ������� ������ ���� ) 
    public string TeamData;                 // ������ URL ( �����̺��� ���� �̸��� �޾ƿ´� ) 
    public string UserTeamData;             // ����� ����ü ���� URL ( ������� �� �̸��� �޾ƿ´� ) 
    public string AllTeamData;              // ��� ���� ���� URL ( ���� ���� ��� ������� �����͸� ������ ) 
    public string ChatLogData;              // ä�� �α׸� �ѱ�� URL 
    UnityWebRequest wwwData;                // ��û�� ���� �޾Ƽ� �����ϴ� ���� 
    UnityWebRequest wwwData_2;              // ��û�� ���� �޾Ƽ� �����ϴ� ���� 

    [Header("Text")]
    public Text NickNameText = null;        // �г����� �޾ƿ������� Text
    public Text StatusText;                 // ���� ������ ���� ���¸� Ȯ���ϱ� ���� Text

    // ���� Panel
    [Header("Public")]
    public GameObject Server;               // �г���, ����, �ο�, �α׾ƿ� ���� UI 
    public GameObject LoginPanelObj;        // �α��� UI Panel ������Ʈ 
    public GameObject LobbyPanelObj;        // �κ� UI Panel ������Ʈ 

    // ������ 
    [Header("Admin")]
    public GameObject ConnectServerObj;     // ������ ���� ���� ������Ʈ ( ����ڿ� �����ϱ� ���� ���� ) 

    // �����
    [Header("User")]
    public GameObject UserConnectServerObj; // ����� ���� ���� ������Ʈ ( �����ڿ� �����ϱ� ���� ���� ) 

    [Header("AdminCheck")]
    public bool adminCheck = false;

    // ��ư 
    [Header("CreateRoomButton")]
    public GameObject[] createRoom;          // �����ڰ� �����ϴ� ������ A 
    public GameObject[] Create_Next_Previous;

    [Header("InputRoomButton")]
    public GameObject[] inputRoom;           // ����ڰ� �����ϴ� ������ A 
    public GameObject[] Input_Next_Previous;

    [Header("CreateTeamRoom")]
    public GameObject createTeam;           // ������ ���� �� UI Panel
    public GameObject inputTeam;            // ������ ������ �� UI Panel
    public GameObject[] createTeamRoom;     // ���� ���� ��ư �迭 ( �� �������� �ִ� 10���� ��� ) 
    public GameObject[] inputTeamRoom;      // �濡 �� ��ư �迭 ( �� �������� �ִ� 10���� ��� ) 

    [Header("Test Map")]
    public GameObject map;                  // �� ������ ( �� �ʿ� �̸��� ���� ���ָ� �� ) 
    public GameObject player;               // �÷��̾� ������ ( �� ĳ���Ϳ� �̸��� ���� ���ָ� �� ) 
    public GameObject web;                  // 
    public GameObject keybo;                  // 
    public string playername;               // ��ư�� ������ �� �� ��ư�� Name ���� �����ϱ� ���� ���� ( ĳ���� �̸��� ���� ) 
    public string mapname;                  // ��ư�� ������ �� �� ��ư�� Namw ���� �����ϱ� ���� ���� ( �� �̸��� ���� ) 

    [Header("Lobby")]
    public GameObject Lobby;
    public GameObject LobbyMainPlayer;      // �κ��� Player ������Ʈ 
    //public GameObject webView;              // �κ��� webView ������Ʈ 


    [Header("-----�ǽð� ä�� �ý��� ����-----")]
    [Header("LobbyPanel")]
    public GameObject LobbyPanel;           // �κ� Panel
    public Text WelcomeText;                // �г��� + ȯ���մϴ� Text
    public Text LobbyInfoText;              // ���� �κ��� �ο��� Text
    public Button[] CellBtn;                // ���� ã�� ���� n���� �� ��ư 
    public Button PreviousBtn;              // �� ����Ʈ ������ ���� ��ư 
    public Button NextBtn;                  // �� ����Ʈ �ڷ� ���� ��ư 

    [Header("RoomPanel")]
    public GameObject ChatSystem;           // ä�� UI ������Ʈ 
    public GameObject RoomPanel;            // ä�� + ���� �ִ� Room ������Ʈ 
    public Text ListText;                   // ���� ������ �ִ� ������ �̸� ǥ�� 
    public Text RoomInfoText;               // ���̸�, ���� �ο�, �ִ� �ο� ǥ�� 
    public Text[] ChatText;                 // ä��â�� ���� ���ٷ� ������ �� TextField�� ������ ���� 
    public InputField ChatInput;            // ä���� �Է��� InputField 
    public bool chatCheck = false;

    [Header("ETC")]
    public PhotonView PV;                   // ���� �ڱ� �ڽ��� PhotonView�� ���� 

    List<RoomInfo> myList = new List<RoomInfo>();       // �� ����Ʈ ������ ���� List Ÿ�� ���� 
    int currentPage = 1, maxPage, multiple;             // �� ����Ʈ �ѱ�� ��ư ���� ���� ( ���� ������, �ִ� ������, ���������� �ִ��� ) 

    // ���� ���� �� ���� Ǯ�� ���� ����
    public int curPage = 0;                 // ���� ������ ���� ����
    public int endPage = 0;                 // ������ ������ ���� ���� 
    public int pageCount = 1;               // ������ �Ѿ ���� ���� 

    public bool UserCheck = false;

    public static NetworkManager Instance;

    public GameObject lodingPanel;

    public bool checkRoom = false;
    //--------------------------------------------- ���� ȭ�� ũ�� ---------------------------------------------// 
    void Awake()
    {
        // �������� �� ȭ�� ���� 
        Screen.SetResolution(362, 530, false);
    }

    //--------------------------------------------- �� ���� URL ---------------------------------------------//  
    void Start()
    {
        // ��ư�� ������ �� �̵��� URL 
        // (���ڸ����̿��� jsp ������ ��������� ���� URL�� �Է�)
        // ��Ʈ�������� �̿��Ͽ� �ܺο����� ������ �� �ֵ��� �� 
        LoginUrl = "http://223.131.75.181:1356/Metaverse_war_exploded/Login.jsp";
        UserData = "http://223.131.75.181:1356/Metaverse_war_exploded/UserData.jsp";
        LogoutURL = "http://223.131.75.181:1356/Metaverse_war_exploded/Logout.jsp";

        // �� ������ �������� ���� URL
        TeamData = "http://223.131.75.181:1356/Metaverse_war_exploded/TeamData.jsp";
        UserTeamData = "http://223.131.75.181:1356/Metaverse_war_exploded/UserTeamData.jsp";
        AllTeamData = "http://223.131.75.181:1356/Metaverse_war_exploded/TeamAllData.jsp";

        // ä�÷α� ��������
        ChatLogData = "http://223.131.75.181:1356/Metaverse_war_exploded/ChatLog.jsp";
    }

    //--------------------------------------------- ������� ����, �κ��� �ο�, ä��â ���� ���� ---------------------------------------------// 
    void Update() 
    {
        // ���¸� ��Ÿ���� text
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();

        // Lobby�� ����� �ִ��� Ȯ���ϴ� text
        // ��üPlayer �߿��� Room�� �� �ִ� ����� ���� �ϰ� �κ� ����� �ִ��� Ȯ�� 
        LobbyInfoText.text = "<color=#0000ff>" + (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms)  + "�κ� / " + PhotonNetwork.CountOfPlayers + "����" + "</color>";

        // Enter�� ������ �� ä��â�� �Է��� �� �ְ� �ϱ� ���� ���� 
        // Enter�� ������ �濡 ���� �Ͽ��� �� ĳ���͸� ���߰� ä���� ĥ �� �ְ� �� 
        if (Input.GetButtonDown("ChatStart") && RoomPanel.activeSelf == true) {
            Debug.Log("enter ����");
            // Player �±��� ������Ʈ�� �ӵ��� ���̰� ȸ���� ���� 
            ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            thirdPersonController.MoveSpeed = 0.0f;
            thirdPersonController.SprintSpeed = 0.0f;
            thirdPersonController.turnStop = true;
            thirdPersonController.JumpCheck = true;

            SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
            smoothFollow.turnOff = true;

            chatCheck = true;
            // ä��â UI�� Ȱ��ȭ 
            ChatSystem.SetActive(true);
            // ä��â�� InputField�� Ȱ��ȭ �Ͽ� �ٷ� ä���� ĥ �� �ֵ��� �� 
            ChatInput.ActivateInputField();
            // ������ �Լ� 
            Send();
        }
        // �ٽ� �����̰� ������ �濡�� ���콺�� Ŭ���ϸ� ������ �� �ֵ��� �� 
        else if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && RoomPanel.activeSelf == true && chatCheck) {
            // �ӵ��� 50 �ְ� ȸ���� Ǯ�� 
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

    //--------------------------------------------- ���� ���� �Լ�  ---------------------------------------------//  
    public void Connect()
    {
        // ��Ʈ��ũ�� ������ �Ǿ��� �� 
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // ���� ������ ���������� �Ǿ��� �� 
        print("�������ӿϷ�");
        NickNameText.text = null;
        // ������� �����͸� �����´� 
        StartCoroutine(UserNick());
        Screen.SetResolution(1920, 1080, true);

        lodingPanel.SetActive(true);
        // �κ� ĳ������ ��ġ�� 0, 3, 0���� �ʱ�ȭ
        LobbyMainPlayer.transform.position = new Vector3(0f, 1f, -9f);

        // ���������� ������ �� �� �ٷ� �κ�� �̵� 
        PhotonNetwork.JoinLobby();
    }

    //--------------------------------------------- Lobby ���� �Լ�  ---------------------------------------------// 
    public void JoinLobby()
    {
        // �κ� ���� 
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("�κ����ӿϷ�");
       
        // �� ������ �����ϰ� ������ �г����� �����´� 
        //StartCoroutine(UserNick());
        RoomPanel.SetActive(false);             // Room Panel ��Ȱ��ȭ 
        Lobby.SetActive(true);                  // �κ� Floor Ȱ��ȭ 
        //webView.SetActive(true);


        // player ������Ʈ�� ã�Ƽ� �� ���� Start �Լ��� ���� 
        // ī�޶� �濡�� ������ �� �κ� ĳ���͸� ��� ����� ���ؼ� Start �Լ��� ���� �ϴ� �� �̴�. 
        LobbyPlayer lobbyPlayer = GameObject.Find("Female1").GetComponent<LobbyPlayer>();
        lobbyPlayer.Start();

        SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
        smoothFollow.roteta();

        ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();
        buttonEvent.OptionClose();
        buttonEvent.SoundOptionClose();


        // �濡�� ������ �κ�� ���ƿ����� 
        // ����Ʈ�� �ʱ�ȭ�� ����� ���װ� �߻����� �ʴ´�. 
        // �̷��� �ؾ� �߰��� ����� ������ �ʴ´�. 
        myList.Clear();
    }


    public void OnPhotonInstantiate(PhotonMessageInfo info)    
    {
        //if (info.photonView.gameObject.name==("WebViewPrefab")) {
        //    WebViewRPC.Instance._Prefab = web;
        //    WebViewRPC.Instance.Initalize();
        //}
       
    }

    //--------------------------------------------- NickName�� �������� �ڷ�ƾ ---------------------------------------------// 
    IEnumerator UserNick()
    {
        // �ܺ� ��ũ��Ʈ�� ������������ Find�� ��� 
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        // ID�� Ű���̹Ƿ� ID�� ���� �����´�. 
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(UserData, form);
        yield return wwwData.SendWebRequest();

        // ��ȯ���� �����ϰ� ������ �����Ͽ� ���ڿ��� ���� 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        // �޾ƿ� ���ڿ��� ,�� �������� ������ �迭�� ���� 
        string[] reSplit = re.Split(',');
        // �迭�� ���ڿ��� �� ������ Ȯ�� 
        // ��� [0] : �г��� [1] : 1 �Ǵ� 0 ( 1�� admin 0�� user ) 
        for (int i = 0; i < reSplit.Length; i++)
            Debug.Log(reSplit[i]);

        // �г����� �޾ƿͼ� ������ ���� 
        NickNameText.text = reSplit[0];
        PhotonNetwork.LocalPlayer.NickName = reSplit[0];
        

        // �κ� Panel���� ���ӽ� �����Ǵ� ���� 
        WelcomeText.text = "<color=#0000ff>" + PhotonNetwork.LocalPlayer.NickName + "</color>" + "�� ȯ���մϴ�";
        

        // �޾ƿ� ���� admin ������ �� 
        // �����ڿ� ������� ȭ���� �����Ͽ� ���� ��ư Ȱ��ȭ�� ���� �ٸ��� �� 
        // ������ ���� ��ư�� ���� ( ���� �����ڵ� �����濡 �����ϱ� ��ư�� Ȱ��ȭ �� ���� ) 
        if (reSplit[1] == "1") {
            adminCheck = true;
            ConnectServerObj.SetActive(false);          // ������ ���� ���� ȭ�� ��Ȱ��ȭ 
            //Server.SetActive(true);                     // �г����� ������ �ִ� Panel Ȱ��ȭ 
            createTeam.SetActive(false);                // ���� ����� ��ư ��Ȱ��ȭ 
            inputTeam.SetActive(false);                 // ���� �����ϱ� ��ư ��Ȱ��ȭ 
            for (int i = 0; i < createRoom.Length; i++) {
                inputRoom[i].SetActive(false);
                createRoom[i].SetActive(true);
            }
            for (int i = 0; i < Create_Next_Previous.Length; i++) {
                Create_Next_Previous[i].SetActive(true);
                Input_Next_Previous[i].SetActive(false);
            }
        }
        // �޾ƿ� ���� user ������ �� 
        // ������ ���� ��ư�� ���� 
        else if (reSplit[1] == "0" ) {
            adminCheck = false;
            UserConnectServerObj.SetActive(false);      // ����� ���� ���� ȭ�� ��Ȱ��ȭ       
            //Server.SetActive(true);                     // �г����� ������ �ִ� Panel Ȱ��ȭ 
            createTeam.SetActive(false);                // ���� ����� ��ư ��Ȱ��ȭ 
            inputTeam.SetActive(false);                 // ���� �����ϱ� ��ư ��Ȱ��ȭ 
            for (int i = 0; i < createRoom.Length; i++) {
                inputRoom[i].SetActive(true);
                createRoom[i].SetActive(false);
            }
            for (int i = 0; i < Create_Next_Previous.Length; i++) {
                Create_Next_Previous[i].SetActive(false);
                Input_Next_Previous[i].SetActive(true);
            }
        }
      
        // �޸� ���� ������ ���� 
        wwwData.Dispose();
    }

    //--------------------------------------------- ���� ������ ���� �� �Լ� ---------------------------------------------// 
    public void Disconnect()
    {
        // ������ ���� �� ( �α׾ƿ��� �� �� )
        StartCoroutine(Logout());                       

        LoginPanelObj.SetActive(true);                  // �α��� Panel Ȱ��ȭ 
        ConnectServerObj.SetActive(false);              // ������ ���� ���� Panel ��Ȱ��ȭ 
        UserConnectServerObj.SetActive(false);          // ����� ���� ���� Panel ��Ȱ��ȭ
        LobbyPanelObj.SetActive(false);                 // �κ� Panel ��Ȱ��ȭ 
        Server.SetActive(false);                        // �г��� Panel ��Ȱ��ȭ 
        Lobby.SetActive(false);
        //webView.SetActive(false);
        NickNameText.text = null;                       // �г���â �ʱ�ȭ 
        UserCheck = false;

        Screen.SetResolution(362, 530, false);
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        loginManager.IDInputField.text = "";
        loginManager.PWInputField.text = "";
        // ������ ���漭���� ��Ʈ��ũ�� ���� 
        PhotonNetwork.Disconnect();                 
    }

    public override void OnDisconnected( DisconnectCause cause )
    {
        // Disconnect() �Լ��� ����� �� 
        print("�������");
        // �κ� Panel�� �� Panel�� ��Ȱ��ȭ 
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
    }

    //--------------------------------------------- Logout ���� �������� �ڷ�ƾ ---------------------------------------------// 
    // �α��� ���¸� 0���� ����� �ִ� LogOut �鼭���� �ҷ��� 
    IEnumerator Logout()
    {
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(LogoutURL, form);
        yield return wwwData.SendWebRequest();
        
        // ��ư�� ���� ���� �ڷ�ƾ ( ���� �����ϱ�, ���� �����ϱ� ��ư ) 
        StartCoroutine(TeamButtonOff());     
    }
    IEnumerator TeamButtonOff()
    {
        //------------------------------------- ���� ����� ��ư ���� -------------------------------------//
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(TeamData, form);
        yield return wwwData.SendWebRequest();

        // ��ȯ���� �����ϰ� ������ �����Ͽ� ���ڿ��� ���� 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        // ������ ,�� �������� �Ǹ� \n ���� �߰��� �Ǳ⶧���� ������ ,�� �����ش�
        re = re.TrimEnd(',');
        Debug.Log(re);
        string[] reSplit = re.Split(',');

        // �迭�� �Ѿ�� ������ ������� ���� 
        // ������ ��ư�� ������ 10������ ������ ��ü 10���� ��ư�� ��Ȱ��ȭ 
        if(reSplit.Length > 10) {
            for (int i = 0; i < 10; i++) 
                createTeamRoom[i].SetActive(false);
        }
        // ������ ��ư�� ������ 10�� ���� ������ ������ ��ŭ�� ��Ȱ��ȭ 
        else {
            for (int i = 0; i < reSplit.Length; i++) 
                createTeamRoom[i].SetActive(false);
        }

        //--------------------------------------------- ���� �����ϱ� ��ư ���� ---------------------------------------------// 
        wwwData_2 = UnityWebRequest.Post(UserTeamData, form);
        yield return wwwData_2.SendWebRequest();

        // ��ȯ���� �����ϰ� ������ �����Ͽ� ���ڿ��� ���� 
        string str_2 = wwwData.downloadHandler.text;
        string re_2 = string.Concat(str_2.Where(x => !char.IsWhiteSpace(x)));
        // ������ ,�� �������� �Ǹ� \n ���� �߰��� �Ǳ⶧���� ������ ,�� �����ش�
        re_2 = re_2.TrimEnd(',');
        Debug.Log(re_2);
        string[] reSplit_2 = re_2.Split(',');

        // �迭�� �Ѿ�� ������ ������� ���� 
        // ������ ��ư�� ������ 10������ ������ ��ü 10���� ��ư�� ��Ȱ��ȭ
        if (reSplit_2.Length > 10) {
            for (int i = 0; i < 10; i++) 
                inputTeamRoom[i].SetActive(false);
        }
        // ������ ��ư�� ������ 10�� ���� ������ ������ ��ŭ�� ��Ȱ��ȭ 
        else {
            for (int i = 0; i < reSplit_2.Length; i++)
                inputTeamRoom[i].SetActive(false);
        }
        wwwData.Dispose();
    }

    //--------------------------------------------- ���� ����� ��ư�� ������ ���ֱ� ���� �Լ� ---------------------------------------------// 
    // ����ư -2 , ����ư -1
    public void TeamListClick(int num)
    {
        // ���� ��ư�� ������ �� 
        if (num == -2) {
            // ��ü������ ��ư�� �� ��Ȱ��ȭ 
            for (int i = 0; i < 10; i++)
                createTeamRoom[i].SetActive(false);
            // ���� �ٽ� Ȱ��ȭ
            CreateTeam();
            // ���� �������� 0���� ũ�� ���� �������� �Ѿ�ٴ� �� 
            // ���� �������� ���ƿ��� �ϱ� ���ؼ� Count�� ���̰� �迭�� ���� 10 ����
            // ��ư�� Ŭ���� reSplit�� �迭 ���� 10 ���� ��Ŵ 
            if (curPage > 0) {
                pageCount--;
                curPage -= 10;
            }
            // ���� �������� 0���� �۰ų� ������ �������� �ѱ� ���� ���ٴ� ���̹Ƿ� 
            // ���� 0���� �ʱ�ȭ �Ͽ� �迭�� �ʰ��ϴ� ������ ���´�. 
            else if (curPage <= 0)
                curPage = 0;
        }
        // ���� ��ư�� ������ �� 
        else if (num == -1) {
            // ��ü ��ư�� ��Ȱ��ȭ �� �ٽ� Ȱ��ȭ 
            for (int i = 0; i < 10; i++)
                createTeamRoom[i].SetActive(false);
            CreateTeam();
            // ���� �������� 0���� ũ�ų� ���� ���������� + 10 �� ���� ������ ������ ������ ������ ���� �������� �ѱ� �� �ְ� �� 
            // ���� �������� 0 ���� �����ϰ� ������ �������� �⺻ ���� 10���� �����Ͽ� 10��ŭ ���� ���̰� ���� 
            // ���� �������� ������ ������ �ΰ��� Ȯ���Ϸ��� 10�� �����༭ �� ���� ������ Ȯ�� �Ͽ��� �Ѵ�
            // ��ư�� Ŭ���� reSplit�� �迭 ���� 10 ���� ��Ŵ 
            if (curPage >= 0 && (curPage + 10) < endPage) {
                pageCount++;
                curPage += 10;
            }
            // ������ ��������� ���� �������� ������������ -10 ���� ���� 
            // ���̰� 10��ŭ ���� ������ -10 ���� �־��־� �迭�� �ʰ��ϴ� ������ ���´�.
            else if (curPage + 10 >= endPage)
                curPage = endPage - 10;
        }
    }

    //--------------------------------------------- ���� ���� �Լ� ---------------------------------------------// 
    public void CreateTeam()
    {
        LobbyPanel.SetActive(false);        // �κ� Panel ��Ȱ��ȭ 
        createTeam.SetActive(true);         // ���� ���� UI Ȱ��ȭ 
        inputTeam.SetActive(false);         // ���� �����ϱ� UI ��Ȱ��ȭ 
        Server.SetActive(false);            // server�� �ִ� UI ��Ȱ��ȭ 
        StartCoroutine(cTeam());            // ���� ���� ���� �ڷ�ƾ 
    }
    
    IEnumerator cTeam()
    {
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        // ID�� Ű���̹Ƿ� ID�� ���� �����´�. 
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(TeamData, form);
        yield return wwwData.SendWebRequest();

        // ��ȯ���� �����ϰ� ������ �����Ͽ� ���ڿ��� ���� 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x))); 
        // ������ ,�� �������� �Ǹ� \n ���� �߰��� �Ǳ⶧���� ������ ,�� �����ش�
        re = re.TrimEnd(',');
        Debug.Log(re);
        // �޾ƿ� ���ڿ��� ,�� �������� ������ �迭�� ����
        
        string[] reSplit = re.Split(',');

        for (int i = 0; i < reSplit.Length; i += 2)
            Debug.Log(reSplit[i]);

        // ���� ���� ���� ��ư�� Ȱ��ȭ �ϱ� ���� if�� 
        if (re != "") {
            // �� ��ư�� �˸´� �� �̸� �ֱ� 
            for (int i = 0; i < reSplit.Length; i++) {
                // ���� 10���� ���� �� 
                if (reSplit.Length > 10) {
                    // ������ Count��ŭ ( ��ŭ �������� ����� �������� else������ ���� ) 
                    // ���� ���� 33���� �ִٸ� pageCount�� 3������ �ö󰣴�.
                    // ���� 3������ ������ �� 10���� ��ư�� Ȱ��ȭ �Ѵ�. ( �̷л� �� 30���� ��ư�� Ȱ��ȭ �ϰ� �� ) 
                    // ������ �������� �� 4���̱� ������ 4��° �������� 
                    // else ������ ó���� �ϰ� �ȴ�. 
                    // else ���� �翬�ϰ� �������� ó���Ͽ� ��ư�� Ȱ��ȭ �Ѵ�. 33�� 10���� ���� �������� 3�̱� ������ 3���� ��ư�� Ȱ��ȭ�� 
                    // ���� �ִ� ���� ������ �°� ���� ��ư���� Ȱ��ȭ�� �ȴ�. 
                    if (pageCount <= (reSplit.Length / 10)) {
                        for (int j = 0; j < 10; j++) {
                            // Room + ���� ���� ������Ʈ�� ButtonValues��ũ��Ʈ�� ���� 
                            // �� ��ư�� text�� ������ �� �̸��� ���� 
                            // Name ������ �� �̸��� ���� ( Name ������ ButtonValues��ũ��Ʈ�� ���� ) 
                            createTeamRoom[j].SetActive(true);
                            ButtonValues buttonValues = GameObject.Find("Room" + j).GetComponent<ButtonValues>();
                            createTeamRoom[j].transform.GetChild(0).GetComponent<Text>().text = reSplit[j + curPage];
                            buttonValues.Name = reSplit[j + curPage];
                        }
                    }
                    else {
                        for (int j = 0; j < reSplit.Length % 10; j++) {
                            // Room + ���� ���� ������Ʈ�� ButtonValues��ũ��Ʈ�� ���� 
                            // �� ��ư�� text�� ������ �� �̸��� ���� 
                            // Name ������ �� �̸��� ���� ( Name ������ ButtonValues��ũ��Ʈ�� ���� ) 
                            createTeamRoom[j].SetActive(true);
                            ButtonValues buttonValues = GameObject.Find("Room" + j).GetComponent<ButtonValues>();
                            createTeamRoom[j].transform.GetChild(0).GetComponent<Text>().text = reSplit[j + curPage];
                            buttonValues.Name = reSplit[j + curPage];
                        }
                    }
                }
                // ���� 10���� ���� ���� �� 
                // reSplit�� ���� ��ŭ ��ư�� Ȱ��ȭ 
                else {
                    createTeamRoom[i].SetActive(true);
                    // Room + ���� ���� ������Ʈ�� ButtonValues��ũ��Ʈ�� ���� 
                    // �� ��ư�� text�� ������ �� �̸��� ���� 
                    // Name ������ �� �̸��� ���� ( Name ������ ButtonValues��ũ��Ʈ�� ���� ) 
                    ButtonValues buttonValues = GameObject.Find("Room" + i).GetComponent<ButtonValues>();
                    createTeamRoom[i].transform.GetChild(0).GetComponent<Text>().text = reSplit[i + curPage];
                    buttonValues.Name = reSplit[i + curPage];
                }
                // �ִ� �������� ���ؼ� �����ϴ� ���� 
                // �ִ� �������� ���� ��ü ������ 10���� ���� ���ӱⰡ 0�̸� 10,20 .. ó�� �� �������� ������ �� 10�� ���Ͽ� �ִ� ��ư�� ������ ���ϰ� 
                // 0�� �ƴ϶�� 1�������� �� �������̱� ������ + 1�� �ϰ� 10�� ���Ͽ� �ִ� ��ư�� ������ ���Ѵ�. 
                // �� ���� ���׿����ڸ� ����Ͽ� ���� ���� 
                endPage = (reSplit.Length % 10 == 0) ? (reSplit.Length / 10) * 10 : (reSplit.Length / 10 + 1) * 10;
            }
        }
        
        // �޸� ���� ���� 
        wwwData.Dispose();
    }
    public void CreateTeamRoom()
    {
        // ��ư�� �˸´� �� �� ���� 
        // ��ư�� Ŭ���� �� �ش� ��ư�� �̺�Ʈ�� ���� 
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        // ��ư�� value ���� index�� ���� 
        int index = clickObj.GetComponent<ButtonValues>().value;
        // ��ư�� Name ���� TeamName�� ���� 
        string TeamName = clickObj.GetComponent<ButtonValues>().Name;
        Debug.Log(index + TeamName);

        // ��ư�� �ִ� ������ 10�� index�� 0 ~ 9���� ���� 
        // ��ư�� ������ �� �� ��ư�� value ���� i ���� ���� ���� ���� ���� 
        // ĳ���Ϳ� ���� �������� ������ ���� �޽��� 
        for (int i =0; i < 10; i++) {
            if (index == i) {
                if (mapname != "" && playername != "") {
                    //ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
                    //thirdPersonController.CameraObj();
                    PhotonNetwork.CreateRoom(TeamName, new RoomOptions { MaxPlayers = 8 });
                }
                else if (mapname=="")
                    Debug.Log("���� �������ּ���.");
                else if (playername=="")
                    Debug.Log("ĳ���͸� ������ �ּ���");
            }
        }
    }

    //--------------------------------------------- ������ ���� �Լ� ---------------------------------------------// 
    // �������� �̸��� A, B, C �� ���� 
    string[] room = {"A", "B", "C", "D"};    
    public void CreateRoomA() 
    {
        // A�� ����, ���� �ο��� �ִ� 21�� ( ������ �Ѹ� ������ ���� ) 
        // ���� ���� �̸��� ���ٸ� �������ڷ� + Room���� ���̸��� �������� �ƴϸ� ���� �Է��� ���̸�����       
        // ĳ���Ϳ� ���� �������� ������ ���� �޽��� 
        if (mapname != "" && playername != "") {
            PhotonNetwork.CreateRoom(room[0] == "" ? "Room" + Random.Range(0, 100) : room[0], new RoomOptions { MaxPlayers = 20 });
        }
        else if (playername == "")
            Debug.Log("ĳ���͸� ������ �ּ���");
    }

    public void CreateRoomB()
    {
        // B�� ����, ���� �ο��� �ִ� 21�� 
        // ���� ���� �̸��� ���ٸ� �������ڷ� + Room���� ���̸��� �������� �ƴϸ� ���� �Է��� ���̸�����
        // ĳ���Ϳ� ���� �������� ������ ���� �޽��� 
        if (mapname != "" && playername != "")
            PhotonNetwork.CreateRoom(room[1] == "" ? "Room" + Random.Range(0, 100) : room[1], new RoomOptions { MaxPlayers = 20 });
        else if (playername == "")
            Debug.Log("ĳ���͸� ������ �ּ���");
    }

    public void CreateRoomC()
    {
        // C�� ����, ���� �ο��� �ִ� 21�� 
        // ���� ���� �̸��� ���ٸ� �������ڷ� + Room���� ���̸��� �������� �ƴϸ� ���� �Է��� ���̸����� 
        // ĳ���Ϳ� ���� �������� ������ ���� �޽��� 
        if (mapname != "" && playername != "")
            PhotonNetwork.CreateRoom(room[2] == "" ? "Room" + Random.Range(0, 100) : room[2], new RoomOptions { MaxPlayers = 20 });
        else if (playername == "")
            Debug.Log("ĳ���͸� ������ �ּ���");
    }

    public void CreateRoomD()
    {
        // C�� ����, ���� �ο��� �ִ� 21�� 
        // ���� ���� �̸��� ���ٸ� �������ڷ� + Room���� ���̸��� �������� �ƴϸ� ���� �Է��� ���̸����� 
        // ĳ���Ϳ� ���� �������� ������ ���� �޽��� 
        if (mapname != "" && playername != "")
            PhotonNetwork.CreateRoom(room[3] == "" ? "Room" + Random.Range(0, 100) : room[3], new RoomOptions { MaxPlayers = 20 });
        else if (playername == "")
            Debug.Log("ĳ���͸� ������ �ּ���");
    }

    //--------------------------------------------- ���� �����ϱ� ��ư�� ������ ���ֱ� ���� �Լ� ---------------------------------------------// 
    // ����ư -2 , ����ư -1
    // ���� ����� ��ư�� ������ ���ֱ� ���� �Լ��� ������ �����ϴ�.
    public void TeamInputListClick(int num)
    {
        // ���� ��ư�� ������ �� 
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
        // ���� ��ư�� ������ �� 
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

    //--------------------------------------------- ���� �����ϱ� �Լ� ---------------------------------------------// 
    public void InputTeam()
    {
        LobbyPanel.SetActive(false);        // �κ� UI �� ������Ʈ ��Ȱ��ȭ 
        createTeam.SetActive(false);        // ���� �����ϱ� UI ��Ȱ��ȭ 
        inputTeam.SetActive(true);          // ���� �����ϱ� UI Ȱ��ȭ 
        Server.SetActive(false);            // server���� UI ��Ȱ��ȭ 
        StartCoroutine(iTeam());            // ���� �����ϱ� �ڷ�ƾ 
    }
    IEnumerator iTeam()
    {
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        WWWForm form = new WWWForm();
        // ID�� Ű���̹Ƿ� ID�� ���� �����´�. 
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(UserTeamData, form);
        yield return wwwData.SendWebRequest();

        // ��ȯ���� �����ϰ� ������ �����Ͽ� ���ڿ��� ���� 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        // ������ ,�� �������� �Ǹ� \n ���� �߰��� �Ǳ⶧���� ������ ,�� �����ش�
        re = re.TrimEnd(',');
        Debug.Log(re);
        // �޾ƿ� ���ڿ��� ,�� �������� ������ �迭�� ���� 
        string[] reSplit = re.Split(',');

        // ������ /**���� ���� �Լ�**/ �� �ڷ�ƾ�� ������ ����̴�. 
        if (re != "") {
            // �� ��ư�� �˸´� �� �̸� �ֱ� 
            for (int i = 0; i < reSplit.Length; i++) {
                // ������ �����ִ� ���� 10���� ���� �� 
                if (reSplit.Length > 10) {
                    // ������ Count��ŭ ( ��ŭ �������� ����� �������� else������ ���� ) 
                    // ���� �����μ����ִ¹��� 33���� �ִٸ� pageCount�� 3������ �ö󰣴�.
                    // ���� 3������ ������ �� 10���� ��ư�� Ȱ��ȭ �Ѵ�. ( �̷л� �� 30���� ��ư�� Ȱ��ȭ �ϰ� �� ) 
                    // ������ �������� �� 4���̱� ������ 4��° �������� 
                    // else ������ ó���� �ϰ� �ȴ�. 
                    // else ���� �翬�ϰ� �������� ó���Ͽ� ��ư�� Ȱ��ȭ �Ѵ�. 33�� 10���� ���� �������� 3�̱� ������ 3���� ��ư�� Ȱ��ȭ�� 
                    // ���� �ִ� ���� ������ �°� ���� ��ư���� Ȱ��ȭ�� �ȴ�. 
                    if (pageCount <= (reSplit.Length / 10)) {
                        for (int j = 0; j < 10; j++) {
                            inputTeamRoom[j].SetActive(true);
                            ButtonValues buttonValues = GameObject.Find("InputRoom" + j).GetComponent<ButtonValues>();
                            inputTeamRoom[j].transform.GetChild(0).GetComponent<Text>().text = reSplit[j + curPage];
                            buttonValues.Name = reSplit[j + curPage];
                        }
                    }
                    // �������� Ȱ���Ͽ� ���� ������ �������� ��ư���� Ȱ��ȭ 
                    else {
                        for (int j = 0; j < reSplit.Length % 10; j++) {
                            inputTeamRoom[j].SetActive(true);
                            ButtonValues buttonValues = GameObject.Find("InputRoom" + j).GetComponent<ButtonValues>();
                            inputTeamRoom[j].transform.GetChild(0).GetComponent<Text>().text = reSplit[j + curPage];
                            buttonValues.Name = reSplit[j + curPage];
                        }
                    }
                }
                // ������ �����ִ� ���� 10���� ���� ���� �� 
                else {
                    inputTeamRoom[i].SetActive(true);
                    ButtonValues buttonValues = GameObject.Find("InputRoom" + i).GetComponent<ButtonValues>();
                    inputTeamRoom[i].transform.GetChild(0).GetComponent<Text>().text = reSplit[i + curPage];
                    buttonValues.Name = reSplit[i + curPage];
                }
                // �ִ� �������� ���ؼ� �����ϴ� ���� 
                // �ִ� �������� ���� ��ü ������ 10���� ���� ���ӱⰡ 0�̸� 10,20 .. ó�� �� �������� ������ �� 10�� ���Ͽ� �ִ� ��ư�� ������ ���ϰ� 
                // 0�� �ƴ϶�� 1�������� �� �������̱� ������ + 1�� �ϰ� 10�� ���Ͽ� �ִ� ��ư�� ������ ���Ѵ�. 
                // �� ���� ���׿����ڸ� ����Ͽ� ���� ���� 
                endPage = (reSplit.Length % 10 == 0) ? (reSplit.Length / 10) * 10 : (reSplit.Length / 10 + 1) * 10;
            }
        }

        // �޸� ���� ���� 
        wwwData.Dispose();
    }

    public void InputTeamRoom()
    {
        // ��ư�� Ŭ���̺�Ʈ�������� 
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        // Ŭ���� ��ư�� value���� Name���� �����ͼ� ������ 
        int index = clickObj.GetComponent<ButtonValues>().value;
        string TeamName = clickObj.GetComponent<ButtonValues>().Name;
        Debug.Log(index + TeamName);

        // Ŭ���� ��ư�� value ���� i ���� ������ �� ��ư�� Name ���� ������ �� ������ ���� 
        // �� ����� ���� �������� �ʰ� ĳ���͸� �����ϱ� ������ ĳ���͸� �������� �ʰ� �����ϸ� ���� �߻� 
        for (int i = 0; i < 10; i++) {
            if (index == i) {
                if (playername != "")
                    PhotonNetwork.JoinRoom(TeamName);
                else
                    Debug.Log("ĳ���͸� ������ �ּ���");
            }
        }
    }

    //--------------------------------------------- ������ �����ϱ� �Լ� ---------------------------------------------// 
    public void JoinRoomA()
    {
        // A�� ����
        // ĳ���͸� �������� ���� �� ���� �޽��� 
        if (playername != "")
            PhotonNetwork.JoinRoom(room[0]);
        else
            Debug.Log("ĳ���͸� ������ �ּ���");
    }

    public void JoinRoomB()
    {
        // B�� ����
        // ĳ���͸� �������� ���� �� ���� �޽��� 
        if (playername != "")
            PhotonNetwork.JoinRoom(room[1]);
        else
            Debug.Log("ĳ���͸� ������ �ּ���");
    }

    public void JoinRoomC()
    {
        // C�� ����
        // ĳ���͸� �������� ���� �� ���� �޽��� 
        if (playername != "")
            PhotonNetwork.JoinRoom(room[2]);
        else
            Debug.Log("ĳ���͸� ������ �ּ���");
    }
    public void JoinRoomD()
    {
        // C�� ����
        // ĳ���͸� �������� ���� �� ���� �޽��� 
        if (playername != "")
            PhotonNetwork.JoinRoom(room[3]);
        else
            Debug.Log("ĳ���͸� ������ �ּ���");
    }

    //--------------------------------------------- ������� ���� ������ ���� �Լ� ( �����ϸ鼭 ���� ����, ���� �� ���� �Լ� ) ������� ���� �� ���� ---------------------------------------------// 
    public void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(room[0], new RoomOptions { MaxPlayers = 20 }, null);
    }

    // ������ ���� ���  
    public void JoinRandomRoom() 
    { 
        PhotonNetwork.JoinRandomRoom(); 
    }

    //--------------------------------------------- �� ������ �Լ� ---------------------------------------------// 
    public void LeaveRoom()
    {
        // ��� ���� ������ ������ ������ ������ ���� �ı��ǵ��� ���� 
        lodingPanel.SetActive(true);
        checkRoom = false;

        UserCheck = false;
        StartCoroutine(AllTeam());

        // ���� ������ Map�� ��Ȱ��ȭ ���ش�. 
        // �������� �ı��Ͽ� ��Ȱ��ȭ�� �Ѵ� 
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
        // ID�� Ű���̹Ƿ� ID�� ���� �����´�. 
        form.AddField("id", loginManager.IDInputField.text);

        wwwData = UnityWebRequest.Post(AllTeamData, form);
        yield return wwwData.SendWebRequest();

        // ��ȯ���� �����ϰ� ������ �����Ͽ� ���ڿ��� ���� 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        // ������ ,�� �������� �Ǹ� \n ���� �߰��� �Ǳ⶧���� ������ ,�� �����ش�
        re = re.TrimEnd(',');
        Debug.Log(re);
        // �޾ƿ� ���ڿ��� ,�� �������� ������ �迭�� ���� 
        string[] reSplit = re.Split(',');


        wwwData_2 = UnityWebRequest.Post(UserData, form);
        yield return wwwData_2.SendWebRequest();
        // ��ȯ���� �����ϰ� ������ �����Ͽ� ���ڿ��� ���� 
        string str_2 = wwwData_2.downloadHandler.text;
        string re_2 = string.Concat(str_2.Where(x => !char.IsWhiteSpace(x)));
        // �޾ƿ� ���ڿ��� ,�� �������� ������ �迭�� ���� 
        string[] reSplit_2 = re_2.Split(',');
        // �迭�� ���ڿ��� �� ������ Ȯ�� 
        // ��� [0] : �г��� [1] : 1 �Ǵ� 0 ( 1�� admin 0�� user ) 
        for (int i = 0; i < reSplit_2.Length; i++)
            Debug.Log(reSplit_2[i]);


        // ����� �����濡�� ���� ���� �� 
        for (int i = 0; i < reSplit.Length; i += 2) {
            // ���濡�� ������ �� ���� ������ ��� 
            if(PhotonNetwork.CurrentRoom.Name == reSplit[i] && reSplit[i + 1]=="1") {
                // hashtable�� ����Ͽ� Ŀ���� ������Ƽ�� �����. 
                ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
                // isKicked �̶�� ������ true ���� �߰� 
                hashtable.Add("isKicked", true);
                Debug.Log(reSplit[i]);
                for (int j = 0; j < PhotonNetwork.PlayerList.Length; j++) {
                    // �� �ο� ��ü���� isKicked�� true ���� �ο� 
                    PhotonNetwork.PlayerList[j].SetCustomProperties(hashtable);
                    Debug.Log(PhotonNetwork.PlayerList[j]);
                }
                // ������ ���� ������ 
                PhotonNetwork.LeaveRoom();
            }
            // ���濡�� ���� �� �� ���� ������ ��� 
            // �׳� Leave Room �� �����ϸ� �ȴ�.
            else if (PhotonNetwork.CurrentRoom.Name == reSplit[i] && reSplit[i + 1]=="0") {
                if (PhotonNetwork.InRoom)
                    PhotonNetwork.LeaveRoom();
                else if (PhotonNetwork.InLobby)
                    yield return 0;
            }
            // �����濡�� ������ �� �������� �������� ��� 
            else if ((PhotonNetwork.CurrentRoom.Name == "A" || PhotonNetwork.CurrentRoom.Name == "B" || PhotonNetwork.CurrentRoom.Name == "C") && reSplit_2[1] == "1") {
                // hashtable�� ����Ͽ� Ŀ���� ������Ƽ�� �����. 
                ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
                // isKicked �̶�� ������ true ���� �߰� 
                hashtable.Add("isKicked", true);
                for (int j = 0; j < PhotonNetwork.PlayerList.Length; j++) {
                    // �� �ο� ��ü���� isKicked�� true ���� �ο� 
                    PhotonNetwork.PlayerList[j].SetCustomProperties(hashtable);
                    Debug.Log(PhotonNetwork.PlayerList[j]);
                }
                // �����ڴ� ���� ������
                PhotonNetwork.LeaveRoom();
            }
            // �����濡�� ���� �� �� �������� ������� ��� 
            // �׳� Leave Room �� �����ϸ� �ȴ�. 
            else if((PhotonNetwork.CurrentRoom.Name == "A" || PhotonNetwork.CurrentRoom.Name == "B" || PhotonNetwork.CurrentRoom.Name == "C") && reSplit_2[1] == "0") {
                if (PhotonNetwork.InRoom)
                    PhotonNetwork.LeaveRoom();
                else if (PhotonNetwork.InLobby)
                    yield return 0;
            }
        }

        // �޸� ���� ���� 
        wwwData.Dispose();
    }

    //--------------------------------------------- ���� �Ǵ� �����ڰ� ���� ������ �� �����鿡�� �ο��� ������Ƽ ������ �� �������� ---------------------------------------------// 
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == PhotonNetwork.LocalPlayer) {
            // isKicked property�� �����Ұ��
            if (changedProps["isKicked"] != null) {
                // isKicked�� true�� ���
                if ((bool)changedProps["isKicked"]) {
                    // isKicked ������Ƽ�� �����ϰ� ���� ������. 
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

    //--------------------------------------------- �� ������ �Ϸ�Ǿ��� �� �Լ� ---------------------------------------------// 
    public override void OnCreatedRoom()
    {
        print("�游���Ϸ�");
        Server.SetActive(false);                // server ���� UI ��Ȱ��ȭ 
        LobbyPanel.SetActive(false);            // �κ� UI ��Ȱ��ȭ 
        createTeam.SetActive(false);            // ���� ���� UI ��Ȱ��ȭ 
        inputTeam.SetActive(false);             // ���� ���� UI ��Ȱ��ȭ 
        Lobby.SetActive(false);                 // �κ� ��Ȱ��ȭ 
        UserCheck = true;

        // �� ���� ������ ���� 
        // mapname�� �� ��ư�� ���� �� �߻��ϴ� �Լ����� ���� �� ( MapValue() �Լ� ) 
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
    //--------------------------------------------- �� �����Ҷ� ���� �̸��� �����ϴ� �Լ� ---------------------------------------------// 
    public void MapValue()
    {
        // Ŭ���� �߻��ϴ� �̺�Ʈ�� �����ϴ� ���� ���� �� Name�� ���� 
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        string TeamName = clickObj.GetComponent<ButtonValues>().Name;

        // �� ��ư�� ������ �ִ� ���� �̸��� mapname ������ ����
        // ��ư�� ����� ���� ��� ���������� ������ �� �ִ�.
        mapname = TeamName;
        Debug.Log(mapname);
    }


    //--------------------------------------------- �� ������ �Ϸ�Ǿ��� �� �Լ� ---------------------------------------------// 
    public override void OnJoinedRoom()
    {
        lodingPanel.SetActive(true);
        print("�������Ϸ�");
        Server.SetActive(false);                // server UI ��Ȱ��ȭ
        LobbyPanel.SetActive(false);            // �κ� Panel ��Ȱ��ȭ 
        createTeam.SetActive(false);            // ���� ���� UI ��Ȱ��ȭ 
        inputTeam.SetActive(false);             // ���� ���� UI ��Ȱ��ȭ 
        Lobby.SetActive(false);                 // �κ� ��Ȱ��ȭ 

        checkRoom = true;
        //RoomPanel.SetActive(true);              // �� Panel Ȱ

        // ������ ���� �� �÷��̾� ����ȭ 
        // ĳ���� �̸��� �޾Ƽ� ĳ���� ���� 
        SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
        smoothFollow.roteta();
        player = PhotonNetwork.Instantiate(playername, Vector3.zero, Quaternion.identity);

        // ���� ���� ������ text�� ǥ�� 
        // �ִ� ������ ���� ����� �ִ���
        RoomRenewal();

        ChatInput.text = "";                // ä�� �Է� â�� �ʱ�ȭ 

        // ��� ä�ñ���� �ʱ�ȭ 
        for (int i = 0; i < ChatText.Length; i++) 
            ChatText[i].text = "";

    }

    //--------------------------------------------- ĳ���͸� ������ �� ĳ������ �̸��� �����ϴ� �Լ� ---------------------------------------------// 
    public void PlayerNameValue()
    {
        // Ŭ���� �߻��ϴ� �̺�Ʈ�� �����ϴ� ���� ���� �� Name�� ���� 
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        string TeamName = clickObj.GetComponent<ButtonValues>().Name;

        // ĳ���� ��ư�� ������ �ִ� ĳ������ �̸��� playername ������ ����
        // ��ư�� ����� ĳ���Ͱ� ��� ���������� ������ �� �ִ�.
        playername = TeamName;
        Debug.Log(playername);
    }

    //--------------------------------------------- �� ����Ⱑ �����Ͽ��� �� ---------------------------------------------// 
    public override void OnCreateRoomFailed( short returnCode, string message )
    {
        print("�游������");
    }
    //--------------------------------------------- ���� ������� ���� �ʾ� �� ������ �����Ͽ��� �� ---------------------------------------------// 
    public override void OnJoinRoomFailed( short returnCode, string message )
    {
        print("����������");
    }

    //--------------------------------------------- ������� �ʴ� ����� ����... ---------------------------------------------// 
    public override void OnJoinRandomFailed( short returnCode, string message )
    {
        print("�淣����������");

    }
    //----------------------------------------------------------------------------------------------------------------------// 

    [ContextMenu("����")]
    // ���� ���� ���� 
    void Info()
    {
        if (PhotonNetwork.InRoom) {
            print("���� �� �̸� : " + PhotonNetwork.CurrentRoom.Name);
            print("���� �� �ο��� : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("���� �� �ִ��ο��� : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "�濡 �ִ� �÷��̾� ��� : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else {
            print("������ �ο� �� : " + PhotonNetwork.CountOfPlayers);
            print("�� ���� : " + PhotonNetwork.CountOfRooms);
            print("��� �濡 �ִ� �ο� �� : " + PhotonNetwork.CountOfPlayersInRooms);
            print("�κ� �ִ���? : " + PhotonNetwork.InLobby);
            print("����ƴ���? : " + PhotonNetwork.IsConnected);
        }
    }


    //--------------------------------------------- �渮��Ʈ ���� �Լ� ---------------------------------------------// 
    #region �渮��Ʈ ����
    // ����ư -2 , ����ư -1 , �� ����
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
        // �ִ�������
        // ���� ���� 9����� �������� 3���� ��������� �Ѵ�.
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // ����, ������ư
        // ó�� �������̸� ���� ��ư�� ��Ȱ��ȭ 
        // ������ �������� ���� ��ư�� ��Ȱ��ȭ 
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // �������� �´� ����Ʈ ����
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++) {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<Text>().text = (multiple + i < myList.Count) ? (myList[multiple + i].PlayerCount - 1) + "/" + (myList[multiple + i].MaxPlayers - 1) : "";
        }
    }

    // �� ����Ʈ�� ���� �������� �̵� ��Ű�� �� ������ ������ 
    // ����Ʈ�� ������ �� ( ������ ������ ���� �����ǰ�, ������ ���� �������� ) 
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

    //------------------------------------------- ä�� �ý��� -------------------------------------------//
    public void ChatEnd()
    {
        ChatSystem.SetActive(false);
    }

    public override void OnPlayerEnteredRoom( Player newPlayer )
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");
    }

    public override void OnPlayerLeftRoom( Player otherPlayer )
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "���� �����ϼ̽��ϴ�</color>");
    }

    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : ", ");

        // Room�ȿ��� ����� �̸�, �ο���, �ִ��ο����� ǥ�� 
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + (PhotonNetwork.CurrentRoom.PlayerCount) + "�� / " + (PhotonNetwork.CurrentRoom.MaxPlayers) + "�ִ�";
    }
    #endregion


    #region ä��
    public void Send()
    {
        // PV.RPC ���� 
        // PV.RPC ( RPC �Լ� �̸�, RpcTarget. ���� ���, �Ű�����( ���� �Ѱ��� ) )
        // ���� ����� All�� �� �� �濡 �ִ� ����ο��� �� �� ���� 
        if (!(ChatInput.text == "")) {
            PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text);
            StartCoroutine(ChatLog());
        }
        ChatInput.text = "";
    }
    IEnumerator ChatLog()
    {
        WWWForm form = new WWWForm();
        // WWWform�� id, pw, nick �ʵ带 �����ϰ� 
        // ���� inputField�� �Է��� ���̵�, ��й�ȣ, �г����� �߰��Ѵ�.
        form.AddField("chat_log", ChatInput.text);
        form.AddField("team_name", PhotonNetwork.CurrentRoom.Name);
        form.AddField("nick", PhotonNetwork.NickName);
        Debug.Log(ChatInput.text + PhotonNetwork.CurrentRoom.Name + PhotonNetwork.NickName);
        

        // Post ������� �α���URL�� WWWform�ʵ� ���� wwwData ������ ����  
        wwwData = UnityWebRequest.Post(ChatLogData, form);
        yield return wwwData.SendWebRequest();

        // ������ �����͸� �޾Ƽ� ���ڿ� ���·� �����ϰ� 
        // ������ �������ν� ����Ƽ���� ����� �� �ִ� �����ͷ� ���� 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        Debug.Log(re);

        // �޸� ������ ���� �ϱ� ���� �޸� ���� 
        wwwData.Dispose();
    }


    // RPC�� �÷��̾ �����ִ� �� ��� �ο����� �����Ѵ�
    // �ſ� �߿��� �Լ� RPC
    [PunRPC] 
    void ChatRPC( string msg )
    {
        bool isInput = false;
        
        for (int i = 0; i < ChatText.Length; i++)
            // ä���� text�߿��� ����ִ� ������ ã�´�.
            // ����ִ� ������ ������ �Ű����� ���� �Ѱ��ش� 
            if (ChatText[i].text == "") {
                isInput = true;             // ä�� �� text�� ���ٸ� true�� Ȯ�� 
                ChatText[i].text = msg;
                break;
            }

        // ���� ����ִ� ������ ��� text�� ���� ���Ͽ��ٸ� 
        if (!isInput) // ������ ��ĭ�� ���� �ø�
        {
            for (int i = 1; i < ChatText.Length; i++) 
                ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion
}