/// FILE: GameControllerV2.cs
/// PURPOSE: A script controlling the main game loop.
/// 
/// FUNCTIONS:
/// 
/// void Awake()
///     Called when the object is created, to be given singleton functionality
/// 
/// void Start()
///     Initializes variables to a default state at the beginning
/// 
/// void Update()
///     For debug purposes, keyboard input
///     F7: restart the game
/// 
/// public void SetCompany(int x)
///     Initiates which starting 'company' the player chooses, input as x,
///     runs setters to the appropriate choice,
///     updates the variables, and then displays the next decision
/// 
/// public void UpdateDisplay___()
///     Set of functions which updates the text to be displayed
/// 
/// public void Display___()
///     Set of functions which pops up a prompt box which contains game information
/// 
/// public void IncrementMonth()
///     Moves the 'game step' forward, adding onto the score,
///     and updating the display to match
/// 
/// public void RollError()
///     Compares the error rate variable to see if an error has occured randomly,
///     if the error rate is greater than or equal to the random roll, an error has occured
/// 
/// public void NextEvent()
///     Calls RollError() to roll the dice for a bad event,
///     displays the next event
/// 
/// public void ActivateEvent(bool bad_event_occured)
///     Called in tandem with RollError(),
///     contains the main function of what a bad event entails, i.e., lowering scores
/// 
/// public void EventYesNo(bool x)
///     Checks if the player has pressed yes or no for a choice,
///     calling ActivateChoice with a bool parameter
/// 
/// public void ActivateChoice(bool x)
///     Begins the mini-game to be played by the player to determine score increase
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Security.AccessControl;

// imported scripts:
using DG.Tweening;

public class GameControllerV2 : MonoBehaviour
{
    public static GameControllerV2 Instance;    // for singleton

    public string playerName;

    // "Scenes":
    public GameObject background;               // background image

    public GameObject scn_main;                 // main scene
    public GameObject[] companies;              // the three company buttons

    public bool backup_hdd = false;
    public bool backup_usb = false;
    public bool backup_cld = false;
    //public int backup_type;                     // 1 = external; 2 = internal; 3 = cloud

    // public GameObject scn_phishing;             // --
    public GameObject scn_quiz_password;        // password "minigame scene"
    public GameObject scn_filebackup;           // file backup "minigame scene"
    public GameObject scn_phishing_v2;          // alternative phishing scene 
    public GameObject scn_virus_presentation;   // virus presentation scene
    public GameObject scn_virus_quiz;           // virus quiz scene
    public GameObject scn_caesar_cipher;        // caesar cipher scene
    public GameObject scn_one_time_pad;         // one time encryption scene     -----------------> New scene for the one time pad
    public GameObject scn_RSA;                  // RSA encryption scene          -----------------> New scene for the RSA encryption
    public GameObject stage_custom_topics;

    public GameObject[] all_stages;

    // Numbers:
    public int network_power;                   // main score
    public int monthly_np;                      // network power increase per week
    public int current_month;                   // --
    public float error_rate;                    // percentage of a bad event happening

    // UI:
    public Text display_np;                     // text game object to update
    public Text display_month;                  // --
    public Text display_error_rate;             // --

    public Button btn_menu;                     // --

    public GameObject decision_box;             // decision box objects
    public Text decision_text;                  // --
    public string current_decision_text;        // --
    public Text increased_text;                 // --

    public Text company_title;                  // text displaying chosen company

    public GameObject event_box;                // event box objects
    public GameObject single_event_box;         // event box for only one "choice"
    public Text event_text;                     // --
    public string current_event_text;           // --
    public Text choice_text;                    // --
    public string current_choice_text;          // --
    public Text final_choice_text;              // --
    public Text final_event_text;               // --

    public GameObject q_event_box;              // quarterly event box objects
    public Text q_event_text;                   // --
    public Text name_perk_1;                    // --
    public Text info_perk_1;                    // --
    public Text name_perk_2;                    // --
    public Text info_perk_2;                    // --

    public GameObject skip_topics_button;      // button to skip to custom topics.

    //private Color original_color;               // store original color of text

    public bool in_dialogue;                    // dialogue flag to check if currently in dialogue

    public bool mitigate_event;                 // flag to mitigate bad event

    public List<Tweener> tweens = new List<Tweener>();  // for DOTween killing

    public int lastMonth = 99; // set this to a large number until the number of custom topics is known.

    // Backup events that may occur
    // Certain cases added multiple times to increase the chance of that specific event over others
    public ArrayList dataLossEvents = new ArrayList() 
    {
            "Hard drive failure!",
            "Hard drive failure!",
            "Hard drive failure!",
            "Hard drive failure!",
            "Hard drive failure!",
            "Employee steals physical backups!",
            "Employee loses sensitive info on USB!",
            "Fire in building!",
    };

    // starting company values
    // TODO: may need adjustments
    private int comp_s_start_np     = 100;
    private int comp_m_start_np     = 200;
    private int comp_l_start_np     = 500;

    private int comp_s_monthly_np   = 10;
    private int comp_m_monthly_np   = 20;
    private int comp_l_monthly_np   = 50;

    private float comp_s_error_rate = 0.1f;
    private float comp_m_error_rate = 0.2f;
    private float comp_l_error_rate = 0.4f;

    // state machine for game loop
    public enum State
    {
        title,      // 0
        instruct,   // 1
        options,    // 2
        main,       // 3
        end,        // 4
        about       // 5
    }

    public State CURRENT_STATE = State.title;

    // state machine for chosen company
    public enum Company
    {
        none,   // 0
        small,  // 1
        med,    // 2
        large   // 3
    }

    public Company CHOSEN_COMPANY = Company.none;

    // runs before start
    void Awake()
    {
        // simple singleton
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        network_power   = 0;
        current_month   = 0;
        monthly_np      = 0;
        error_rate      = 0.0f;

        //original_color = txt_title.color;
        in_dialogue = false;

        mitigate_event = false;

        ChooseNPCName();
    }

