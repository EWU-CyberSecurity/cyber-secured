using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInBackWall : MonoBehaviour
{
    public float pos_y;
    public float pos_end;
    public float apr_spd;

	// Use this for initialization
	void Start()
    {
        pos_y   = transform.position.y;
        pos_end = 3.5f;
        apr_spd = 0.7f;
	}
	
	// Update is called once per frame
	void Update()
    {
        if(transform.position.y != pos_end)
        {
            pos_y = Approach(pos_y, pos_end, apr_spd);
            transform.position = new Vector2(0, pos_y);
        }
	}

    //this accomplishes the same thing as lerp
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
