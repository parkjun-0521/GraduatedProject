using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private Transform cam;

    Rigidbody rb;
    Animator anim;
    CapsuleCollider col;
    Camera pcam;

    [Header("Move")]
    bool ismove = false;
    public float moveX;
    public float moveZ;
    public float Speed = 0.08f;
    bool isjump = false;
    public float jumppower = 50.0f;

    bool jumpD;
    public float camspeed;

    void keyInput()
    {
        jumpD = Input.GetButtonDown("Jump");
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        pcam = GetComponentInChildren<Camera>();

    }

    void Update()
    {
        pjump();
    }
    void FixedUpdate()
    {
        keyInput();
        pmove();     
        pray();
      
    }


    public void pmove()
    {
        Vector3 moveVec = new Vector3(moveX, 0f, moveZ);
        ismove = moveVec.magnitude != 0;
        anim.SetBool("ismove", ismove);
        if (ismove)
        {
        
        }
        //  LookMouseCursor();


        // anim.transform.forward = moveVec;
        this.transform.forward = moveVec;
        transform.Translate(new Vector3(moveX, 0f, moveZ).normalized * Speed);
    }

    public void pjump()
    {
        if (jumpD && !isjump)
        {
            isjump = true;
            anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumppower, ForceMode.Impulse);
        }
    }

    public void pray()
    {
        //Debug.DrawRay(rb.position, transform.up * 0.5f, new Color(250, 0, 0));
        if (Physics.Raycast(transform.position, transform.up * -1, 0.4f, LayerMask.GetMask("ground")))
        {
            //if (rb.velocity.y <= 0)
                isjump = false;
        }
    }
   
}
