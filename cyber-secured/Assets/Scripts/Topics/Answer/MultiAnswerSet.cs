
namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents 4 possible answers, one of which is correct.
    /// </summary>
    class MultiAnswerSet
    {
        private string[] answers = new string[4];
        private int[] correctAnswer; // some of 0 - 3. 

        public void addAnswer(string answer, int index)
        {
            this.answers[index] = answer;
        }
    }
}
