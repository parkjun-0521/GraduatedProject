using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;

public class customizing : MonoBehaviour
{
    public GameObject[] character_body = new GameObject[6];
    private Color[] color = new Color[5];
    [Header("Materials")]
    [Tooltip("Customizing your character")]
    public Material[] body_mat;
    private Material[] hair_mat = new Material[3];
    private Color[] hair_def = new Color[3];

    public GameObject[] fe1ava = new GameObject[10];
    public GameObject[] fe2ava = new GameObject[10];
    public GameObject[] fe3ava = new GameObject[10];
    public GameObject[] m1ava = new GameObject[10];
    public GameObject[] m2ava = new GameObject[10];
    public GameObject[] m3ava = new GameObject[10];

    //어떤 캐릭터가 선택됐는지 체크하기위함
    private int a_chk = 0;

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
        if (netmgr.itemName[0] != null)
        {
            //팀방 만드는 경우
            if(netmgr.createTeam.activeSelf == true)
            {
                netmgr.ColorAvatar();
                netmgr.HatAvatar();
                netmgr.TopAvatar();
                netmgr.PantsAvatar();
                netmgr.BagAvatar();
            }

            //공개방 들어가는 경우
            if(netmgr.lodingPanel.activeSelf == true)
            {
                netmgr.ColorPublicAvatar();
                netmgr.HatPublicAvatar();
                netmgr.TopPublicAvatar();
                netmgr.PantsPublicAvatar();
                netmgr.BagPublicAvatar();
            }

            //팀방 입장하는 경우
            if (netmgr.inputTeam.activeSelf == true)
            {
                netmgr.InputColorAvatar();
                netmgr.InputHatAvatar();
                netmgr.InputTopAvatar();
                netmgr.InputPantsAvatar();
                netmgr.InputBagAvatar();
            }

            //어떤 캐릭터가 선택됬는지 체크
            if(a_chk == 0)
            {
              avatar_fe1();
            }
            if(a_chk == 1)
            {
              avatar_fe2();
            }
            if(a_chk == 2)
            {
              avatar_fe3();
            }
            if(a_chk == 3)
            {
              avatar_m1();
            }
            if(a_chk == 4)
            {
              avatar_m2();
            }
            if(a_chk == 5)
            {
              avatar_m3();
            }
        }
    }

    // body_mat과 hair_mat들을 선택된 캐릭터에 맞게 바꿔주기
    public void body_changeFe1()
    {
        body_mat = character_body[0].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[0];         // 머리1
        hair_mat[1] = body_mat[1];        // 머리2
        hair_def[0] = hair_mat[0].color;      // 원래머리1
        hair_def[1] = hair_mat[1].color;    // 원래머리2
        a_chk = 0;
    }

    public void body_changeFe2()
    {
        body_mat = character_body[1].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[0];
        hair_mat[1] = body_mat[14];
        hair_def[0] = hair_mat[0].color;
        hair_def[1] = hair_mat[1].color;
        a_chk = 1;
    }
    public void body_changeFe3()
    {
        body_mat = character_body[2].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[0];
        hair_mat[1] = body_mat[8];
        hair_def[0] = hair_mat[0].color;
        hair_def[1] = hair_mat[1].color;
        a_chk = 2;
    }
    public void body_changeM1()
    {
        body_mat = character_body[3].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[0];
        hair_mat[1] = body_mat[7];
        hair_def[0] = hair_mat[0].color;
        hair_def[1] = hair_mat[1].color;
        a_chk = 3;
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
        a_chk = 4;
    }
    public void body_changeM3()
    {
        body_mat = character_body[5].GetComponent<SkinnedMeshRenderer>().materials;
        hair_mat[0] = body_mat[0];
        hair_mat[1] = body_mat[14];
        hair_def[0] = hair_mat[0].color;
        hair_def[1] = hair_mat[1].color;
        a_chk = 5;
    }

    // 아바타 착용
    public void avatar_fe1()
    {
        // 선택된 체크박스의 Text와 일치하는 머리색
        if(netmgr.itemName[0].Equals("color1"))
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
        else                                             // 기본머리
        {
            hair_mat[0].color = hair_def[0];
            hair_mat[1].color = hair_def[1];
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
        else
        {
            for(int i=0; i<4; i++)
            fe1ava[i].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 상의     
        if (netmgr.itemName[2].Equals("top0"))
        {
            fe1ava[4].SetActive(true);
        }
        else
        {
            fe1ava[4].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 하의
        if (netmgr.itemName[3].Equals("pants1"))
        {
            fe1ava[5].SetActive(true);
        }
        else
        {
            fe1ava[5].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 가방
        if (netmgr.itemName[5].Equals("bag0"))
        {
            fe1ava[6].SetActive(true);
            fe1ava[7].SetActive(false);
        }

        else if (netmgr.itemName[5].Equals("bag1"))
        {
            fe1ava[6].SetActive(false);
            fe1ava[7].SetActive(true);
        }
        else
        {
            fe1ava[6].SetActive(false);
            fe1ava[7].SetActive(false);
        }
    }
    public void avatar_fe2()
    {
        // 선택된 체크박스의 Text와 일치하는 머리색
        if (netmgr.itemName[0].Equals("color0"))
        {
            hair_mat[0].color = hair_def[0];
            hair_mat[1].color = hair_def[1];
        }
        else if (netmgr.itemName[0].Equals("color1"))
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
            for (int i = 0; i < 4; i++)
            {
                fe2ava[i].SetActive(false);
            }
            fe2ava[0].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat1"))
        {
            for (int i = 0; i < 4; i++)
            {
                fe2ava[i].SetActive(false);
            }
            fe2ava[1].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat2"))
        {
            for (int i = 0; i < 4; i++)
            {
                fe2ava[i].SetActive(false);
            }
            fe2ava[2].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat3"))
        {
            for (int i = 0; i < 4; i++)
            {
                fe2ava[i].SetActive(false);
            }
            fe2ava[3].SetActive(true);
        }
        else
        {
            for(int i=0;i<4; i++)
            {
                fe2ava[i].SetActive(false);
            }
        }

        // 선택된 체크박스의 Text와 일치하는 상의     
        if (netmgr.itemName[2].Equals("top0"))
        {
            fe2ava[4].SetActive(true);
        }
        else
        {
            fe2ava[4].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 하의
        if (netmgr.itemName[3].Equals("pants1"))
        {
            fe2ava[5].SetActive(true);
        }
        else
        {
            fe2ava[5].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 가방
        if (netmgr.itemName[5].Equals("bag0"))
        {
            fe2ava[6].SetActive(true);
            fe2ava[7].SetActive(false);
        }

        else if (netmgr.itemName[5].Equals("bag1"))
        {
            fe2ava[6].SetActive(false);
            fe2ava[7].SetActive(true);
        }
        else
        {
            fe2ava[6].SetActive(false);
            fe2ava[7].SetActive(false);
        }
    }
    public void avatar_fe3()
    {
        // 선택된 체크박스의 Text와 일치하는 머리색
        if (netmgr.itemName[0].Equals("color0"))
        {
            hair_mat[0].color = hair_def[0];
            hair_mat[1].color = hair_def[1];
        }
        else if (netmgr.itemName[0].Equals("color1"))
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
            for (int i = 0; i < 4; i++)
            {
                fe3ava[i].SetActive(false);
            }
            fe3ava[0].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat1"))
        {
            for (int i = 0; i < 4; i++)
            {
                fe3ava[i].SetActive(false);
            }
            fe3ava[1].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat2"))
        {
            for (int i = 0; i < 4; i++)
            {
                fe3ava[i].SetActive(false);
            }
            fe3ava[2].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat3"))
        {
            for (int i = 0; i < 4; i++)
            {
                fe3ava[i].SetActive(false);
            }
            fe3ava[3].SetActive(true);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                fe3ava[i].SetActive(false);
            }
        }

        // 선택된 체크박스의 Text와 일치하는 상의     
        if (netmgr.itemName[2].Equals("top0"))
        {
            fe3ava[4].SetActive(true);
        }
        else
        {
            fe3ava[4].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 하의
        if (netmgr.itemName[3].Equals("pants1"))
        {
            fe3ava[5].SetActive(true);
        }
        else
        {
            fe3ava[5].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 가방
        if (netmgr.itemName[5].Equals("bag0"))
        {
            fe3ava[6].SetActive(true);
            fe3ava[7].SetActive(false);
        }

        else if (netmgr.itemName[5].Equals("bag1"))
        {
            fe3ava[6].SetActive(false);
            fe3ava[7].SetActive(true);
        }
        else
        {
            fe3ava[6].SetActive(false);
            fe3ava[7].SetActive(false);
        }   
    }
    public void avatar_m1()
    {
        // 선택된 체크박스의 Text와 일치하는 머리색
        if (netmgr.itemName[0].Equals("color0"))
        {
            hair_mat[0].color = hair_def[0];
            hair_mat[1].color = hair_def[1];
        }
        else if (netmgr.itemName[0].Equals("color1"))
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
            for (int i = 0; i < 4; i++)
            {
                m1ava[i].SetActive(false);
            }
            m1ava[0].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat1"))
        {
            for (int i = 0; i < 4; i++)
            {
                m1ava[i].SetActive(false);
            }
            m1ava[1].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat2"))
        {
            for (int i = 0; i < 4; i++)
            {
                m1ava[i].SetActive(false);
            }
            m1ava[2].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat3"))
        {
            for (int i = 0; i < 4; i++)
            {
                m1ava[i].SetActive(false);
            }
            m1ava[3].SetActive(true);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                m1ava[i].SetActive(false);
            }
        }

        // 선택된 체크박스의 Text와 일치하는 상의     
        if (netmgr.itemName[2].Equals("top0"))
        {
            m1ava[4].SetActive(true);
        }
        else
        {
            m1ava[4].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 하의
        if (netmgr.itemName[3].Equals("pants1"))
        {
            m1ava[5].SetActive(true);
        }
        else
        {
            m1ava[5].SetActive(false);
        }   

        // 선택된 체크박스의 Text와 일치하는 가방
        if (netmgr.itemName[5].Equals("bag0"))
        {
            m1ava[6].SetActive(true);
            m1ava[7].SetActive(false);
        }

        else if (netmgr.itemName[5].Equals("bag1"))
        {
            m1ava[6].SetActive(false);
            m1ava[7].SetActive(true);
        }  
        else
        {
            m1ava[6].SetActive(false);
            m1ava[7].SetActive(false);
        }
    }
    public void avatar_m2()
    {
        // 선택된 체크박스의 Text와 일치하는 머리색
        if (netmgr.itemName[0].Equals("color0"))
        {
            hair_mat[0].color = hair_def[0];
            hair_mat[1].color = hair_def[1];
        }
        else if (netmgr.itemName[0].Equals("color1"))
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
            for (int i = 0; i < 4; i++)
            {
                m2ava[i].SetActive(false);
            }
            m2ava[0].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat1"))
        {
            for (int i = 0; i < 4; i++)
            {
                m2ava[i].SetActive(false);
            }
            m2ava[1].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat2"))
        {
            for (int i = 0; i < 4; i++)
            {
                m2ava[i].SetActive(false);
            }
            m2ava[2].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat3"))
        {
            for (int i = 0; i < 4; i++)
            {
                m2ava[i].SetActive(false);
            }
            m2ava[3].SetActive(true);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                m2ava[i].SetActive(false);
            }
        }

        // 선택된 체크박스의 Text와 일치하는 상의     
        if (netmgr.itemName[2].Equals("top0"))
        {
            m2ava[4].SetActive(true);
        }
        else
        {
            m2ava[4].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 하의
        if (netmgr.itemName[3].Equals("pants1"))
        {
            m2ava[5].SetActive(true);
        }
        else
        {
            m2ava[5].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 가방
        if (netmgr.itemName[5].Equals("bag0"))
        {
            m2ava[6].SetActive(true);
            m2ava[7].SetActive(false);
        }

        else if (netmgr.itemName[5].Equals("bag1"))
        {
            m2ava[6].SetActive(false);
            m2ava[7].SetActive(true);
        }
        else
        {
            m2ava[6].SetActive(false);
            m2ava[7].SetActive(false);
        }
    }
    public void avatar_m3()
    {
        // 선택된 체크박스의 Text와 일치하는 머리색
        if (netmgr.itemName[0].Equals("color0"))
        {
            hair_mat[0].color = hair_def[0];
            hair_mat[1].color = hair_def[1];
        }
        else if (netmgr.itemName[0].Equals("color1"))
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
            for (int i = 0; i < 4; i++)
            {
                m3ava[i].SetActive(false);
            }
            m3ava[0].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat1"))
        {
            for (int i = 0; i < 4; i++)
            {
                m3ava[i].SetActive(false);
            }
            m3ava[1].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat2"))
        {
            for (int i = 0; i < 4; i++)
            {
                m3ava[i].SetActive(false);
            }
            m3ava[2].SetActive(true);
        }
        else if (netmgr.itemName[1].Equals("hat3"))
        {
            for (int i = 0; i < 4; i++)
            {
                m3ava[i].SetActive(false);
            }
            m3ava[3].SetActive(true);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                m3ava[i].SetActive(false);
            }
        }

        // 선택된 체크박스의 Text와 일치하는 상의     
        if (netmgr.itemName[2].Equals("top0"))
        {
            m3ava[4].SetActive(true);
        }
        else
        {
            m3ava[4].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 하의
        if (netmgr.itemName[3].Equals("pants1"))
        {
            m3ava[5].SetActive(true);
        }
        else
        {
            m3ava[5].SetActive(false);
        }

        // 선택된 체크박스의 Text와 일치하는 가방
        if (netmgr.itemName[5].Equals("bag0"))
        {
            m3ava[6].SetActive(true);
            m3ava[7].SetActive(false);
        }

        else if (netmgr.itemName[5].Equals("bag1"))
        {
            m3ava[6].SetActive(false);
            m3ava[7].SetActive(true);
        }
        else
        {
            m3ava[6].SetActive(false);
            m3ava[7].SetActive(false);
        }
    }
}
