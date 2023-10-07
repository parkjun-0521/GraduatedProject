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
        // 로비캐릭터의 CamPivot 오브젝트를 카메라에 target 변수에 저장 
        // 이로써 로비캐릭터에 회전하는 카메라를 부착
        tr = GetComponent<Transform>();
        Camera.main.GetComponent<SmoothFollow>().target = tr.Find("CamPivot").transform;
        transform.localRotation = Quaternion.identity;
    }

    // 충돌했을 때 
    public void OnTriggerEnter(Collider other)
    {
        // 로비맵에 있는 3개의 포탈에 각각 입장하였을 때 
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

    // 충돌범위에서 나왔을 때 
    public void OnTriggerExit(Collider other)
    {
        // 로비맵에 있는 3개의 포탈 범위에서 벗어났을 때 
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
