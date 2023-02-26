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
        // ISMine �����ΰ� �� �����̵��� ���� 
        // �� ��ǻ�Ϳ��� ������ �ϸ� ���� IsMine �����̴� 
        // ������ ������ ��ǻ�Ϳ����� IsMine ���°� �ƴϱ� ������ ���� if ���ǹ��� ���ٸ� ���� �����϶� �ٸ������ ���� �����δ�
        // ���� IsMine�� ���¿����� �����̵��� �ϸ� �� ��ǻ�Ϳ����� ���� �����δ�. 
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
