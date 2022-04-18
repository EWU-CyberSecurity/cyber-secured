using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeTile 
{
    //office tile is for the A* pathfinding 
    //these are the individual tiles that are used for the method 
    
    //holds the location in the 2d array of the parent tile
    private int parentX, parentY;
    private int xPos, yPos;//location in 2d array
    private int state; //0 = unchecked, 1 = in que, 2 = checked
    private bool validSpace;//for the pathfinding this is used to make barriers
    private int cost,goalDistance;//cost is distance traveled to get to this position and goalDistance is the distance left to the goal
    private OfficeTile next; //used for que linked list
    public OfficeTile()
    {
        //state should start as unchecked
        state = 0;
        cost = 0;
        //real parent x and y are always greater than -1
        // so a -1 here represents that it has no parent in the search
        parentX = -1;
        parentY = -1;
        next = null;
        validSpace = true;
    }
    public OfficeTile(int newXPos, int newYPos)
    {
        //state should start as unchecked
        state = 0;
        cost = 0;
        //real parent x and y are always greater than -1
        // so a -1 here represents that it has no parent in the search
        parentX = -1;
        parentY = -1;
        goalDistance = -1;
        next = null;
        validSpace = true;
        xPos = newXPos;
        yPos = newYPos;
    }
    public int GetState()
    {
        return state;
    }
    public void SetState(int newState)
    {
        //state should always be 0, 1, or 2
        if (newState < 0 || newState > 2)
            return;
        
        state = newState;
    }
    public int GetCost()
    {
        return cost;
    }
    public void SetCost(int newCost)
    {
        cost = newCost;
    }
    public int GetGoalDistance()
    {
        return goalDistance;
    }
    public void SetGoalDistance(int newGoalDistance)
    {
        goalDistance = newGoalDistance;
    }
    public int GetParentX()
    {
        return parentX;
    }
    public void SetParentX(int newParentX)
    {
        parentX = newParentX;
    }
    public int GetParentY()
    {
        return parentY;
    }
    public void SetParentY(int newParentY)
    {
        parentY = newParentY;
    }
    public int GetXPos()
    {
        return xPos;
    }
    public void SetXPos(int newXPos)
    {
        xPos = newXPos;
    }
    public int GetYPos()
    {
        return yPos;
    }
    public void YPos(int newYPos)
    {
        yPos = newYPos;
    }
    public OfficeTile GetNext()
    {
        return next;
    }
    public void SetNext(OfficeTile newNext)
    {
        next = newNext;
    }

    public bool GetValidSpace()
    {
        return validSpace;
    }
    public void SetValidSpace(bool newValidSpace)
    {
        validSpace = newValidSpace;
    }
    public void Reset()
    {
        //state should start as unchecked
        state = 0;
        cost = 0;
        //real parent x and y are always greater than -1
        // so a -1 here represents that it has no parent in the search
        parentX = -1;
        parentY = -1;
        cost = 0;
        next = null;
    }
}
