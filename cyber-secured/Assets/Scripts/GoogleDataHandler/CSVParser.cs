using UnityEngine;

public class CSVParser : MonoBehaviour
{
    public string topicsSheet { get; set; }
    public string questionsSheet { get; set; }

    // Get the sheets from the DownloadManager
    public void RetrieveSheets(string top, string ques)
    {

        this.topicsSheet = top;
        this.questionsSheet = ques;

        Debug.LogWarning("In another class: \n" + topicsSheet);
        Debug.LogWarning("In another class: \n" + questionsSheet);
    }
}
