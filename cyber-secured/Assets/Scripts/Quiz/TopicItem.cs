using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopicItem
{
    public Topic top; 
    public List<string> Dialogues;
    public List<string> Topics;
    public List<string> Questions;
    public List<string> Answers;

    public TopicItem(List<string> dialogue, List<string> topic, List<string> question, List<string> answer)
    {
        dialogue = top.getDialogueList();
        topic = top.getTopicsList();
        question = top.getQuestionsList();
        answer = top.getAnswersList();
    }


}


internal class Dialogues
{
    
    

}

internal class Questions
{



    internal class Answers
    {

        

        //TF
        internal class TF
        {

        }
        //MultiChoice
        internal class MultiChoice
        {

        }
        //Fill in the blank
        internal class FillInBlank
        {

        }
    }
}