using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform SpawnedEnemy;

    private float timeSinceSpawn = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Random.Range(0, 10000) - timeSinceSpawn) <= 0)
        {
            Instantiate(SpawnedEnemy, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), SpawnedEnemy.rotation);
            timeSinceSpawn = 0;
        }
        else
        {
            timeSinceSpawn += Time.deltaTime;
        }
    }
}