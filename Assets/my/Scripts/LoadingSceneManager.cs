using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour {

    public GameObject lodingPanel;

    [SerializeField] 
    Image progressBar;
    
    public float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (progressBar.fillAmount < 0.4f) {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.6f, timer);
        }
        else if(progressBar.fillAmount >= 0.4f && progressBar.fillAmount < 0.7f) {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.8f, timer);
        }
        else if (progressBar.fillAmount >= 0.7f && progressBar.fillAmount < 0.98f) {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
        }
        else {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 2f, timer);
            StartCoroutine(CloseLoding());
        }
    }
    IEnumerator CloseLoding()
    {
        yield return new WaitForSeconds(0.8f);
        if(progressBar.fillAmount == 1) {
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            timer = 0.0f;
            progressBar.fillAmount = 0.0f;
            lodingPanel.SetActive(false);
            networkManager.Server.SetActive(true);

            if (networkManager.checkRoom) {
                networkManager.RoomPanel.SetActive(true);
                networkManager.Server.SetActive(false);
            }
        }
    }
}