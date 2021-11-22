
namespace Assets.Scripts.Topics
{
    /// <summary>
    /// This class represents the answer to some question.
    /// Maybe this should be an interface.
    /// </summary>
    public abstract class Answer
    {
        // after this is merged setting this can be done in subclass constructors. 
        protected string explanation;

        // Display the dialogue if it's correct.
        // The answer classes can override this to put
        // specific dialogue, while this method could create
        // the dialogue box. We can look at examples on how this
        // is already being done in the game. 
        public void displayCorrectDialogue()
        {

        }

        // same thing for if they get it wrong. 
        public void displayIncorrectDialogue()
        {

        }
        // Method for moving the correct buttons and stuff
        // to the right places. This is overridden by the different
        // answer subclasses because they all have different stuff to do
        // for this.
        public abstract void displayAnswer();

        public string getExplanation()
        {
            return explanation;
        }
    }
}
