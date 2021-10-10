using UnityEngine;
using UnityEngine.UI;
using System;

public class MovingImagesAndText : MonoBehaviour {

    private DialogueManager dialog; // -------

    public GameObject blackBackground; // Turn it off at the start to see better the images
    public GameObject girl;
    public GameObject boy;
    public GameObject envelope;
    public Text helloItIsAnnText;
    public Text encrypted_message;
    public GameObject arrow;
    public GameObject sceneTenToCheck;
    public GameObject blue_frame;
    public GameObject red_frame;
    public GameObject green_frame;
    public GameObject back_btn_inDialogBox;

    public GameObject squares_random; // To move them as a group, located on top of the window - random
    public Text[] squaresT_random ;
    public GameObject squares_original; // To move them as a group, located on top of the window - original
    public Text[] squaresT_original;
    public GameObject squares_result; // To move them as a group, located on top of the window - result
    public Text[] squaresT_result;

    //public Text textToMove;
    private Rigidbody2D girlRB;
    private Rigidbody2D boyRB;
    private Rigidbody2D envelopeRB;
    private Rigidbody2D helloItIsAnnTextRB;
    private Rigidbody2D encrypted_messageRB;
    private Rigidbody2D arrowRB;
    private Rigidbody2D squares_randomRB;
    private Rigidbody2D squares_originalRB;
    private Rigidbody2D squares_resultRB;

    private Vector2 velocity;

    private bool arrowInPosition = true; // In sentence 6, when arrow in postion allow the arrow to go from left to right again.

    //private float timePassed = 0; // For some reason using StartCoroutine() didn't work, had a problem with infinite loop. This varible is to count the time.
    private byte array_index = 0;
    private byte encrypted_message_index = 0;
    private String letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    // Use this for initialization
    void Awake()
    {
        girlRB = girl.GetComponent<Rigidbody2D>();
        boyRB = boy.GetComponent<Rigidbody2D>();
        envelopeRB = envelope.GetComponent<Rigidbody2D>();
        helloItIsAnnTextRB = helloItIsAnnText.GetComponent<Rigidbody2D>();
        encrypted_messageRB = encrypted_message.GetComponent<Rigidbody2D>();
        arrowRB = arrow.GetComponent<Rigidbody2D>();
        squares_randomRB =   squares_random.GetComponent<Rigidbody2D>();
        squares_originalRB = squares_original.GetComponent<Rigidbody2D>();
        squares_resultRB =   squares_result.GetComponent<Rigidbody2D>();
        //green_frameRB = green_frame.GetComponent<Rigidbody2D>();

        velocity = new Vector2(-5, 0); // controling the x and y posstion, will move 5 units on the x direction to the left

        //Get an access to the DialogueManager script to manage the demonstration according to the line displayed:
        dialog = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        //controller = GameObject.Find("OneTimePadSceneController").GetComponent<OneTimePadSceneController>();

        blackBackground.SetActive(false); //DO NOT FORGET TO TURN IT ON AT THE END OF THE DEMONSTRATION !!!!!!
        envelope.SetActive(false);// Wait to display the envelope with sentence 4:
        arrow.SetActive(false);
        back_btn_inDialogBox.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(dialog.currentSentenceDisplayed);

        if(dialog.currentSentenceDisplayed == 3)
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
            //If both pictures of the boy and the girl are in place, start to move the envelope until its x position is 4.3:
            if (boy.transform.position.x <= 7.0 && girl.transform.position.x >= -7.0 && envelope.transform.position.x < 4.3)
            {
                envelope.SetActive(true);
                envelopeRB.MovePosition(envelopeRB.position + velocity * (-1) * Time.fixedDeltaTime);
            }

            if(envelope.transform.position.x >= 4.3) // If the envelope in its posstion:
            {
                dialog.setProceed(true); // Let the user continue
            }
        }

        //If the sentennce number displayed is 4 and the position of the text in not at posstion y=3:
        if (dialog.currentSentenceDisplayed == 4 && helloItIsAnnText.transform.position.y > 3)
        {
            dialog.setProceed(false); // Lock the user from proceeding

            boy.SetActive(false);
            girl.SetActive(false);
            envelope.SetActive(false);

            helloItIsAnnTextRB.MovePosition(helloItIsAnnTextRB.position + new Vector2(0, -5) * Time.fixedDeltaTime);
        }

        if (helloItIsAnnText.transform.position.y < 3 && dialog.getFinished_typing() == true)
            dialog.setProceed(true);
        
        //For senctence 5 I possioned the arrow at  x = -3.80 and will move it to  x=3.85
        if (dialog.currentSentenceDisplayed == 5 && arrow.transform.position.x < 4)
        {
            dialog.setProceed(false); // lock the continue button
            arrow.SetActive(true); // Show the arrow in the screen
            arrowRB.MovePosition(arrowRB.position + velocity * (-1) * Time.fixedDeltaTime);

            if (arrow.transform.position.x >= 3.85)// after the arrow got to the end of the sentence, release the lock
            {
                dialog.setProceed(true);
            }
        }

