using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float speed = 1f;
        if (Input.GetKeyDown("w"))
        {
            if(true)//make contact with floor a requirement
            {
                Vector3 movement = new Vector3(this.gameObject.GetComponent<Rigidbody2D>().velocity.x, 4, 0) * speed;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = movement;
            }
        }
        if (Input.GetKey("a"))
        {
            Vector3 movement = new Vector3(-1, this.gameObject.GetComponent<Rigidbody2D>().velocity.y, 0) * speed;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = movement;
        }
        if (Input.GetKey("d"))
        {
            Vector3 movement = new Vector3(1, this.gameObject.GetComponent<Rigidbody2D>().velocity.y, 0) * speed;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = movement;
        }

    }
}
