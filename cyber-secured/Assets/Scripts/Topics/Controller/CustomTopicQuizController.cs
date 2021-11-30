using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Topics;
using UnityEngine.UI;

public class CustomTopicQuizController : MonoBehaviour
{
    private List<Topic> AddedTopics;
    private Topic currentTopic;
    private int topicNum = -1;
    private GameObject scn_main;
    private int topicCount;

    public GameObject fillin_components;
    public GameObject true_false_components;
    public GameObject multianswer_components;

    public GameObject questionBox;

    public CSVDownloader downloader;
    public CSVParser parser;

    void Awake()
    {
        downloader = GameObject.Find("CSVDownloader").GetComponent<CSVDownloader>();
        parser = GameObject.Find("CSVParser").GetComponent<CSVParser>();

        string topicSheet = downloader.GetTopicSheet();
        string questionsSheet = downloader.GetQuestionSheet();

        this.AddedTopics = parser.createListTopic(topicSheet, questionsSheet);
        this.topicCount = this.AddedTopics.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void trueFalseButtonClicked(bool trueWasClicked)
    {
        bool correct = currentTopic.trueFalseButtonClicked(trueWasClicked);
        displayAnswerFeedback(correct);
    }

    public void onSubmitButtonClicked()
    {
        bool correct = currentTopic.onFillInSubmitButtonClicked();
        displayAnswerFeedback(correct);
    }

    public void onMultiAnswerButton1Clicked()
    {
        bool correct = currentTopic.onMultiAnswerButton1Clicked();
        displayAnswerFeedback(correct);
    }
    public void onMultiAnswerButton2Clicked()
    {
        bool correct = currentTopic.onMultiAnswerButton2Clicked();
        displayAnswerFeedback(correct);
    }

    public void onMultiAnswerButton3Clicked()
    {
        bool correct = currentTopic.onMultiAnswerButton3Clicked();
        displayAnswerFeedback(correct);
    }

    public void onMultiAnswerButton4Clicked()
    {
        bool correct = currentTopic.onMultiAnswerButton4Clicked();
        displayAnswerFeedback(correct);
    }

    private void displayAnswerFeedback(bool correct)
    {
        if (correct)
        {
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);
            // the amount to increase NP could be added to the spreadsheet.
            GameControllerV2.Instance.IncreaseNP(2);
        }
        else
        {
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(2);
        }
    }

    public string getNextTopicName()
    {
        if (topicCount == 0) return "";

        if (currentTopic == null)
        {
            return AddedTopics[0].getName();
        }
        else if (topicNum + 1 == AddedTopics.Count)
        {
            return "";
        }
        return AddedTopics[topicNum + 1].getName();
    }

    // Gets next topic and presents its associated quiz
    public void nextTopic()
    {
        topicNum++;
        // Gets next topic and triggers starting dialogue
        currentTopic = AddedTopics[topicNum];
        GameObject.Find("stage_custom_topics").SetActive(true);
        questionBox.SetActive(true);

        currentTopic.start();
    }

    public void skipTopic()
    {
        topicNum++;
        currentTopic = AddedTopics[topicNum];
        Debug.Log("Skipped a topic, current topic name: " + currentTopic.getName());
    }

    // advance to the next topic item.
    public void nextTopicItem()
    {
        // clear the fill in text box.
        fillin_components.transform.Find("InputField").GetComponent<InputField>().text = "";
        currentTopic.nextQuestion();
    }

    public int getTopicCount()
    {
        return topicCount;
    }
}
