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
        private List<MultiAnswerSet> answerPool = new List<MultiAnswerSet>();
    }
}
