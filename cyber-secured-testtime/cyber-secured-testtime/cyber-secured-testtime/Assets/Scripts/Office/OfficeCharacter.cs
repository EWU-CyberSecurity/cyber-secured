using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeCharacter : MonoBehaviour
{
    //playerprefs for scene reload
    //OfficeCharacterPosX
    //OfficeCharacterPosY

    //this is the method that will control the movement of the office character
    public GameObject control; //this will be how control is accessed
    public Vector2[] path,nextPath = null;//path is the path that is currently being walked on
    public bool stepping;
    public int step;
    public float speed;
    public float speedX, speedY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(path != null)
        {
            
            if(stepping)
            {
                Stepping();
            }
            else
            {

                if (step < path.Length)
                {
                    StartStep();
                }
                else
                {
                    path = null;
                }
            }
        }
        if (PlayerPrefs.GetInt("ManualReset") == 1)
            SceneReset();
    }
    private void Stepping()
    {

        //change the position
        float newPosX = this.GetComponent<Transform>().position.x + speedX;
        float newPosY = this.GetComponent<Transform>().position.y + speedY;
        //check for passing limit
        bool oversteppedX = false, oversteppedY = false;


        //we set it to >= so that we can finish easy even if xSpeed =0

        if((speedX > 0 && newPosX >= path[step].x) ||
            (speedX < 0 && newPosX <= path[step].x))
        {
            oversteppedX = true;
            newPosX = path[step].x;
        }
        if ((speedY > 0 && newPosY >= path[step].y) ||
            (speedY < 0 && newPosY <= path[step].y))
        {
            oversteppedY = true;
            newPosY = path[step].y;
        }
        this.GetComponent<Transform>().position = new Vector3(newPosX, newPosY, this.GetComponent<Transform>().position.z);

        //made it
        if(oversteppedX && oversteppedY)
        {
            Vector2 tilePos = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
            tilePos = control.GetComponent<TileDisplay>().MapToTilePos(tilePos);

            PlayerPrefs.SetInt("startX", (int)tilePos.x);
            PlayerPrefs.SetInt("startY", (int)tilePos.y);
            control.GetComponent<TileDisplay>().ChangeStart(this.gameObject.GetComponent<Transform>().position);
            if(nextPath != null && nextPath.Length != 0)
            {
                path = nextPath;
                step = 1;
                nextPath = null;
                stepping = false;
            }
            else
            {
                step++;
                stepping = false;
                if (step == path.Length)
                {
                    path = null;
                }
            }
            
        }

        PlayerPrefs.SetFloat("OfficeCharacterPosX", this.GetComponent<Transform>().position.x);
        PlayerPrefs.SetFloat("OfficeCharacterPosY", this.GetComponent<Transform>().position.y);

    }
    private void StartStep()
    {
        //calculates values for step
        stepping = true;



        //calculate distance between start and finish
        //find speed x and speed y for moving at given speed

        if (this.GetComponent<Transform>().position.x > path[step].x)
            speedX = -speed;
        else
            speedX = speed;
        if (this.GetComponent<Transform>().position.y > path[step].y)
            speedY = -speed;
        else
            speedY = speed;
        float distanceX = Mathf.Abs(this.GetComponent<Transform>().position.x - path[step].x);
        float distanceY = Mathf.Abs(this.GetComponent<Transform>().position.y - path[step].y);

        //this keeps speed constant while moving diaganally
        if (distanceX > 0.01f && distanceY > 0.01f )
        {
            speedX = speedX / Mathf.Sqrt(2);
            speedY = speedY / Mathf.Sqrt(2);
        }


    }
    public void SetPath(Vector2[] newPath)
    {
        path = newPath;
        step = 0;
        return;
    }
    public void SetNextPath(Vector2[] newPath)
    {
        nextPath = newPath;
        return;
    }

    public void SceneReset()
    {
        float posX = PlayerPrefs.GetFloat("OfficeCharacterPosX");
        float posY = PlayerPrefs.GetFloat("OfficeCharacterPosY");
        this.GetComponent<Transform>().position = new Vector3(posX, posY, this.GetComponent<Transform>().position.z);
        
    }
}
