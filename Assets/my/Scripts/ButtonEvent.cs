using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class ButtonEvent : MonoBehaviourPunCallbacks {
    //================================== ������ ��Ż�� �������� �� �߻��ϴ� Button �̺�Ʈ ==================================//
    [Header("--------������--------")]
    public GameObject RoomCreate_Input;                 // ���� ����� ���� ��ü���� ������Ʈ
    public GameObject[] LobbyPanel_Player;              // ĳ���� ���� �迭 ���� 
    public GameObject[] LobbyPanel_NextButton;          // ĳ���� ���� �� �������� �̵��ϱ� ���� ���� ���� 
    public GameObject LobbyPanel_PreviousButton;        // �������� �̵��ϱ� ���� ���� 

    public GameObject[] Women_Men;                      // ���� ĳ���͸� �����ϱ� ���� ���� 

    public GameObject[] publicCreateRoom;               // �� ����� ��      ( ������ )
    public GameObject[] publicInputRoom;                // �� �����ϱ� ��    ( ����� )

    public GameObject publicplayerBackGround;           // ĳ���� ���� UI �ڹ�� 
    public GameObject publicItemBackGround;             // �ƹ�Ÿ ���� UI �ڹ�� 

    public GameObject publicItemList;                   // �ƹ�Ÿ ��ü�� �����ϰ� �ִ� UI ������Ʈ 
    public GameObject publicNextButton;                 // �������� �̵� �� �� �ִ� ��ư ������Ʈ    
    public GameObject publicPreviousButton;             // �������� �̵� �� �� �ִ� ��ư ������Ʈ

    public int pCount = 0;                              // ���� �������� Ȯ���� Flag ���� 
    public int publicRoomCount = 3;                     // ���� ������ �� ��, ����, ����, �ܿ� �� ������ �� �ֵ��� �����ϱ� ���� ���� 

    //================================== ���� ���� ��Ż�� ���� �� �߻��ϴ� Button �̺�Ʈ ==================================//
    [Header("--------���� ����--------")]
    public GameObject[] TeamInput_Player;               // ���� ������ ĳ���� ������ ���� �迭 ���� 
    public GameObject[] TeamInput_NextButton;           // ĳ���� ���� �� �������� �Ѿ �� �ִ� ��ư 
    public GameObject TeamInput_PreviousButton;         // �������� ���ư� �� �ִ� ��ư ������Ʈ 

    public GameObject[] TeamInput_Women_Men;            // ���� ĳ���͸� �ٲٱ� ���� ��ư ������Ʈ 

    public GameObject playerBackGround;                 // ĳ���͸� ������ �� �ڹ��
    public GameObject InputplayerTeaminput;             // ���� �� �� UI �ڹ�� 
    public GameObject InputItemBackGround;              // �ƹ�Ÿ�� ������ �� UI �� ��� 


    public GameObject TeamInputNext;                    // �������� �̵��� �� �ִ� ��ư ������Ʈ 
    public GameObject TeamInputPrevious;                // �������� ���ư� �� �ִ� ��ư ������Ʈ 

    public GameObject TeamInputRoom;                    // �� ���� �����ϱ� ���� ���� 
    public GameObject InputItemList;                    // �ƹ�Ÿ�� ������ �ִ� ������Ʈ ���� 
    public GameObject inputNextButton;                  // �������� �Ѿ�� ��ư ������Ʈ  
    public int iCount = 0;                              // ���° ���������� �����ϱ� ���� ���� 

    //================================== ���� ���� ��Ż�� ���� �� �߻��ϴ� Button �̺�Ʈ ==================================//
    [Header("--------���� ����--------")]
    public GameObject[] TeamCreate_Player;              // ���� ������ ĳ���� ������ ���� �迭 ���� 
    public GameObject[] TeamCreate_NextButton;          // ĳ���͸� �����ϸ� �ٷ� �Ѿ �� �ֵ��� �ϱ� ���� ���� ���� 
    public GameObject TeamCreate_PreviousButton;        // ���� �������� �������� ���ư��� ���� ��ư ������Ʈ 

    public GameObject[] TeamCreate_Women_Men;           // ���� ĳ���͸� �ٲٱ� ���� ��ư ������Ʈ 
        
    public GameObject[] TeamCreateMap;                  // ���濡�� �����ϴ� 4���� ���� ������ �迭 
    public GameObject TeamRoomCreate;                   // �� ���ÿ� ���õ� UI ��ü�� ������ �ִ� ������Ʈ 

    public GameObject CreateplayerBackGround;           // ĳ���� ������ �� �ڹ��
    public GameObject CreateItemBackGround;             // ĳ���͸� ������ �� �ڹ�� 
    public GameObject CreateItemSelectBackGround;       // �ƹ�Ÿ�� ������ �� �ڹ�� 
    public GameObject CreateRoomBackGround;             // ���� ������ �� �ڹ�� 

    public GameObject CreateItemList;                   // �ƹ�Ÿ üũ�ڽ��� ��ü UI�� �����ϰ� �ִ� ������Ʈ 
    public GameObject CreateTeamNextSelect;             // UI���� �������� �Ѿ�� ��ư ������Ʈ 
    public GameObject CreateTeamPreSelect;              // UI���� �������� ���ư��� ��ư ������Ʈ 

    public GameObject TeamCreateRoom;                   // ���� 10���� ��ư�� ������ �ִ� ������Ʈ 
    public GameObject TeamCreateNext;                   // ������ 10���� �Ѿ��� �� �������� �Ѱܼ� Ȯ���� �� �ִ� ��ư ������Ʈ 
    public GameObject TeamCreatePrevious;               // �������� �Ѱܼ� �������� Ȯ���� �� �ִ� ��ư ������Ʈ 

    public int count = 0;                               // ���� ��ư�� ������ �� ���° ������ ���� Ȯ���ϱ� ���� ���� 

    public int roomCount = 0;                           // ���� ������ �� ���° ������ Ȯ�� �ϱ� ���� ���� 

    //================================== �ɼ�â�� ������ �� �߻��ϴ� Button �̺�Ʈ ==================================//
    [Header("--------�ɼ� â--------")]
    public GameObject option;                   // �ɼ� UI ������Ʈ 
    public GameObject soundPanel;               // ���� ���� ���� UI ������Ʈ 
    bool soundOptionCheck = false;              // ���� �ɼ� â ���� Flag ���� 

    public GameObject Lobby;                    // �κ� ������Ʈ 
    public GameObject roomPanel;                // Room������ �ִ� UI Panel ������Ʈ 
    bool optionCheck = false;                   // �ɼ� â ���� Flag ���� 

    //================================== �ɼ� â���� ���� ���� ���� Button �̺�Ʈ ==================================//
    public Button Mic;                          // ����ũ ��ư ������Ʈ 
    public Button Speker;                       // ����Ŀ ��ư ������Ʈ 
    public Recorder NetworkVoiceManager;        // ���̽� ��Ʈ��ũ �Ŵ��� ���� ( ���� ������ ���ؼ��� �ʿ� ) 
    public PunVoiceClient punVoiceClient;       // photon ���̽� ���� ���� ���� ( ����Ŀ ������ ���ؼ� �ʿ� ) 

    int micCount = 0;                           // ����ũ�� �����ִ��� Ȯ���ϱ� ���� Flag ���� 
    public Image curImage;                      // ������ �����ϴ� �̹���
    public Sprite changeSprite;                 // �ٲ���� �̹���
    public Sprite changeCurSprite;              // �ٲ� ���� �̹��� ( �̹����� �ٲٰ� ���� �̹����� ���� ) 

    int spekerCount = 0;                        // ����Ŀ�� �����ִ��� Ȯ���ϱ� ���� Flag ���� 
    public Image curSpekerImage;                // ������ �����ϴ� �̹���
    public Sprite changeSpekerSprite;           // �ٲ���� �̹���
    public Sprite changeCurSpekerSprite;        // �ٲ� ���� �̹��� ( �̹����� �ٲٰ� ���� �̹����� ���� ) 

    //================================== �׸��� ���� ���� ���� ==================================//
    [Header("--------�׸���--------")]
    public GameObject DrawPanel;
    public GameObject mainCamera;

    //=====================================================================//
    public GameObject preimg;
    public GameObject[] preava;
    public Camera precam;
    NetworkManager netManager;

    //=============================== �α��� â�� �����ϴ� ��ư�� �̺�Ʈ ======================================//
    // Ȩ�������� �̵��ϱ� ��ư�� ������ �� �߻��ϴ� �̺�Ʈ 
    public void WithRium_Move()
    {
        Application.OpenURL("http://withrium.com/"); // ��ũ �� ����Ʈ �ּ�
    }

    // ȸ������ ��ư�� ������ �� �߻��ϴ� �̺�Ʈ 
    public void WithRium_UserCreate()
    {
        Application.OpenURL("http://withrium.com/signup"); // ��ũ �� ����Ʈ �ּ�
    }

    //=============================== ������ ���� ��Ż���� �� �� �ִ� Button �̺�Ʈ ===============================//
    public void LobbyPanel_Select_Next()
    {
        // ĳ���� ���� �� �Ͼ�� �̺�Ʈ 

        // ĳ���͸� �������� �� ĳ���� ��ư�� �� ��Ȱ��ȭ 
        // ���� ���� ��ư�� ��Ȱ��ȭ 
        for(int i = 0; i < LobbyPanel_Player.Length; i++) 
            LobbyPanel_Player[i].SetActive(false);
        for (int i = 0; i < Women_Men.Length; i++)
            Women_Men[i].SetActive(false);
        for(int i =0;i< LobbyPanel_NextButton.Length; i++)    
            LobbyPanel_NextButton[i].SetActive(false);
        
        LobbyPanel_PreviousButton.SetActive(true);              // �ƹ�Ÿ ���ÿ��� ĳ���� �������� ���ư� �� ���� ��ư Ȱ��ȭ 
        publicplayerBackGround.SetActive(false);                // ĳ���� ����â�� �ڹ�� ��Ȱ��ȭ 
        publicNextButton.SetActive(true);                       // �ƹ�Ÿ ���� �� �ʼ������� �̵��� �� �ִ� ���� ��ư Ȱ��ȭ 
        publicPreviousButton.SetActive(true);                   // ĳ���� �������� ���ư� �� �ִ� ��ư Ȱ��ȭ 
        pCount++;                                               // ���� �������� �̵��Ͽ��� ������ flag ������ 1 ���� ( ���� 1��������� ���� �˸� ) 
    }

    public void LobbyPanel_Next_Button()
    {
        // �ι�° �ڷΰ��� ��ư�� ������ �� ( �ƹ�Ÿ ������ ������ �� ) 

        publicRoomCount = 0;                                        // �� ���� â���� ���� �ʱ⿡ ������ ó�� ������ �ʱ�ȭ �ϱ� ���ؼ� �� ���� �ʱ�ȭ 
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();           // LoginManager ������Ʈ�� ã�Ƽ� �����´�.  
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();   // NetworkManager ������Ʈ�� ã�Ƽ� �����´�. 

        for (int i = 0; i < networkManager.itemName.Length; i++) 
            Debug.Log(networkManager.itemName[i]);                  // �������� �����Դ��� Ȯ�� ( ����� )                
            
        publicItemBackGround.SetActive(false);                      // �ƹ�Ÿ ���� �ڹ�� ��Ȱ��ȭ 
        publicItemList.SetActive(false);                            // �ƹ�Ÿ üũ �ڽ� ��Ȱ��ȭ 
        publicNextButton.SetActive(false);                          // ���� ��ư ��Ȱ��ȭ ( ���� �����ϸ� �ڵ����� ������ �̵��Ǳ� ������ ���� ��ư�� �ʿ䰡 ���� ) 

        RoomCreate_Input.SetActive(true);                           // �� ���� Ȱ��ȭ 

        // �������� ���� ������� �� ������ ���� ������ UI â�� �ٸ� 
        // 1 �� ���� ������ ( ���� ���� �����ϴ� ��ư�� ������ ) 
        // 0 �� ���� ����� ( ���� ���� �����ϴ� ��ư�� ������ ) 
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

        pCount++;           // ���� ��ư�� ������ �̵��Ͽ��� ������ flag ���� ���� ( flag ���� == 2 )
    }

    public void LobbyPanel_Select_Previous()
    {
        // ���� ��ư�� ������ �����ϴ� �̺�Ʈ 

        // �ƹ�Ÿ â���� ĳ���� ����â���� ���ư� ��
        if (pCount == 1) {
            RoomCreate_Input.SetActive(false);          // �� ��ư ��Ȱ��ȭ
            LobbyPanel_PreviousButton.SetActive(false); // ���� ��ư ��Ȱ��ȭ 
            publicPreviousButton.SetActive(false);      // ���� ��ư ��Ȱ��ȭ 
            publicNextButton.SetActive(false);          // ���� ��ư ��Ȱ��ȭ 

            Women();                                    // �⺻���� ���� ĳ���Ͱ� �������� �� 
            for (int i = 0; i < Women_Men.Length; i++)
                Women_Men[i].SetActive(true);           // ���� ���� ��ư Ȱ��ȭ 
            publicplayerBackGround.SetActive(true);     // ĳ���� ���� �ڹ�� Ȱ��ȭ 

            pCount--;                                   // ������ Flag ���� ���� ( ���� Flag == 0 ) 
        }
        // �� ���� â���� �ƹ�Ÿ ���� â���� ���ư� �� 
        else if(pCount == 2) {
            publicItemBackGround.SetActive(true);       // �ƹ�Ÿ ���� �ڹ�� Ȱ��ȭ 
            publicItemList.SetActive(true);             // �ƹ�Ÿ üũ �ڽ� Ȱ��ȭ 
            publicNextButton.SetActive(true);           // ���� ��ư Ȱ��ȭ 

            RoomCreate_Input.SetActive(false);          // ĳ���� ���� ��ư ��Ȱ��ȭ

            pCount--;                                   // ������ Flag ���� ���� ( ���� Flag == 1 )     
        }
    }
    public void Women()
    {
        // ���� ĳ���� ��ư Ȱ��ȭ 
        for (int i = 0; i < 3; i++) {
            LobbyPanel_Player[i].SetActive(true);
            LobbyPanel_Player[i+3].SetActive(false);
        }
    }
    public void Men()
    {
        // ���� ĳ���� ��ư Ȱ��ȭ 
        for (int i = 0; i < 3; i++) {
            LobbyPanel_Player[i].SetActive(false);
            LobbyPanel_Player[i + 3].SetActive(true);
        }
    }

    // �� ���� ( �⺻������ ���� ��, ����, ����, �ܿ��� �ִ�. �̰��� ������� �������� �Ѵ�. ) 
    // ������ ���� ��ư�� ������ �ܿ��� ������ �ܿ￡�� ���� ��ư�� ������ ���� ���´�. 
    public void PublicCreateRoomNext()
    {
        // �������� �� �� ���� ��ư�� ����������� �� 
        if (publicRoomCount == 0) {             // ������ ���� 
            publicRoomCount++;
            MapRotation(publicRoomCount);
        }
        else if (publicRoomCount == 1) {        // �������� ���� 
            publicRoomCount++;
            MapRotation(publicRoomCount);
        }
        else if (publicRoomCount == 2) {        // �������� �ܿ� 
            publicRoomCount++;
            MapRotation(publicRoomCount);
        }
        else if (publicRoomCount == 3) {        // �ܿ￡�� ��
            publicRoomCount = 0;                // ������ ���� ���� �� ���� �ʱ�ȭ 
            MapRotation(publicRoomCount);
        }

        for (int i = 0; i < publicInputRoom.Length; i++)
            publicInputRoom[i].SetActive(false);
    }
    public void PublicCreateRoomPrevious()
    {
        // �������� �� �� ���� ��ư�� ����������� �� 
        if (publicRoomCount == 0) {             // ������ �ܿ�  
            publicRoomCount = 3;                // �ܿ� ������ �ױ� ���� �� ���� �ʱ�ȭ 
            MapRotation(publicRoomCount);
        }   
        else if (publicRoomCount == 1) {        // �������� �� 
            publicRoomCount--;
            MapRotation(publicRoomCount);
        }
        else if (publicRoomCount == 2) {        // �������� ���� 
            publicRoomCount--;
            MapRotation(publicRoomCount);
        }
        else if (publicRoomCount == 3) {        // �ܿ￡�� ���� 
            publicRoomCount--;
            MapRotation(publicRoomCount);
        }

        for (int i = 0; i < publicInputRoom.Length; i++)
            publicInputRoom[i].SetActive(false);
    }
    // �� ��ư�� ���� Ű�鼭 ��ư�� �����ش�. 
    public void MapRotation(int flag)
    {
        // �� ��ư�� ��ü������ �� 
        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(false);

         // ���� �� Flag ������ �´� �� ��ư�� Ȱ��ȭ ��Ŵ 
        publicCreateRoom[flag].SetActive(true);
    }

    // �� ���� ( �⺻������ ���� ��, ����, ����, �ܿ��� �ִ�. �̰��� ������� �������� �Ѵ�. ) 
    // ������ ���� ��ư�� ������ �ܿ��� ������ �ܿ￡�� ���� ��ư�� ������ ���� ���´�. 
    public void PublicInputRoomNext()
    {
        // ������� �� �� ���� ��ư�� ����������� �� 
        if (publicRoomCount == 0) {             // ������ ����  
            publicRoomCount++;
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 1) {        // �������� ���� 
            publicRoomCount++;
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 2) {        // �������� �ܿ� 
            publicRoomCount++;
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 3) {        // �ܿ￡�� ��
            publicRoomCount = 0;                // ������ ���� ���� �� ���� �ʱ�ȭ 
            MapRotationPlayer(publicRoomCount);
        }

        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(false);
    }

    public void PublicInputRoomPrevious()
    {
        if (publicRoomCount == 0) {             // ������ �ܿ�  
            publicRoomCount = 3;                // �ܿ� ������ �ױ� ���� �� ���� �ʱ�ȭ 
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 1) {        // �������� �� 
            publicRoomCount--;
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 2) {        // �������� ���� 
            publicRoomCount--;
            MapRotationPlayer(publicRoomCount);
        }
        else if (publicRoomCount == 3) {        // �ܿ￡�� ���� 
            publicRoomCount--;
            MapRotationPlayer(publicRoomCount);
        }

        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(false);
    }

    public void MapRotationPlayer(int flag)
    {
        // �� ��ư�� ��ü������ �� 
        for (int i = 0; i < publicInputRoom.Length; i++)
            publicInputRoom[i].SetActive(false);

        // ���� �� Flag ������ �´� �� ��ư�� Ȱ��ȭ ��Ŵ 
        publicInputRoom[publicRoomCount].SetActive(true);
    }
    //=============================== ���� ���� ��Ż���� �� �� �ִ� Button �̺�Ʈ ===============================//
    public void TeamInput_Select_Next()
    {
        // ���� ���忡�� ������ư�� �������� 
        for (int i = 0; i < TeamInput_Player.Length; i++)
            TeamInput_Player[i].SetActive(false);                   // ĳ���� ���� ��ư ��Ȱ��ȭ
        for (int i = 0; i < TeamInput_Women_Men.Length; i++)
            TeamInput_Women_Men[i].SetActive(false);                // ���� ���� ��ư ��Ȱ��ȭ 
        for (int i = 0; i < TeamInput_NextButton.Length; i++) 
            TeamInput_NextButton[i].SetActive(false);               // ���� ��ư ��Ȱ��ȭ ( ĳ���Ϳ� �ִ� ���� ��ư�� ��Ȱ��ȭ ) 
        playerBackGround.SetActive(false);                          // ĳ���� ���� �� ��� ��Ȱ��ȭ 

        TeamInput_PreviousButton.SetActive(true);                   // ���� ��ư Ȱ��ȭ 
        inputNextButton.SetActive(true);                            // ���� ��ư Ȱ��ȭ ( �ƹ�Ÿ ������ �� �������� �Ѿ�� ���� ��ư ) 

        iCount++;                                                   // �������� Ȯ���ϱ� ���� Flag ���� ( flag == 1 ) 
    }

    public void TeamInput_Select_Next_Button()
    {
        // �ƹ�Ÿ ���� ���� ���� ��ư�� ������ �� 
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();           // NetworkManager ��ũ��Ʈ�� �ִ� ������Ʈ�� �����´�. 
        for (int i = 0; i < networkManager.itemName.Length; i++) {
            Debug.Log(networkManager.itemName[i]);                  // �ƹ�Ÿ ����Ʈ�� ����� �����Դ��� Ȯ�� ( ����� )
        }
        InputItemList.SetActive(false);                             // �ƹ�Ÿ üũ�ڽ� ��Ȱ��ȭ 
        InputItemBackGround.SetActive(false);                       // �ƹ�Ÿ ���� �� ��� ��Ȱ��ȭ 
        TeamInputPrevious.SetActive(true);                          // �� ���� ���� ��ư Ȱ��ȭ 
        TeamInputNext.SetActive(true);                              // �� ���� ���� ��ư Ȱ��ȭ 

        InputplayerTeaminput.SetActive(true);                       // �� ���� UI Ȱ��ȭ 
        inputNextButton.SetActive(false);                           // ���� ��ư ��Ȱ��ȭ 
        iCount++;                                                   // �������� Ȯ���ϱ� ���� Flag ���� ( flag == 2 ) 
    }

    public void TeamInput_Select_Previous()
    {
        // ���� ��ư�� ������ �� �߻��ϴ� �̺�Ʈ 
        // �ƹ�Ÿ ����â���� ���� ��ư�� ������ �� 
        if (iCount == 1) {
            TeamInput_Women();
            for (int i = 0; i < TeamInput_Women_Men.Length; i++)
                TeamInput_Women_Men[i].SetActive(true);             // ���� ���� ��ư Ȱ��ȭ 

            TeamInput_PreviousButton.SetActive(false);              // ���� ��ư ��Ȱ��ȭ 
            TeamInputPrevious.SetActive(false);                     // ���� ��ư ��Ȱ��ȭ
            TeamInputNext.SetActive(false);                         // ���� ��ư ��Ȱ��ȭ 
            inputNextButton.SetActive(false);                       // ���� ��ư ��Ȱ��ȭ
            playerBackGround.SetActive(true);                       // ĳ���� ���� �� ��� Ȱ��ȭ 
            iCount--;                                               // ������ Ȯ���� ���� Flag ���� ( flag == 0 �� ) 
        }
        // �� ���ÿ��� ���� ��ư�� ������ �� 
        else if(iCount == 2) {                          
            InputItemList.SetActive(true);                          // �ƹ�Ÿ üũ�ڽ� Ȱ��ȭ 
            InputItemBackGround.SetActive(true);                    // �ƹ�Ÿ �� ��� Ȱ��ȭ 
            inputNextButton.SetActive(true);                        // ���� ��ư Ȱ��ȭ 
            InputplayerTeaminput.SetActive(false);                  // ������ UI ��Ȱ��ȭ 
            TeamInputPrevious.SetActive(false);                     // �� ���� ���� ��ư ��Ȱ��ȭ 
            TeamInputNext.SetActive(false);                         // �� ���� ���� ��ư ��Ȱ��ȭ 
            iCount--;                                               // ������ Ȯ���� ���� Flag ���� ( flag == 1 �� )        
        }
    }

    public void TeamInput_Women()
    {
        // ���� ĳ���� ��ư�� �����ϵ��� �� 
        for (int i = 0; i < 3; i++) {
            TeamInput_Player[i].SetActive(true);
            TeamInput_Player[i + 3].SetActive(false);
        }
    }
    public void TeamInput_Men()
    {
        // ���� ĳ���� ��ư�� �����ϵ��� �� 
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

    //=============================== �׸��� ���� �Լ� ===============================//
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
    //=============================== �α��� ���н� ���� ���� ��� ===============================//

    public void LoginErrorMessageClose()
    {
        // �α��� ���н� UI�� ǥ�����ش�. 
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        loginManager.rectLogin.anchoredPosition = new Vector2(400, 400);
    }
}
