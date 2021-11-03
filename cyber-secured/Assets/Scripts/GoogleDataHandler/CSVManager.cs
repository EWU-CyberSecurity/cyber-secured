using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CSVManager : MonoBehaviour
{
    private string topicSheet;
    private string questionSheet;

    public Dictionary<string, object> categories = new Dictionary<string, object>();

    private static readonly string sheetID = "2PACX-1vTWkk_93-6edDxrGqz-gSsMgNmkVfIg_yQjIqypDpfpCw-rGIgYu5HqgK8ZHhA4SpzjGTrWh74DbLtD";

    private readonly string spreadsheet1URL = "https://docs.google.com/spreadsheets/d/e/" + sheetID + "/pub?output=csv&gid=932839450";   //QuestionObj gid - 932839450
    private readonly string spreadsheet2URL = "https://docs.google.com/spreadsheets/d/e/" + sheetID + "/pub?output=csv&gid=2122133137";  //TopicObj gid - 2122133137
    
    private string uwr_response = " ";
    private bool isLoaded = false;
    

    private void Start()
    {
        LoadJson();
    }

    //this coroutine to get the response
    public IEnumerator GetJson(string url)
    {
        isLoaded = false;
        UnityWebRequest uwr = UnityWebRequest.Get(url);

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.LogFormat("Error downloading file: <color=red>{0}</color> | Error code: <color=red>{1}</color>", uwr.downloadHandler.text, uwr.error);
        }
        else
        {
            var jsonResponce = MiniJSON.Json.Deserialize(uwr.downloadHandler.text) as Dictionary<string, object>;
            //saving data to a variable
            categories = jsonResponce;
        }
    }

    //this one to wait for response and decode
    private IEnumerator WaitingForJson()
    {
        while (!isLoaded)
            yield return new WaitForSeconds(0.1f);

    }

    //this one to be called when you need the json (probably in Start() method)
    private void LoadJson()
    {
        Debug.LogWarning("Starting download");

        StartCoroutine(GetJson(spreadsheet1URL));
        StartCoroutine(GetJson(spreadsheet2URL));
        StartCoroutine(WaitingForJson());

        Debug.LogWarning("Finished Download");
        StopAllCoroutines();


    }
}
