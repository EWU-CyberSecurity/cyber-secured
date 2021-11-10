
using System.Linq;
using UnityEngine;
using UnityScript.Steps;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents 4 possible answers, one or more of which is correct.
    /// </summary>
    class MultiAnswerSet
    {
        private string[] answers;
        private int[] correctAnswers; // some of 0 - 3. 

        public MultiAnswerSet(string option1, string option2,
            string option3, string option4, int[] correctAnswers)
        {
            answers = new string[] { option1, option2, option3, option4 };

            if (correctAnswers.Length == 0)
            {
                Debug.LogError("An empty list of correct answers was supplied to MultiAnswerSet");
            }

            this.correctAnswers = correctAnswers;
        }

        public string getAnswer(int answer_num)
        {
            return answers[answer_num];
        }

        public bool isCorrectAnswer(int answer)
        {
            return correctAnswers.Contains(answer);
        }
    }
}
