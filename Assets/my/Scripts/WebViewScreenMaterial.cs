using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WebViewScreenMaterial : MonoBehaviourPunCallbacks {
    public RenderTexture renderTexture;     // 웹뷰를 화면을 가지고 있는 객체생성 
    public Material targetMaterial;         // 현재 오브젝트의 Material 
    public PhotonView PV;                   // 동기화를 위한 포톤 변수 

    void Update()
    {
        // RPC 동기화 ( 변경된 물질을 동기화 ) 
        PV.RPC("MaterialSet", RpcTarget.All, PV.ViewID);
    }

    [PunRPC]
    void MaterialSet(int photonViewID)
    {
        // 포톤뷰의 정보를 가져온다
        PhotonView targetPhotonView = PhotonView.Find(photonViewID);

        // 현재오브젝트의 Material을 renderTexture가 비추고 있는 것으로 변경
        if (targetPhotonView != null) {
            Renderer renderer = targetPhotonView.GetComponent<Renderer>();
            if (renderer != null) {
                Material targetMaterial = renderer.sharedMaterial;
                targetMaterial.mainTexture = renderTexture;
            }
        }
    }
}
