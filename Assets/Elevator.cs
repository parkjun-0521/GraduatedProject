using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class Elevator : MonoBehaviour
{
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

    private String btntag;
    private uint isup=0;
    // Start is called before the first frame update
    void Start()
    {
        evanim = GetComponentsInChildren<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        buttonray();
    }

    void buttonray()
    {
        Ray btnray1 = new Ray(button1f.transform.position - new Vector3(0,1.0f,0), transform.right);
        RaycastHit hitinfo;
        Debug.DrawRay(button1f.transform.position - new Vector3(0, 1.0f, 0), transform.right, new Color(1,0,0));
        if (Physics.Raycast(btnray1, out hitinfo, 1f))
        {
            btntag = hitinfo.collider.tag;
            if (btntag == "Player" && Input.GetButtonDown("interact") && isup == 0)
            {
                Debug.Log(btntag);
                evanim[0].SetTrigger("isClick");
                evanim[1].SetTrigger("isClick");
            }
            else if(btntag == "Player" && Input.GetButtonDown("interact") && isup == 1)
            {
                evanim[2].SetTrigger("isClick");
                evanim[3].SetTrigger("isClick");
            }
        }

        Ray btnray2 = new Ray(button2f.transform.position - new Vector3(0, 1.0f, 0), transform.right);
        RaycastHit hitinfo2;
        Debug.DrawRay(button2f.transform.position - new Vector3(0, 1.0f, 0), transform.right, new Color(1, 0, 0));
        if (Physics.Raycast(btnray2, out hitinfo2, 1f))
        {
            btntag = hitinfo2.collider.tag;
            if (btntag == "Player" && Input.GetButtonDown("interact") && isup == 0)
            {
                Debug.Log(btntag);
                evanim[0].SetTrigger("isClick");
                evanim[1].SetTrigger("isClick");
            }
            else if (btntag == "Player" && Input.GetButtonDown("interact") && isup == 1)
            {
                evanim[2].SetTrigger("isClick");
                evanim[3].SetTrigger("isClick");
            }
        }

        Ray btnray3 = new Ray(buttonin.transform.position, transform.right * -1);
        RaycastHit hitinfo3;
        Debug.DrawRay(buttonin.transform.position, transform.right * -1, new Color(1, 0, 0));
        if (Physics.Raycast(btnray3, out hitinfo3, 1f))
        {
            btntag = hitinfo3.collider.tag;
            if (btntag == "Player" && Input.GetButtonDown("interact") && isup == 0)
            {
                Debug.Log(btntag);
                evanim[0].SetTrigger("isClick");
                evanim[1].SetTrigger("isClick");
            }
            else if (btntag == "Player" && Input.GetButtonDown("interact") && isup == 1)
            {
                evanim[2].SetTrigger("isClick");
                evanim[3].SetTrigger("isClick");
            }
        }

        Ray btnray4 = new Ray(buttonupdown.transform.position + new Vector3(0.6f, 1.0f, -0.2f), transform.forward);
        RaycastHit hitinfo4;
        Debug.DrawRay(buttonupdown.transform.position + new Vector3(0.6f, 1.0f, -0.2f), transform.forward, new Color(1, 0, 0));
        if (Physics.Raycast(btnray4, out hitinfo4, 1f))
        {
            btntag = hitinfo4.collider.tag;
            if (btntag == "Player" && Input.GetButtonDown("interact") && isup == 0)
            {              
                evanim[4].SetTrigger("Up");
                isup = 1;
            }
            else if(btntag == "Player" && Input.GetButtonDown("interact") && isup == 1)
            {                
                evanim[4].SetTrigger("Down");
                isup = 0;
            }
        }
    }
}

