using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class dlman : MonoBehaviour
{

    /*
    void Start()
    {
        topicSheet = Download(spreadsheet1URL);
        questionSheet = Download(spreadsheet2URL);

        CSVParser csvParser = gameObject.AddComponent<CSVParser>();

        Debug.LogWarning("Finished Downloading, moving to parser");

        csvParser.RetrieveSheets(topicSheet, questionSheet);
    }

    public string Download(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SendWebRequest();

            while (!www.isDone)
            {

            }



            if (www.isHttpError)
            {
                Debug.LogError("UnityWebError: " + www.error);
                return null;
            }
            else
            {
                return www.downloadHandler.text;
            }
        }
    }*/
}
