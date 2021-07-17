using UnityEngine;
using UnityEngine.UI;
using System;

public class RSA_illustration : MonoBehaviour {

    private DialogueManager dialog; // -------

    public GameObject blackBackground; // Turn it off at the start to see better the images
    public GameObject girl;
    public GameObject boy;
    public GameObject envelope;
    public GameObject message;
    public GameObject sceneElevenToCheck;
    public GameObject private_frame_bob;//bob
    public GameObject public_frame_bob;//bob
    public GameObject private_frame_alice; // alice
    public GameObject public_frame_alice; // alice
    public GameObject lock_image;
    public GameObject back_btn;

    //Public/private keys:
    private GameObject[] keys;
    public GameObject key_1;//alice private
    public GameObject key_2;//alice public
    public GameObject key_3;//bob private
    public GameObject key_4;//bob public

    private Rigidbody2D girlRB;
    private Rigidbody2D boyRB;
    private Rigidbody2D envelopeRB;
    private Rigidbody2D messageRB;
    private Rigidbody2D private_frame_bob_RB;
    private Rigidbody2D public_frame_bob_RB;
    private Rigidbody2D private_frame_alice_RB;
    private Rigidbody2D public_frame_alice_RB;
    private Rigidbody2D key_1RB;
    private Rigidbody2D key_4RB;

    private Vector2 velocity;
    private bool communication;
    private bool reverse = false;
    private bool reverseLastPart = false;

