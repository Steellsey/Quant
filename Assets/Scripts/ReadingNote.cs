using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class ReadingNote : MonoBehaviour
{
    [SerializeField]private GameObject PlayerArmature;
    private ThirdPersonController thirdPersonController;
    
    [SerializeField]private GameObject NotePanel; // UI панель, с которой будет происходить взаимодействие
    [SerializeField]private GameObject message; // Текстовое поле UI, в котором будет отображаться текст записки
	[SerializeField]private TextAsset Message; // Файл с текстом записки
	[SerializeField]private GameObject PressFText;
	
	private bool enter; // Находится ли игрок в коллайдере записки
	private bool ReadMenuOpened; // Открыта ли записка
	private bool open_close_ON; // Находится ли в движении


    void Awake(){
        thirdPersonController = PlayerArmature.GetComponent<ThirdPersonController>();

		
    }

    void Update()
    {
		if(ReadMenuOpened){ //Удержание меню
			Cursor.lockState = CursorLockMode.Confined;
       		Cursor.visible = true;
			Time.timeScale = 0f;
			NotePanel.SetActive (true);
			thirdPersonController.LockCameraPosition = true;
		}
		if(Input.GetKeyDown(KeyCode.F) && enter){ // Передача нажатия кнопки открытия записки
			open_close_ON = true;
		}
    }

    void FixedUpdate(){
        if(open_close_ON){ // Если идёт попытка открыть записку
            ReadMenuOpened = true; // Переключение меню
			open_close_ON = false;
        }
    }

	void OnTriggerEnter(Collider col)
	{
		if(Message.text != null) message.GetComponent<Text>().text = Message.text;
		if (col.tag == "Player") {
			enter = true;
			PressFText.SetActive (true);
			PressFText.GetComponent<Text>().text = "Нажмите F чтобы открыть запись";
			}
		}

    void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player") {
			enter = false;
			PressFText.SetActive (false);
		}
	}

	public void Leave(){ // При нажатии кнопки "Закрыть"
		ReadMenuOpened = false;
		thirdPersonController.LockCameraPosition = false;
		Time.timeScale = 1f;
		NotePanel.SetActive (false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
