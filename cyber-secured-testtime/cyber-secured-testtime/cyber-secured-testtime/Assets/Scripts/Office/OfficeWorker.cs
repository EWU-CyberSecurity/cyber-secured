using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeWorker : MonoBehaviour
{
    //playerprefs 
    //work0 - work14


    // Start is called before the first frame update
    public string workerID;//set these in scene
    public GameObject officeCharacter;
    public GameObject officeControl;
    private float acceptableDistance = 1f;
    private bool wantsInteract = false;
    //private GameObject myPing;
    public GameObject myPing;
    private bool fixStartPos = false;

    private int randTop = 10000;

    void Start()
    {
        //myPing = Instantiate(ping,this.transform);

    }

    // Update is called once per frame
    void Update()
    {
        if (fixStartPos)
        {
            
            Vector2 pos = officeControl.GetComponent<TileDisplay>().MapToTilePos(new Vector2(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y));
            pos = officeControl.GetComponent<TileDisplay>().MapToRealPos(pos);
            this.GetComponent<Transform>().position = new Vector3(pos.x, pos.y, -1);
            fixStartPos = false;
        }
        if(wantsInteract == false && Random.Range(0,randTop) == 0)
        {
            Debug.Log("ping");
            myPing.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            wantsInteract = true;
            PlayerPrefs.SetInt(workerID, 1);
        }

        if (Input.GetMouseButtonDown(1) && wantsInteract)
        {
            AttemptInteract();
        }
        if (PlayerPrefs.GetInt("ManualReset") == 1)
            SceneReset();
    }
    public void AttemptInteract()
    { 
        Vector3 mPos = GameObject.Find("OfficeCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        float clickDistanceX = Mathf.Abs(mPos.x - this.gameObject.GetComponent<Transform>().position.x);
        float clickDistanceY = Mathf.Abs(mPos.y - this.gameObject.GetComponent<Transform>().position.y);
        Debug.Log(clickDistanceX);
        if (clickDistanceX < acceptableDistance && clickDistanceY < acceptableDistance)
        {
            float playerDistanceX = Mathf.Abs(officeCharacter.GetComponent<Transform>().position.x - this.gameObject.GetComponent<Transform>().position.x);
            float playerDistanceY = Mathf.Abs(officeCharacter.GetComponent<Transform>().position.y - this.gameObject.GetComponent<Transform>().position.y);
            if (playerDistanceX < acceptableDistance && playerDistanceY < acceptableDistance)
            {
                wantsInteract = false;
                PlayerPrefs.DeleteKey(workerID);
                myPing.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

                GameObject.Find("OfficeControl").GetComponent<CameraControl>().ToggleCamera();
                GameObject.Find("QuizHelp").GetComponent<QuizHelp>().PickQuestion(0);

            }
        }
    }
    public void SceneReset()
    {
        if (PlayerPrefs.HasKey(workerID))
        {
            wantsInteract = true;
            myPing.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
