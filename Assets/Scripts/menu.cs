using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    [Header("SelectMenu")]
    public GameObject selmenu;
    public bool isMenuOn = false;

    private ChangeCharacter pl;
    
    // Start is called before the first frame update
    void Start()
    {
        pl= GetComponent<ChangeCharacter>();

    }

    // Update is called once per frame
    void Update()
    {    
        SelectMenu();
    }

    public void SelectMenu()
    { 
        

            if (!isMenuOn)
            {
                if (Input.GetButtonDown("menu"))
                {
                    selmenu.SetActive(true);
                    isMenuOn = true;
                    Cursor.lockState = CursorLockMode.None;
                    for (int i = 0; i < pl.player.Length; i++)
                    {
                        pl.player[i].GetComponent<ThirdPersonController>().enabled = false;
                    }
                }
            }
            else if (isMenuOn)
            {
                if (Input.GetButtonDown("menu"))
                {
                    selmenu.SetActive(false);
                    isMenuOn = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    for (int i = 0; i < pl.player.Length; i++)
                    {
                        pl.player[i].GetComponent<ThirdPersonController>().enabled = true;
                    }       
            }
        }
    }

    public void CursorLock()
    {      
        Cursor.lockState = CursorLockMode.Locked;
        for (int i = 0; i < pl.player.Length; i++)
        {
            pl.player[i].GetComponent<ThirdPersonController>().enabled = true;
        }
        isMenuOn = false;

    }
    
}
