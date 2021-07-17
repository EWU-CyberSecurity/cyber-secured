using UnityEngine;
using UnityEngine.UI;

public class OneTimePadSceneController : MonoBehaviour {

    //public GameObject obj;
    private DialogueManager dialog;
    private MovingImagesAndText movingObjects; // object to the script to move images on the screen

    private GameObject scn_main;
    //public GameObject button_menue;// the button in the main scene that allow you to start over the game
    //public Text txt_company; // No need for that too

    public  GameObject demonstration; // drag and drop the Illustration child object of the scn_one_time_pad

    public GameObject questions; //The object that suppoes to handle the qustions 

	// Use this for initialization
	void Awake () {
        
        // displays opening text
        GameObject.Find("dlg_one_time_pad_illustration").GetComponent<DialogueTrigger>().TriggerDialogue();

        // glitch animation
        FindObjectOfType<GlitchCamera>().StartGlitch(); //GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();

        //Deactivating the scn_main to show the animation better without the background:
        scn_main = GameObject.Find("scn_main");
        scn_main.SetActive(false);


        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialog = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void FixedUpdate()
    {
        if(dialog.currentSentenceDisplayed == 14)
        {
            demonstration.SetActive(false);
            questions.SetActive(true);

            scn_main.SetActive(true);
        }
    }
}
