using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topic
{
    /*-----------------------Lists----------------------------*/
    private List<string> Dialogues = new List<string>();
    private List<string> Topics = new List<string>();
    private List<string> Questions = new List<string>();
    private List<string> Answers = new List<string>();


    //Base constructor
    public Topic()
    {
        this.Dialogues = null;
        this.Topics = null;
        this.Questions = null;
        this.Answers = null;
    }

    //Constructor that should be called from the parser, 
    public Topic(List<string> dialogues, List<string> topics, List<string> questions, List<string> answers)
    {
        setDialoguesList(dialogues);
        setTopicsList(topics);
        setQuestionsList(questions);
        setAnswersList(answers);
    }

    /*--------------------Getters/Setter----------------------*/
    public List<string> getDialogueList()
    {
        return this.Dialogues;
    }
    public void setDialoguesList(List<string> dialogues)
    {
        this.Dialogues = dialogues;
    }
    /*--------------------------------------------------------*/ 
    public List<string> getTopicsList()
    {
        return this.Topics;
    }
    public void setTopicsList(List<string> topics)
    {
        this.Topics = topics;
    }
    /*--------------------------------------------------------*/
    public List<string> getQuestionsList()
    {
        return this.Questions;
    }

    public void setQuestionsList(List<string> questions)
    {
        this.Questions = questions;
    }
    /*--------------------------------------------------------*/
    public List<string> getAnswersList()
    {
        return this.Answers;
    }

    public void setAnswersList(List<string> answers)
    {
        this.Answers = answers;
    }
    /*--------------------End Getter/Setters------------------*/


}
