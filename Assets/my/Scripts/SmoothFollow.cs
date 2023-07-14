using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {
    // 플레이어를 기준으로 따라다니는 카메라 

    // 기준이 되는 타겟을 지정 
    public Transform target;

    // 카메라의 회전값을 저장할 변수 선언 
    private float xRotate, yRotate, xRotateMove, yRotateMove;

    // 카메라 회전 속도 
    public float rotateSpeed = 400.0f;

    private Transform tr;
    public float curAngle;

    // 카메라 회전을 막기위해 선언한 Flag 변수 
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
        // 타겟이 없을 경우 에러발생을 막기위해 예외 처리 
        if (!target)
            return;

        // 타겟이 있고 실제 카메라를 회전하느 로직 구간 
        if (Input.GetMouseButton(1) && !turnOff) // 오른쪽 마우스를 클릭한 경우
        {
            // 위아래로는 내가 의도한거랑 반대로 움직이도록해야 위로 올렸을 때 위로 카메라 시점이 전환된 
            xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            yRotate = transform.eulerAngles.y + yRotateMove;
            xRotate = xRotate + xRotateMove;

            // 위 아래 최대 값을 고정 카메라가 360도 회전하지 않도록 한다. 
            xRotate = Mathf.Clamp(xRotate, -80, 80); // 위, 아래 고정

            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
        }
        curAngle = transform.rotation.y;
        transform.position = target.position;

        // 카메라가 타겟을 항상 바라보도록한다. 
        transform.LookAt(target);
    }
}