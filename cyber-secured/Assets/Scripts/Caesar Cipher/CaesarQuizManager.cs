using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CaesarQuizManager : MonoBehaviour {

    public int round = 1;

    public Text decryptionQuestionText;
    public Text encryptedText;
    public Text encryptionQuestionText;
    public Text regularText;
    public Text bonusText;

    // 3 rounds - decryption, encryption, bonus
    public GameObject decryptedRound;
    public GameObject encryptedRound;
    public GameObject bonusRound;

    // continue button
    public GameObject nextButton;
 
    private List<string> plaintexts = new List<string>() {
        "hello world", "cyber secured", "happy holidays", "trust nobody", "get to the chopper", "baby shark", "flappy bird" };
    private string ciphertext;
    // private string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private int shift = 0;
    private int phraseLocation = 0;

    // starts with decryption question
    void Start() {
        decryptedRound.SetActive(true);
        encryptedRound.SetActive(false);
        bonusRound.SetActive(false);
        shift = Random.Range(1, 5);
        phraseLocation = Random.Range(0, plaintexts.Count);

        Debug.Log("using " + plaintexts[phraseLocation]);

        encryptedText.text = ShiftChars(plaintexts[phraseLocation], shift);
        decryptionQuestionText.text = "Decrypt the following text using a Caesar Cipher. It has been encrypted using a right shift of " + shift;
    }

    // proceed to next question
    public void NextSet() {
        switch (round) {
        case 1:
            // removes potential capitalization mistakes
            if (decryptedRound.GetComponentInChildren<InputField>().textComponent.text.ToLower() == plaintexts[phraseLocation]) {

                // play beep sound
                GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

                // increase NP
                GameControllerV2.Instance.IncreaseNP(5);

                // Feedback for correct answer
                GameObject.Find("DCorrect").GetComponent<DialogueTrigger>().TriggerDialogue();

            } else {
                // Decrease NP
                GameControllerV2.Instance.DecreaseNP(5);

                // Feedback for incorrect answer
                string[] dIncorrect = { "Whoops.. You weren't able to decrypt the text. The correct answer was \"" + plaintexts[phraseLocation] + "\"" };
                GameObject.Find("DIncorrect").GetComponent<DialogueTrigger>().dialogue.setSentences(dIncorrect);
                GameObject.Find("DIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
                
            }
            // Next round
            round++;
            plaintexts.RemoveAt(phraseLocation);
            shift = Random.Range(1, 5);
            phraseLocation = Random.Range(0, plaintexts.Count);
            ciphertext = ShiftChars(plaintexts[phraseLocation], shift);

            encryptionQuestionText.text = "Encrypt the following text using a Caesar Cipher, with a right shift of " + shift;
            regularText.text = plaintexts[phraseLocation];

            decryptedRound.SetActive(false);
            encryptedRound.SetActive(true);
            break;
        case 2:
            // removes potential capitalization mistakes
            if (encryptedRound.GetComponentInChildren<InputField>().textComponent.text.ToLower() == ciphertext.ToLower()) {

                Debug.Log("Entering encryption round");

                // play beep sound
                GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

                // increase NP
                GameControllerV2.Instance.IncreaseNP(5);

                // Feedback for correct answer
                // Feedback for correct answer
                GameObject.Find("ECorrect").GetComponent<DialogueTrigger>().TriggerDialogue();

               
            } else {
                // Decrease NP
                GameControllerV2.Instance.DecreaseNP(5);

                // Feedback for incorrect answer
                string[] eIncorrect = { "Whoops.. You weren't able to properly encrypt the text. The correct encryption was \"" + ciphertext.ToLower() + "\"" };
                GameObject.Find("EIncorrect").GetComponent<DialogueTrigger>().dialogue.setSentences(eIncorrect);
                GameObject.Find("EIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();
            }

            // Next round
            round++;
            plaintexts.RemoveAt(phraseLocation);
            shift = Random.Range(1, 5);
            phraseLocation = Random.Range(0, plaintexts.Count);

            bonusText.text = ShiftChars(plaintexts[phraseLocation], shift);

            encryptedRound.SetActive(false);
            bonusRound.SetActive(true);

            break;
        case 3:
            // removes potential capitalization mistakes
            if (bonusRound.GetComponentInChildren<InputField>().textComponent.text.ToLower() == plaintexts[phraseLocation]) {

                // play beep sound
                GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

                // increase NP
                GameControllerV2.Instance.IncreaseNP(25);

                // Feedback for correct answer
                GameObject.Find("BCorrect").GetComponent<DialogueTrigger>().TriggerDialogue();

                // glitch screen
                GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

                // deactivate quiz, and display results
                GameControllerV2.Instance.scn_caesar_cipher.SetActive(false);

                GameControllerV2.Instance.DisplayDecision();

                // don't need script after this
                Destroy(this);
            } else {
                // Decrease NP
                GameControllerV2.Instance.DecreaseNP(5);

                // Feedback for incorrect answer
                string[] bIncorrect = { "Whoops.. You weren't able to crack the code. The message was \"" + plaintexts[phraseLocation] + "\"" };
                GameObject.Find("BIncorrect").GetComponent<DialogueTrigger>().dialogue.setSentences(bIncorrect);
                GameObject.Find("BIncorrect").GetComponent<DialogueTrigger>().TriggerDialogue();

                // Next scene
                GameControllerV2.Instance.scn_caesar_cipher.SetActive(false);
                GameControllerV2.Instance.DisplayDecision();
                Destroy(this);
            }
            break;
        default:
            Debug.Log("Default Case");
            break;

        }
    }

    public string ShiftChars(string sample, int shift)
    {
        char[] buffer = sample.ToCharArray();
        char d = 'a';

        for( int i = 0; i < buffer.Length; i++)
        {
            if (char.IsWhiteSpace(buffer[i]))
            {
                continue;
            }
            
            // buffer[i] = letters[(letters.IndexOf(buffer[i].ToString().ToUpper()) + shift) % 26];
            buffer[i] = (char)((((buffer[i] + shift) - d) % 26) + d);
        }

        return new string(buffer);

        /*
        StringBuilder sb = new StringBuilder();
        foreach (char c in sample)
        {
            if (char.IsWhiteSpace(c))
            {
                sb.Append(' ');
                continue;
            }
            int x = letters.IndexOf(c.ToString().ToUpper());
            x = (x + shift) % 26;
            sb.Append(letters[x]);
        }
        Debug.Log(sb.ToString());
        return sb.ToString();*/
    }
}