using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHelpMessage : MonoBehaviour
{
    public GameObject message; // Текстовое поле UI, в котором будет отображаться текст подсказки
    public string helpMessage;
    private int questStep;
    public int questStepNew;

    void Start()
    {
        helpMessage = "Выясните, что происходит на станции";
        questStep = 0;
        questStepNew = 1;
    }

    void FixedUpdate()
    {
        if(message.GetComponent<Text>().text != helpMessage && questStepNew>questStep){
            message.GetComponent<Text>().text = helpMessage;
            questStep = questStepNew;
        }
    }
}