    void setName()
    {
        SceneControllerTitle sct = GameObject.Find("scn_title_CONTROL").GetComponentInChildren<SceneControllerTitle>();
        playerName = sct.getPlayerName();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F7))
        {
            ResetGame();
        }
    }

    // resets the game
    public void ResetGame()
    {
        AudioListener.volume = 0.5f;
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ChooseNPCName()
    {
        string[] names = { "Stu Steiner", "Dan Tappan", "Holger Carlsen", "Cyrus Wells", "John Smith", "Arthur Hawk", "Ludwig Vance", "Laurence Vicar", "Stephen Bright", "Robert Rigney" };
        System.Random rnd = new System.Random();
        string chosen = names[rnd.Next(0, names.Length)];
        GameObject.Find("txt_name").GetComponent<Text>().text = chosen;
    }

    // /!\ important game control functions below:

    // GETTERS AND SETTERS
    public int GetState()
    {
        return (int)CURRENT_STATE;
    }

    public void SetState(int x)
    {
        CURRENT_STATE = (State)x;
    }

    public int GetNetworkPower()
    {
        return network_power;
    }

    public void SetNetworkPower(int x)
    {
        network_power = x;
    }

    public void IncreaseNP(int x)
    {
        GameObject.FindObjectOfType<MainUIController>().PopText(display_np ,true); // true = green; false = red

        network_power += x;
        UpdateDisplayNP();
    }

    public void DecreaseNP(int x)
    {
        GameObject.FindObjectOfType<MainUIController>().PopText(display_np, false);

        network_power -= x;
        UpdateDisplayNP();
    }

    public int GetMonthlyNP()
    {
        return monthly_np;
    }

    public void SetMonthlyNP(int x)
    {
        monthly_np = x;
    }

    public void IncreaseMonthlyNP(int x)
    {
        monthly_np += x;
    }

    public void DecreaseMonthlyNP(int x)
    {
        monthly_np -= x;
    }

    public void SetErrorRate(float x)
    {
        error_rate = x;
    }

    public void IncreaseErrorRate(float x)
    {
        Debug.Log("Error rate increased by " + x);

        GameObject.FindObjectOfType<MainUIController>().PopText(display_error_rate, false);

        // clamps increase (no % above 100)
        if((error_rate + x) > 1)
        {
            error_rate = 1;
        } else {
            error_rate += x;
        }

        UpdateDisplayErrorRate();
    }

    public void DecreaseErrorRate(float x)
    {
        Debug.Log("Error rate decreased by " + x);

        GameObject.FindObjectOfType<MainUIController>().PopText(display_error_rate, true);

        // clamps decrease (no negative %)
        if((error_rate - x) < 0)
        {
            error_rate = 0;
        } else {
            error_rate -= x;
        }

        UpdateDisplayErrorRate();
    }

    // returns true if in dialogue, false if not
    public bool InDialogue()
    {
        return in_dialogue;
    }

    // switch for dialogue flag
    public void DialogueSwitch()
    {
        if(!in_dialogue)
        {
            in_dialogue = true;
        } else {
            in_dialogue = false;
        }
    }

    // set the starting company
    public void SetCompany(int x)
    {
        setName();

        if (!in_dialogue)
        {
            // should only be clicked when company is chosen (clicked)
            CHOSEN_COMPANY = (Company)x;

            Debug.Log("Company chosen: " + CHOSEN_COMPANY);

            // play a beep sound
            GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

            // destroy the choices buttons (see function below)
            StartCoroutine(ClickedChoice());

            // run setters depending on the company chosen
            switch(CHOSEN_COMPANY)
            {
                case(Company.small):
                {
                    SetNetworkPower (comp_s_start_np);
                    SetMonthlyNP    (comp_s_monthly_np);
                    SetErrorRate    (comp_s_error_rate);

                    company_title.text = "Max Square";

                    current_decision_text = playerName + ", You have chosen to work with a small company. " +
                        "Less people, however\n it won't be stress-free.";
                } break;

                case(Company.med):
                {
                    SetNetworkPower (comp_m_start_np);
                    SetMonthlyNP    (comp_m_monthly_np);
                    SetErrorRate    (comp_m_error_rate);

                    company_title.text = "Just Triangle";

                    current_decision_text = playerName + ", You have chosen to work with a medium-sized company." +
                        "\nThere will be stress.";
                } break;

                case(Company.large):
                {
                    SetNetworkPower (comp_l_start_np);
                    SetMonthlyNP    (comp_l_monthly_np);
                    SetErrorRate    (comp_l_error_rate);

                    company_title.text = "Circle Zilla";

                    current_decision_text = playerName + ", You have chosen to work with a large company. " +
                        "More people, more stress.";
                } break;
            }

            UpdateDisplayMonth();
            UpdateDisplayNP();
            DisplayErrorRate();
            UpdateDisplayErrorRate();
            DisplayDecision();
            skip_topics_button.SetActive(false);
        }
    }

    // coroutine accompanying SetCompany() when choosing company
    IEnumerator ClickedChoice()
    {
        // get all choice button objects
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("FirstChoice");

        // deactivate all buttons first
        foreach(GameObject b in buttons)
        {
            b.GetComponent<Button>().interactable = false;
            b.GetComponent<CanvasGroup>().DOFade(0, 1);
        }

        // move buttons away
        foreach(GameObject b in buttons)
        {
            b.transform.DOLocalMoveY(600, 1);
            Destroy(b, 1);
        }

        // bring in company's title
        /*company_title.gameObject.SetActive(true);
        company_title.transform.DOLocalMoveY(250, 1);*/
        yield return null;
    }

    // DISPLAY:
    // TODO: if possible, refactor display functions into a different script

    // kills all currently animating tweening sequences
    public void KillTweens()
    {
        foreach(Tweener t in tweens)
        {
            t.Kill();
        }
    }

    public void UpdateDisplayNP()
    {
        display_np.text = "Network Power (NP): " + network_power;
    }

    public void DisplayNP()
    {
        display_np.gameObject.SetActive(true);
        UpdateDisplayNP();
    }

    private string ErrorLevelAsString()
    {
        string error_level = "";

        if(error_rate <= 0.05)
        {
            error_level = "Mild";
        } else if(error_rate > 0.05 && error_rate <= 0.15) {
            error_level = "Minor";
        } else if(error_rate > 0.15 && error_rate <= 0.3) {
            error_level = "Moderate";
        } else if(error_rate > 0.3 && error_rate <= 0.5) {
            error_level = "Serious";
        } else if(error_rate > 0.5 && error_rate <= 0.75) {
            error_level = "Severe";
        } else if(error_rate > 0.75) {
            error_level = "Disastrous";
        }

        return error_level;
    }

    public void UpdateDisplayErrorRate()
    {
        display_error_rate.text = "Error Rate: " + ErrorLevelAsString();
    }

    public void DisplayErrorRate()
    {
        display_error_rate.gameObject.SetActive(true);
    }

    public void UpdateDisplayMonth()
    {
        display_month.text = "Month: " + current_month;
    }

    public void DisplayMonth()
    {
        display_month.gameObject.SetActive(true);
        UpdateDisplayMonth();
    }

    // "decision" is more like "results"
    public void UpdateDisplayDecision()
    {
        decision_text.text = current_decision_text;
        increased_text.text = "<i>+ " + monthly_np + " Network Power</i>";
    }

    public void DisplayDecision()
    {
        Debug.Log("Results displayed.");
        UpdateDisplayDecision();

        // reset display box scale, activate it, then enlarge it
        decision_box.transform.localScale = new Vector3(0, 0, 0);
        decision_box.gameObject.SetActive(true);
        tweens.Add(decision_box.transform.DOScale(new Vector3(1, 1, 1), 1));
    }

    public void HideDecision()
    {
        Debug.Log("current month? " + current_month + " last month? " + lastMonth);
        if(current_month != lastMonth)
        {
            if(!in_dialogue)
            {
                // stop all animations
                KillTweens();

                // hide the decision box
                decision_box.gameObject.SetActive(false);

                // when this is called, decision's continue button has been pressed
                IncrementMonth();

                // start the next event
                Dialogue end_dialogue = GameObject.Find("dlg_end").GetComponent<DialogueTrigger>().dialogue;
                end_dialogue.sentences[0] = end_dialogue.sentences[0].Replace("[x]", lastMonth.ToString());
                NextEvent();
            }
        } else { // If the year is FINISHED! No more events to display!
            
            decision_box.gameObject.SetActive(false);

            Dialogue end_dialogue = GameObject.Find("dlg_end").GetComponent<DialogueTrigger>().dialogue;

            end_dialogue.sentences[0] = end_dialogue.sentences[0].Replace("[x]", lastMonth.ToString());

            // change dialogue to reflect score
            switch (CHOSEN_COMPANY) {

            case (Company.small): {
                end_dialogue.sentences[2] =
                    "A total network power score of " + network_power + "..." +
                    "\nMonthly network power at " + monthly_np + "..." +
                    "\nError rate at a " + ErrorLevelAsString().ToLower() + " level...";

                    if(GetNetworkPower() < 100) {
                        end_dialogue.sentences[3] =
                            "Cyber Security is challenging! You let your company down this time... Better luck at your next job!";
                    }

                    if(GetNetworkPower() >= 100 && GetNetworkPower() < 250) {

                        // Downgrade one level if error rate is too high
                        if(error_rate > .3f) {
                            end_dialogue.sentences[3] =
                            "Cyber Security is challenging! You let your company down this time... Better luck at your next job!";
                        } else {
                            end_dialogue.sentences[3] =
                            "Good job, but there is room for improvement before you can consider yourself a cyber security expert.";
                        }
                    }

                    if(GetNetworkPower() >= 250) {

                        // Downgrade one level if error rate is too high
                        if(error_rate > .3f) {
                            end_dialogue.sentences[3] =
                            "Good job, but there is room for improvement before you can consider yourself a cyber security expert.";
                        } else {
                            end_dialogue.sentences[3] =
                            "Great job! You should consider yourself a cyber security pro. Do you think you're up to the challenge of a larger company?";
                        }
                    }

                    break;
                }

            case (Company.med): {
                end_dialogue.sentences[2] =
                        "A total network power score of " + network_power + "..." +
                        "\nMonthly network power at " + monthly_np + "..." +
                        "\nError rate at a " + ErrorLevelAsString().ToLower() + " level...";

                    if (GetNetworkPower() < 200) {
                        end_dialogue.sentences[3] =
                            "Cyber Security is challenging! You let your company down this time... Better luck at your next job! (Maybe try a smaller company)";
                    }

                    if (GetNetworkPower() >= 200 && GetNetworkPower() < 400) {

                        // Downgrade one level if error rate is too high
                        if (error_rate > .3f) {
                            end_dialogue.sentences[3] =
                            "Cyber Security is challenging! You let your company down this time... Better luck at your next job! (Maybe try a smaller company)";
                        } else {
                            end_dialogue.sentences[3] =
                            "Good job, but there is room for improvement before you can consider yourself a cyber security expert.";
                        }
                        
                    }

                    if (GetNetworkPower() >= 400) {

                        // Downgrade one level if error rate is too high
                        if (error_rate > .3f) {
                            end_dialogue.sentences[3] =
                            "Good job, but there is room for improvement before you can consider yourself a cyber security expert.";
                        } else {
                            end_dialogue.sentences[3] =
                            "Great job! You should consider yourself a cyber security pro. Do you think you're up to the challenge of a larger company?";
                        }
                    }

                    break;
                }

            case (Company.large): {
                end_dialogue.sentences[2] =
                        "A total network power score of " + network_power + "..." +
                        "\nMonthly network power at " + monthly_np + "..." +
                        "\nError rate at a " + ErrorLevelAsString().ToLower() + " level...";

                    if (GetNetworkPower() < 700) {
                        end_dialogue.sentences[3] =
                            "Cyber Security is challenging! You let your company down this time... Better luck at your next job! (Maybe try a smaller company)";
                    }

                    if (GetNetworkPower() >= 700 && GetNetworkPower() < 1000) {

                        // Downgrade one level if error rate is too high
                        if(error_rate > .3) {
                            end_dialogue.sentences[3] =
                            "Cyber Security is challenging! You let your company down this time... Better luck at your next job! (Maybe try a smaller company)";
                        } else {
                            end_dialogue.sentences[3] =
                            "Good job, but there is room for improvement before you can consider yourself a cyber security expert.";
                        }
                        
                    }

                    if (GetNetworkPower() >= 1000) {

                        // Downgrade one level if error rate is too high
                        if (error_rate > .3) {
                            end_dialogue.sentences[3] =
                            "Good job, but there is room for improvement before you can consider yourself a cyber security expert.";
                        } else {
                            end_dialogue.sentences[3] =
                            "Great job! You should consider yourself a cyber security expert!";
                        }
                    }
                    break;
                }
            }
            
            GameObject.Find("dlg_end").GetComponent<DialogueTrigger>().TriggerDialogue();
            
        }
    }

    public void UpdateDisplayEvent()
    {
        //TODO: modularize event_text and q_event_text (different scripts)
        event_text.text = current_event_text;
        choice_text.text = current_choice_text;
        q_event_text.text = current_event_text;
    }

    public void DisplayEvent(GameObject g)
    {
        Debug.Log("Recap displayed.");

        // reset the event box alpha, fade it in, reset its position, activate it, then center it
        g.GetComponent<CanvasGroup>().alpha = 0;
        tweens.Add(g.GetComponent<CanvasGroup>().DOFade(1, 1));
        g.transform.localPosition = new Vector3(-200, -50, 1);
        g.gameObject.SetActive(true);
        tweens.Add(g.transform.DOLocalMoveX(0, 1));

        UpdateDisplayEvent();
    }


    public void DisplayFinalEvent(GameObject g) {
        
        Debug.Log("Recap displayed.");

        // reset the event box alpha, fade it in, reset its position, activate it, then center it
        g.GetComponent<CanvasGroup>().alpha = 0;
        tweens.Add(g.GetComponent<CanvasGroup>().DOFade(1, 1));
        g.transform.localPosition = new Vector3(-200, -50, 1);
        g.gameObject.SetActive(true);
        tweens.Add(g.transform.DOLocalMoveX(0, 1));

        UpdateDisplayEvent();
    }

    //This function will be called from the nextEvent() when the user clicked 
    //the "next month" button in the decision box
    public void DisplayEvent()
    {
        DisplayEvent(event_box);
    }

    public void DisplayFinalEvent() {
        DisplayFinalEvent(single_event_box);
    }

    // DataBreach() gets called from ActivateChoice(bool x)
    public bool DataBreach() { // Gets called from ActivateChoice(bool x)
        System.Random randomInt = new System.Random();
        int chanceBreachOccurs = randomInt.Next(1, 100);

        if(chanceBreachOccurs < 59) {
            return true;
        }

        return false;
    }

    public void DisplayQuarterlyEvent()
    {
        if (current_month == lastMonth)
        {
            q_event_box.transform.Find("txt_title").gameObject.GetComponent<Text>().text = "Final Recap";
        }
        DisplayEvent(q_event_box);
    }

    public void HideEvent()
    {
        event_box.gameObject.SetActive(false);
        q_event_box.gameObject.SetActive(false);
    }

    // increments month and np
    public void IncrementMonth()
    {
        current_month++;

        IncreaseNP(monthly_np);

        UpdateDisplayMonth();
    }

    IEnumerator DataLossCoroutine(float delay, Func<bool> waitUntil) {

        if (delay > 0) {
            yield return new WaitForSeconds(delay);
        }

        if (waitUntil != null) {
            yield return new WaitUntil(waitUntil);
        }

        // Checks if backup occurs, and if it is protected against
        if (DataLoss()) {
            string dataLossEvent = DataLossEvent();
            CheckProtection(dataLossEvent);
        }
    }

    // Random problem based on backup chosen/not chosen
    public bool DataLoss() {
        System.Random randomInt = new System.Random();
        int chanceDataLossOccurs = randomInt.Next(1, 3);
        Debug.Log("Random event number (1-4) : " + chanceDataLossOccurs.ToString());
             // Data loss has a 1 in 10 chance of happening
             // Will be another chance every month
             if (chanceDataLossOccurs == 2) {
                 return true;
             }

             return false;
    }

    // Pseudo random way to choose which data loss event happens
    // 1/2 chance for hard drive failure, 1/5 for dropbox hack (IF CLOUD CHOSEN)
    // 1/10 for all the rest
    public string DataLossEvent() {
        System.Random randomNum = new System.Random();
        int chooseEvent = randomNum.Next(0, dataLossEvents.Count - 1);
        //Debug.Log("Random event chosen: " + dataLossEvents[chooseEvent].ToString());

        //for(int i = 0; i < dataLossEvents.Count - 1; i++) {
        //    Debug.Log("Possible events on list: " + dataLossEvents[i].ToString() + "\n"); 
       // }

        return (dataLossEvents[chooseEvent].ToString());
    }

    // Only occurs if cloud is chosen as a backup
    // Adds the possibility of dropbox being hacked to the list of data loss events
    public void AddCloudEventToList() {
        dataLossEvents.Add("Dropbox hacked!");
        dataLossEvents.Add("Dropbox hacked!");
    }

    // Checks the different possible events
    // Returns true if user has appropriate protection, false otherwise
    // Penalty if no protection
    public void CheckProtection(string badEvent) {
        
        if(badEvent == "Hard drive failure!") {

            // Lowers probability for hard drive failure to occur again from 50% to ~10%
            dataLossEvents.RemoveRange(0, 4);
            dataLossEvents.Add("Employee steals physical backups!");
            dataLossEvents.Add("Employee steals physical backups!");
            dataLossEvents.Add("Employee steals physical backups!");
            dataLossEvents.Add("Fire in building!");
            dataLossEvents.Add("Fire in building!");
            dataLossEvents.Add("Fire in building!");
            dataLossEvents.Add("Employee loses sensitive info on USB!");
            dataLossEvents.Add("Employee loses sensitive info on USB!");
            dataLossEvents.Add("Employee loses sensitive info on USB!");

            if (backup_usb || backup_cld) {
                DisplayBackupOutcome("dlg_hdd_pass");
            } else {
                DisplayBackupOutcome("dlg_hdd_fail");
                GameControllerV2.Instance.DecreaseNP(125);
            }
        }
        
        if(badEvent == "Dropbox hacked!") {
            if(backup_usb || backup_hdd) {
                DisplayBackupOutcome("dlg_dropbox_pass");
            } else {
                DisplayBackupOutcome("dlg_dropbox_fail");
                // Increase error rate 30%, reduce NP 50
                GameControllerV2.Instance.DecreaseNP(50);
                GameControllerV2.Instance.IncreaseErrorRate(0.3f);
            }
        }
        
        if(badEvent == "Employee steals physical backups!") {
            if(backup_cld) {
                DisplayBackupOutcome("dlg_stolen_pass");
            } else {
                DisplayBackupOutcome("dlg_stolen_fail");
                // Increase error rate 30%, reduce NP 50
                GameControllerV2.Instance.DecreaseNP(50);
                GameControllerV2.Instance.IncreaseErrorRate(0.3f);
            }
        }

        if (badEvent == "Fire in building!") {
            if (backup_cld) {
                DisplayBackupOutcome("dlg_fire_pass");
            } else {
                DisplayBackupOutcome("dlg_fire_fail");
                // Decrease NP 100
                GameControllerV2.Instance.DecreaseNP(100);
            }
        }

        if (badEvent == "Employee loses sensitive info on USB!") {
            if(backup_cld || backup_hdd) {
                DisplayBackupOutcome("dlg_loss_pass");
            } else {
                DisplayBackupOutcome("dlg_loss_fail");
                // Increase error rate 30%, reduce NP 50
                GameControllerV2.Instance.DecreaseNP(50);
                GameControllerV2.Instance.IncreaseErrorRate(0.3f);
            }
        }
    }

    // Final outcome of backup scene + random probability it occurs
    // Prints out in form of dialogue
    public void DisplayBackupOutcome(string backupEvent) {
        GameObject.Find(backupEvent).GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    // "events" handling below:

    public bool RollError()
    {
        // Random.value returns a random number between 0.0 and 1.0 [inclusive]
        // ex: small company error rate: 0.1 >= 0.2345 (no-error); 0.1 >= 0.0543 (error)
        float rand = UnityEngine.Random.value;
        if(error_rate >= rand)
        {
            Debug.Log(error_rate + " > " + rand + " Bad event occurred.");
            return true;
        } else {
            Debug.Log(error_rate + " < " + rand + " Bad event avoided.");
            return false;
        }
    }

    // lineup next event --------------------------------------------------------------------------------------------------------------------------------
    public void NextEvent() 
    {
        Debug.Log("Month " + current_month + " event triggered.");
        Debug.Log("last month? " + lastMonth);
        ActivateEvent(RollError());

        if(current_month == lastMonth + 1) {
            DisplayFinalEvent();
        } else if(current_month == 3 || current_month == 6 || current_month == 9 || current_month == lastMonth) {
            DisplayQuarterlyEvent();
        } else {
            DisplayEvent();
        }
    }
    
    // IMPORTANT: what to do when a bad event occurs
    // ebox - event box (ctrl+f to find this easily)
    public void ActivateEvent(bool bad_event_occurred)
    {
        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

        switch(current_month)
        {
            // Month 1 event - password quiz offered
            case(1): {

                // First month has no chance of bad events/failure
                current_event_text = GoodMessage();
                
                // Password minigame
                current_choice_text = "First up on the agenda, " + playerName +
                "\nWould you like to hold a password strength training session?\n" +
                "<b>Cost: 10% of NP</b>";
                return;
            }

            // Month 2 event - backup offered
            case(2):
            {
                if(bad_event_occurred)
                {
                    // decrease NP by a scaling amount
                    int temp_np = network_power;

                    // Amount decreased by
                    if(CHOSEN_COMPANY == Company.small)
                    {
                        DecreaseNP(10);
                    } else if(CHOSEN_COMPANY == Company.med) {
                        DecreaseNP(20);
                    } else {
                        DecreaseNP(50);
                    }

                    // calculate difference
                    int np_difference = temp_np - network_power; 

                    // Event that occurred
                    current_event_text = "Attention " + playerName + ", An employee's email has been hacked.\n" +
                    "<i>NP has decreased by " + np_difference + ".</i>";

                } else {
                    current_event_text = GoodMessage();
                }

                // Backup minigame
                current_choice_text = "The company is making progress, " + playerName + ", thanks to your help." +  
                "\nWould you like to execute a company-wide file backup plan?\n" +
                "<b>Cost: 10% of NP</b>";
                return;
            }

            // Month 3 event - perk offered
            case(3):
            {
                // increase NP for the first "third" of the game
                int temp_np = network_power;
                IncreaseNP(monthly_np * 2);

                // calculate difference
                int np_difference = network_power - temp_np; 

                current_event_text += "\nProgress bonus received. " +
                                      "<i>NP has increased by " + np_difference + ".</i>";

                // left perk text
                name_perk_1.text = "Firmware Update";
                info_perk_1.text = "Error rate decreases notably";

                // Right perk text
                name_perk_2.text = "Now Hiring";
                info_perk_2.text = "Monthly NP increases by 20";

                if (bad_event_occurred) {
                    // decrease monthly NP by a scaling amount - 10% of monthly NP
                    DecreaseMonthlyNP(Mathf.RoundToInt(monthly_np * 0.1f));

                    current_event_text = "Company has reported low earnings. Morale is low.\n" +
                        "<i>Monthly NP has decreased by 10%.</i>";
                } else {
                    current_event_text = GoodMessage();
                }
                return;
            }

            // month 4 event
            case(4):
            {
                // decrease NP by a scaling amount
                int temp_np = network_power; 

                // Amount decreased by
                if (CHOSEN_COMPANY == Company.small) {
                    DecreaseNP(10);
                } else if (CHOSEN_COMPANY == Company.med) {
                    DecreaseNP(20);
                } else {
                    DecreaseNP(50);
                }

                // calculate difference 
                int np_difference = temp_np - network_power; 

                if (bad_event_occurred) {
                    // Event that occurs
                    current_event_text = "Not enough people for current project." +
                                         "<i>NP has decreased by " + np_difference + ".</i>";
                } else {
                    current_event_text = GoodMessage();
                }
                
                // Phishing minigame
                current_choice_text = "An employee has fallen for a phishing attempt. " +
                    playerName + " could you hold a company meeting to discuss the dangers of phishing?" +
                    "\n<b>Cost: 10% of NP</b>";
                return;
            }

            // month 5 event
            case(5):
            {
                if (bad_event_occurred) 
                {
                    //GameControllerV2.Instance.DecreaseMonthlyNP(10);

                    // decrease monthly NP by a scaling amount - 10% of monthly NP
                    DecreaseMonthlyNP(Mathf.RoundToInt(monthly_np * 0.1f));

                    current_event_text = playerName + ", Someone in your company loses a USB with sensitive,unprotected information on it. " +
                                         "\n<i>Monthly NP has decreased by 10 %.</i>";
                } else {
                    current_event_text = GoodMessage();
                }
                
                // Virus presentation
                current_choice_text = playerName + ", Do you feel the need to brush up on computer viruses." +
                    "\nDo some research? (No penalty for declining.)" +
                    "\n<b>Cost: 10% of NP</b>";
                return;
            }

            // month 6 event
            case(6):
            {
                //int temp_np = network_power; --> showed a console message that it was never used

                IncreaseNP(monthly_np * 2);

                // calculate difference
                //int np_difference = network_power - temp_np; --> showed a console message that it was never used

                name_perk_1.text = "Honeypot";
                info_perk_1.text = "NP increases by 20%\nError rate decreases";

                name_perk_2.text = "Antivirus";
                info_perk_2.text = "Error rate decreases significantly";

                if (bad_event_occurred) {

                    // decrease monthly NP by half
                    DecreaseMonthlyNP(monthly_np / 2);

                    current_event_text = "Company has reported low earnings. Morale is low." +
                        "\n<i>Monthly NP has halved.</i>";
                        
                } else {
                    current_event_text = GoodMessage();
                }
                
                // Caesar Cipher
                current_choice_text = playerName + ", an employee wants to email sensitive information. " +
                "Would you like to learn about encryption?" +
                "\n<b>Cost: 20 NP</b>";
                return;
            }

            // month 7 event
            case(7):
            {
                if (bad_event_occurred) {

                    // decrease NP by a scaling amount
                    int temp_np = network_power;

                    // decrease np by 30%
                    DecreaseNP(Mathf.RoundToInt(network_power * 0.3f));

                    // calculate difference
                    int np_difference = temp_np - network_power;

                    current_event_text = playerName + ", An employee has fallen for a phishing attempt, " +
                                         "\ncausing some parts of your system to be compromised." +
                                         "\n<i>NP has decreased by " + np_difference + ".</i>";
                        
                } else {
                    current_event_text = GoodMessage();
                }

                return;

            }

            // month 8 event
            case (8):
            {

                if (bad_event_occurred)
                {
                    current_event_text = playerName + ", An employee downloads malware without realizing."
                                                    + "\nNP has decreased by 30.";
                }
                else
                {
                    current_event_text = GoodMessage();
                }

                // Virus quiz
                current_choice_text = playerName + ", The boss requests all employees to take a quiz to prove their knowledge on viruses." +
                    "\n<b>50 NP penalty for declining</b>";
                return;
            }

            // month 9 event
            case (9):
            {
                // increase NP for the first "third" of the game
                int temp_np = network_power;
                IncreaseNP(monthly_np * 2);

                // calculate difference
                int np_difference = network_power - temp_np;

                current_event_text += "\nProgress bonus received. " +
                                      "<i>NP has increased by " + np_difference + ".</i>";
                // left perk text
                name_perk_1.text = "Try SMS marketing";
                info_perk_1.text = "Error rate decreases notably";

                // Right perk text
                name_perk_2.text = "Launch a new secured product";
                info_perk_2.text = "Monthly NP increases by 20";

                if (bad_event_occurred)
                {
                    // decrease monthly NP by a scaling amount - 10% of monthly NP
                    DecreaseMonthlyNP(Mathf.RoundToInt(monthly_np * 0.1f));

                    current_event_text = "Company has reported high volume of expenses.\n" +
                                         "<i>Monthly NP has decreased by 10%.</i>";
                }
                else
                {
                     current_event_text = GoodMessage();
                }

                return;
            }

            // month 10 event
            case(10):
            {
                Debug.Log("CASE 10 ----   1");

                if(bad_event_occurred)
                {
                    if(mitigate_event)
                    {
                        Debug.Log("Bad event mitigated.");
                        mitigate_event = false;
                        current_event_text = GoodMessage();
                        return;
                    }

                    // decrease monthly NP by a scaling amount - 10% of monthly NP
                    DecreaseMonthlyNP(Mathf.RoundToInt(monthly_np * 0.1f));

                    current_event_text = playerName + ", Someone in your company loses a USB with sensitive,unprotected information on it. " +
                            "\n<i>Monthly NP has decreased by 10 %.</i>";
                    //GameControllerV2.Instance.DecreaseMonthlyNP(10);

                } else {
                    current_event_text = GoodMessage();
                }
                
                current_choice_text = playerName + ", A significant part of your learning process as an IT specialist is to get knowledge of one time pad encryption. " +
                        "Solve the following challenge to advance your knowledge.";

                return;
            }
            
            // month 11 event
            case (11):
            {
                if (bad_event_occurred)
                {
                    // decrease NP by a scaling amount
                    int temp_np = network_power;

                    // Amount decreased by
                    if (CHOSEN_COMPANY == Company.small)
                    {
                        DecreaseNP(10);
                    }
                    else if (CHOSEN_COMPANY == Company.med)
                    {
                        DecreaseNP(20);
                    }
                    else
                    {
                        DecreaseNP(50);
                    }

                    // calculate difference
                    int np_difference = temp_np - network_power;

                    // Event that occurred
                    current_event_text = playerName +
                                         ", your system was cracked by experienced hackers because you have not updated your system in a long time.\n" +
                                         "<i>NP has decreased by " + np_difference + ".</i>";
                }
                else
                {
                    current_event_text = GoodMessage();
                }

                // decrease monthly NP by a scaling amount - 5% of monthly NP
                DecreaseMonthlyNP(Mathf.RoundToInt(monthly_np * 0.05f));

                current_choice_text = playerName + ", would you like to learn about RSA? That is one of the most important encryption technique today!" +
                                      "\n<b>Cost: 5% of Monthly NP</b>";
                return;
            }

            // custom topic event
            default:
                if (current_month != lastMonth)
                {
                    GameObject.Find("Canvas").transform.Find("stage_custom_topics").gameObject.SetActive(true);
                    CustomTopicQuizController controller = GameObject.Find("stage_custom_topics").GetComponent<CustomTopicQuizController>();

                    lastMonth = 12 + controller.getTopicCount();

                    current_event_text = GoodMessage();

                    current_choice_text = "Would you like to learn about " + controller.getNextTopicName() + "?";

                    return;
                }

                break;
        }

        if (current_month == lastMonth)
        {
            Debug.Log("does this happen?");
            // this is the end of the game.
            if (bad_event_occurred)
            {
                // decrease NP by a scaling amount
                int temp_np = network_power;

                // Amount decreased by
                if (CHOSEN_COMPANY == Company.small)
                {
                    DecreaseNP(10);
                }
                else if (CHOSEN_COMPANY == Company.med)
                {
                    DecreaseNP(20);
                }
                else
                {
                    DecreaseNP(50);
                }

                // calculate difference
                int np_difference = temp_np - network_power;

                // Event that occurred
                current_event_text = "Your company suffers from a heavy deficit.\n" +
                                     "<i>NP has decreased by " + np_difference + ".</i>";

            }
            else
            {
                current_event_text = GoodMessage();
            }

            name_perk_1.text = "Promotion";
            info_perk_1.text = "You're being offered a higher position";

            name_perk_2.text = "Back to School";
            info_perk_2.text = "Getting a M.S degree for an increase in your paycheck";
        }
    }

    // Returns a random positive message
    private string GoodMessage()
    {
        string[] good_messages = {
            "The month goes by smoothly.",
            "The month goes by perfectly.",
            "The month goes by very well.",
            "The month goes by effortlessly.",
            "The month goes by simply.",
            "The month goes by easily.",
            "The month goes by uncomplicatedly."
        };

        int rand = UnityEngine.Random.Range(0, good_messages.Length);
        string a_good_message = good_messages[rand];

        return a_good_message;
    }

    public void EventYesNo(bool x)
    {
        if(!in_dialogue)
        {
            // stop all tweens
            KillTweens();

            HideEvent();
            ActivateChoice(x);
        }
    }

    // coroutine to transition to events
    IEnumerator TransitionToEvent(GameObject g)
    {
        // glitch screen
        GameObject.FindObjectOfType<GlitchCamera>().StartGlitch();
        g.SetActive(true);
        yield return null;
    }

    // dbox - decision box (ctrl+f to find this easily)
    public void ActivateChoice(bool x)
    {
        Debug.Log("Choice chosen: " + x);

        // play a beep sound
        GameObject.Find("SoundManager").GetComponent<AudioControllerV2>().PlaySound(1);

        switch(current_month)
        {
            // month 1 choice
            case(1):
            {
                if(x) // if "yes" has been chosen
                {
                    // decrease np by 10%
                    DecreaseNP(Mathf.RoundToInt(network_power * 0.1f));

                    // activate password quiz
                    StartCoroutine(TransitionToEvent(scn_quiz_password));

                } else { // if "no" has been chosen

                    // error rate increases by 10-20%
                    IncreaseErrorRate(UnityEngine.Random.Range(0.1f, 0.2f));

                    current_decision_text = playerName + ", you ignore the need for a good password. " +
                        "<i>Error rate has increased.</i>";

                    DisplayDecision();
                }

                return;
            }

            // month 2 choice
            case(2):
            {
                if(x)
                {
                    // decrease np by 10%
                    DecreaseNP(Mathf.RoundToInt(network_power * 0.1f));

                    // activate file backup choice
                    StartCoroutine(TransitionToEvent(scn_filebackup));
                } else {
                    // error rate increase by 10-20%
                    float rand_er = UnityEngine.Random.Range(0.1f, 0.2f);
                    IncreaseErrorRate(rand_er);

                    current_decision_text = playerName + ", you ignore the need to backup your files. " +
                        "<i>Error rate has increased.</i>";

                    DisplayDecision();
                }

                return;
            }

            // month 3 choice
            case(3):
            {
                // left perk or right perk
                if (x)
                {
                    // reduce error rate by 30% of current error rate
                    DecreaseErrorRate(error_rate * 0.3f);

                    current_decision_text = playerName + ", firmware updates have been administered throughout the company. " +
                        "<i>Error rate has been decreased significantly.</i>";

                    // trigger this piece of dialogue
                    GameObject.Find("dlg_firmware").GetComponent<DialogueTrigger>().TriggerDialogue();

                    DisplayDecision();
                } else {
                    // increase monthly np by 20
                    IncreaseMonthlyNP(20);

                    current_decision_text = playerName + ", your company has hired fresh new faces. " +
                        "<i>Monthly NP has increased by 20.</i>";

                    DisplayDecision();
                }

                return;
            }

            // month 4 choice
            case(4):
            {
                if (x) {
                    // decrease np by 10%
                    DecreaseNP(Mathf.RoundToInt(network_power * 0.1f));

                    // activate file phishing quiz
                    StartCoroutine(TransitionToEvent(scn_phishing_v2));

                    StartCoroutine(DataLossCoroutine(1f, () => !scn_phishing_v2.activeSelf));

                } else {
                    // error rate increase by 10-20%
                    float rand_er = UnityEngine.Random.Range(0.1f, 0.2f);
                    IncreaseErrorRate(rand_er);

                    current_decision_text = playerName + ", you ignore the need to learn about phishing. " +
                        "<i>Error rate has increased.</i>";
                    
                    StartCoroutine(DataLossCoroutine(0, null));

                    DisplayDecision();

                }

                return;
            }

            // month 5 choice
            case(5):
            {
                if (x)
                {
                    // decrease np by 10%
                    DecreaseNP(Mathf.RoundToInt(network_power * 0.1f));

                    // activate file Virus Presentation
                    StartCoroutine(TransitionToEvent(scn_virus_presentation));

                    StartCoroutine(DataLossCoroutine(1f, () => !scn_virus_presentation.activeSelf));

                    //current_decision_text = "Virus learning mini-game goes here.";

                    //DisplayDecision();
                } else {
                    current_decision_text = playerName + ", you have decided not to learn about viruses.";

                    StartCoroutine(DataLossCoroutine(0, null));

                    DisplayDecision();
                }

                return;
            }

            // month 6 choice 
            case(6):
            {
                // left perk or right perk
                if (x)
                {
                    int temp_np = network_power;

                    // increase np by 20%
                    IncreaseNP(Mathf.RoundToInt(network_power * 0.2f));

                    int np_difference = network_power - temp_np; // calculate difference

                    current_decision_text = playerName + ", a honeypot is protecting your company's systems. " +
                        "\n<i>NP has increased by " + np_difference + ". " +
                        "\nError rate has decreased.</i>";

                    // trigger this piece of dialogue
                    GameObject.Find("dlg_honeypot").GetComponent<DialogueTrigger>().TriggerDialogue();

                    DisplayDecision();
                } else {
                    // half current error rate
                    DecreaseErrorRate(error_rate / 2);

                    current_decision_text = playerName + ", antivirus software is protecting your company's systems. " +
                        "<i>Error rate has decreased significantly.</i>";

                    // trigger this piece of dialogue
                    GameObject.Find("dlg_antivirus").GetComponent<DialogueTrigger>().TriggerDialogue();

                    DisplayDecision();
                }

                return;
            }

            // month 7 choice
            case(7):
            {

                if (x) {
                    // decrease np by 20
                    DecreaseNP(20);

                    // activate file Caesar Cipher Quiz
                    StartCoroutine(TransitionToEvent(scn_caesar_cipher));

                    current_decision_text = playerName + ", you feel more comfortable with encryption now.";

                    // StartCoroutine(DataLossCoroutine(1f, () => !scn_caesar_cipher.activeSelf));

                } else {
                    current_decision_text = playerName + ", you have decided not to learn about encryption.";

                    StartCoroutine(DataLossCoroutine(0, null));

                    DisplayDecision();
                }

                return;
            }

            // month 8 choice
            case(8):
            {
                if (x)
                {
                    // activate file Virus Presentation
                    StartCoroutine(TransitionToEvent(scn_virus_quiz));

                    StartCoroutine(DataLossCoroutine(1f, () => !scn_virus_quiz.activeSelf));

                } else {
                        
                    current_decision_text = playerName + ", you have made a poor choice in the eyes of your boss.";

                    DecreaseNP(50);

                    StartCoroutine(DataLossCoroutine(0, null));

                    DisplayDecision();
                }

                return;
            }

            // month 9 choice QUARTER
            case(9):
            {
                // left perk or right perk
                if (x) //If user chose SMS marketing 
                {
                    // reduce error rate by 30% of current error rate
                    DecreaseErrorRate(error_rate * 0.3f);

                    current_decision_text = playerName + ", perfect! Early indications show SMS to be far more responsive." +
                                                "\n<i>Error rate has been decreased significantly.</i>";
                }
                else //If user chose secured product option 
                {
                    // increase monthly np by 20
                    IncreaseMonthlyNP(20);

                    current_decision_text = playerName + ", your company has launch a new product that allows any business to search for security experts on our site. " +
                                            "\n<i>Monthly NP has increased by 20%.</i>";
                }

                DisplayDecision();
                return;
            }

            // month 10 choice
            
            case(10):
            {
                if (x) // if "yes" has been chosen
                {
                    // error rate increases by 10-20%
                    DecreaseErrorRate(UnityEngine.Random.Range(0.1f, 0.2f));

                    // activate Illustration 
                    StartCoroutine(TransitionToEvent(scn_one_time_pad));

                    // trigger this piece of dialogue
                    GameObject.Find("dlg_one_time_pad_illustration").GetComponent<DialogueTrigger>().TriggerDialogue();
                    
                    current_decision_text = "Very good! " + playerName + " You gained a very important information that will benefit you in the future!";
                }
                else// if "no" has been chosen
                { 
                        
                    // decrease np by 10%
                    DecreaseNP(Mathf.RoundToInt(network_power * 0.1f));

                    current_decision_text = playerName + ", you gave up a very useful information that could help you in the future. " +
                        "\n<i>NP rate has decreased.</i>";

                    DisplayDecision();
                }

                return;
            }
            
            // month 11 choice
            case(11):
            {
                if(x) // chose yes
                {
                    // error rate decrease by 10-20%
                    DecreaseErrorRate(UnityEngine.Random.Range(0.1f, 0.2f));

                    // activate Illustration 
                    StartCoroutine(TransitionToEvent(scn_RSA));

                    current_decision_text = "Great decision! " + playerName + " Learning the importance of RSA is very important since many technologies relay on it!" +
                            "Good luck!";

                    //DisplayDecision(); // That might have to be deleted..... <<<<----------CHECK
                } 
                else // chose no
                {
                    // decrease np by 10%
                    DecreaseNP(Mathf.RoundToInt(network_power * 0.1f));

                    current_decision_text = "Not a wise decision, " + playerName + ", you gave up a very important lesson on RSA encryption!" +
                            "\n<i>NP rate has decreased.</i> ";

                    DisplayDecision();
                }

                return;
            }

            default:
            {
                if (current_month != lastMonth)
                {
                    CustomTopicQuizController controller = GameObject.Find("stage_custom_topics")
                        .GetComponent<CustomTopicQuizController>();
                    if (x)
                    {
                        Debug.Log("next topic, activate stuff");
                        controller.nextTopic();
                    }
                    else
                    {
                        // another spot where the penalty for declining could be defined in the google sheet.
                        DecreaseNP(Mathf.RoundToInt(network_power * 0.1f));

                        current_decision_text = "You decided not to learn about " + controller.getNextTopicName() +
                                                "\n<i>NP rate has decreased.</i> ";
                        controller.skipTopic();

                        DisplayDecision();
                    }
                    return;
                }
                break;
            }
        }

        if (current_month == lastMonth)
        {
            if (x) // Promotion - chose the first option
            {
                // increase np by 10%
                IncreaseNP(Mathf.RoundToInt(network_power * 0.1f)); 

                current_decision_text = playerName + ", you have chosen to get a promotion at your current job!" +
                                            "\n<i>NP has increased by 10%</i>";
            }
            else
            {
                // Back to School - chose the second option
                DecreaseErrorRate(error_rate * 0.2f);

                current_decision_text = playerName + ", you have chosen to go back to school to get your M.S degree! That's a very good decision" +
                                        "\n<i> Error rate has decreased by 20%</i> ";
            }

            DisplayDecision();
            increased_text.text = "End Game";
        }
    }
}