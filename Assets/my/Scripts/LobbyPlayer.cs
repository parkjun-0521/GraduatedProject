using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyPlayer : MonoBehaviourPunCallbacks {

    public bool turnStop = false;
    public float rotateSpeed = 500.0f;
    private Transform tr;

    public void Start()
    {
        // �κ�ĳ������ CamPivot ������Ʈ�� ī�޶� target ������ ���� 
        // �̷ν� �κ�ĳ���Ϳ� ȸ���ϴ� ī�޶� ����
        tr = GetComponent<Transform>();
        Camera.main.GetComponent<SmoothFollow>().target = tr.Find("CamPivot").transform;
        transform.localRotation = Quaternion.identity;
    }

    // �浹���� �� 
    public void OnTriggerEnter(Collider other)
    {
        // �κ�ʿ� �ִ� 3���� ��Ż�� ���� �����Ͽ��� �� 
        if (other.tag == "Portal") {
            MovePortal movePortal = GameObject.Find("PublicZoon").GetComponent<MovePortal>();
            movePortal.Enter();
        }
        else if (other.tag == "CreatePortal") {
            MovePortal movePortal = GameObject.Find("CreateTeamZoon").GetComponent<MovePortal>();
            movePortal.CreateEnter();
        }
        else if (other.tag == "InputPortal") {
            MovePortal movePortal = GameObject.Find("InputTeamZoon").GetComponent<MovePortal>();
            movePortal.InputEnter();
        }
    }

    // �浹�������� ������ �� 
    public void OnTriggerExit(Collider other)
    {
        // �κ�ʿ� �ִ� 3���� ��Ż �������� ����� �� 
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
