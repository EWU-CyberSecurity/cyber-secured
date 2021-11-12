using Assets.Scripts.Topics;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class CSVManager : MonoBehaviour
{
    private string topicSheet;
    private string questionSheet;

    public Parser parser;

    private static readonly string sheetID = "2PACX-1vTWkk_93-6edDxrGqz-gSsMgNmkVfIg_yQjIqypDpfpCw-rGIgYu5HqgK8ZHhA4SpzjGTrWh74DbLtD";  //Swap this out with the latest ID in the url if something breaks

    private readonly string QuestionObjURL = "https://docs.google.com/spreadsheets/d/e/" + sheetID + "/pub?output=csv&gid=932839450";   //QuestionObj gid - 932839450
    private readonly string TopicObjURL = "https://docs.google.com/spreadsheets/d/e/" + sheetID + "/pub?output=csv&gid=2122133137";  //TopicObj gid - 2122133137

    private void Start()
    {
        // Suppressing a C# warning, having it wont affect anything but figured it would be better to just not have it show up in console.
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        TaskHandlerAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    }

    async Task TaskHandlerAsync()
    {
        // Download the CSVs from the urls above and then send the strings to the parser.
        questionSheet = await GetText(QuestionObjURL);
        topicSheet = await GetText(TopicObjURL);

        Debug.LogWarning("Finished Downloading");
        CSVReader csvReader = new CSVReader();
        List<Topic> topics = csvReader.createListTopic(questionSheet, topicSheet);

        parser.RecieveTopicsList(topics);
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