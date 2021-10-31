using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParser : MonoBehaviour
{
    private string topicsSheet;
    private string questionsSheet;

    private List<string> topicList;
    private List<string> dialList;
    private List<string> quesList;
    private List<string> ansList;


    //Get the sheets from the DownloadManager
    public void RetrieveSheets(string top, string ques)
    {
        this.topicsSheet = top;
        this.questionsSheet = ques;

        TopicSheetParser(topicsSheet);
        QuestionSheetParser(questionsSheet);

        //Debug.LogWarning(topicsSheet);
       //Debug.LogWarning(questionsSheet);
    }
    
    private void TopicSheetParser(string topicsSheet)
    {
        //loop through the 
    }

    private void QuestionSheetParser(string questionsSheet)
    {
        
    }

    //Helper methods



    //Getters for the finished Lists

    public List<string> GetTopicList()
    {
        return this.topicList;
    }

    public List<string> GetDialList()
    {
        return this.dialList;
    }

    public List<string> GetQuesList()
    {
        return this.quesList;
    }

    public List<string> GetAnswerList()
    {
        return this.ansList;
    }
}
