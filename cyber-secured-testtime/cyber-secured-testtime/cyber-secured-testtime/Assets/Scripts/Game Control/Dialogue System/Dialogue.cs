/// FILE: DisplayMonthNP.cs
/// PURPOSE: this is a class just holds lines of dialogue; doesn't need to derive from MonoBehavior
/// 

using UnityEngine;

[System.Serializable]           //when making a class like this, it has to be serializable to show up in the inspector
public class Dialogue           
{
    public string name;         //the name of the NPC

    [TextArea(3, 10)]           //set the size of the boxes in the inspector
    public string[] sentences;  //a list of the lines of dialogue

    public void setSentences(string[] newSentences)
    {
        this.sentences = newSentences;
    }
}
