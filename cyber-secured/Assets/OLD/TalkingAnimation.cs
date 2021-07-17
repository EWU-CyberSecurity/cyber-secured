using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingAnimation : MonoBehaviour
{
    /*Vector2 original_pos;
    float shake_amount;

	// Use this for initialization
	void Start ()
	{
        original_pos = this.transform.position;
        InvokeRepeating("Shake", 0, 0.1f);
        Invoke("StopShake", 1.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
        //InvokeRepeating("Shake", 0, 0.1f);
	}

    void Shake()
    {
        float random_shake = Random.value * shake_amount * 2 - shake_amount;
        Vector2 pos = this.transform.position;
        pos.y += random_shake;
        this.transform.position = pos;
    }

    void StopShake()
    {
        CancelInvoke("Shake");
        this.transform.position = original_pos;
    }*/
}
