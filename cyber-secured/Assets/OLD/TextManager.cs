using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject portrait; //in inspector drag the portrait
    public GameObject text_box; //in inspector drag a panel object
    public Text text_in_box;    //in inspector drag the corresponding text

    public TextAsset text_file; //to grab a text file
    public string[] text_lines; //to put dialogue into an array

    public int current_line;    //current line of dialogue
    public int end_at_line;     //end of dialogue

    float elapsed_time;         //keep track of time for lerp

    // Use this for initialization
    void Start()
    {
        //import the text into the text_lines array
        if(text_file != null)
        {
            text_lines = (text_file.text.Split('\n'));  //split up the text file between new lines
        }

        //sets the number of lines of dialogue til the end
        if(end_at_line == 0)
        {
            end_at_line = text_lines.Length - 1;
        }

        text_box.transform.localScale = new Vector2(1.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update()
    {
        //changes the text in the textbox to display the current line
        text_in_box.text = text_lines[current_line];

        //when there is no more text
        if(current_line == end_at_line)
        {
            //move dialogue elements out
            LerpPos(portrait, new Vector2(-12.0f, -2.5f), 0.7f);
            LerpPosOut(text_box);
        } else {
            //move dialogue elements in
            LerpPos(portrait, new Vector2(-6.0f, -2.5f), 0.7f);
            LerpPosIn(text_box);
        }

        //TODO: BUG when spamming the key
        //input for next line
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(current_line != end_at_line)
            {
                elapsed_time = 0; //reset time for lerp
                current_line++; //go to the next line
            }
        }
	}

    void FixedUpdate()
    {
        
    }

    //TODO: can do polymorphism better probably
    //Moves a game object smoothly from one location to another
    void LerpPos(GameObject g, Vector2 end, float ease)
    {
        elapsed_time += Time.deltaTime;
        Vector2 current_pos = g.transform.position;
        g.transform.position = Vector2.Lerp(current_pos, end, ease * elapsed_time);
    }

    void LerpPosIn(GameObject g)
    {
        Vector2 current_pos = g.transform.position;
        LerpPos(g, new Vector2(2.5f, current_pos.y), 0.3f);
    }

    void LerpPosOut(GameObject g)
    {
        Vector2 current_pos = g.transform.position;
        LerpPos(g, new Vector2(15.0f, current_pos.y), 0.3f);
    }
}
