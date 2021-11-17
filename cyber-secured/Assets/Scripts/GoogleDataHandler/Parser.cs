using Assets.Scripts.Topics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parser : MonoBehaviour
{
    List<Topic> topicList;
    
    [SerializeField] public Text questionsPrompt;
    [SerializeField] public Text topic_name;
    [SerializeField] public Text topic_id;
    [SerializeField] public Text question_id;

    [SerializeField] public Button btn1;
    [SerializeField] public Button btn2;
    [SerializeField] public Button btn3;
    [SerializeField] public Button btn4;

    [SerializeField] public Button continueBtn;

    private string QuestionPrompt = " ";
    private string QuestionID = " ";
    private string topicID = " ";
    private string QuestionType = " ";
    private string topicName = " ";

    private string Answer1 = " ";
    private string Answer2 = " ";
    private string Answer3 = " ";
    private string Answer4 = " ";

    private void readTopicsList(List<Topic> topicsSheet)
    {
        foreach(Topic t in topicsSheet)
        {
            topicName = t.TopicName;
            topicID = t.TopicID;
            List<Question> ti = t.questions;

            Assets.Scripts.Topics.Dialogue sd = t.start;
            Assets.Scripts.Topics.Dialogue ed = t.end;
            foreach (Question q in ti)
            {
                QuestionPrompt = q.questionText;
                QuestionID = q.questionID;
                QuestionType = q.questionType;
                
                Answer a = q.answer;
                if (q.questionType == "MultiChoice")
                {
                    Answer1 = a.returnAnswer()[0];
                    Answer2 = a.returnAnswer()[1];
                    Answer3 = a.returnAnswer()[2];
                    Answer4 = a.returnAnswer()[3];
                    DisplayMultiChoiceQuestion(QuestionPrompt, QuestionType, QuestionID, topicName, topicID, 
                                               Answer1, Answer2, Answer3, Answer4);
                }                            
                if (q.questionType == "FillIn")
                {
                    Answer1 = a.returnAnswer()[0];
                    DisplayFillInQuestion(QuestionPrompt, QuestionType, QuestionID, topicName, Answer1);
                } 
                if(q.questionType == "TF")
                {
                    Answer1 = a.returnAnswer()[0];
                    Answer2 = a.returnAnswer()[1];
                    DisplayTFQuestion(QuestionPrompt, QuestionType, QuestionID, topicName, Answer1, Answer2);
                }
            }
        }
    }

    public void onClicked()
    {
        GameObject.Find("ContinueBtn").GetComponentInChildren<Text>().text = " Clicked";
    }

    private void DisplayMultiChoiceQuestion(string questionProm, string questionType, string questionID, string topic_name,string topicID,string ans1, string ans2, string ans3, string ans4)
    {
        GameObject.Find("text_question").GetComponentInChildren<Text>().text = questionProm;
        GameObject.Find("question_id").GetComponentInChildren<Text>().text = "Question ID: " + questionID;
        GameObject.Find("topic_name").GetComponentInChildren<Text>().text = "Topic Name: " + topic_name;
        GameObject.Find("topic_id").GetComponentInChildren<Text>().text = "Topic ID " + topicID;

        GameObject.Find("btn1").GetComponentInChildren<Text>().text = ans1;
        GameObject.Find("btn2").GetComponentInChildren<Text>().text = ans2;
        GameObject.Find("btn3").GetComponentInChildren<Text>().text = ans3;
        GameObject.Find("btn4").GetComponentInChildren<Text>().text = ans4;
    }

    private void DisplayFillInQuestion(string questionProm, string questionType, string questionID, string topic_name, string ans)
    {
        GameObject.Find("text_question").GetComponentInChildren<Text>().text = questionProm;
        GameObject.Find("question_id").GetComponentInChildren<Text>().text = questionProm;
        GameObject.Find("topic_name").GetComponentInChildren<Text>().text = topic_name;
        GameObject.Find("topic_id").GetComponentInChildren<Text>().text = questionID;

        GameObject.Find("btn1").GetComponentInChildren<Text>().text = ans;
    }

    private void DisplayTFQuestion(string questionProm, string questionType, string questionID, string topic_name, string ans1, string ans2)
    {
        GameObject.Find("text_question").GetComponentInChildren<Text>().text = questionProm;
        GameObject.Find("question_id").GetComponentInChildren<Text>().text = questionProm;
        GameObject.Find("topic_name").GetComponentInChildren<Text>().text = topic_name;
        GameObject.Find("topic_id").GetComponentInChildren<Text>().text = questionID;

        GameObject.Find("btn1").GetComponentInChildren<Text>().text = ans1;
        GameObject.Find("btn2").GetComponentInChildren<Text>().text = ans2;
    }

    public void RecieveTopicsList(List<Topic> topics)
    {
        this.topicList = topics;
        readTopicsList(topicList);
    }
}