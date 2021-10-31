using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVManager : MonoBehaviour
{
    public DownloadManager downloadManager;
    public CSVParser csvParser;

    private string topicSheet;
    private string questionSheet;

    private List<string> topicList;
    private List<string> dialList;
    private List<string> quesList;
    private List<string> ansList;

    void Start()
    {
        downloadManager.download();

        //set the sheets here to send them to the parser and anywhere else we need.
        //Get a string of the CSV table
        topicSheet = downloadManager.GetTopicTextFile(); //TopicObject table
        questionSheet = downloadManager.GetQuestionTextFile(); //QuestionObject table

        Debug.LogWarning("Finished Downloading, moving to parser");
        Debug.LogWarning(topicSheet);
        Debug.LogWarning(questionSheet);


        //Parsing the Strings to lists
        csvParser.RetrieveSheets(topicSheet,questionSheet);

        //Lists
        topicList = csvParser.GetTopicList();
        dialList = csvParser.GetDialList();
        quesList = csvParser.GetQuesList();
        ansList = csvParser.GetAnswerList();
        
        // display to screen

    }
}
