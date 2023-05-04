using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyPlayer : MonoBehaviourPunCallbacks {
    public float moveSpeed;
    public float speed = 3f;

    Rigidbody charRigidbody;
    public bool turnStop = false;

    public PhotonView PV;

    private Transform tr;

    private float yRotate, yRotateMove;
    public float rotateSpeed = 500.0f;
    public void Start()
    {
        //charRigidbody = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        Camera.main.GetComponent<SmoothFollow>().target = tr.Find("CamPivot").transform;
        transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
    //    // ISMine �����ΰ� �� �����̵��� ���� 
    //    // �� ��ǻ�Ϳ��� ������ �ϸ� ���� IsMine �����̴� 
    //    // ������ ������ ��ǻ�Ϳ����� IsMine ���°� �ƴϱ� ������ ���� if ���ǹ��� ���ٸ� ���� �����϶� �ٸ������ ���� �����δ�
    //    // ���� IsMine�� ���¿����� �����̵��� �ϸ� �� ��ǻ�Ϳ����� ���� �����δ�. 
    //    float hAxis = Input.GetAxisRaw("Horizontal");
    //    float vAxis = Input.GetAxisRaw("Vertical");

    //    Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;

        //if (Input.GetMouseButton(0)) {
        //    yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

        //    yRotate = transform.eulerAngles.y + yRotateMove;

        //    transform.eulerAngles = new Vector3(0, yRotate, 0);
        //}

    //    inputDir = Camera.main.transform.TransformDirection(inputDir);
    //    charRigidbody.velocity = inputDir * moveSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal") {
            MovePortal movePortal = GameObject.Find("PublicZoon").GetComponent<MovePortal>();
            movePortal.Enter();
        }
        else if (other.tag == "CreatePortal") {
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            networkManager.CreateTeam();

            

            ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();
            buttonEvent.TeamCreate_Women();
            for (int i = 0; i < buttonEvent.TeamCreate_Women_Men.Length; i++)
                buttonEvent.TeamCreate_Women_Men[i].SetActive(true);
            buttonEvent.TeamCreate_PreviousButton.SetActive(false);
            buttonEvent.CreateplayerBackGround.SetActive(true);
            buttonEvent.TeamCreateNext.SetActive(false);
            buttonEvent.TeamCreatePrevious.SetActive(false);
            buttonEvent.CreateItemList.SetActive(true);
            buttonEvent.CreateItemBackGround.SetActive(true);
            buttonEvent.count = 0;
        }
        else if (other.tag == "InputPortal") {
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            networkManager.InputTeam();

            ButtonEvent buttonEvent = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();
            buttonEvent.TeamInput_Women();
            for (int i = 0; i < buttonEvent.TeamInput_Women_Men.Length; i++)
                buttonEvent.TeamInput_Women_Men[i].SetActive(true);
            buttonEvent.TeamInput_PreviousButton.SetActive(false);
            buttonEvent.TeamInputNext.SetActive(false);
            buttonEvent.TeamInputPrevious.SetActive(false);
            buttonEvent.playerBackGround.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Portal") {
            MovePortal movePortal = GameObject.Find("PublicZoon").GetComponent<MovePortal>();
            movePortal.Exit();
        }
        else if (other.tag == "CreatePortal") {
            MovePortal movePortal = GameObject.Find("CreateTeamZoon").GetComponent<MovePortal>();
            movePortal.Exit();
        }
        else if (other.tag == "InputPortal") {
            MovePortal movePortal = GameObject.Find("InputTeamZoon").GetComponent<MovePortal>();
            movePortal.Exit();
        }
    }
}
