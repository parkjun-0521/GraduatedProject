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
    // �⺻ ���� ���� 
    [Header("LoginPanel")]
    public InputField IDInputField;             // ���̵� �Է� inputField 
    public InputField PWInputField;             // ��й�ȣ �Է� inputField 
    public GameObject LoginPanelObj;            // �α��� Panel ������Ʈ 
    [Header("CreateAccountPanel")]
    public InputField NewIDInputField;          // ���� ���� ���̵� �Է� inputField 
    public InputField NewPWInputField;          // ���� ���� ��й�ȣ �Է� inputField
    public InputField NickNameInputField;       // ���� ���� �г��� �Է� inputField 
    public GameObject CreateAccountPanelObj;    // ���� ���� Panel ������Ʈ 

    // ������ 
    [Header("Admin")]
    public GameObject ConnectServerObj;         // ������ ���� ���� ������Ʈ

    // �����
    [Header("User")]
    public GameObject UserRoomServerObj;        // ����� ���� ���� ������Ʈ 

    // ���� Panel
    [Header("Public")]
    public GameObject Server;                   // �г��� + �� �̸� InputField ( �� �����忡�� InputField ����� ���� )
    public GameObject LobbyPanelObj;            // ���� ���ӽ� ��� �κ� ������ ( ������ : �� ����, ����� : �� ���� ) 
    public GameObject RoomPanelObj;             // �κ񿡼� �� ���ӽ� ��� �� ������Ʈ ( ä�� + ���� ����� )
    public GameObject createTeam;
    public GameObject inputTeam;
    private Transform tr;

    [Header("Lobby")]
    public GameObject LobbyRoom;
    public GameObject LobbyMainPlayer;
    public GameObject publicPortal;
    public GameObject CreateTeamPortal;
    public GameObject InputTeamPortal;
    
    // DB���� ���� 
    [Header("Database")]
    UnityWebRequest wwwData;                    // �����ͺ��̽��� ������ �����ϱ� ���� ������ ����
                                                // 
    [Header("Url")]
    public string LoginUrl;                     // �鼭���� ����� �α��� URL
    public string CreateUrl;                    // �鼭���� ����� �������� URL
    // Use this for initialization

    public GameObject loginErrorMessage;
    public RectTransform rectLogin;
    //--------------------------------------------- URL �ʱ�ȭ ---------------------------------------------//
    void Start()
    {
        // ��ư�� ������ �� �̵��� URL (���ڸ����̿��� jsp ������ ��������� ���� URL�� �Է�)
        // �α��� URL
        LoginUrl = "http://223.131.75.181:1356//Metaverse_war_exploded/Login.jsp";
        // �������� URL 
        CreateUrl = "http://223.131.75.181:1356/Metaverse_war_exploded/NewUserCreate.jsp";

        rectLogin = loginErrorMessage.GetComponent<RectTransform>();

    }

    //--------------------------------------------- �α��� ��ư ---------------------------------------------// 
    public void LoginButton()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        // ���� �Է��� ���̵� ��й�ȣ�� console����� 
        Debug.Log(IDInputField.text);
        Debug.Log(PWInputField.text);

        // �� WWWForm ��ü�� ���� 
        // �� ���� ����� ���� form ��ü ���� 
        WWWForm form = new WWWForm();
        // WWWform�� id, pw �ʵ带 �����ϰ� 
        // ���� inputField�� �Է��� ���̵�, ��й�ȣ�� �߰��Ѵ�.
        form.AddField("id", IDInputField.text);
        form.AddField("pw", PWInputField.text);

        // Post ������� �α���URL�� WWWform�ʵ� ���� wwwData ������ ����  
        wwwData = UnityWebRequest.Post(LoginUrl, form);
        // wwwData�� ���� ������ ���� �� ���� ���� ���� ���� 
        yield return wwwData.SendWebRequest();              

        // ��(�� ���� ����)���� ������ �����͸� ���ڿ� ���·� ����
        // ���ʿ����� ���ڿ� ���·� �����͸� ������ �־�� �� 
        string str = wwwData.downloadHandler.text;
        // �޾ƿ� ���ڿ��� �յ� ������ �� ����
        // jsp�� �޾ƿ��ϱ� ���� �ٹٲ� 3���� ���ܼ� �����Ͱ� ����� �޾����� �ʴ� ������ �߻�, ���� ���ڿ��� ������ �������ν� ���� �ذ� 
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        // ���� �����Ϳ� ������ �� �������� ����� �Ǵ��� Ȯ�� 
        Debug.Log(re);

        // �鿡���� ������ �α��� ���θ� �Ǵ� 
        // ������, �����, �α��� ���и� �Ǵ� 
        if (re == "1") {
            Debug.Log("�α��� ����");
            // �α��� ������ ( ������ �����̴� ) 
            // �α��� Panel�� ��Ȱ��ȭ, ������ ���� ���� Panel�� Ȱ��ȭ 
            LoginPanelObj.SetActive(false);
           // ConnectServerObj.SetActive(true);
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            networkManager.Connect();
            networkManager.LoginlodingPanel.SetActive(true);
        }
        else if (re == "0") {
            Debug.Log("�α��� ����");
            // �α��� ������ ( ����� �����̴� ) 
            // �α��� Panel�� ��Ȱ��ȭ, ����� ���� ���� Panel�� Ȱ��ȭ 
            LoginPanelObj.SetActive(false);
            //UserRoomServerObj.SetActive(true);
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            networkManager.Connect();
            networkManager.LoginlodingPanel.SetActive(true);
        }
        else {
            rectLogin.anchoredPosition = new Vector2(0,0);
            Debug.Log("�α��� ����");
        }
        // �޸� ���� ������ ���� �޸� ���� 
        wwwData.Dispose();
    }

    //--------------------------------------------- �α��� ȭ���� �������� ��ư ---------------------------------------------// 
    public void OpenCreateAccountButton()
    {
        // �α��� Panel ��Ȱ��ȭ, �������� Panel Ȱ��ȭ 
        LoginPanelObj.SetActive(false);
        CreateAccountPanelObj.SetActive(true);
    }
    
    //--------------------------------------------- �������� �������� �������� ��ư ---------------------------------------------// 
    public void CreateAccountButton()
    {
        StartCoroutine(CreateUser());
    }

    // �α��ΰ� ������ ������� ���� ����Ѵ�  
    IEnumerator CreateUser()
    {
        WWWForm form = new WWWForm();
        // WWWform�� id, pw, nick �ʵ带 �����ϰ� 
        // ���� inputField�� �Է��� ���̵�, ��й�ȣ, �г����� �߰��Ѵ�.
        form.AddField("id", NewIDInputField.text);
        form.AddField("pwd", NewPWInputField.text);
        form.AddField("nick", NickNameInputField.text);

        // Post ������� �α���URL�� WWWform�ʵ� ���� wwwData ������ ����  
        wwwData = UnityWebRequest.Post(CreateUrl, form);
        yield return wwwData.SendWebRequest();
        Debug.Log(wwwData.downloadHandler.text);

        // ������ �����͸� �޾Ƽ� ���ڿ� ���·� �����ϰ� 
        // ������ �������ν� ����Ƽ���� ����� �� �ִ� �����ͷ� ���� 
        string str = wwwData.downloadHandler.text;
        string re = string.Concat(str.Where(x => !char.IsWhiteSpace(x)));
        Debug.Log(re);

        // ���� ������ �Ϸᰡ �Ǹ� �α��� �������� ���ư��� �Լ� 
        Back();

        // �޸� ������ ���� �ϱ� ���� �޸� ���� 
        wwwData.Dispose();
    }
    // ���� ������ �Ϸᰡ �Ǹ� �α��� �������� ���ư��� �Լ� 
    public void Back()
    {
        // �α��� Panel Ȱ��ȭ, �������� Panel ��Ȱ��ȭ 
        LoginPanelObj.SetActive(true);
        CreateAccountPanelObj.SetActive(false);
    }

    //--------------------------------------------- �������� �� Room���� �κ�� �̵��ϴ� ��ư ---------------------------------------------// 
    public void BackLobby()
    {
        // Room�� ��Ȱ��ȭ, Lobby Ȱ��ȭ 
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