        //Sentence 6, generate the numbers
        if (dialog.currentSentenceDisplayed == 6)
        {
            if(arrowInPosition)
            {
                dialog.setProceed(false); // lock the continue button
                arrowInPosition = !arrowInPosition; // now the varible is false and we won't repeat that again
                // move the arrow to the first number - for now I chose the easy way 
                arrowRB.transform.position = new Vector3(-8.3f, 2.30f);
                arrow.SetActive(false);    
            }

            // drag out the text upward
            if(helloItIsAnnText.transform.position.y < 8)
            {
                helloItIsAnnTextRB.MovePosition(helloItIsAnnTextRB.position + new Vector2(0, 5) * Time.fixedDeltaTime);    
            }
            // lower the numbers into the screen
            if(squares_random.transform.position.y > -3) // > 0
            {
                squares_randomRB.MovePosition(squares_randomRB.position + new Vector2(0,-5) * Time.fixedDeltaTime);
            }

            if(squares_random.transform.position.y <= 0) // Show the arrow in it's new postion
            {
                arrow.SetActive(true);  
            }

            if (arrowRB.position.x < 7.70f)
            {
                arrowRB.MovePosition(arrowRB.position + new Vector2(-3, 0) * (-1) * Time.fixedDeltaTime);

                // Now we're checking the position of the arrow, in order to generate each random number to a squar: 
                if (arrowRB.position.x > -8f    && arrowRB.position.x < -7.8f) 
                {
                    squaresT_random[0].text = "" + UnityEngine.Random.Range(1, 26);
                }
                if (arrowRB.position.x > -6.5f  && arrowRB.position.x < -6.3f) 
                {
                    squaresT_random[1].text = "" + UnityEngine.Random.Range(1, 26);
                }
                if (arrowRB.position.x > -5.0f  && arrowRB.position.x < -4.8f) 
                {
                    squaresT_random[2].text = "" + UnityEngine.Random.Range(1, 26);
                }
                if (arrowRB.position.x > -3.7f  && arrowRB.position.x < -3.5f) 
                {
                    squaresT_random[3].text = "" + UnityEngine.Random.Range(1, 26);
                }
                if (arrowRB.position.x > -2.2f  && arrowRB.position.x < -2.0f) 
                {
                    squaresT_random[4].text = "" + UnityEngine.Random.Range(1, 26);
                }
                if (arrowRB.position.x > -0.74f && arrowRB.position.x < -0.60f) 
                {
                    squaresT_random[5].text = "" + UnityEngine.Random.Range(1, 26);
                }
                if (arrowRB.position.x > 0.6f   && arrowRB.position.x < 0.74f) 
                {
                    squaresT_random[6].text = "" + UnityEngine.Random.Range(1, 26); 
                }
                if (arrowRB.position.x > 2.0f   && arrowRB.position.x < 2.2f) 
                {
                    squaresT_random[7].text = "" + UnityEngine.Random.Range(1, 26);
                }
                if (arrowRB.position.x > 3.5f   && arrowRB.position.x < 3.7f) 
                {
                    squaresT_random[8].text = "" + UnityEngine.Random.Range(1, 26);
                }
                if (arrowRB.position.x > 4.8f   && arrowRB.position.x < 5.0f) 
                {
                    squaresT_random[9].text = "" + UnityEngine.Random.Range(1, 26);
                }
                if (arrowRB.position.x > 6.3f   && arrowRB.position.x < 6.5f) 
                {
                    squaresT_random[10].text = "" + UnityEngine.Random.Range(1, 26);
                }
                if (arrowRB.position.x > 7.4f   && arrowRB.position.x < 7.8f) 
                {
                    squaresT_random[11].text = "" + UnityEngine.Random.Range(1, 26);
                    dialog.setProceed(true); 
                    blue_frame.SetActive(true);
                }
            }
        }

        if(dialog.currentSentenceDisplayed == 7) // The third step to acccumulate the numbers
        {
            
            if (arrow.activeSelf == true)
            {
                arrow.SetActive(false);
                dialog.setProceed(false); // lock the continue button
            }
                
            if (squares_result.transform.position.y > -6)
            {
                squares_resultRB.MovePosition(squares_resultRB.position + new Vector2(0, -5) * Time.fixedDeltaTime);
            }
            if (squares_original.transform.position.y > -4.5) // as long that the squars are not in the place continue move:
            {
                squares_originalRB.MovePosition(squares_originalRB.position + new Vector2(0, -5) * Time.fixedDeltaTime);
            }
            else // Squars are in the place so turn on the frames:
            {
                if (green_frame.activeSelf == false && red_frame.activeSelf == false)
                {
                    red_frame.SetActive(true);
                    green_frame.SetActive(true);
                }

                if(squares_result.transform.position.y < -6)
                    dialog.setProceed(true); // lock the continue button
            }
        }

