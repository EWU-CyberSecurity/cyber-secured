using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
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

    private string returnData;

    // https://stackoverflow.com/questions/24255472/download-export-public-google-spreadsheet-as-tsv-from-command-line/28494469#28494469
    // URL format as of 10/21/21 - https://docs.google.com/spreadsheets/d/e/<sheetID>/pub?output=csv&gid=<GID>

    public void download()
    {
        // START COROUTINE
        Debug.LogWarning("Starting coroutine 1");
        StartCoroutine(GetData(spreadsheet1URL, (value) => {
            Debug.LogWarning("setCSV 1");
            setCSV(value, ctr);
            ctr++;
        }));
        
        Debug.LogWarning("Starting coroutine 2");
        StartCoroutine(GetData(spreadsheet2URL, (value) => {

            Debug.LogWarning("setCSV 2");
            setCSV(value, ctr);
            ctr++;
        }));
    }

    public IEnumerator GetData(string uri, System.Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            if (callback != null)
                callback(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.data); // this log is returning the requested data. 
            if (callback != null)
                callback(www.downloadHandler.text);
        }
    }
    
    private void setCSV(string data, int ctr)
    {
        if (ctr == 1)
        {
            this.textFileQuestionObj = data;
            //Debug.LogWarning(textFileQuestionObj); // prints null
        }
        else
        {
            this.textFileTopicObj = data;
            //Debug.LogWarning(textFileTopicObj); //prints null
        }
    }

    public string GetQuestionTextFile()
    {
        return this.textFileQuestionObj;
    }
    public string GetTopicTextFile()
    {
        return this.textFileTopicObj;
    }
}


/*
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
    
    Current code 


    or figure out how to do async/wait version 

 */