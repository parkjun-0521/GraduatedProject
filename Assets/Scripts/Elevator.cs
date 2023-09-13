using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using Photon.Pun;

public class Elevator : MonoBehaviour
{
    public static Elevator elevator;

    [Header("Button")]
    public GameObject button1f;
    public GameObject button2f;
    public GameObject buttonin;
    public GameObject buttonupdown;

    [Header("Door")]
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;

    [Header("Floor")]
    public GameObject floor1;

    public Animator[] evanim;

    public PhotonView PV;

    public int floorvalue = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (elevator == null)
        {
            elevator = this;
        }
        evanim = GetComponentsInChildren<Animator>();

    }

    private void Update()
    {
       // Elevator_work();
        Elevator_work2();
    }

    [PunRPC]
    public void Eledooropen1f()
    {
        evanim[0].SetTrigger("isClick");
        evanim[1].SetTrigger("isleft");
    }
    [PunRPC]
    public void Eledooropen2f()
    {
        evanim[2].SetTrigger("isClick2F");
        evanim[3].SetTrigger("isLeft2F");
    }
    [PunRPC]
    public void Eleup()
    {
        evanim[4].SetTrigger("Up");
    }
    [PunRPC]
    public void Eledown()
    {
        evanim[4].SetTrigger("Down");
    }

    [PunRPC]
    public void Elevator_work()
    {
        if (Input.GetMouseButtonDown(0))
    	    {
        	    RaycastHit hitInfo = new RaycastHit();
        	    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "elebtn")
        	    {
                    Debug.Log("Click");
                    if (floor1.transform.position.y < -7)
                    {
                        PV.RPC("Elevator.elevator.Eledooropen1f", RpcTarget.All);
                    }
                    else if (floor1.transform.position.y > -5)
                    {
                        PV.RPC("Elevator.elevator.Eledooropen2f", RpcTarget.All);
                    }
                    else if (floor1.transform.position.y < -7)
                    {
                        PV.RPC("Elevator.elevator.Eleup", RpcTarget.All);
                    }
                    else if (floor1.transform.position.y > -5)
                    {
                        PV.RPC("Elevator.elevator.Eledown", RpcTarget.All);
                    }
                }
    	    }
    }
    public void Elevator_work2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "elebtn")
            {
                Debug.Log("btnClick");
                Debug.Log(floor1.transform.position.y);
                if (floor1.transform.position.y < 1)
                {
                    evanim[0].SetTrigger("isClick");
                    evanim[1].SetTrigger("isleft");
                }
                else
                {
                    evanim[2].SetTrigger("isClick2F");
                    evanim[3].SetTrigger("isLeft2F");
                }              
            }
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.name == "Elebtn1")
            {
                Debug.Log("1F_Click");
                Debug.Log(floor1.transform.position.y);
                if (floor1.transform.position.y > 1)
                {                              
                    evanim[4].SetTrigger("Down");
                    floorvalue = 1;
                }
                else
                {
                    Debug.Log("You're already on the 1F!");
                }
            }
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.name == "Elebtn2")
            {
                Debug.Log("2F_Click");
                Debug.Log(floor1.transform.position.y);
                if (floor1.transform.position.y < 1)
                {
                    evanim[4].SetTrigger("Up");
                    floorvalue = 2;
                }
                else
                {
                    Debug.Log("You're already on the 2F!");
                }
            }
        }
    }
}

