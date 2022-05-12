using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeMovement : MonoBehaviour
{
    private float xGoal = 0, yGoal= 0;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            xGoal = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            yGoal = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            Debug.Log("x" + xGoal);
            Debug.Log("y" + yGoal);
        }
        if(Mathf.Abs(xGoal - this.GetComponent<Transform>().position.x)>= 0.1)
        {
            if (xGoal > this.GetComponent<Transform>().position.x)
            {
                Move(0);
            }
            else if (xGoal < this.GetComponent<Transform>().position.x)
            {
                Move(1);
            }
        }
        if (Mathf.Abs(yGoal - this.GetComponent<Transform>().position.y) >= 0.1)
        {
            if (yGoal > this.GetComponent<Transform>().position.y)
            {
                Move(2);
            }
            else if (yGoal < this.GetComponent<Transform>().position.y)
            {
                Move(3);
            }
        }
    }
    private void Move(int dir)
    {
        switch (dir)
        {
            case 0:
                this.transform.position = new Vector3(this.transform.position.x + speed,this.transform.position.y,-0.1f);
                break;
            case 1:
                this.transform.position = new Vector3(this.transform.position.x - speed, this.transform.position.y, -0.1f);
                break;
            case 2:
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + speed, -0.1f);
                break;
            case 3:
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - speed, -0.1f);
                break;
        }

    }
}
