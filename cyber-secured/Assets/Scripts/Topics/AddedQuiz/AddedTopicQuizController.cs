using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Topics;

public class AddedTopicQuizController : MonoBehaviour
{
    private List<Topic> AddedTopics;
    private Topic currentTopic;
    private GameObject scn_main;
    private int topicCount;

    public GameObject fillin_components;
    public GameObject true_false_components;
    public GameObject multianswer_components;

    public GameObject questionBox;

    CSVManager manager;
    CSVReader reader;

    void Awake()
    {
        manager = GameObject.Find("CSVManager").GetComponent<CSVManager>();
        reader = GameObject.Find("CSVReader").GetComponent<CSVReader>();
        string topicSheet = manager.GetTopicSheet();
        string questionsSheet = manager.GetQuestionSheet();

        this.AddedTopics = reader.createListTopic(topicSheet, questionsSheet);
        this.topicCount = this.AddedTopics.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string getNextTopicName()
    {
        if (topicCount == 0) return "";

        if (currentTopic == null)
        {
            return AddedTopics[0].getName();
        }

        return currentTopic.getName();
    }

    // Gets next topic and presents it's associated quiz
    public void nextTopic()
    {
        // Gets next topic and triggers starting dialogue
        if (currentTopic == null)
        {
            currentTopic = AddedTopics[0];
            GameObject.Find("stage_custom_topics").SetActive(true);
            questionBox.SetActive(true);
        }
        currentTopic.start();
    }

    public int getTopicCount()
    {
        return topicCount;
    }
}
