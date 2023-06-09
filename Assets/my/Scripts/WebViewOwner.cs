using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WebViewOwner : MonoBehaviourPunCallbacks 
{
    // 캐럭터가 입장시 권한을 넘겨줌 
    // 2번째 캐릭터 입장 시 2번째 웹뷰를 넘겨주고 웹뷰 카메라 활성화 // 방 나갈시 웹뷰 카메라 끄기 
    // 3번째 캐릭터 입장 시 3번째 웹뷰를 넘겨줌 

    public GameObject[] webViewCam;

    void Start()
    {
        
    }
}
