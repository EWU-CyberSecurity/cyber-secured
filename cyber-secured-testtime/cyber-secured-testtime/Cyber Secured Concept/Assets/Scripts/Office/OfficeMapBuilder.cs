using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeMapBuilder : MonoBehaviour
{
    public int topBoundry, bottomBoundry, leftBoundry, rightBoundry;
    public GameObject floorTile;
    public GameObject floorHolder;
    void Start()
    {
        CreateFloor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateFloor()
    {
        int x, y;
        for(x =  bottomBoundry; x <= topBoundry; x++)
        {
            for (y = leftBoundry; y <= rightBoundry; y++)
            {
                GameObject tile = Instantiate(floorTile);
                tile.GetComponent<Transform>().position = new Vector3(x,y,0);
                tile.transform.parent = floorHolder.transform;
            }
        }
    }
}
