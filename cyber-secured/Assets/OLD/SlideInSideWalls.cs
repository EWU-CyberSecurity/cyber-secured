using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInSideWalls : MonoBehaviour {

    public float scale_x;
    public float pos_end;
    public float apr_spd;

	// Use this for initialization
	void Start () {
        scale_x = transform.localScale.x;
        pos_end = 0.8f;
        apr_spd = 0.05f;
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y != pos_end)
        {
            scale_x = Approach(scale_x, pos_end, apr_spd);
            transform.localScale = new Vector2(scale_x, transform.localScale.y);
        }
	}

    public float Approach(float start, float ending, float difference)
    {
        float result;

        if(start < ending)
        {
            result = Mathf.Min(start + difference, ending);
        } else {
            result = Mathf.Max(start - difference, ending);
        }

        return result;
    }
}
