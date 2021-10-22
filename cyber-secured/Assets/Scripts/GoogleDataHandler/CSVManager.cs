using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVManager : MonoBehaviour
{
    public DownloadManager downMan;
    public Parser parser;

    private string topicsSheet;
    private string questionsSheet;
    void Start()
    {
        downMan.startDownloading();//Start the downloading of the sheets

        //set the sheets here to send them to the parser and anywhere else we need.
        topicsSheet = downMan.GetTopicTextFile(); 
        questionsSheet = downMan.GetQuestionTextFile();
        
        parser.ReceivingSheets(topicsSheet, questionsSheet);
        parser.Update(); // Call the update method in parser to print to the screen.
    }
}