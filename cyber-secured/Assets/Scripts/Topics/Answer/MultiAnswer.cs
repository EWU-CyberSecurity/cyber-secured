using System.Collections.Generic;

namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents a pool of multiple choice answers,
    /// which is a list of MultiAnswerSets. One of these sets will be
    /// displayed.
    /// </summary>
    class MultiAnswer : Answer
    {
        private List<MultiAnswerSet> answerPool = new List<MultiAnswerSet>(); // all the possible answer sets
        private MultiAnswerSet displayedSet; // the set of answers that is actually displayed

        public void addAnswerSet(MultiAnswerSet newSet)
        {
            this.answerPool.Add(newSet);
        }

        public override void displayAnswer()
        {
            // Change the text on the buttons and move them to the right spot.
            // Maybe this is where displayedSet is set by choosing a random set from the answerPool.
            // Also use ShuffleAnswers to shuffle them, hopefully we can get that
            // to work. 
        }

        // When the button is clicked check if it belongs to the right answer
        public void onButton1Clicked()
        {

        }

        public void onButton2Clicked()
        {

        }

        public void onButton3Clicked()
        {

        }

        public void onButton4Clicked()
        {

        }
    }
}
