using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberCharacter : MonoBehaviour
{
    private float iFrame = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(iFrame >= 0)
            iFrame -= Time.deltaTime;

        if (Input.GetKey("w"))
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z);
        if (Input.GetKey("s"))
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 0.01f, this.gameObject.transform.position.z);
        if (Input.GetKey("a"))
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - 0.01f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        if (Input.GetKey("d"))
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + 0.01f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

    public void TakeDamage(int damage)
    {
        if(iFrame <= 0)
        {
            this.gameObject.GetComponent<NPCharacterInterface>().NPDamage(damage);
            iFrame = 1.0f;
        }
    }
}
