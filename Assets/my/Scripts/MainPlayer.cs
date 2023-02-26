using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MainPlayer : MonoBehaviourPunCallbacks
{
    public float moveSpeed;
    public  float speed = 3f;

    Rigidbody charRigidbody;
    public bool turnStop = false;

    public PhotonView PV;

    private Transform tr;

    void Start()
    {
        charRigidbody = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        if (PV.IsMine)
            Camera.main.GetComponent<SmoothFollow>().target = tr.Find("CamPivot").transform;
    }

    void Update()
    {
        // ISMine 상태인것 만 움직이도록 설정 
        // 내 컴퓨터에서 접속을 하면 내가 IsMine 상태이다 
        // 하지만 상태의 컴퓨터에서는 IsMine 상태가 아니기 때문에 만약 if 조건문이 없다면 내가 움직일때 다른사람도 같이 움직인다
        // 따라서 IsMine인 상태에서만 움직이도록 하면 내 컴퓨터에서는 나만 움직인다. 
        if (PV.IsMine) {
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");

            Vector3 inputDir = new Vector3(hAxis, 0, vAxis).normalized;

            //if(!turnStop)
                //transform.LookAt(transform.position + inputDir);
            if (Input.GetMouseButton(0)) {
                transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
            }
            inputDir = Camera.main.transform.TransformDirection(inputDir);
            charRigidbody.velocity = inputDir * moveSpeed;
        }
    }
}
