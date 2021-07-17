using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject [] questions;
	public GameObject next;

	public static int i = 0;

	// Use this for initialization
	void Start () {

		next.SetActive (false);

		questions = GameObject.FindGameObjectsWithTag ("question");
		for (int j = 1; j < 10; j++) {
			questions [j].SetActive (false);
		}

	}
	
	// Update is called once per frame
	void Update () {	
		
			
	}

	public void nextSet()
	{
		next.SetActive (false);

		if (i < 10) {
			questions [i].SetActive (false);
			if (i != 9) {
				questions [i + 1].SetActive (true);
				i++;
			}
			//if (i == 9)
			//	next.SetActive (false);
		}


	}
}
