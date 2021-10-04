using UnityEngine;
using UnityEngine.UI;

public class OTPChoiceManager : MonoBehaviour {

    public Text text;

    public bool correct;
    public bool disable;

    public GameObject questions;
    private OneTimePadQuizManager scriptInQuestions;

    private void Awake()
    {
        scriptInQuestions = questions.GetComponent<OneTimePadQuizManager>();
    }

    public void message() {
        
        if (correct) {

            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

            text.text = "Correct!";

            // increase NP by 2
            GameControllerV2.Instance.IncreaseNP(2);

            scriptInQuestions.setTrueFalse(true);
        } else {

            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(2);
            
            GameControllerV2.Instance.DecreaseNP(2);

            text.text = "Incorrect!";

            scriptInQuestions.setTrueFalse(false);
        }

        disable = true;

        scriptInQuestions.NextSet(); // updating to the second round
    }
}
