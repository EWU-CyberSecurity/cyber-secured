using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Assets.Scripts.Topics;

public class CSVReader : MonoBehaviour
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    //public List<Topic> AddedTopicList { get; set; }

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        string data = file;

        var lines = Regex.Split(data, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }

    // Takes in the strings of Google sheet CSV's containing the questions and topics
    // and returns a list of Topic objects representing the topics
    public List<Topic> createListTopic(string topicSheet, string questionsSheet)
    {
        List<Dictionary<string, object>> questionsData = Read(questionsSheet);
        List<Dictionary<string, object>> topicData = Read(topicSheet);
        Dictionary<string, Topic> topics = new Dictionary<string, Topic>();

        // Goes through the topics CSV and creates a dictionary of Topic objects
        // and keys them with their TopicID
        foreach (Dictionary<string, object> topic in topicData)
        {
            Assets.Scripts.Topics.Dialogue startDialogue = new Assets.Scripts.Topics.Dialogue((string)topic["startDialogue"]);

            Topic nextTopic = new Topic((string)topic["topicID"], (string)topic["topicName"], startDialogue);

            topics.Add((string)topic["topicID"], nextTopic);
        }

        foreach (Dictionary<string, object> question in questionsData)
        {
            Question nextQuestion = new Question((string)question["questionText"]);
            nextQuestion.questionType = (string)question["questionType"];

            if (nextQuestion.questionType == "MultiChoice")
            {
                string ans1 = (string)question["answer1"];
                string ans2 = (string)question["answer2"];
                string ans3 = (string)question["answer3"];
                string ans4 = (string)question["answer4"];

                int[] correctAnswers = { 0, 0, 0, 0 };

                if (ans1.Contains("<*>"))
                    correctAnswers[0] = 1;
                else if (ans2.Contains("<*>"))
                    correctAnswers[1] = 1;
                else if (ans3.Contains("<*>"))
                    correctAnswers[2] = 1;
                else if (ans4.Contains("<*>"))
                    correctAnswers[3] = 1;

                MultiAnswerSet newAnswers = new MultiAnswerSet(ans1, ans2, ans3, ans4, correctAnswers);

                MultiAnswer newMulti = new MultiAnswer();
                newMulti.addToAnswerPool(newAnswers);

                nextQuestion.setAnswer(newMulti);
            }
            else if (nextQuestion.questionType == "FillIn")
            {
                FillInAnswer newFillIn = new FillInAnswer((string)question["answer1"]);
                nextQuestion.setAnswer(newFillIn);
            }
            else if (nextQuestion.questionType == "TF")
            {
                TFAnswer newTF = new TFAnswer();
                string trueAns = (string)question["answer1"];
                if (trueAns.Contains("<*>"))
                {
                    newTF.isTrue = true;
                }
                else
                {
                    newTF.isTrue = false;
                }

                nextQuestion.setAnswer(newTF);
            }
            string AssociatedTopicID = (string)question["topicID"];
            // Adds the Question object, which is a TopicItem, into the list
            // for the Topic object
            topics[AssociatedTopicID].AddQuestion(nextQuestion);

        }
        List<Topic> teacherAddedTopics = new List<Topic>(topics.Values);
        return teacherAddedTopics;
    }
}