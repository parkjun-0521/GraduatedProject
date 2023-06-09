using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class ButtonEvent : MonoBehaviour
{
    //====================================================================//
    public GameObject RoomCreate_Input;
    public GameObject[] LobbyPanel_Player;
    public GameObject[] LobbyPanel_NextButton;
    public GameObject LobbyPanel_PreviousButton;

    public GameObject[] Women_Men;

    public GameObject[] publicCreateRoom;
    public GameObject[] publicInputRoom;

    public GameObject publicplayerBackGround;
    public GameObject publicItemBackGround;

    public GameObject publicItemList;
    public GameObject publicNextButton;
    public GameObject publicPreviousButton;

    public int pCount = 0;
    public int publicRoomCount = 3;
    //====================================================================//
    public GameObject[] TeamInput_Player;
    public GameObject[] TeamInput_NextButton;
    public GameObject TeamInput_PreviousButton;

    public GameObject[] TeamInput_Women_Men;

    public GameObject playerBackGround;

    public GameObject TeamInputRoom;
    public GameObject TeamInputNext;
    public GameObject TeamInputPrevious;

    public GameObject InputplayerTeaminput;
    public GameObject InputItemBackGround;

    public GameObject InputItemList;
    public GameObject inputNextButton;
    public int iCount = 0;
    //====================================================================//
    public GameObject[] TeamCreate_Player;
    public GameObject[] TeamCreate_NextButton;
    public GameObject TeamCreate_PreviousButton;

    public GameObject[] TeamCreate_Women_Men;

    public GameObject[] TeamCreateMap;
    public GameObject TeamRoomCreate;

    public GameObject CreateplayerBackGround;
    public GameObject CreateItemBackGround;
    public GameObject CreateItemSelectBackGround;
    public GameObject CreateRoomBackGround;

    public GameObject CreateItemList;
    public GameObject CreateTeamNextSelect;
    public GameObject CreateTeamPreSelect;

    public GameObject TeamCreateRoom;
    public GameObject TeamCreateNext;
    public GameObject TeamCreatePrevious;
    public int count = 0;

    public int roomCount = 0;
    //====================================================================//
    public GameObject option;
    public GameObject soundPanel;
    bool soundOptionCheck = false;


    public GameObject Lobby;
    public GameObject roomPanel;
    bool optionCheck = false;

    //====================================================================//
    public Button Mic;
    public Button Speker;
    public Recorder NetworkVoiceManager;
    public PunVoiceClient punVoiceClient;

    int micCount = 0;
    public Image curImage;     //기존에 존제하는 이미지
    public Sprite changeSprite;   //바뀌어질 이미지
    public Sprite changeCurSprite;

    int spekerCount = 0;
    public Image curSpekerImage;     //기존에 존제하는 이미지
    public Sprite changeSpekerSprite;   //바뀌어질 이미지
    public Sprite changeCurSpekerSprite;

    //=====================================================================//
    public void WithRium_Move()
    {
        Application.OpenURL("http://withrium.com/"); // 링크 걸 사이트 주소
    }
    public void WithRium_UserCreate()
    {
        Application.OpenURL("http://withrium.com/signup"); // 링크 걸 사이트 주소
    }

    //====================================================================//
    public void LobbyPanel_Select_Next()
    {
        for(int i = 0; i < LobbyPanel_Player.Length; i++) 
            LobbyPanel_Player[i].SetActive(false);
        for (int i = 0; i < Women_Men.Length; i++)
            Women_Men[i].SetActive(false);
        for(int i =0;i< LobbyPanel_NextButton.Length; i++)    
            LobbyPanel_NextButton[i].SetActive(false);
        LobbyPanel_PreviousButton.SetActive(true);
        publicplayerBackGround.SetActive(false);
        publicNextButton.SetActive(true);
        publicPreviousButton.SetActive(true);
        pCount++;
    }

    public void LobbyPanel_Next_Button()
    {
        publicRoomCount = 0;
        Debug.Log(publicRoomCount);
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        for (int i = 0; i < networkManager.itemName.Length; i++) {
            Debug.Log(networkManager.itemName[i]);
        }
        publicItemBackGround.SetActive(false);
        publicItemList.SetActive(false);
        RoomCreate_Input.SetActive(true);
        publicNextButton.SetActive(false);
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
        pCount++;
    }

    public void LobbyPanel_Select_Previous()
    {
        if (pCount == 1) {
            Women();
            for (int i = 0; i < Women_Men.Length; i++)
                Women_Men[i].SetActive(true);
            RoomCreate_Input.SetActive(false);
            //LobbyPanel_NextButton.SetActive(true);
            LobbyPanel_PreviousButton.SetActive(false);
            publicPreviousButton.SetActive(false);
            publicplayerBackGround.SetActive(true);
            publicNextButton.SetActive(false);
            pCount--;
        }
        else if(pCount == 2) {
            publicItemBackGround.SetActive(true);
            publicItemList.SetActive(true);
            RoomCreate_Input.SetActive(false);
            publicNextButton.SetActive(true);
            pCount--;
        }
    }

    public void Women()
    {
        for (int i = 0; i < 3; i++) {
            LobbyPanel_Player[i].SetActive(true);
            LobbyPanel_Player[i+3].SetActive(false);
        }
    }
    public void Men()
    {
        for (int i = 0; i < 3; i++) {
            LobbyPanel_Player[i].SetActive(false);
            LobbyPanel_Player[i + 3].SetActive(true);
        }
    }

    public void PublicCreateRoomNext()
    {
        if (publicRoomCount == 0) {
            publicRoomCount++;
            for (int i = 0; i < publicCreateRoom.Length; i++)
                publicCreateRoom[i].SetActive(false);
            publicCreateRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 1) {
            publicRoomCount++;
            for (int i = 0; i < publicCreateRoom.Length; i++)
                publicCreateRoom[i].SetActive(false);
            publicCreateRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 2) {
            publicRoomCount++;
            for (int i = 0; i < publicCreateRoom.Length; i++)
                publicCreateRoom[i].SetActive(false);
            publicCreateRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 3) {
            publicRoomCount = 0;
            for (int i = 0; i < publicCreateRoom.Length; i++)
                publicCreateRoom[i].SetActive(false);
            publicCreateRoom[publicRoomCount].SetActive(true);
        }

        for (int i = 0; i < publicInputRoom.Length; i++)
            publicInputRoom[i].SetActive(false);
    }

    public void PublicCreateRoomPrevious()
    {
        if (publicRoomCount == 0) {
            publicRoomCount = 3;
            for (int i = 0; i < publicCreateRoom.Length; i++)
                publicCreateRoom[i].SetActive(false);
            publicCreateRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 1) {
            publicRoomCount--;
            for (int i = 0; i < publicCreateRoom.Length; i++)
                publicCreateRoom[i].SetActive(false);
            publicCreateRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 2) {
            publicRoomCount--;
            for (int i = 0; i < publicCreateRoom.Length; i++)
                publicCreateRoom[i].SetActive(false);
            publicCreateRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 3) {
            publicRoomCount--;
            for (int i = 0; i < publicCreateRoom.Length; i++)
                publicCreateRoom[i].SetActive(false);
            publicCreateRoom[publicRoomCount].SetActive(true);
        }

        for (int i = 0; i < publicInputRoom.Length; i++)
            publicInputRoom[i].SetActive(false);
    }
    
    public void PublicInputRoomNext()
    {
        if (publicRoomCount == 0) {
            publicRoomCount++;
            for (int i = 0; i < publicInputRoom.Length; i++)
                publicInputRoom[i].SetActive(false);
            publicInputRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 1) {
            publicRoomCount++;
            for (int i = 0; i < publicInputRoom.Length; i++)
                publicInputRoom[i].SetActive(false);
            publicInputRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 2) {
            publicRoomCount++;
            for (int i = 0; i < publicInputRoom.Length; i++)
                publicInputRoom[i].SetActive(false);
            publicInputRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 3) {
            publicRoomCount = 0;
            for (int i = 0; i < publicInputRoom.Length; i++)
                publicInputRoom[i].SetActive(false);
            publicInputRoom[publicRoomCount].SetActive(true);
        }

        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(false);
    }

    public void PublicInputRoomPrevious()
    {
        if (publicRoomCount == 0) {
            publicRoomCount = 3;
            for (int i = 0; i < publicInputRoom.Length; i++)
                publicInputRoom[i].SetActive(false);
            publicInputRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 1) {
            publicRoomCount--;
            for (int i = 0; i < publicInputRoom.Length; i++)
                publicInputRoom[i].SetActive(false);
            publicInputRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 2) {
            publicRoomCount--;
            for (int i = 0; i < publicInputRoom.Length; i++)
                publicInputRoom[i].SetActive(false);
            publicInputRoom[publicRoomCount].SetActive(true);
        }
        else if (publicRoomCount == 3) {
            publicRoomCount--;
            for (int i = 0; i < publicInputRoom.Length; i++)
                publicInputRoom[i].SetActive(false);
            publicInputRoom[publicRoomCount].SetActive(true);
        }


        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(false);
    }
    //========================================================================//
    public void TeamInput_Select_Next()
    {
        for (int i = 0; i < TeamInput_Player.Length; i++)
            TeamInput_Player[i].SetActive(false);
        for (int i = 0; i < TeamInput_Women_Men.Length; i++)
            TeamInput_Women_Men[i].SetActive(false);
        for (int i = 0; i < TeamInput_NextButton.Length; i++) 
            TeamInput_NextButton[i].SetActive(false);
        TeamInput_PreviousButton.SetActive(true);
        playerBackGround.SetActive(false);
        inputNextButton.SetActive(true);
        iCount++;
    }

    public void TeamInput_Select_Next_Button()
    {
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        for (int i = 0; i < networkManager.itemName.Length; i++) {
            Debug.Log(networkManager.itemName[i]);
        }
        InputItemList.SetActive(false);
        InputItemBackGround.SetActive(false);
        TeamInputPrevious.SetActive(true);
        TeamInputNext.SetActive(true);

        InputplayerTeaminput.SetActive(true);
        inputNextButton.SetActive(false);
        iCount++;
    }

    public void TeamInput_Select_Previous()
    {
        if (iCount == 1) {
            TeamInput_Women();
            for (int i = 0; i < TeamInput_Women_Men.Length; i++)
                TeamInput_Women_Men[i].SetActive(true);

            TeamInput_PreviousButton.SetActive(false);
            TeamInputNext.SetActive(false);
            TeamInputPrevious.SetActive(false);
            playerBackGround.SetActive(true);
            inputNextButton.SetActive(false);
            iCount--;
        }
        else if(iCount == 2) {
            InputItemList.SetActive(true);
            InputItemBackGround.SetActive(true);
            inputNextButton.SetActive(true);
            InputplayerTeaminput.SetActive(false);
            TeamInputPrevious.SetActive(false);
            TeamInputNext.SetActive(false);
            iCount--;
        }
    }

    public void TeamInput_Women()
    {
        for (int i = 0; i < 3; i++) {
            TeamInput_Player[i].SetActive(true);
            TeamInput_Player[i + 3].SetActive(false);
        }
    }
    public void TeamInput_Men()
    {
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
                TeamCreate_Player[i].SetActive(false);
            for (int i = 0; i < TeamCreate_Women_Men.Length; i++)
                TeamCreate_Women_Men[i].SetActive(false);
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
            count--;
        }
        else if (count == 2) {
            TeamRoomCreate.SetActive(false);
            CreateItemList.SetActive(true);
            CreateTeamNextSelect.SetActive(true);
            CreateplayerBackGround.SetActive(true);
            CreateItemSelectBackGround.SetActive(false);
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

    //-----------------------------------------------------------------------------------//

    public void LoginErrorMessageClose()
    {
        LoginManager loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();

        loginManager.rectLogin.anchoredPosition = new Vector2(400, 400);
    }
}
