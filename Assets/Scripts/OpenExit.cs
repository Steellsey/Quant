using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using UnityEngine.SceneManagement; 

public class OpenExit : MonoBehaviour
{
    public GameObject otherGameObject;
    private ThirdPersonController thirdPersonController;

	public GameObject OpenDoorPanel; // UI панель, с которой будет происходить взаимодействие
	public GameObject EndPanel; // UI панель, с которой будет происходить взаимодействие
	public InputField inputF;
	public GameObject alert;
	public GameObject message;
	public GameObject PressFText;

	[SerializeField]private string Password;
	[SerializeField]private string Message;

	private bool PasswordMenuOpened; // Открыто ли меню для ввода пароля
	private bool enter; // Находится ли игрок в коллайдере
	private bool open_close_ON;

	public AudioSource done_sound;
	public AudioSource error_sound;

	//public GameObject interaction_image;


    private void Awake(){
        thirdPersonController = otherGameObject.GetComponent<ThirdPersonController>();
    }

	private void Update(){
		if(PasswordMenuOpened){ //Удержание меню
			Cursor.lockState = CursorLockMode.Confined;
       		Cursor.visible = true;
			Time.timeScale = 0f;
			OpenDoorPanel.SetActive (true);
			thirdPersonController.LockCameraPosition = true;
		}
		if(Input.GetKeyDown(KeyCode.F) && !open_close_ON && enter){ // Передача нажатия кнопки открытия терминала
			open_close_ON = true;
		}
	}

	private void FixedUpdate () {
		if(open_close_ON){ // Если идёт попытка открыть терминал
                PasswordMenuOpened = true; // Переключение меню ввода пароля
                open_close_ON = false;
		}
		if(enter){
			PressFText.GetComponent<Text>().text = "Нажмите F чтобы открыть Терминал";
			PressFText.SetActive (true);
		}
}

	private void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			enter = true;
			if(Message != null) message.GetComponent<Text>().text = Message;
		}
	}

    private void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player") {
			enter = false;
			PressFText.SetActive (false);
			alert.SetActive (false);
		}
	}

    public void EnterPassword(){ // При нажатии кнопки "Ввести пароль"
		if (inputF.text == Password){ // Если пароль совпал
			alert.SetActive (false);
			//thirdPersonController.LockCameraPosition = false;
			PasswordMenuOpened = false;
			//Time.timeScale = 1f;
			OpenDoorPanel.SetActive (false);
			//Cursor.lockState = CursorLockMode.Locked;
			//Cursor.visible = false;
			open_close_ON = false;
			EndPanel.SetActive (true);
		}
		else{
			alert.SetActive (true); // Переключение видимости сообщении о вводе неправильного пароля
		}
    }

	public void ExitPassword(){ // При нажатии кнопки "Отмена"
		thirdPersonController.LockCameraPosition = false;
		alert.SetActive (false);
		PasswordMenuOpened = false;
		Time.timeScale = 1f;
		OpenDoorPanel.SetActive (false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
