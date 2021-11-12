
namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents an answer that is either true or false,
    /// and therefore has 2 buttons.
    /// </summary>
    class TFAnswer : Answer
    {
        public bool isTrue { get; set; }

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
        
        public override string[] returnAnswer()
        {
            string[] s = { "True", "False" };
            return s;
        }

        public override void displayAnswer()
        {
            // This is an easy one, just put the two buttons
            // in the right place. No changing text or anything.
        }
    }
}
