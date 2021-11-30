using UnityEngine;

namespace Assets.Scripts.Topics
{
    class CustomTopicHandler : MonoBehaviour
    {

        public void onMultiAnswerButton1Clicked()
        {
            Debug.Log("button 1 clicked");
            // Call the right handler in the multi answer. 
        }

        public void onMultiAnswerButton2Clicked()
        {
            // Call the right handler in the multi answer. 
            Debug.Log("button 2 clicked");
        }

        public void onMultiAnswerButton3Clicked()
        {
            // Call the right handler in the multi answer. 
            Debug.Log("button 3 clicked");
        }

        public void onMultiAnswerButton4Clicked()
        {
            // Call the right handler in the multi answer. 
            Debug.Log("button 4 clicked");
        }

        public void onTrueButtonClicked()
        {
            // Call the true button handler
        }

        public void onFalseButtonClicked()
        {
            // Call the false button handler
        }

        public void onFillInContinueButtonClicked()
        {
            // Call the continue button handler for the fill in
        }

        public void onDialogueContinueButtonClicked()
        {
            // Call the continue button handler for the dialogue
        }
    }
}
