using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Assets.Scripts.Topics;

// Code courtesy of https://github.com/tikonen/blog/tree/master/csvreader
public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

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
    public List<Topic> createListTopic(string questionsCSV, string topicsCSV)
    {
        List<Dictionary<string, object>> questionsData = Read(questionsCSV);
        List<Dictionary<string, object>> topicData = Read(topicsCSV);
        Dictionary<string, Topic> topics = new Dictionary<string, Topic>();

        // Goes through the topics CSV and creates a dictionary of Topic objects
        // and keys them with their TopicID
        foreach (Dictionary<string, object> topic in topicData)
        {
            Topic nextTopic = new Topic();
            nextTopic.TopicID = (string)topic["topicID"];
            nextTopic.TopicName = (string)topic["topicName"];

            Assets.Scripts.Topics.Dialogue start = new Assets.Scripts.Topics.Dialogue();
            Assets.Scripts.Topics.Dialogue end = new Assets.Scripts.Topics.Dialogue();
            start.AddDialogue((string)topic["startDialogue"]);
            end.AddDialogue((string)topic["endDialogue"]);
            nextTopic.items.Add(start);
            nextTopic.items.Add(end);

            topics.Add((string)topic["topicID"], nextTopic);
        }

        foreach (Dictionary<string, object> question in questionsData)
        {
            Question nextQuestion = new Question();
            nextQuestion.questionID = (string)question["questionID"];
            nextQuestion.questionType = (string)question["questionType"];
            nextQuestion.questionText = (string)question["questionText"];

            string AssociatedTopicID = (string)question["topicID"];
            // Adds the Question object, which is a TopicItem, into the list
            // for the Topic object
            topics[AssociatedTopicID].items.Add(nextQuestion);
        }
        List<Topic> FinalList = new List<Topic>(topics.Values);
        return FinalList;
    }
}