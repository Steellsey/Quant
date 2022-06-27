using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class MenuPaused : MonoBehaviour
{
    public GameObject otherGameObject;
    private ThirdPersonController thirdPersonController;

    public static bool GameIsPaused = false;

    public GameObject Menu;
    // Update is called once per frame

    void Awake(){
        thirdPersonController = otherGameObject.GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                
            }
            else
            {
                Pause();
                
            }
        }
    }
    public void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1f;
        thirdPersonController.LockCameraPosition = false;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
         
    }

    public void Pause()
    {
        Menu.SetActive(true);
        Time.timeScale = 0f;
        //LockCameraPosition = true;
        thirdPersonController.LockCameraPosition = true;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        
    }

    public void Settings()
    {
      
        SceneManager.LoadScene(4);
    }
}