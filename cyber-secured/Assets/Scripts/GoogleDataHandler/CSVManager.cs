using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVManager : MonoBehaviour
{
    public DownloadManager downloadMananager;
    public CSVParser csvParser;

    private string topicsSheet;
    private string questionsSheet;

    private List<string> topicList;
    private List<string> dialList;
    private List<string> quesList;
    private List<string> ansList;

    void Start()
    {
        downloadMananager.startDownloading();//Start the downloading of the sheets

        //set the sheets here to send them to the parser and anywhere else we need.
        //Get a string of the CSV table
        topicsSheet = downloadMananager.GetTopicTextFile(); //TopicObject table
        questionsSheet = downloadMananager.GetQuestionTextFile(); //QuestionObject table

        Debug.LogWarning("Finished Downloading, moving to parser");

        //Parsing the Strings to lists
        csvParser.RetrieveSheets(topicsSheet,questionsSheet);

        //Lists
        topicList = csvParser.GetTopicList();
        dialList = csvParser.GetDialList();
        quesList = csvParser.GetQuesList();
        ansList = csvParser.GetAnswerList();

        //Parsing to screen
        


        // display to screen

    }
}

//List<Dictionary<string, object>> data = CSVReader.Read("example");
//List<Dictionary<string, object>> data = CSVReader.Read("example");
//List<Dictionary<string, object>> data = CSVReader.Read("example");
//List<Dictionary<string, object>> data = CSVReader.Read("example");