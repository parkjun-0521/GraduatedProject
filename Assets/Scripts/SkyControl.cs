using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SkyControl : MonoBehaviour
{
    public static SkyControl skycon;

    public Material morningmat;
    public Material nightmat;
    public Material sunsetmat;
 //   public GameObject morning;
 //   public GameObject night;
 //   public GameObject sunset;

  //  public Color morningfog;
  //  public Color nightfog;

    public bool ismorning = true;
    public bool issunset = false;

    public PhotonView PV;
    
    public void Start()
    {
        skycon = this;
        RenderSettings.skybox = morningmat;
    }

    
    public void Update()
    {
 //       PV.RPC("movesky", RpcTarget.All);
        movesky();
    }

    public void OnTriggerEnter(Collider other)
    {   // Player 태그를 가진 NPC의 콜라이더에 들어왔을 때 낮,밤 전환
        if (other.CompareTag("Player"))
        {
            if (ismorning == true)
            {
                PV.RPC("changesunset", RpcTarget.AllBuffered);
                issunset = true;
                ismorning = false;
           //     changenight();
            }
            else if (issunset == true)
            {
                PV.RPC("changenight", RpcTarget.AllBuffered);
                issunset = false;
                
            }
            else
            {
                PV.RPC("changemorning", RpcTarget.AllBuffered);
                ismorning = true;
           //     changemorning();
            }
        }
    }

    
    public void movesky()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.6f);
    }


    [PunRPC]
    public void changemorning()
    {
        if(RenderSettings.skybox == nightmat)
        {
            RenderSettings.skybox = morningmat;
          /* RenderSettings.fogColor = morningfog;
            morning.SetActive(true);
            night.SetActive(false);
            sunset.SetActive(false);*/
            ismorning = true;
        }
    }

    [PunRPC]
    public void changesunset()
    {
        if(RenderSettings.skybox == morningmat)
        {
            RenderSettings.skybox = sunsetmat;
           /* sunset.SetActive(true);
            morning.SetActive(false);
            night.SetActive(false);*/
        }       
    }

    [PunRPC]
    public void changenight()
    {
        if(RenderSettings.skybox == sunsetmat)
        {
            RenderSettings.skybox = nightmat;
          /*  RenderSettings.fogColor = nightfog;
            morning.SetActive(false);
            night.SetActive(true);
            sunset.SetActive(false);*/
            ismorning = false;
        }
    }
}
