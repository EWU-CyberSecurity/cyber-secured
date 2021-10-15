using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadManager : MonoBehaviour
{
    // Start is called before the first frame update
    //change the "1isvQQd1lTvI0AewqS21pJmDxsEFiicxMb7-_zmlkhis" with what ever the new id is.
    private string spreadsheetURL = "https://docs.google.com/spreadsheets/d/1isvQQd1lTvI0AewqS21pJmDxsEFiicxMb7-_zmlkhis/export?format=csv";
    void Start()
    {
        StartCoroutine(GetText()); // at the  start of the game, start a coroutine to download the latest spreadsheet.

        //TODO in case the url breaks, go use a preexisting copy of the csv that is stored in the game already?
    }

    IEnumerator GetText() //Grab the text verison of the csv. 
    {
        UnityWebRequest www = UnityWebRequest.Get(spreadsheetURL); //make a UnityWebRequest with the spreadsheetURL
        yield return www.SendWebRequest(); //send the request to download the file

        if (www.error != null) // if error
        {
            Debug.Log(www.error); //log it to the console
        }
        else // else display the error to the console.
        {
            // Show results as text
            Debug.LogWarning(www.downloadHandler.text);

            //TODO save it or do something with the .text 

            //I do not know what to do next...
        }
    }
}
