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
        PV.RPC("MaterialSet", RpcTarget.All);
    }

    [PunRPC]
    void MaterialSet()
    {
        targetMaterial.mainTexture = renderTexture;
    }
}
