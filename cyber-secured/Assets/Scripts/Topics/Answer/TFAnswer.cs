
namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents an answer that is either true or false,
    /// and therefore has 2 buttons.
    /// </summary>
    class TFAnswer : Answer
    {
        private bool isTrue;

        public void onTrueClicked()
        {
            if (isTrue)
            {
                base.displayCorrectDialogue();
            }
            else
            {
                base.displayIncorrectDialogue();
            }
        }

        public void onFalseClicked()
        {
            if (!isTrue)
            {
                base.displayCorrectDialogue();
            }
            else
            {
                base.displayIncorrectDialogue();
            }
        }

        public override void displayAnswer()
        {
            // This is an easy one, just put the two buttons
            // in the right place. No changing text or anything.
        }
    }
}
