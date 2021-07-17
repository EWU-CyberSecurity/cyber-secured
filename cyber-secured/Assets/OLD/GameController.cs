using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameController instance; //for singleton

    public enum State //enum for state machine
    {
        title,
        main,
        game_over
    }

    public State CURRENT_STATE;

    public GameObject[] computers;

    public float elapsed_time;

    public float current_month; //may be using a monthly system like we previously discussed

    //runs before start
    void Awake()
    {
        //singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start()
    {
        //setting controller to the right state (useful only in debugging)
        if(SceneManager.GetActiveScene().name == "scn_00_title")
        {
            Debug.Log("Title Screen");
            CURRENT_STATE = State.title;
        }

        if(SceneManager.GetActiveScene().name == "scn_01_main")
        {
            Debug.Log("Main Game");
            CURRENT_STATE = State.main;
        }
	}
	
	// Update is called once per frame
	void Update()
    {
        //saved for input
	}

    //for movement, etc.
    private void FixedUpdate()
    {
        switch(CURRENT_STATE)
        {
            case(State.title):
            {
                //do something in title scene
            } break;

            case(State.main):
            {
                if(computers.Length == 0)
                {
                    GetComputers();
                }

                StartCoroutine("moveComputers");
            } break;

            case(State.game_over):
            {
                //do something in game over scene
            } break;
        }
    }

    private void GetComputers()
    {
        computers = GameObject.FindGameObjectsWithTag("computer");
    }

    IEnumerator moveComputers()
    {
        elapsed_time += Time.deltaTime;
        foreach(GameObject computer in computers)
        {
            Vector2 current_pos = computer.transform.position;
            computer.transform.position = Vector2.Lerp(
                current_pos,
                new Vector2(current_pos.x, 0),
                0.5f * elapsed_time);
        }
        yield return null;
    }

    public void SceneTransition(int index)
    {
        SceneManager.LoadScene(index);
    }
}
