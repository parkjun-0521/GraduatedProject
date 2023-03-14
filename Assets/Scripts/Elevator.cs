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
        evanim[2].SetTrigger("isClick");
        evanim[3].SetTrigger("isleft");
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
}

