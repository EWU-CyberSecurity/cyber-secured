using System;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

public class CSVManager : MonoBehaviour
{
    private string topicSheet;
    private string questionSheet;

    public CSVParser parser;

    private static readonly string sheetID = "2PACX-1vTWkk_93-6edDxrGqz-gSsMgNmkVfIg_yQjIqypDpfpCw-rGIgYu5HqgK8ZHhA4SpzjGTrWh74DbLtD";  //Swap this out with the latest ID in the url if something breaks

    private readonly string spreadsheet1URL = "https://docs.google.com/spreadsheets/d/e/" + sheetID + "/pub?output=csv&gid=932839450";   //QuestionObj gid - 932839450
    private readonly string spreadsheet2URL = "https://docs.google.com/spreadsheets/d/e/" + sheetID + "/pub?output=csv&gid=2122133137";  //TopicObj gid - 2122133137

    private void Start()
    {
        TaskHandlerAsync();
    }

    async Task TaskHandlerAsync() 
    {
        // Download the CSVs from the urls above and then send the strings to the parser.
        questionSheet = await GetText(spreadsheet1URL);
        topicSheet = await GetText(spreadsheet2URL);

        parser.RetrieveSheets(topicSheet, questionSheet);
    }

    // This is from https://github.com/mofrison/Unity3d-Network, modifying it may break stuff.
    private static async Task<UnityWebRequest> SendWebRequest(UnityWebRequest request, 
            CancellationTokenSource cancelationToken = null, System.Action<float> progress = null)
    {
        while (!Caching.ready)
        {
            if (cancelationToken != null && cancelationToken.IsCancellationRequested)
            {
                return null;
            }
            await Task.Yield();
        }
        
        request.SendWebRequest();

        while (!request.isDone)
        {
            if (cancelationToken != null && cancelationToken.IsCancellationRequested)
            {
                request.Abort();
                request.Dispose();

                return null;
            }
            else
            {
                progress?.Invoke(request.downloadProgress);
                await Task.Yield();
            }
        }
        progress?.Invoke(1f);
        return request;
    }

    // This is from https://github.com/mofrison/Unity3d-Network, modifying it may break stuff.
    public static async Task<string> GetText(string url)
    {
        var uwr = await SendWebRequest(UnityWebRequest.Get(url));
        if (uwr != null && !uwr.isHttpError && !uwr.isNetworkError)
        {
            return uwr.downloadHandler.text;
        }
        else
        {
            throw new Exception("[Network] error: " + uwr.error + " " + uwr.url);
        }
    }
}