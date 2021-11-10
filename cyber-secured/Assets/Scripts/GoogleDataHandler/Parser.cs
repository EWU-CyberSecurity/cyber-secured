﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parser : MonoBehaviour
{
    private string parTopicSheet;
    private string parQuestionSheet;

    private readonly int start = 1; //As the sheet row actually starts at the second row, row 0 is headers so we skip that.

    //how to change the text in button 1, GameObject.Find("btn1").GetComponentInChildren<Text>().text = "la di da";
    [SerializeField] public Text questionsPrompt;
    [SerializeField] public Button btn1;
    [SerializeField] public Button btn2;
    [SerializeField] public Button btn3;
    [SerializeField] public Button btn4;

    private string QuestionPrompt = "What is a virus";
    private string Answer1 = "A malicious piece of code";
    private string Answer2 = "A youtube video";
    private string Answer3 = "A book";
    private string Answer4 = "a email";

    public void ReceivingSheets(string topicsSheet, string questionsSheet)
    {
        parQuestionSheet = questionsSheet;
        parTopicSheet = topicsSheet;
    }

    private void readQuestionSheet(string topicsSheet)
    {
        //skip the first row (headers)

    }


    public void Update()
    {
        DisplayMutliChoiceQuestion(QuestionPrompt, Answer1, Answer2,Answer3, Answer4);
    }

    private void DisplayMutliChoiceQuestion(string questionProm, string ans1, string ans2, string ans3, string ans4)
    {
        GameObject.Find("text_question").GetComponentInChildren<Text>().text = questionProm;
        GameObject.Find("btn1").GetComponentInChildren<Text>().text = ans1;
        GameObject.Find("btn2").GetComponentInChildren<Text>().text = ans2;
        GameObject.Find("btn3").GetComponentInChildren<Text>().text = ans3;
        GameObject.Find("btn4").GetComponentInChildren<Text>().text = ans4;
    }

    public void GetSheets(string TopicsObject, string QuestionObject)
    {
        Debug.LogWarning("1");
        //Get the files in and saved to their own strings in the class, Start parsing through the sheets.
        Debug.LogWarning(TopicsObject);
        Debug.LogWarning(QuestionObject);
    }

    /*public void DisplayMutliChoiceQuestion()
    {
        GameObject.Find("text_question").GetComponentInChildren<Text>().text = QuestionPrompt;
        GameObject.Find("btn1").GetComponentInChildren<Text>().text = Answer1;
        GameObject.Find("btn2").GetComponentInChildren<Text>().text = Answer2;
        GameObject.Find("btn3").GetComponentInChildren<Text>().text = Answer3;
        GameObject.Find("btn4").GetComponentInChildren<Text>().text = Answer4;
    }*/
}