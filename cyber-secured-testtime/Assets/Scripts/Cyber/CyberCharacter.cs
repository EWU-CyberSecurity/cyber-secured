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
        this.gameObject.GetComponent<NPCharacterInterface>().NPDamage(damage);
    }
}
