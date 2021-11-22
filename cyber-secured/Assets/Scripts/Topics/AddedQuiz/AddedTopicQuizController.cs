using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Topics;

public class AddedTopicQuizController : MonoBehaviour
{
    private List<Topic> AddedTopics;
    private Topic currentTopic;
    private GameObject scn_main;
    private int topicCount;
    private int currentTopicListID = 0;

    CSVManager manager;
    CSVReader reader;

    void Awake()
    {
        manager = new CSVManager();
        reader = new CSVReader();
        string topicSheet = manager.GetTopicSheet();
        string questionsSheet = manager.GetQuestionSheet();

        this.AddedTopics = reader.createListTopic(topicSheet, questionsSheet);
        this.topicCount = this.AddedTopics.Count;

        Debug.Log("TOPIC COUNT? " + topicCount);

        scn_main = GameObject.Find("scn_main");
        scn_main.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Gets next topic and presents it's associated quiz
    public void nextQuiz()
    {
        // Gets next topic and triggers starting dialogue
        currentTopic = AddedTopics[currentTopicListID];
        Assets.Scripts.Topics.Dialogue start = currentTopic.start;
        start.startItem();

        // TO DO: trigger quiz, trigger end dialogue
        // Using Password Quiz as a reference
    }
}
