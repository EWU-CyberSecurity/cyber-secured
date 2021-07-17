using UnityEngine;

public class Password_SceneController : MonoBehaviour
{

    //public GameObject obj;
    private DialogueManager dialog;
    private GameObject scn_main;

    public GameObject temporaryObject;
    public GameObject questions;
    public GameObject menuButton;

    // Use this for initialization
    void Awake()
    {

        // displays opening text
        GameObject.Find("dlg_password_intro").GetComponent<DialogueTrigger>().TriggerDialogue();

        // glitch animation
        FindObjectOfType<GlitchCamera>().StartGlitch(); //GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

        //Deactivating the scn_main to show the animation better without the background:
        scn_main = GameObject.Find("scn_main");
        scn_main.SetActive(false);


        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialog = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }
    /*
    private void FixedUpdate()
    {
        if (dialog.currentSentenceDisplayed == 2)
        {
            temporaryObject.SetActive(false);
            questions.SetActive(true);

            scn_main.SetActive(true);
            menuButton.SetActive(false);
        }
    }
    */
}
