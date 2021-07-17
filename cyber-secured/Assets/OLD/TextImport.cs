using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImport : MonoBehaviour
{
    public TextAsset text_file; //to grab a text file
    public string[] text_lines; //to put dialogue into an array

	// Use this for initialization
	void Start ()
	{
        if(text_file != null)
        {
            //this splits up the text file between new lines
            text_lines = (text_file.text.Split('\n'));
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
