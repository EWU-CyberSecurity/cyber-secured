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

    void Awake()
    {
        CSVReader googleData = new CSVReader();
        CSVParser getCSVStrings = new CSVParser();
        string topicSheet = getCSVStrings.topicsSheet;
        string questionsSheet = getCSVStrings.questionsSheet;

        this.AddedTopics = googleData.createListTopic(topicSheet, questionsSheet);
        this.topicCount = this.AddedTopics.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Gets next topic and it's associated quiz, planning on a separate manager for the quiz
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
