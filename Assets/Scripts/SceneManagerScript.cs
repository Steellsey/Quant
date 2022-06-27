using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;                  // библиотека для работы со сценами

public class SceneManagerScript : MonoBehaviour
{
    public void NextLevel(int _sceneNumber)                  // Метод смены сцены с номером сцены
    {
        SceneManager.LoadScene(_sceneNumber);                  // Выбор сцены (может быть сценой 0,1)
         Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    public void Exit()                              // Метод выхода из приоложения
    {
        Application.Quit();                         // Сам выход
    }
}

   
