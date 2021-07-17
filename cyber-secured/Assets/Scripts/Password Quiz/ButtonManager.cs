using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    public Text text;

    public bool correct;
    public bool disable;

    public GoogleAnalyticsV4 googleAnalytics;

    public int round;

    private string[] explanation = {
        "\"I am infatuated with you\" is much longer and uses spaces. \"I am infatuated with you\" can be improved so it doesn't have words; Eg: \"i^m infat 2 ated w/ y0u\"",
            "Since \"2 b OR !tuby\" uses numbers, capital and lowercase letters, and symbols, it is more complex than the other passwords.", 
            "\"RoundRobin\" is the worst of these options because a hacker could assume the bank's name was used in the password somehow because it is easy to remember.",
            "\"H20 b@ttle\" cannot be found in the dictionary, and seems more random than the others. dihydrogenmonoxide can be strong because of length, but it's technically a 2-word combo.",
            "A Password Manager is secure software that handles your passwords for you. Helping your co-worker remember his password is better than leaving a password in readable form."
    };

    private string[] affirmation = { "Bingo! ", "That's correct! ", "Good job. ", "You got it. ", "Nice work. " };
    private string[] disdain = { "That's not it :( ", "That's incorrect! ", "Actually... ", "There's a better answer. ", "Try again! " };

    private int random;

    public void message()
    {

        random = Random.Range(0, 5);
        //googleAnalytics.LogScreen("Main Menu"); ------------------ need to fix it!!!!!!!!!!------

        //googleAnalytics.LogEvent("asfdasf", "asdfasd", "safsf", 33847);
        if (correct)
        {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

            text.text = affirmation[random] + explanation[round - 1];

            // increase NP by 2
            GameControllerV2.Instance.IncreaseNP(2);
        }
        else
        {
            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(2);

            GameObject.Find("Password_questions").GetComponent<SceneControllerPassword>().DecreaseLife();
            text.text = disdain[random] + explanation[round - 1];
        }

        disable = true;
    }

}
