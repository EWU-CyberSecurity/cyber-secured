
namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents an answer where you have to write something
    /// in a text box.
    /// </summary>
    public class FillInAnswer : Answer
    {
        public string correctAnswer { get; set; }

        public override void displayAnswer()
        {
            // Move the input field and continue button (plus anything else that's needed)
            // to the right place. There is an example of this kind of thing happening in
            // the caesar cipher quiz.
        }

        public void OnContinueButtonClicked()
        {
            // Check if the text in the input field is the correct answer.
        }
    }
}
