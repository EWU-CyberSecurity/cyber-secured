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
    private int currentTopicListID = 0;

    CSVManager manager;
    CSVReader reader;

    void Awake()
    {
        GameObject.Find("stage_custom_topics").SetActive(true);

        manager = GameObject.Find("CSVManager").GetComponent<CSVManager>();
        reader = GameObject.Find("CSVReader").GetComponent<CSVReader>();
        string topicSheet = manager.GetTopicSheet();
        string questionsSheet = manager.GetQuestionSheet();

        this.AddedTopics = reader.createListTopic(topicSheet, questionsSheet);
        this.topicCount = this.AddedTopics.Count;

        /*scn_main = GameObject.Find("scn_main");
        scn_main.SetActive(false);*/

        currentTopic = AddedTopics[0];
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

    public int getTopicCount()
    {
        return topicCount;
    }
}
