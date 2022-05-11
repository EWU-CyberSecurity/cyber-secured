using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder : MonoBehaviour
{
    //playerPrefs save goalX,goalY
    //this will decide the ammount of tiles for office pathfinding
    public int officeX, officeY;
    private OfficeTile queHead;
    private OfficeTile queSpecial = null;//the node with the smallest distance to goal
    public bool pathFound;
    public OfficeTile[][] officeTileSpace;
    public Vector2Int[] path;
    private bool createdMap = false;
    private int delay = 0;

    public int goalX, goalY, startX, startY;
    void Start()
    {
        officeTileSpace = new OfficeTile[officeX][];
        int x, y;
        for (x = 0; x < officeX; x++)
        {
            officeTileSpace[x] = new OfficeTile[officeY];
            for (y = 0; y < officeY; y++)
            {
                officeTileSpace[x][y] = new OfficeTile(x, y);
            }
        }

        //the cubicles
        for (x = 2; x <= 15; x++)
        {
            officeTileSpace[x][3].SetValidSpace(false);
            officeTileSpace[x][4].SetValidSpace(false);
            officeTileSpace[x][7].SetValidSpace(false);
            officeTileSpace[x][8].SetValidSpace(false);

            officeTileSpace[x][11].SetValidSpace(false);
            officeTileSpace[x][12].SetValidSpace(false);
            officeTileSpace[x][15].SetValidSpace(false);
            officeTileSpace[x][16].SetValidSpace(false);
        }
        officeTileSpace[8][7].SetValidSpace(true);
        officeTileSpace[8][8].SetValidSpace(true);
        officeTileSpace[9][7].SetValidSpace(true);
        officeTileSpace[9][8].SetValidSpace(true);
        officeTileSpace[8][11].SetValidSpace(true);
        officeTileSpace[8][12].SetValidSpace(true);
        officeTileSpace[9][11].SetValidSpace(true);
        officeTileSpace[9][12].SetValidSpace(true);

        //set the corners to false
        officeTileSpace[0][0].SetValidSpace(false);
        officeTileSpace[0][1].SetValidSpace(false);
        officeTileSpace[1][0].SetValidSpace(false);
        officeTileSpace[1][1].SetValidSpace(false);

        officeTileSpace[0][18].SetValidSpace(false);
        officeTileSpace[0][19].SetValidSpace(false);
        officeTileSpace[1][18].SetValidSpace(false);
        officeTileSpace[1][19].SetValidSpace(false);

        officeTileSpace[18][0].SetValidSpace(false);
        officeTileSpace[18][1].SetValidSpace(false);
        officeTileSpace[19][0].SetValidSpace(false);
        officeTileSpace[19][1].SetValidSpace(false);

        officeTileSpace[18][18].SetValidSpace(false);
        officeTileSpace[18][19].SetValidSpace(false);
        officeTileSpace[19][18].SetValidSpace(false);
        officeTileSpace[19][19].SetValidSpace(false);

        //other bits
        officeTileSpace[0][9].SetValidSpace(false);
        path = null;
        for (x = 6; x <= 15; x++)
        {
            officeTileSpace[19][x].SetValidSpace(false);
        }
        for (x = 8; x <= 13; x++)
        {
            officeTileSpace[18][x].SetValidSpace(false);
        }
        officeTileSpace[6][16].SetValidSpace(true);
        officeTileSpace[7][16].SetValidSpace(true);
    }
    private void Update()
    {

        //all this is temporary for testing purposes
        if (createdMap == false)
        {
            this.gameObject.GetComponent<TileDisplay>().CreateDisplay(officeTileSpace);
            createdMap = true;
        }
        this.gameObject.GetComponent<TileDisplay>().UpdateDisplay(officeTileSpace, startX, startY, goalX, goalY, path);
    }
    public void ResizeOffice()
    {
        officeTileSpace = new OfficeTile[officeX][];
        int x, y;
        for (x = 0; x < officeX; x++)
        {
            officeTileSpace[officeX] = new OfficeTile[officeY];
            for (y = 0; y < officeY; y++)
            {
                officeTileSpace[x][y] = new OfficeTile();
            }
        }
    }
    public bool FindPathSlowStart(int startX, int startY, int goalX, int goalY)
    {
        //returns true if settup is good

        //check for edge cases
        if (startX < 0 || startY < 0 || goalX < 0 || goalY < 0 ||
            startX >= officeX || startY >= officeY || goalX >= officeX || goalY >= officeY)
        {
            //out of bounds
            return false;
        }
        //reset tiles
        int x, y;
        for (x = 0; x < officeX; x++)
        {
            for (y = 0; y < officeY; y++)
            {
                officeTileSpace[x][y].Reset();
            }
        }
        // no need to clear que because office tiles resetting resets next so all the could be left is queHead
        // so setting queHead to be something else gets rid of old head
        queHead = officeTileSpace[startX][startY];
        queHead.SetState(1);
        queHead.SetGoalDistance(CalculateGoalDistance(goalX, goalY, queHead));
        return true;
    }
    public Vector2Int[] FindPathSlow(int startX, int startY, int goalX, int goalY)
    {
        if (queSpecial == null)
            queSpecial = queHead;
        else if (queSpecial.GetGoalDistance() > queHead.GetGoalDistance())
        {
            //if(queSpecial.GetCost() >  queHead.GetCost())
            queSpecial = queHead;
        }
        //add neighbors 
        //add neighbors adds the neighbors to the list and also calculates their distance from the goal
        AddNeighbors(goalX, goalY);
        //remove head
        queHead.SetState(2);
        queHead = queHead.GetNext();
        //sort que
        SortQue();
        //goalFound

        bool specialend = false;
        if (queHead == null)//no path
        {
            queHead = queSpecial;
            //the node with smallest crow distance and use it
            specialend = true;
        }
        if ((queHead.GetXPos() == goalX && queHead.GetYPos() == goalY) || ((Mathf.Abs(queHead.GetXPos() - goalX) <= 1) && (Mathf.Abs(queHead.GetYPos() - goalY) <= 1)) || specialend)
        {

            //walk back through parents

            int count = 0;
            OfficeTile curr = queHead;
            while (curr.GetParentX() != -1)
            {
                curr = officeTileSpace[curr.GetParentX()][curr.GetParentY()];
                count++;
            }
            curr = queHead;
            Vector2Int[] pathSpaces = new Vector2Int[count];
            count--; //count -1 is last index because we are walking backwards
            while (curr.GetParentX() != -1)
            {
                pathSpaces[count] = new Vector2Int(curr.GetXPos(), curr.GetYPos());
                curr = officeTileSpace[curr.GetParentX()][curr.GetParentY()];
                count--;
            }
            return pathSpaces;

        }
        return null;
    }
    public Vector2Int[] FindPath(int startX, int startY, int goalX, int goalY)
    {
        FindPathSlowStart(startX, startY, goalX, goalY);
        while (path == null)
        {
            path = FindPathSlow(startX, startY, goalX, goalY);
        }
        PlayerPrefs.SetInt("goalX",goalX);
        PlayerPrefs.SetInt("goalY", goalY);
        return path;
    }
    public Vector2Int[] FindPath(int startX, int startY)
    {
        goalX = PlayerPrefs.GetInt("goalX");
        goalY = PlayerPrefs.GetInt("goalY");
        //for when scene is reloaded
        FindPathSlowStart(startX, startY, goalX, goalY);
        while (path == null)
        {
            path = FindPathSlow(startX, startY, goalX, goalY);
        }
        return path;
    }
    private void AddNeighbors(int goalX, int goalY)
    {
        // adds all valid neighboring tiles to the que
        // this incudes diagonal neighbors
        // a neighbor is valid if its validSpace = true and (if its state = 0 or its new totalCost would be smaller from this new parent)
        // also totalCost is the cost + the distance from the goal
        int x, y;
        for (x = -1; x <= 1; x++)
        {
            for (y = -1; y <= 1; y++)
            {
                int movementCost = 0;
                if (Mathf.Abs(x) + Mathf.Abs(y) == 2)
                    movementCost = 14;//this is because distance traveld to a corner piece is sqrt2 so we approximate
                if (Mathf.Abs(x) + Mathf.Abs(y) == 1)
                    movementCost = 10;


                int trueX = queHead.GetXPos() + x;
                int trueY = queHead.GetYPos() + y;

                //this is so we dont go off of the grid
                if (trueX > -1 && trueX < officeTileSpace.Length && trueY > -1 && trueY < officeTileSpace[0].Length)
                {
                    //this is so we dont add the queHead
                    if (!(x == 0 && y == 0))
                    {
                        OfficeTile curr = officeTileSpace[trueX][trueY];
                        //we cant use invalid spaces
                        if (curr.GetValidSpace())
                        {
                            // unchecked just get thrown in
                            if (curr.GetState() == 0)
                            {

                                //this gets rid of walking through corners
                                if(x!= 0 && y != 0)//diagonal move
                                {
                                    OfficeTile currXNeighbor = officeTileSpace[trueX][queHead.GetYPos()];
                                    OfficeTile currYNeighbor = officeTileSpace[queHead.GetXPos()][trueY];
                                    if(currXNeighbor.GetValidSpace() == false && currYNeighbor.GetValidSpace() == false)
                                    {
                                        //add it to the list
                                        curr.SetNext(queHead.GetNext());
                                        queHead.SetNext(curr);

                                        curr.SetState(1);
                                        curr.SetCost(queHead.GetCost() + movementCost);
                                        curr.SetParentX(queHead.GetXPos());
                                        curr.SetParentY(queHead.GetYPos());

                                        curr.SetGoalDistance(CalculateGoalDistance(goalX, goalY, curr));
                                        //curr.SetGoalDistance(CalculateGoalDistance(startX, startY, curr));
                                    }
                                }
                                else
                                {
                                    //add it to the list
                                    curr.SetNext(queHead.GetNext());
                                    queHead.SetNext(curr);

                                    curr.SetState(1);
                                    curr.SetCost(queHead.GetCost() + movementCost);
                                    curr.SetParentX(queHead.GetXPos());
                                    curr.SetParentY(queHead.GetYPos());

                                    curr.SetGoalDistance(CalculateGoalDistance(goalX, goalY, curr));
                                    //curr.SetGoalDistance(CalculateGoalDistance(startX, startY, curr));
                                }
                            }
                            //this is a space in que so we just find it and change what is needed
                            else if (curr.GetState() == 1)
                            {
                                float potentialCost = queHead.GetCost() + movementCost;

                                if (potentialCost < curr.GetCost())
                                {
                                    //no need to add it to list just change a few things

                                    curr.SetCost(queHead.GetCost() + movementCost);
                                    curr.SetParentX(queHead.GetXPos());
                                    curr.SetParentY(queHead.GetYPos());

                                    // no need to change distance from goal as that is taken care of when added to the list
                                }
                            }
                            //this is a checked space get thrown in if they can get a better value
                            else if (curr.GetState() == 2)
                            {
                                //check if it gets better value
                                float potentialCost = queHead.GetCost() + movementCost;

                                if (potentialCost < curr.GetCost())
                                {
                                    //add it to the list
                                    curr.SetNext(queHead.GetNext());
                                    queHead.SetNext(curr);

                                    curr.SetState(1);
                                    curr.SetCost(queHead.GetCost() + movementCost);
                                    curr.SetParentX(queHead.GetXPos());
                                    curr.SetParentY(queHead.GetYPos());

                                    curr.SetGoalDistance(CalculateGoalDistance(goalX, goalY, curr));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void SortQue()
    {
        //we dont really need to sort the que we just find which has the smallest value
        int x, y;
        int count = 0;
        OfficeTile curr = queHead;
        //get count so we know the length 
        while (curr != null)
        {
            count++;
            curr = curr.GetNext();
            if (count == 100)
            {
                Debug.Log("Bruh");
                break;
            }
        }
        if (count < 2)//if only one element exists then that is the smallest element
            return;
        float numToBeat = queHead.GetCost() + queHead.GetGoalDistance();
        curr = queHead.GetNext();
        OfficeTile prev = queHead;
        for (x = 1; x < count; x++)
        {
            if (numToBeat > curr.GetCost() + curr.GetGoalDistance())
            {
                numToBeat = curr.GetCost() + curr.GetGoalDistance();
                //swap head and curr
                if (x == 1)
                {
                    //swapping head and head.next()


                    queHead.SetNext(curr.GetNext());//s1
                    curr.SetNext(queHead);//s2
                    queHead = curr;//s3
                    curr = queHead.GetNext();

                    //x1h-->x2-->x3

                    //s1
                    //x1h-->x3
                    //x2-->x3

                    //s2
                    //x2-->x1h-->x3

                    //s3
                    //x2h-->x1-->x3
                }
                else
                {
                    //make this so simple that i cant screw it up anymore
                    OfficeTile x1 = queHead;
                    OfficeTile x2 = queHead.GetNext();
                    OfficeTile x3 = prev;
                    OfficeTile x4 = curr;
                    OfficeTile x5 = curr.GetNext();

                    //swap x1 and x4
                    //x1->x2---- x3->x4->x5

                    x1.SetNext(x5);
                    x3.SetNext(x1);
                    x4.SetNext(x2);
                    queHead = curr;
                    curr = x1;
                }
            }
            prev = curr;
            curr = curr.GetNext();
        }
    }

    private int CalculateGoalDistance(int goalX, int goalY, OfficeTile tile)
    {
        //for the distance we take the ammount of diagonals * 1.4 + the number of straights
        // so we just need to find which one is bigger

        int distanceX = Mathf.Abs(tile.GetXPos() - goalX);
        int distanceY = Mathf.Abs(tile.GetYPos() - goalY);
        int distanceFromGoal;
        if (distanceX == distanceY)
        {
            distanceFromGoal = distanceX * 14;
        }
        else if (distanceX > distanceY)
        {
            distanceFromGoal = distanceY * 14 + (distanceX - distanceY) * 10;
        }
        else
        {
            distanceFromGoal = distanceX * 14 + (distanceY - distanceX) * 10;
        }
        return distanceFromGoal;
    }

    public void UpdateDisplay()
    {
        this.gameObject.GetComponent<TileDisplay>().UpdateDisplay(officeTileSpace, startX, startY, goalX, goalY, path);
    }

    public void NewPath()
    {
        path = null;
        path = FindPath(startX, startY, goalX, goalY);
        this.gameObject.GetComponent<TileDisplay>().UpdateDisplay(officeTileSpace, startX, startY, goalX, goalY, path);
    }
    public void NewPathFromReload()
    {
        path = null;
        goalX = PlayerPrefs.GetInt("goalX");
        goalY = PlayerPrefs.GetInt("goalY");
        startX = PlayerPrefs.GetInt("startX");
        startY = PlayerPrefs.GetInt("startY");
        path = FindPath(startX, startY, goalX, goalY);
        this.gameObject.GetComponent<TileDisplay>().UpdateDisplay(officeTileSpace, startX, startY, goalX, goalY, path);
        path = FindPath(startX, startY, goalX, goalY);
        this.gameObject.GetComponent<TileDisplay>().UpdateDisplay(officeTileSpace, startX, startY, goalX, goalY, path);
    }
}

