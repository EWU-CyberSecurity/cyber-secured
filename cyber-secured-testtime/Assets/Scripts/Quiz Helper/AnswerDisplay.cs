using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public int counter = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (counter >0)
        {
            counter--;
        }
        if(counter == 1)
        {
            this.gameObject.GetComponent<Camera>().depth = -4;
        }
    }
    public void ShowForTime(int time)
    {
        counter = time;
    }
}
