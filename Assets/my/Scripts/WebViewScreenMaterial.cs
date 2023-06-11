using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WebViewScreenMaterial : MonoBehaviourPunCallbacks {
    public RenderTexture renderTexture;
    public Material targetMaterial;
    public PhotonView PV;

    void Update()
    {
        PV.RPC("MaterialSet", RpcTarget.All, PV.ViewID);
    }

    [PunRPC]
    void MaterialSet(int photonViewID)
    {
        PhotonView targetPhotonView = PhotonView.Find(photonViewID);
        if (targetPhotonView != null) {
            Renderer renderer = targetPhotonView.GetComponent<Renderer>();
            if (renderer != null) {
                Material targetMaterial = renderer.sharedMaterial;
                targetMaterial.mainTexture = renderTexture;
            }
        }
    }
}
