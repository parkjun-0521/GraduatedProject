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
            MovePortal movePortal = GameObject.Find("PublicZoon").GetComponent<MovePortal>();
            movePortal.CreateEnter();
        }
        else if (other.tag == "InputPortal") {
            MovePortal movePortal = GameObject.Find("PublicZoon").GetComponent<MovePortal>();
            movePortal.InputEnter();
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
