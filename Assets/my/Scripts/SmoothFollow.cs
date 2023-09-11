using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {
    // �÷��̾ �������� ����ٴϴ� ī�޶� 

    // ������ �Ǵ� Ÿ���� ���� 
    public Transform target;

    // ī�޶��� ȸ������ ������ ���� ���� 
    private float xRotate, yRotate, xRotateMove, yRotateMove;

    // ī�޶� ȸ�� �ӵ� 
    public float rotateSpeed = 400.0f;

    private Transform tr;
    public float curAngle;

    // ī�޶� ȸ���� �������� ������ Flag ���� 
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
        // Ÿ���� ���� ��� �����߻��� �������� ���� ó�� 
        if (!target)
            return;

        // Ÿ���� �ְ� ���� ī�޶� ȸ���ϴ� ���� ���� 
        if (Input.GetMouseButton(1) && !turnOff) // ������ ���콺�� Ŭ���� ���
        {
            // ���Ʒ��δ� ���� �ǵ��ѰŶ� �ݴ�� �����̵����ؾ� ���� �÷��� �� ���� ī�޶� ������ ��ȯ�� 
            xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            yRotate = transform.eulerAngles.y + yRotateMove;
            xRotate = xRotate + xRotateMove;

            // �� �Ʒ� �ִ� ���� ���� ī�޶� 360�� ȸ������ �ʵ��� �Ѵ�. 
            xRotate = Mathf.Clamp(xRotate, -80, 80); // ��, �Ʒ� ����

            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
        }
        curAngle = transform.rotation.y;
        transform.position = target.position;

        // ī�޶� Ÿ���� �׻� �ٶ󺸵����Ѵ�. 
        transform.LookAt(target);
    }
}