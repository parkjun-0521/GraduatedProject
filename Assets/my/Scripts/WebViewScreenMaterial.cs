using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WebViewScreenMaterial : MonoBehaviourPunCallbacks {
    public RenderTexture renderTexture;     // ���並 ȭ���� ������ �ִ� ��ü���� 
    public Material targetMaterial;         // ���� ������Ʈ�� Material 
    public PhotonView PV;                   // ����ȭ�� ���� ���� ���� 

    void Update()
    {
        // RPC ����ȭ ( ����� ������ ����ȭ ) 
        PV.RPC("MaterialSet", RpcTarget.All, PV.ViewID);
    }

    [PunRPC]
    void MaterialSet(int photonViewID)
    {
        // ������� ������ �����´�
        PhotonView targetPhotonView = PhotonView.Find(photonViewID);

        // ���������Ʈ�� Material�� renderTexture�� ���߰� �ִ� ������ ����
        if (targetPhotonView != null) {
            Renderer renderer = targetPhotonView.GetComponent<Renderer>();
            if (renderer != null) {
                Material targetMaterial = renderer.sharedMaterial;
                targetMaterial.mainTexture = renderTexture;
            }
        }
    }
}
