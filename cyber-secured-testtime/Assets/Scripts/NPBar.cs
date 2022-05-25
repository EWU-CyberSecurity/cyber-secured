using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPBar : MonoBehaviour
{
    //np = network power
    //used to show np and have a centralized np location 
    //this is to be attatched to the np bar object
    public float rightBound, leftBound; //the limit x and y to be set in scene
    public float fullLength; //the difference between the bounds set in scene
    public float np = 0.2f; //between 0 and 1 inclusive float basically percentage

    void Update()
    {
        //when scene resets load old values
        if (PlayerPrefs.GetInt("ManualReset") == 1)
            SceneReset();
    }
    public void ChangeNP(float newNP)
    {
        //changes the np to new value and puts it to closest in bounds number if out of bounds
        //this also updates the gameObject display of np

        //put value between 1 and 0 inclusive
        if (newNP > 1)
            newNP = 1;
        if (newNP < 0)
            newNP = 0;

        //changes np and also np in player prefs for scene reset
        np = newNP;
        PlayerPrefs.SetFloat("NP", newNP);

        //change the np bar to represent percentage full
        //starts from left side and will cover np *100 percent of the full length space
        GameObject bar = this.gameObject;
        bar.transform.localScale = new Vector3(fullLength* np, bar.transform.localScale.y, bar.transform.localScale.z);
        bar.transform.localPosition = new Vector3(rightBound + fullLength*0.5f * np, bar.transform.localPosition.y, bar.transform.localPosition.z);
        //Position has the 0.5 to keep midpoint in a position such that the left side of the bar is touching the left bound

        //color gradient changes from red to blue as the value increases
        float red = 1 - newNP ;
        float blue = newNP;
        bar.GetComponent<SpriteRenderer>().color = new Color(red,blue* 0.7f,blue);
    }
    public void SceneReset()
    {
        //when the game resets that means a question was answered and so np is changed depending on whether the answer was right or wrong
        if(PlayerPrefs.GetInt("QuestionRight") == 1)
            ChangeNP(PlayerPrefs.GetFloat("NP") + 0.05f);
        else
            ChangeNP(PlayerPrefs.GetFloat("NP") - 0.05f);
    }
}
