using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using StarterAssets;

public class OpenDoor : MonoBehaviour {
    public GameObject otherGameObject;
    private ThirdPersonController thirdPersonController;

	public GameObject OpenDoorPanel; // UI панель, с которой будет происходить взаимодействие
	public InputField inputF;
	public GameObject alert;
	public GameObject message;
	public GameObject PressFText;

	[SerializeField]private string Password;
	[SerializeField]private string Message;

	public bool open_to_right;

	public bool locked; // Заблокирована ли дверь
	private bool is_open; // Открыта ли
	[SerializeField]private float open_speed = 150f; // Скорость открывания
	[SerializeField]private float open_dist = 1f; // расстояние открывания
	private float start_dist; // Начальная позиция
	private bool open_close_ON; // Находится ли в движении
	private bool PasswordMenuOpened; // Открыто ли меню для ввода пароля
	private bool enter; // Находится ли игрок в коллайдере двери

	private float final_dist;

	public AudioSource open_sound;
	public AudioSource close_sound;
	public AudioSource error_sound;

	//public GameObject interaction_image;


    private void Awake(){
        thirdPersonController = otherGameObject.GetComponent<ThirdPersonController>();
    }

	private void Start () {
			start_dist = transform.localPosition.z;
		}

	private void Update(){
		if(PasswordMenuOpened){ //Удержание меню
			Cursor.lockState = CursorLockMode.Confined;
       		Cursor.visible = true;
			Time.timeScale = 0f;
			OpenDoorPanel.SetActive (true);
			thirdPersonController.LockCameraPosition = true;
		}
		if(Input.GetKeyDown(KeyCode.F) && !open_close_ON && enter){ // Передача нажатия кнопки открытия двери
			open_close_ON = true;
		}
	}

	private void FixedUpdate () {
		if(open_close_ON){ // Если идёт попытка открыть дверь
			if(!is_open){ // Если закрыто
				if(locked){ // Если заблокировано
					PasswordMenuOpened = true; // Переключение меню ввода пароля
				}
				else{ // Иначе открытие двери
					float posX = Mathf.MoveTowards(transform.localPosition.x, start_dist + final_dist, open_speed * Time.deltaTime);
					transform.localPosition = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);
					//if(transform.localPosition.x == start_dist + final_dist) Stop_open_close();
					Stop_open_close();
					open_close_ON = false;
					is_open = true;
				}
			}
			else{ // Закрытие двери
					float posX = Mathf.MoveTowards(transform.localPosition.x, start_dist, open_speed * Time.deltaTime);
					transform.localPosition = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);
					if(transform.localPosition.x == start_dist) Stop_open_close();
					open_close_ON = false;
					is_open = false;
					open_close_ON = false;
			}
		}
		if(enter){
			if(!is_open)PressFText.GetComponent<Text>().text = "Нажмите F чтобы открыть дверь";
			else PressFText.GetComponent<Text>().text = "Нажмите F чтобы закрыть дверь";
			PressFText.SetActive (true);
		}
}

	private void OnTriggerEnter(Collider col)
	{
		start_dist = transform.localPosition.z;
		if (col.tag == "Player") {
			enter = true;
			if(open_to_right) final_dist = open_dist;
			else final_dist = -open_dist;
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

	private void Stop_open_close(){ // Завершение процесса закрытия двери
		open_close_ON = false;
		//if(move_or_rot_sound) move_or_rot_sound.Stop();
		//if(close_sound && !is_open) close_sound.Play();
	}

    public void EnterPassword(){ // При нажатии кнопки "Ввести пароль"
		if (inputF.text == Password){ // Если пароль совпал
			alert.SetActive (false);
			Stop_open_close();
			thirdPersonController.LockCameraPosition = false;
			locked = false;
			PasswordMenuOpened = false;
			Time.timeScale = 1f;
			OpenDoorPanel.SetActive (false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			open_close_ON = true;
		}
		else{
			alert.SetActive (true); // Переключение видимости сообщении о вводе неправильного пароля
		}
    }

	public void ExitPassword(){ // При нажатии кнопки "Отмена"
		Stop_open_close();
		thirdPersonController.LockCameraPosition = false;
		alert.SetActive (false);
		PasswordMenuOpened = false;
		Time.timeScale = 1f;
		OpenDoorPanel.SetActive (false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}