using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a class just holds lines of dialogue, so it doesn't need to derive from MonoBehavior

[System.Serializable]           //when making a class like this, it has to be serializable to show up in the inspector
public class Prompt
{
    public string title;        //title of prompt

    [TextArea(3, 10)]           //set the size of the boxes in the inspector
    public string text;         //text to display
}
