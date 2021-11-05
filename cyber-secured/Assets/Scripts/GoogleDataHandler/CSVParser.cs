using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParser : MonoBehaviour
{
    private string topicsSheet;
    private string questionsSheet;

    //Get the sheets from the DownloadManager
    public void RetrieveSheets(string top, string ques)
    {

        this.topicsSheet = top;
        this.questionsSheet = ques;

        Debug.LogWarning("In another class: \n" + topicsSheet);
        Debug.LogWarning("In another class: \n" + questionsSheet);
    }
}
