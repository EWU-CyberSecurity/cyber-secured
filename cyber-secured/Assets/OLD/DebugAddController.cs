using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAddController : MonoBehaviour {

    public GameController gc;

	// Use this for initialization
	void Start () {
        if(GameObject.FindGameObjectWithTag("GameController") == null)
        {
            Instantiate(gc);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
