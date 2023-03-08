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

    //====================================================================//
    public GameObject[] TeamInput_Player;
    public GameObject[] TeamInput_NextButton;
    public GameObject TeamInput_PreviousButton;

    public GameObject[] TeamInput_Women_Men;

    public GameObject playerBackGround;

    public GameObject TeamInputRoom;
    public GameObject TeamInputNext;
    public GameObject TeamInputPrevious;

    //====================================================================//
    public GameObject[] TeamCreate_Player;
    public GameObject[] TeamCreate_NextButton;
    public GameObject TeamCreate_PreviousButton;

    public GameObject[] TeamCreate_Women_Men;

    public GameObject[] TeamCreateMap;
    public GameObject TeamRoomCreate;

    public GameObject CreateplayerBackGround;

    public GameObject TeamCreateRoom;
    public GameObject TeamCreateNext;
    public GameObject TeamCreatePrevious;
    public int count = 0;

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
        RoomCreate_Input.SetActive(true);
        for(int i =0;i< LobbyPanel_NextButton.Length; i++)    
            LobbyPanel_NextButton[i].SetActive(false);
        LobbyPanel_PreviousButton.SetActive(true);
    }

    public void LobbyPanel_Select_Previous()
    {
        Women();
        for (int i = 0; i < Women_Men.Length; i++)
            Women_Men[i].SetActive(true);
        RoomCreate_Input.SetActive(false);
        //LobbyPanel_NextButton.SetActive(true);
        LobbyPanel_PreviousButton.SetActive(false);
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
        for (int i = 0; i < 3; i++)
            publicCreateRoom[i].SetActive(false);
        for (int i = 3; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(true);

        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicInputRoom[i].SetActive(false);
    }

    public void PublicCreateRoomPrevious()
    {
        for (int i = 0; i < 3; i++)
            publicCreateRoom[i].SetActive(true);
        for (int i = 3; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(false);

        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicInputRoom[i].SetActive(false);
    }
    
    public void PublicInputRoomNext()
    {
        for (int i = 0; i < 3; i++)
            publicInputRoom[i].SetActive(false);
        for (int i = 3; i < publicCreateRoom.Length; i++)
            publicInputRoom[i].SetActive(true);

        for (int i = 0; i < publicCreateRoom.Length; i++)
            publicCreateRoom[i].SetActive(false);
    }

    public void PublicInputRoomPrevious()
    {
        for (int i = 0; i < 3; i++)
            publicInputRoom[i].SetActive(true);
        for (int i = 3; i < publicCreateRoom.Length; i++)
            publicInputRoom[i].SetActive(false);

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
        TeamInputNext.SetActive(true);
        TeamInputPrevious.SetActive(true);
        playerBackGround.SetActive(false);
    }

    public void TeamInput_Select_Previous()
    {
        TeamInput_Women();
        for (int i = 0; i < TeamInput_Women_Men.Length; i++)
            TeamInput_Women_Men[i].SetActive(true);
        for (int i = 0; i < TeamInput_NextButton.Length; i++)
            TeamInput_NextButton[i].SetActive(true);
        TeamInput_PreviousButton.SetActive(false);
        TeamInputNext.SetActive(false);
        TeamInputPrevious.SetActive(false);
        playerBackGround.SetActive(true);
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
            TeamRoomCreate.SetActive(true);
            TeamCreateRoomPrevious();
            TeamCreate_PreviousButton.SetActive(true);
            CreateplayerBackGround.SetActive(true);
            count++;
        }
        else if(count == 1) {
            CreateplayerBackGround.SetActive(false);
            for(int i =0; i<TeamCreate_NextButton.Length; i++)
                TeamCreate_NextButton[i].SetActive(false);
            TeamRoomCreate.SetActive(false);
            TeamCreateNext.SetActive(true);
            TeamCreatePrevious.SetActive(true);
            count++;
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
            count--;
        }
        else if(count == 2) {
            CreateplayerBackGround.SetActive(true);
            for (int i = 0; i < TeamCreate_NextButton.Length; i++)
                TeamCreate_NextButton[i].SetActive(true);
            TeamRoomCreate.SetActive(true);
            TeamCreateNext.SetActive(false);
            TeamCreatePrevious.SetActive(false);
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
        for (int i = 0; i < 3; i++)
            TeamCreateMap[i].SetActive(false);
        for (int i = 3; i < TeamCreateMap.Length; i++)
            TeamCreateMap[i].SetActive(true);

    }

    public void TeamCreateRoomPrevious()
    {
        for (int i = 0; i < 3; i++)
            TeamCreateMap[i].SetActive(true);
        for (int i = 3; i < TeamCreateMap.Length; i++)
            TeamCreateMap[i].SetActive(false);

    }

    //-----------------------------------------------------------------------------------//

    public void OptionClose()
    {
        if (Lobby.activeSelf == true) {
            Lobbyplayerthird lobbyplayerthird = GameObject.Find("Female1").GetComponent<Lobbyplayerthird>();
            SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
            option.SetActive(false);
            optionCheck = false;
            lobbyplayerthird.MoveSpeed = 50f;
            lobbyplayerthird.SprintSpeed = 80f;
            lobbyplayerthird.turnStop = false;
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
            ThirdPersonController thirdPersonController = GameObject.Find("Female1").GetComponent<ThirdPersonController>();
            SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
            option.SetActive(true);
            optionCheck = true;
            thirdPersonController.MoveSpeed = 0f;
            thirdPersonController.SprintSpeed = 0f;
            thirdPersonController.turnStop = true;
            smoothFollow.turnOff = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && roomPanel.activeSelf == true && Lobby.activeSelf == false && !optionCheck) {
            ThirdPersonController thirdPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
            SmoothFollow smoothFollow = GameObject.Find("Main Camera").GetComponent<SmoothFollow>();
            option.SetActive(true);
            optionCheck = true;
            thirdPersonController.MoveSpeed = 0f;
            thirdPersonController.SprintSpeed = 0f;
            thirdPersonController.turnStop = true;
            smoothFollow.turnOff = true;
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
}