        //Here the calculation of each value in the result squars:
        if (dialog.currentSentenceDisplayed == 8)
        {
            if (array_index == 0)
                dialog.setProceed(false); // lock the continue button

            if(array_index >= 0 && array_index < 12)
            {
                //Need to take into consideration 26 % 26 = 0, we can't have 0 in the result so we put 26 instead of the 0:
                //Created short if stament to handle the situation were 0 can occure 26 % 26. statment ? true : false.
                squaresT_result[array_index].text = 
                    ((Int32.Parse(squaresT_random[array_index].text) + Int32.Parse(squaresT_original[array_index].text)) % 26) == 0 ? 
                    "26" : "" + ((Int32.Parse(squaresT_random[array_index].text) + Int32.Parse(squaresT_original[array_index].text)) % 26);
                    //"" + ((Int32.Parse(squaresT_random[array_index].text) + Int32.Parse(squaresT_original[array_index].text)) % 26);
                array_index++;
            }

            if (array_index == 11)
            {
                dialog.setProceed(true);
            }
        }

        if (dialog.currentSentenceDisplayed == 9)
        {
            //Sentence 9 is the example, not sure if we want to do something with it.
            if (dialog.getFinished_typing() == false)
                dialog.setProceed(false);
            else
                dialog.setProceed(true);

            if (array_index != 0) // resetting the array index for dialog.currentSentenceDisplayed == 11
                array_index = 0;
        }

        if (dialog.currentSentenceDisplayed == 10)
        {
            if (dialog.getFinished_typing() == false)
                dialog.setProceed(false);
            
            squares_random.SetActive(false);
            blue_frame.SetActive(false);
            squares_original.SetActive(false);
            red_frame.SetActive(false);

            if (squares_result.transform.position.y < -3.1)
            {
                squares_resultRB.MovePosition(squares_resultRB.position + new Vector2(0, 5) * Time.fixedDeltaTime);
            }
            if(dialog.getFinished_typing())
            {
                dialog.setProceed(true);
            }
        }

        if (dialog.currentSentenceDisplayed == 11)
        {
            if(encrypted_message_index == 5 || encrypted_message_index == 8 || encrypted_message_index == 11) // first checking for the next position of the space
            {
                encrypted_message.text += " ";
            }
            else if(array_index < 12)// if no space nedded then put the next letter instead:
            {
                dialog.setProceed(false);

                //Checking if the array length is not over 12.

                //If the result of the first line plus the second line is equal to the third line then there is no need to cycle again 
                if ((int.Parse(squaresT_random[array_index].text) + int.Parse(squaresT_original[array_index].text)) == int.Parse(squaresT_result[array_index].text))
                {
                    //Debug.Log("if----  " + ((int.Parse(squaresT_random[array_index].text) + int.Parse(squaresT_original[array_index].text)) - 1));
                    //Debug.Log("if----  " + letters[(int.Parse(squaresT_random[array_index].text) + int.Parse(squaresT_original[array_index].text)) - 1]); // -1 because its start at 0
                    encrypted_message.text += letters[(int.Parse(squaresT_random[array_index].text) + int.Parse(squaresT_original[array_index].text)) - 1];
                }
                else // there has to be a cycle to find the next letter to encrypt
                {
                    //Debug.Log("else---  " + (int.Parse(squaresT_result[array_index].text) - 1));
                    //Debug.Log("else---  " + letters[int.Parse(squaresT_result[array_index].text) - 1]); // the modulo will return a number that will start from A
                    encrypted_message.text += letters[int.Parse(squaresT_result[array_index].text) - 1]; 

                    /*
                     * I got index out of range! Check again why if this error come up again
                    */
                }
                array_index++; // can't use array_index to count both the encrypted message and the squars arrays
            }

            if(encrypted_message_index != 14)
                encrypted_message_index++;// in the end of the operation rase the index number

            if(encrypted_message_index == 14)
            {
                if (encrypted_message.transform.position.x > 0)
                {
                    encrypted_messageRB.MovePosition(encrypted_messageRB.position + velocity * Time.fixedDeltaTime);
                }
                else
                {
                    dialog.setProceed(true);
                }
            }
        }

        if (dialog.currentSentenceDisplayed == 12)
        {
            //Pull up the green squers and pull down the original text
            if(squares_result.transform.position.y < 1)
                dialog.setProceed(false);
            
            if (squares_result.transform.position.y < 1) // squares up
            {
                squares_resultRB.MovePosition(squares_resultRB.position + new Vector2(0, 5) * Time.fixedDeltaTime);
            }

            if (helloItIsAnnText.transform.position.y > 4) // text down
            {
                helloItIsAnnTextRB.MovePosition(helloItIsAnnTextRB.position + new Vector2(0, -5) * Time.fixedDeltaTime);
            }

            if (helloItIsAnnText.transform.position.y > 4)
            {
                dialog.setProceed(true);
                girl.SetActive(true);
            }
        }
        if (dialog.currentSentenceDisplayed == 13)
        {
            //girl.SetActive(false);
            boy.SetActive(true);
        }
        if(dialog.currentSentenceDisplayed == 14)
        {
            //Start the quiz
            blackBackground.SetActive(true);
        }
    }   
}