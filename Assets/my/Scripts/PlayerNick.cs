using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNick : MonoBehaviourPunCallbacks {
    public PhotonView PV;
    public TMP_Text customText;

   
    void Awake()
    {
        StartCoroutine(Nickname());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) {
            StartCoroutine(SyncDataCoroutine());
        }
    }
    public IEnumerator Nickname()
    {
        yield return new WaitForSeconds(0.3f);
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        customText.text = networkManager.nick;
        Debug.Log(customText.text);
    }

    public IEnumerator SyncDataCoroutine()
    {
        // 데이터가 동기화될 때까지 대기
        yield return new WaitForSeconds(0.3f);
        NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        if (PV != null) {
            if (PV.IsMine == false) {
                customText.text = "";
                customText.text = PV.Owner.NickName;
            }
        }
        Debug.Log(networkManager.nick);
    }
}
