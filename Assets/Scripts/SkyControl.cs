using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SkyControl : MonoBehaviour
{
    public static SkyControl skycon;

    public Material morningmat;
    public Material nightmat;
    public GameObject morning;
    public GameObject night;

  //  public Color morningfog;
  //  public Color nightfog;

    public bool ismorning = true;

    public PhotonView PV;
    
    public void Start()
    {
        skycon = this;
        RenderSettings.skybox = morningmat;
    }

    
    public void Update()
    {
        PV.RPC("movesky", RpcTarget.All);
    }

    public void OnTriggerEnter(Collider other)
    {   // Player 태그를 가진 NPC의 콜라이더에 들어왔을 때 낮,밤 전환
        if (other.tag == "Player")
        {
            if (ismorning == true)
            {
                PV.RPC("changenight", RpcTarget.All);
            }
            else
            {
                PV.RPC("changemorning", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void movesky()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.6f);
    }


    [PunRPC]
    public void changemorning()
    {
        RenderSettings.skybox = morningmat;
      //  RenderSettings.fogColor = morningfog;
        morning.SetActive(true);
        night.SetActive(false);
        ismorning = true;
    }

    [PunRPC]
    public void changenight()
    {
        RenderSettings.skybox = nightmat;
      //  RenderSettings.fogColor = nightfog;
        morning.SetActive(false);
        night.SetActive(true);
        ismorning = false;
    }
}
