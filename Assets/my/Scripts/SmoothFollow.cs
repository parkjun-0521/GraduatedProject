using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {
    public Transform target;
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 500.0f;

    private Transform tr;
    public float curAngle;

    public bool turnOff = false;
    public void Start()
    {
        tr = GetComponent<Transform>();
        roteta();
    }
    public void roteta()
    {
        transform.localRotation = Quaternion.identity;
    }
    void Update()
    {
        if (!target)
            return;
        if (Input.GetMouseButton(1) && !turnOff) // 클릭한 경우
        {
            xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            yRotate = transform.eulerAngles.y + yRotateMove;
            //xRotate = transform.eulerAngles.x + xRotateMove; 

            xRotate = xRotate + xRotateMove;

            xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
        }
        curAngle = transform.rotation.y;
        transform.position = target.position;
        transform.LookAt(target);

    }
}