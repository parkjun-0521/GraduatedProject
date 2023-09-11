using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class WebViewArrangement : MonoBehaviourPunCallbacks {
    
    // 웹뷰 동기화를 위한 변수 선언 
    public GameObject[] webViewScreen;  // 웹뷰를 찍고 있는 카메라 저장 
    public GameObject[] webViewObject;  // 웹뷰 오브젝트 전체를 저장 
}
