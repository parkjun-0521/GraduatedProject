using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class customizing : MonoBehaviour
{
    public GameObject[] character_body = new GameObject[6];
    private Color[] color = new Color[5];
    [Header("Materials")]
    [Tooltip("Customizing your character")]
    public Material[] body_mat;
    private Material[] hair_mat = new Material[3];
    private Color[] hair_def = new Color[3];

    public GameObject[] fe1ava = new GameObject[8];
    public GameObject[] fe2ava = new GameObject[8];
    public GameObject[] fe3ava = new GameObject[8];
    public GameObject[] m1ava = new GameObject[8];
    public GameObject[] m2ava = new GameObject[8];
    public GameObject[] m3ava = new GameObject[8];


    NetworkManager netmgr;
    ButtonEvent btnevn;

    void Start()
    {
        netmgr = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        btnevn = GameObject.Find("ButtonEvent").GetComponent<ButtonEvent>();

        color[0] = new Color(150 / 255f, 0f, 0f);  // red
        color[1] = new Color(0f, 20 / 255f, 150 / 255f); // blue
        color[2] = new Color(200 / 255f, 200 / 255f, 80 / 255f);  // gold
        color[3] = new Color(80 / 255f, 30 / 255f, 90 / 255f);  // violet
        color[4] = new Color(130 / 255f, 130 / 255f, 130 / 255f);  // gray


    }
    private void FixedUpdate()
    {
        netmgr.ColorAvatar();
        netmgr.HatAvatar();
        netmgr.TopAvatar();
        netmgr.PantsAvatar();
        netmgr.BagAvatar();
        avatar_fe1();
    }

    // body_mat과 hair_mat들을 선택된 캐릭터에 맞게 바꿔주기
    public void body_changeFe1()
    {
        body_mat = character_body[0].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[0];         // 머리1
        hair_mat[1] = body_mat[1];        // 머리2
        hair_def[0] = hair_mat[0].color;      // 원래머리1
        hair_def[1] = hair_mat[1].color;    // 원래머리2
    }

    public void body_changeFe2()
    {
        body_mat = character_body[1].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[0];
        hair_mat[1] = body_mat[14];
        hair_def[0] = hair_mat[0].color;
        hair_def[1] = hair_mat[1].color;
    }
    public void body_changeFe3()
    {
        body_mat = character_body[2].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[0];
        hair_mat[1] = body_mat[8];
        hair_def[0] = hair_mat[0].color;
        hair_def[1] = hair_mat[1].color;
    }
    public void body_changeM1()
    {
        body_mat = character_body[3].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[0];
        hair_mat[1] = body_mat[7];
        hair_def[0] = hair_mat[0].color;
        hair_def[1] = hair_mat[1].color;
    }
    public void body_changeM2()
    {
        body_mat = character_body[4].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[2];
        hair_mat[1] = body_mat[3];
        hair_mat[2] = body_mat[1];
        hair_def[0] = hair_mat[0].color;
        hair_def[1] = hair_mat[1].color;
        hair_def[2] = hair_mat[2].color;
    }
    public void body_changeM3()
    {
        body_mat = character_body[5].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[0];
        hair_mat[1] = body_mat[14];
        hair_def[0] = hair_mat[0].color;
        hair_def[1] = hair_mat[1].color;
    }

    // 아바타 착용
    public void avatar_fe1()
    {
        // 선택된 체크박스의 Text와 일치하는 머리색
        if (netmgr.itemName[0].Equals("color0"))
        {
            hair_mat[0].color = hair_def[0];
            hair_mat[1].color = hair_def[1];
        }
        else if(netmgr.itemName[0].Equals("color1"))
        {
            hair_mat[0].color = color[0];
            hair_mat[1].color = color[0];
        }
        else if (netmgr.itemName[0].Equals("color2"))
        {
            hair_mat[0].color = color[1];
            hair_mat[1].color = color[1];
        }
        else if (netmgr.itemName[0].Equals("color3"))
        {
            hair_mat[0].color = color[2];
            hair_mat[1].color = color[2];
        }
        else if (netmgr.itemName[0].Equals("color4"))
        {
            hair_mat[0].color = color[3];
            hair_mat[1].color = color[3];
        }
        else if (netmgr.itemName[0].Equals("color5"))
        {
            hair_mat[0].color = color[4];
            hair_mat[1].color = color[4];
        }

        // 선택된 체크박스의 Text와 일치하는 모자
        if (netmgr.itemName[1].Equals("hat0"))
        {
            for(int i=0; i<4; i++)
            {
                fe1ava[i].SetActive(false);
            }
            fe1ava[0].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat1"))
        {
            for (int i = 0; i < 4; i++)
            {
                fe1ava[i].SetActive(false);
            }
            fe1ava[1].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat2"))
        {
            for (int i = 0; i < 4; i++)
            {
                fe1ava[i].SetActive(false);
            }
            fe1ava[2].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat3"))
        {
            for (int i = 0; i < 4; i++)
            {
                fe1ava[i].SetActive(false);
            }
            fe1ava[3].SetActive(true);
        }

        // 선택된 체크박스의 Text와 일치하는 상의
        if (netmgr.itemName[2].Equals("top0"))
        {
            fe1ava[4].SetActive(true);
        }

        // 선택된 체크박스의 Text와 일치하는 하의
        if (netmgr.itemName[3].Equals("pants0"))
        {
            fe1ava[5].SetActive(true);
        }

        // 선택된 체크박스의 Text와 일치하는 가방
        if (netmgr.itemName[5].Equals("bag0"))
        {
            fe1ava[7].SetActive(false);
            fe1ava[6].SetActive(true);
        }
        else if (netmgr.itemName[5].Equals("bag1"))
        {
            fe1ava[6].SetActive(false);
            fe1ava[7].SetActive(true);
        }
    }
}
