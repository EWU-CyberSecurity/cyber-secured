using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPBar : MonoBehaviour
{
    public float rightBound, leftBound;
    public float fullLength;
    float np = 0.2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("NP") == false)
            ChangeNP(0.2f);
        if (PlayerPrefs.GetInt("ManualReset") == 1)
            SceneReset();
    }
    public void ChangeNP(float newNP)
    {
        if (newNP > 1)
            newNP = 1;
        if (newNP < 0)
            newNP = 0;
        np = newNP;
        PlayerPrefs.SetFloat("NP", newNP);

        GameObject bar = this.gameObject;
        bar.transform.localScale = new Vector3(fullLength* np, bar.transform.localScale.y, bar.transform.localScale.z);
        bar.transform.localPosition = new Vector3(rightBound + 0.5f * np, bar.transform.localPosition.y, bar.transform.localPosition.z);
    }
    public void SceneReset()
    {
        if(PlayerPrefs.GetInt("QuestionRight") == 1)
            ChangeNP(PlayerPrefs.GetFloat("NP") + 0.05f);
        else
            ChangeNP(PlayerPrefs.GetFloat("NP") - 0.05f);
    }
}
