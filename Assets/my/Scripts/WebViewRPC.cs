using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuplex.WebView;
using Photon.Pun;

public class WebViewRPC : MonoBehaviour
{
    public static WebViewRPC Instance;

    public WebViewPrefab _WebViewPrefab;
    public GameObject _Prefab;
    public PhotonView PV;

    public string currentURL = string.Empty;

    private void Awake()
    {
        Instance = this;
    }
    async public void Initalize()
    {

        _WebViewPrefab = _Prefab?.GetComponent<WebViewPrefab>();
 

        await _WebViewPrefab.WaitUntilInitialized();
   
        _WebViewPrefab.WebView.UrlChanged += (sender, eventArgs) => {
            Debug.Log("[SimpleWebViewDemo] URL changed: " + eventArgs.Url);
            OnURLEvent(eventArgs.Url);
        };
    }

    public void OnURLEvent(string URL)
    { 
        currentURL = URL;
        Debug.Log(currentURL);
        PV.RPC("SetURL", RpcTarget.All);
    }


    [PunRPC]
    public void SetURL()
    {
        _WebViewPrefab.WebView.Reload();
        _WebViewPrefab?.WebView?.LoadUrl(currentURL);
    }
}
