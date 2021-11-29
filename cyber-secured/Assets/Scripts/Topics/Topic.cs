using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class is the main container for an entire topic,
    /// which consists of dialogue, and questions. Dialogue and
    /// questions are subclasses of TopicItem
    /// </summary>
    public class Topic
    {
        private int itemNumber;
        private List<TopicItem> topicItems;
        private Dialogue startDialogue;
        private Dialogue endDialogue;
        private string topicID;
        private string topicName;
        private TopicItem currentItem;


        public Topic(string topicID, string topicName,
            Dialogue startDialogue)
        {
            this.topicID = topicID;
            this.topicName = topicName;
            this.startDialogue = startDialogue;
            this.topicItems = new List<TopicItem>();
        }

        public void start()
        {
            AudioControllerV2 audioController = GameObject.Find("SoundManager").GetComponent<AudioControllerV2>();
            audioController.PlayQuizMusic();

            itemNumber = 0;
            startDialogue.startItem();
            Debug.Log("first item is dialogue? " + (topicItems[itemNumber] is Dialogue));
            if (topicItems[itemNumber] is Dialogue) return;

            currentItem = topicItems[itemNumber];

            currentItem.startItem();
        }

        public void AddQuestion(Question question)
        {
            Debug.LogWarning("Adding " + question + " to List<Questions> question");
            this.topicItems.Add(question);
        }

        public void AddDialogue(Dialogue dialogue)
        {
            this.topicItems.Add(dialogue);
        }

        public bool trueFalseButtonClicked(bool trueWasClicked)
        {
            // the current item must be a question instead of dialogue.
            Question currentQuestion = (Question) currentItem;
            currentQuestion.showContinueButton();
            // we can assume this is a true false answer because of this button being clicked.
            return ((TFAnswer) currentQuestion.getAnswer()).OnTrueFalseButtonClicked(trueWasClicked);
        }

        public bool onFillInSubmitButtonClicked()
        {
            Question currentQuestion = (Question) currentItem;
            currentQuestion.showContinueButton();
            return ((FillInAnswer) currentQuestion.getAnswer()).OnSubmitButtonClicked();
        }

        public bool onMultiAnswerButton1Clicked()
        {
            Question currentQuestion = (Question)currentItem;
            currentQuestion.showContinueButton();
            return ((MultiAnswer) currentQuestion.getAnswer()).onButton1Clicked();
        }

        public bool onMultiAnswerButton2Clicked()
        {
            Question currentQuestion = (Question)currentItem;
            currentQuestion.showContinueButton();
            return ((MultiAnswer)currentQuestion.getAnswer()).onButton2Clicked();
        }

        public bool onMultiAnswerButton3Clicked()
        {
            Question currentQuestion = (Question)currentItem;
            currentQuestion.showContinueButton();
            return ((MultiAnswer)currentQuestion.getAnswer()).onButton3Clicked();
        }

        public bool onMultiAnswerButton4Clicked()
        {
            Question currentQuestion = (Question)currentItem;
            currentQuestion.showContinueButton();
            return ((MultiAnswer)currentQuestion.getAnswer()).onButton4Clicked();
        }

        // when we allow dialogue in the middle of a topic this should be nextItem()
        public void nextQuestion()
        {
            // this checks for instance type and also creates a variable that is currentItem cast to Question
            Question currentQuestion = null;
            if (currentItem is Question)
            {
                currentQuestion = (Question) currentItem;
                currentQuestion.hideAnswerComponents();
            }
            else
            {
                Debug.LogError("dialogue in the middle of a quiz isn't supported yet.");
            }

            itemNumber++;
            if (itemNumber == topicItems.Count)
            {
                Debug.Log("move on to the next month...");
                if (currentQuestion != null)
                {
                    currentQuestion.hideContinueButton();
                    currentQuestion.hideQuestionBox();
                }

                GameControllerV2.Instance.stage_custom_topics.SetActive(false);

                GameControllerV2.Instance.current_decision_text = "You passed the " + topicName + " Quiz!\n" +
                                                                  "<i>Error rate has decreased.</i>";
                // once again this could be defined in the google sheet.
                float rand_er = Random.Range(0.05f, 0.1f);
                GameControllerV2.Instance.DecreaseErrorRate(rand_er);

                GameControllerV2.Instance.DisplayDecision();

                AudioControllerV2 audioController = GameObject.Find("SoundManager").GetComponent<AudioControllerV2>();
                audioController.PlayGameMusic();
            }
            else
            {
                currentItem = topicItems[itemNumber];
                currentItem.startItem();
            }
        }

        public string getName()
        {
            return topicName;
        }
    }
}