    void Awake()
    {
        girlRB = girl.GetComponent<Rigidbody2D>();
        boyRB = boy.GetComponent<Rigidbody2D>();
        envelopeRB = envelope.GetComponent<Rigidbody2D>();
        messageRB = message.GetComponent<Rigidbody2D>();

        private_frame_bob_RB = private_frame_bob.GetComponent<Rigidbody2D>();
        public_frame_bob_RB = public_frame_bob.GetComponent<Rigidbody2D>();
        private_frame_alice_RB = private_frame_alice.GetComponent<Rigidbody2D>();
        public_frame_alice_RB = public_frame_alice.GetComponent<Rigidbody2D>();

        key_1RB = key_1.GetComponent<Rigidbody2D>();
        key_4RB = key_4.GetComponent<Rigidbody2D>();

        velocity = new Vector2(-5, 0); // controling the x and y posstion, will move 5 units on the x direction to the left

        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialog = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        //controller = GameObject.Find("OneTimePadSceneController").GetComponent<OneTimePadSceneController>();

        blackBackground.SetActive(false); //DO NOT FORGET TO TURN IT ON AT THE END OF THE DEMONSTRATION !!!!!!
        envelope.SetActive(false);// Wait to display the envelope with sentence 4:

        communication = true;

        back_btn.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(dialog.getFinished_typing());

        if(dialog.currentSentenceDisplayed == 5 || dialog.currentSentenceDisplayed == 6)
        {
            dialog.setProceed(false); // Lock the user from proceeding 

            if(boy.transform.position.x > 7.0) //Move boy pic:
            {
                boyRB.MovePosition(boyRB.position + velocity * Time.fixedDeltaTime);
            }
            if (girl.transform.position.x < -7.0) //Move girl pic:
            {
                girlRB.MovePosition(girlRB.position + velocity * (-1) * Time.fixedDeltaTime);
            }

            if(communication)
            {
                //If both pictures of the boy and the girl are in place, start to move the envelope until its x position is 4.3:
                if (boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0 && envelope.transform.position.x < 4.3)
                {
                    envelope.SetActive(true);
                    envelopeRB.MovePosition(envelopeRB.position + velocity * (-1) * Time.fixedDeltaTime);
                }
                if (envelope.transform.position.x >= 4.3) // If the envelope in its posstion:
                {
                    communication = false;
                    Debug.Log("Now false");
                }
            }
            else
            {
                //If both pictures of the boy and the girl are in place, start to move the envelope until its x position is 4.3:
                if (boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0 && envelope.transform.position.x > -4.1)
                {
                    envelopeRB.MovePosition(envelopeRB.position + velocity * Time.fixedDeltaTime);
                }
                if (envelope.transform.position.x <= -4.1) // If the envelope in its posstion:
                {
                    communication = true;
                }
            }

            if(boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0) // If the envelope in its posstion:
            {
                dialog.setProceed(true); // Let the user continue
            }
        }
        if (dialog.currentSentenceDisplayed == 6)
        {
            dialog.setProceed(false); // Lock the user from proceeding 
            key_1.SetActive(true);
            key_2.SetActive(true);
            key_3.SetActive(true);
            key_4.SetActive(true);
            dialog.setProceed(true); // Lock the user from proceeding
        }

        if (dialog.currentSentenceDisplayed == 7)
        {
            envelope.SetActive(false);
        }

        //In sentence 8 we want to 
        if (dialog.currentSentenceDisplayed == 8)
        {
            dialog.setProceed(false);

            if (message.transform.position.y > 2.5)
            {
                messageRB.MovePosition(messageRB.position + new Vector2(0, -5) * Time.fixedDeltaTime);
            }
            else
                dialog.setProceed(true);
        }

        if (dialog.currentSentenceDisplayed == 9)
        {
            dialog.setProceed(false);

            //Moving to the middle the red box to show Bob's public key 
            if (public_frame_bob.transform.position.x > 0)
            {
                public_frame_bob_RB.MovePosition(public_frame_bob_RB.position + new Vector2(-5, 0) * Time.fixedDeltaTime);
            }
            else // After the red box in the middle let the user continue
                dialog.setProceed(true);
        }

        if (dialog.currentSentenceDisplayed == 10)
        {
            dialog.setProceed(false);

            //check eatch time if the green box is in the middle of the screen to then strat moving both boxed outside:
            if (private_frame_bob.transform.position.x < 0)
                reverse = true;

            //Moving the green box to the middle:
            if (reverse == false && private_frame_bob.transform.position.x > 0) 
                private_frame_bob_RB.MovePosition(private_frame_bob_RB.position + new Vector2(-5, 0) * Time.fixedDeltaTime);

            //After the green box is in the middle the we can start to move both boxes outside to implement decryption:
            if(reverse && private_frame_bob.transform.position.x < 13.0f)
            {
                //The speed of moving the boxes is slow to make sure that the user can read everything:
                private_frame_bob_RB.MovePosition(private_frame_bob_RB.position + new Vector2(2f, 0) * Time.fixedDeltaTime);
                public_frame_bob_RB.MovePosition(public_frame_bob_RB.position + new Vector2(2f, 0) * Time.fixedDeltaTime);
            }
            //else//Allow the user continue:
                //dialog.setProceed(true);

            if(public_frame_bob.transform.position.x > 12.5) // If the red box is outside then continue
                dialog.setProceed(true); 
        }

        if (dialog.currentSentenceDisplayed == 11)
        {
            if (private_frame_alice.transform.position.x < 0)
                dialog.setProceed(false);
            else
                dialog.setProceed(true);

            //setting the reverse for the next sentence (12):
            reverse = false;
                
            //if (dialog.getFinished_typing())
                //dialog.setProceed(true);
            //else
                //dialog.setProceed(false);

            //Moving the red box first and then the yellow 
            if (public_frame_bob.transform.position.x > 0)
                public_frame_bob_RB.MovePosition(public_frame_bob_RB.position + new Vector2(-5, 0) * Time.fixedDeltaTime);
            else if(private_frame_alice.transform.position.x < 0)
                private_frame_alice_RB.MovePosition(private_frame_alice_RB.position + new Vector2(5, 0) * Time.fixedDeltaTime);
                

            //message.SetActive(false);
            //private_frame_bob.SetActive(false);
            //private_frame_alice.SetActive(false);
            //lock_image.SetActive(true);
        }

        if (dialog.currentSentenceDisplayed == 12)
        {
            if(public_frame_bob.transform.position.x <= 12)
                dialog.setProceed(false);
            else
                dialog.setProceed(true);

            //check eatch time if the black box is in the middle of the screen to then strat moving both boxes outside:
            if (public_frame_alice.transform.position.x > 0)
                reverse = true;

            //Moving the black box to the middle
            if(reverse == false && public_frame_alice.transform.position.x < 0)
                public_frame_alice_RB.MovePosition(public_frame_alice_RB.position + new Vector2(5, 0) * Time.fixedDeltaTime);

            //After the black box is in the middle then we can start to move both boxes (balck & yellow) outside to implement decryption:
            if (reverse && private_frame_alice.transform.position.x > -13.0f)
            {
                //The speed of moving the boxes is slow to make sure that the user can read everything:
                private_frame_alice_RB.MovePosition(private_frame_alice_RB.position + new Vector2(-2.5f, 0) * Time.fixedDeltaTime);
                public_frame_alice_RB.MovePosition(public_frame_alice_RB.position + new Vector2(-2.5f, 0) * Time.fixedDeltaTime);
            }

            if(private_frame_alice.transform.position.x < -13.0f && public_frame_alice.transform.position.x < -13.0f)
            {
                if (private_frame_bob.transform.position.x < 0)
                    reverseLastPart = true;
                
                //Moving the green box to the middle:
                if (reverseLastPart == false && private_frame_bob.transform.position.x > 0)
                    private_frame_bob_RB.MovePosition(private_frame_bob_RB.position + new Vector2(-5, 0) * Time.fixedDeltaTime);

                //After the green box is in the middle the we can start to move both boxes outside to implement decryption:
                if (reverseLastPart && private_frame_bob.transform.position.x < 13.0f)
                {
                    //The speed of moving the boxes is slow to make sure that the user can read everything:
                    private_frame_bob_RB.MovePosition(private_frame_bob_RB.position + new Vector2(2.5f, 0) * Time.fixedDeltaTime);
                    public_frame_bob_RB.MovePosition(public_frame_bob_RB.position + new Vector2(2.5f, 0) * Time.fixedDeltaTime);
                }
            }
        }

        if (dialog.currentSentenceDisplayed == 13)
        {
            //Start the quiz
            blackBackground.SetActive(true);
        }
    }   
}