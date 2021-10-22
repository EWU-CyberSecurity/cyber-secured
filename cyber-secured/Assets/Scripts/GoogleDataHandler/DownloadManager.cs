using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class DownloadManager : MonoBehaviour
{
    // https://www.youtube.com/watch?v=niOkw_cZEiM
    private string textFileQuestionObj;
    private string textFileTopicObj;
    private int ctr = 1;
    

    private static readonly string sheetID = "2PACX-1vTWkk_93-6edDxrGqz-gSsMgNmkVfIg_yQjIqypDpfpCw-rGIgYu5HqgK8ZHhA4SpzjGTrWh74DbLtD";
    //readonly = final

    // Start is called before the first frame update
    private readonly string spreadsheet1URL = "https://docs.google.com/spreadsheets/d/e/" + sheetID + "/pub?output=csv&gid=932839450";   //QuestionObj gid - 932839450
    private readonly string spreadsheet2URL = "https://docs.google.com/spreadsheets/d/e/" + sheetID + "/pub?output=csv&gid=2122133137";  //TopicObj gid - 2122133137

    // https://stackoverflow.com/questions/24255472/download-export-public-google-spreadsheet-as-tsv-from-command-line/28494469#28494469
    // URL format as of 10/21/21 - https://docs.google.com/spreadsheets/d/e/<sheetID>/pub?output=csv&gid=<GID>

    public void startDownloading()
    {
        Debug.LogWarning("Start downloading");
        StartCoroutine(DownloadText(spreadsheet1URL)); //QuestionObject
        StartCoroutine(DownloadText(spreadsheet2URL)); //TopicObject
    }

    private IEnumerator DownloadText(string spreadsheetURL)
    {
        UnityWebRequest www = UnityWebRequest.Get(spreadsheetURL);
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log(www.error);
        } else {
            Debug.LogWarning("Downloading sheet " + ctr);
            //Debug.LogWarning(www.downloadHandler.text);
            SetCSV(www.downloadHandler.text, ctr);
            ctr++;
        }
    }
    
    //set the list depending on the ctr (1 is the question, 2 is the topics)
    private void SetCSV(string newCSVText, int ctr)
    {
        if (ctr == 1)
        {
            textFileQuestionObj = newCSVText;
            Debug.LogWarning(textFileQuestionObj);
        } else {
            textFileTopicObj = newCSVText;
            Debug.LogWarning(textFileTopicObj);
        }
    }
    
    public string GetQuestionTextFile()
    {
        return textFileQuestionObj;
    }
    public string GetTopicTextFile()
    {
        return textFileTopicObj;
    }
}
