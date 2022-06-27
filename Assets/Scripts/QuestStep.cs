using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PlayerHelpMessage;

public class QuestStep : MonoBehaviour
{
	private bool enter; // Находится ли игрок в коллайдере
    public GameObject PlayerArmature;
    private PlayerHelpMessage playerHelpMessage;

    public int localQuestStep;
    public string localHelpMessage;

    void Awake(){
        playerHelpMessage = PlayerArmature.GetComponent<PlayerHelpMessage>();
    }

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
                playerHelpMessage.helpMessage = localHelpMessage;
                playerHelpMessage.questStepNew = localQuestStep;
			}
		}
}
