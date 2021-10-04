using UnityEngine;
using UnityEngine.UI;

public class shuffleAnswers : MonoBehaviour {
    
    [SerializeField] private Button[] buttons; // The buttons I want to switch places
    private Rigidbody2D [] buttonsRB;
    private bool[] randoms; // That checkes which button got to be random next 
    private byte position; // The possition of the next button to be placed
    private byte temp; // holding the number that got generated

    public bool password, virus, rsa;

	// Use this for initialization
	void Start () 
    {
        position = 0; // The spot where to put the button;

        randoms = new bool[4];

        buttonsRB = new Rigidbody2D[4];
        for (byte i = 0; i < 4; i++)
            buttonsRB [i] =  buttons[i].GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        //If one index in the array is still false that means the program didn't generate that specific number
        if (randoms[0] == false || randoms[1] == false || randoms[2] == false || randoms[3] == false)
        {
            //Generating a #:
            temp = (byte)Random.Range(0, 4);

            //Checking if that number was generated already:
            if (randoms[temp] == false) // Is button number ___ got genrated already? if not...
            {
                randoms[temp] = true; // mark this index as genrated

                if (password)
                {
                    switch (position) // place the button in its spot
                    { // pleace it in the next postion
                        case 0:
                            buttonsRB[temp].transform.position = new Vector3(-3.274f, -0.475f); //upper left
                            break;
                        case 1:
                            buttonsRB[temp].transform.position = new Vector3(-3.274f, -2.381f); //down left
                            break;
                        case 2:
                            buttonsRB[temp].transform.position = new Vector3(3.554f, -0.475f); // upper right
                            break;
                        case 3:
                            buttonsRB[temp].transform.position = new Vector3(3.554f, -2.381f); //down right
                            break;
                    }
                }
                else if(virus)
                {
                    switch (position) // place the button in its spot
                    { // pleace it in the next postion
                        case 0:
                            buttonsRB[temp].transform.position = new Vector3(-3.1f, -1.543f); //upper left
                            break;
                        case 1:
                            buttonsRB[temp].transform.position = new Vector3(-3.072f, -3.858f); //down left
                            break;
                        case 2:
                            buttonsRB[temp].transform.position = new Vector3(3.072f, -1.543f); // upper right
                            break;
                        case 3:
                            buttonsRB[temp].transform.position = new Vector3(3.100f, -3.858f); //down right
                            break;
                    }
                }
                else // rsa
                {
                    switch (position) // place the button in its spot
                    { // pleace it in the next postion
                        case 0:
                            buttonsRB[temp].transform.position = new Vector3(-4.364f, -0.471f); //upper left
                            break;
                        case 1:
                            buttonsRB[temp].transform.position = new Vector3(-4.364f, -2.378f); //down left
                            break;
                        case 2:
                            buttonsRB[temp].transform.position = new Vector3(4.451f, -0.473f); // upper right
                            break;
                        case 3:
                            buttonsRB[temp].transform.position = new Vector3(4.451f, -2.378f); //down right
                            break;
                    }
                }

                position++; // increase the position location
            }
        }
    }
}
