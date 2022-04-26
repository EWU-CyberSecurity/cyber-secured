using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDisplay : MonoBehaviour
{
    //this whole thing is basically for testing and designing purposes 
    public float xMin, xMax, yMin, yMax;
    public GameObject tilePrefab;
    private GameObject[][] tiles;

    float opacity = 0.0f;//turn on for testing, and off for the real game

    public void CreateDisplay(OfficeTile[][] officeTiles)
    {
        tiles = new GameObject[officeTiles.Length][];
        int x,y;
        for(x = 0; x < officeTiles.Length;x++)
        {
            tiles[x] = new GameObject[officeTiles[x].Length];
            for(y = 0; y < officeTiles[x].Length;y++)
            {
                //create tile
                GameObject tile;
                tile = Instantiate(tilePrefab);
                tiles[x][y] = tile;
                float xSpace = Mathf.Abs(xMin - xMax);
                float xTileSpace = xSpace / officeTiles.Length;
                float ySpace = Mathf.Abs(yMin - yMax);
                float yTileSpace = ySpace / officeTiles.Length;
                //the +0.5f is to make it not have it not go over edge
                tile.GetComponent<Transform>().position = new Vector3(xMin + xTileSpace*(x+0.5f),yMin + yTileSpace * (y + 0.5f), 0);

                tile.GetComponent<Transform>().localScale = new Vector2(xTileSpace, yTileSpace);

                //tile.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            }
        }
    }
    public void UpdateDisplay(OfficeTile[][] officeTiles, int startX, int startY, int goalX, int goalY, Vector2Int[] path)
    {
        int x, y;
        for (x = 0; x < officeTiles.Length; x++)
        {
            for (y = 0; y < officeTiles[x].Length; y++)
            {
                GameObject tile = tiles[x][y];
                int currState = officeTiles[x][y].GetState();
                if(x == startX && y == startY)
                {
                    tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0.4f, opacity);
                }
                else if (x == goalX && y == goalY)
                {
                    tile.GetComponent<SpriteRenderer>().color = new Color(1, 0.2f, 0.4f, opacity);
                }
                else if (officeTiles[x][y].GetValidSpace() == false)
                {
                    tile.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f, opacity);
                }
                else if (currState == 0)
                    tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, opacity);
                else if (currState == 1)
                    tile.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 1f, opacity);
                else if (currState == 2)
                    tile.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.7f, 0.2f, opacity);
            }
        }
        if(path != null)
        {
            for (x = 0; x < path.Length; x++)
            {
                tiles[path[x].x][path[x].y].GetComponent<SpriteRenderer>().color = new Color(0.2f, 1f, 0.2f, opacity);
            }
        }
    }
    public void DeleteDisplay()
    {
        int x, y;
        for (x = 0; x < tiles.Length; x++)
        {
            for (y = 0; y < tiles[x].Length; y++)
            {
                //create tile
                GameObject deleteMe = tiles[x][y];
                Destroy(deleteMe);
            }
        }
    }

    public bool ChangeGoal(Vector3 realGoalPos)
    {
        float realX = realGoalPos.x;
        float realY = realGoalPos.y;
        //check if its out of boudry
        if (realX < xMin || realX > xMax || realY < yMin || realY > yMax)
        {
            Debug.Log("Out of office space");
            return false;
        }

        float realYdistance = Mathf.Abs(yMin - realY);
        float realXdistance = Mathf.Abs(xMin - realX);

        float tileYdistance = Mathf.Abs(yMin - yMax) / tiles.Length;
        float tileXdistance = Mathf.Abs(xMin - xMax) / tiles[0].Length;

        int tileY = (int)Mathf.Floor(realYdistance / tileYdistance);
        int tileX = (int)Mathf.Floor(realXdistance / tileXdistance);

        this.GetComponent<AStarPathfinder>().goalY = tileY;
        this.GetComponent<AStarPathfinder>().goalX = tileX;
        return true;
    }

    public void ChangeStart(Vector3 realGoalPos)
    {
        float realX = realGoalPos.x;
        float realY = realGoalPos.y;
        //check if its out of boudry
        if (realX < xMin || realX > xMax || realY < yMin || realY > yMax)
        {
            return;
        }

        float realYdistance = Mathf.Abs(yMin - realY);
        float realXdistance = Mathf.Abs(xMin - realX);

        float tileYdistance = Mathf.Abs(yMin - yMax) / tiles.Length;
        float tileXdistance = Mathf.Abs(xMin - xMax) / tiles[0].Length;

        int tileY = (int)Mathf.Floor(realYdistance / tileYdistance);
        int tileX = (int)Mathf.Floor(realXdistance / tileXdistance);

        this.GetComponent<AStarPathfinder>().startY = tileY;
        this.GetComponent<AStarPathfinder>().startX = tileX;
    }
    public void ChangeStart(Vector2 newStart)
    {
        this.GetComponent<AStarPathfinder>().startY = (int)newStart.y;
        this.GetComponent<AStarPathfinder>().startX = (int)newStart.x;
    }
    public void ChangeValidSpace(Vector3 realGoalPos)
    {
        float realX = realGoalPos.x;
        float realY = realGoalPos.y;
        //check if its out of boudry
        if (realX < xMin || realX > xMax || realY < yMin || realY > yMax)
        {
            return;
        }

        float realYdistance = Mathf.Abs(yMin - realY);
        float realXdistance = Mathf.Abs(xMin - realX);

        float tileYdistance = Mathf.Abs(yMin - yMax) / tiles.Length;
        float tileXdistance = Mathf.Abs(xMin - xMax) / tiles[0].Length;

        int tileY = (int)Mathf.Floor(realYdistance / tileYdistance);
        int tileX = (int)Mathf.Floor(realXdistance / tileXdistance);

        bool validSpace = this.GetComponent<AStarPathfinder>().officeTileSpace[tileX][tileY].GetValidSpace();

        if(validSpace)
        {
            this.GetComponent<AStarPathfinder>().officeTileSpace[tileX][tileY].SetValidSpace(false);
        }
        else
        {
            this.GetComponent<AStarPathfinder>().officeTileSpace[tileX][tileY].SetValidSpace(true);
        }
    }

    public Vector2 MapToRealPos(Vector2 oldVector)
    {
        //takes tile postion (int,int) and creates real world postion of it
        float realX = oldVector.x;
        float realY = oldVector.y;
        //check if its out of boudry
        float xSpace = Mathf.Abs(xMin - xMax);
        float xTileSpace = xSpace / tiles.Length;
        float ySpace = Mathf.Abs(yMin - yMax);
        float yTileSpace = ySpace / tiles.Length;
        //the +0.5f is to make it not have it not go over edge
        Vector2 newVector = new Vector2(xMin + xTileSpace * (realX+ 0.5f), yMin + yTileSpace * (realY + 0.5f));
        return newVector;
    }
    public Vector2 MapToTilePos(Vector2 oldVector)
    {
        //takes real pos(float,float) and creates tile position of it
        float realX = oldVector.x;
        float realY = oldVector.y;
        //check if its out of boudry
        if (realX > xMax || realX < xMin || realY > yMax || realY < yMin)
            return new Vector2 (-1,-1);

        float xSpace = Mathf.Abs(xMin - xMax);
        float xTileSpace = xSpace / tiles.Length;
        float ySpace = Mathf.Abs(yMin - yMax);
        float yTileSpace = ySpace / tiles.Length;

        float xDistance = Mathf.Abs(xMin - realX);
        float yDistance = Mathf.Abs(yMin - realY);
        int xSpot = (int)Mathf.Floor(xDistance / xTileSpace);
        int ySpot = (int)Mathf.Floor(yDistance / yTileSpace);


        //the +0.5f is to make it not have it not go over edge
        Vector2 newVector = new Vector2(xSpot,ySpot);
        return newVector;
    }
}
