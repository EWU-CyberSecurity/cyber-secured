using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //this whole thing is basically for testing and designing purposes 
    public float xMin, xMax, yMin, yMax;
    public GameObject tilePrefab;
    private GameObject[][] tiles;
    public int heighth, width;

    
    public Sprite fullBlack, edgeTop, edgeLeft, edgeBottom, edgeRight,parralelEdgeTopBottom, parralelEdgeLeftRight,edgeCornerTopLeft,edgeCornerBottomRight,edgeCornerBottomLeft,edgeCornerTopRight,
        threeEdgeGapLeft,threeEdgeGapBottom,threeEdgeGapRight,threeEdgeGapTop,fullEdge;
    public Sprite cornerTopLeft, cornerBottomLeft, CornerBottomRight, cornerTopRight, cornerTLandBR, cornerBLandTR, cornerTLandBL, cornerBLandBR, cornerBRandTR, cornerTRandTL,
        cornerTLandBLandBR, cornerBLandBRandTL, cornerBRandTRandTL, cornerTRandTLandBL, cornerAll;
    public Sprite edgeTopTwoCorners, edgeLeftTwoCorners, edgeBottomTwoCorners, edgeRightTwoCorners, cornerTopLeftTwoEdges, cornerBottomLeftTwoEdges, cornerBottomRightTwoEdges, cornerTopRightTwoEdges;
    public Sprite edgeTopCornerBL, edgeTopCornerBR, edgeLeftCornerBR, edgeLeftCornerTR, edgeBottomCornerTR, edgeBottomCornerTL, edgeRightCornerTL, edgeRightCornerBL;


    float opacity = 0.0f;//turn on for testing, and off for the real game

    void Start()
    {
        CreateMapRandom();
        UpdateDisplay();
    }
    void Update()
    {
        if(Input.GetKeyDown("l"))
        {
            DeleteMap();
            CreateMapRandom();
            UpdateDisplay();
        }
    }
    public void CreateMap()
    {
        tiles = new GameObject[width][];
        int x, y;
        for (x = 0; x < width; x++)
        {
            tiles[x] = new GameObject[heighth];
            for (y = 0; y < heighth; y++)
            {
                //create tile
                GameObject tile;
                tile = Instantiate(tilePrefab);
                tiles[x][y] = tile;
                float xSpace = Mathf.Abs(xMin - xMax);
                float xTileSpace = xSpace / heighth;
                float ySpace = Mathf.Abs(yMin - yMax);
                float yTileSpace = ySpace / width;
                //the +0.5f is to make it not have it not go over edge
                tile.GetComponent<Transform>().position = new Vector3(xMin + xTileSpace * (x + 0.5f), yMin + yTileSpace * (y + 0.5f), 0);

                tile.GetComponent<Transform>().localScale = new Vector2(xTileSpace, yTileSpace);

                //tile.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            }
        }
    }
    public void CreateMapRandom()
    {
        int x, y;
        tiles = new GameObject[width][];
        for (x = 0; x < width; x++)
        {
            tiles[x] = new GameObject[heighth];
            for (y = 0; y < heighth; y++)
            {
                if (Random.Range(1,3) == 1)
                {
                    //create tile
                    GameObject tile;
                    tile = Instantiate(tilePrefab);
                    tiles[x][y] = tile;
                    float xSpace = Mathf.Abs(xMin - xMax);
                    float xTileSpace = xSpace / heighth;
                    float ySpace = Mathf.Abs(yMin - yMax);
                    float yTileSpace = ySpace / width;
                    //the +0.5f is to make it not have it not go over edge
                    tile.GetComponent<Transform>().position = new Vector3(xMin + xTileSpace * (x + 0.5f), yMin + yTileSpace * (y + 0.5f), 0);

                    tile.GetComponent<Transform>().localScale = new Vector2(xTileSpace, yTileSpace);

                    //tile.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
                }
            }
        }
    }

    public void DeleteMap()
    {
        int x, y;
        for (x = 0; x < width; x++)
        {
            for (y = 0; y < heighth; y++)
            {
                if (tiles[x][y] != null)
                {
                    Destroy(tiles[x][y]);
                    tiles[x][y] = null;
                }
            }
        }
    }
    public void FindTileDesign(GameObject tile,int x, int y)
    {
        bool edgeTopB, edgeLeftB, edgeBottomB, edgeRightB;
        bool cornerTopLeftB , cornerBottomLeftB, cornerBottomRightB, cornerTopRightB;
         
        if (x == 0)
            edgeLeftB = false;
        else if (tiles[x -1][y] != null)
            edgeLeftB = false;
        else edgeLeftB = true;

        if (x == width - 1)
            edgeRightB = false;
        else if (tiles[x+1][y] != null)
            edgeRightB = false;
        else edgeRightB = true;

        if (y == 0)
            edgeBottomB = false;
        else if (tiles[x][y-1] != null)
            edgeBottomB = false;
        else edgeBottomB = true;

        if (y == heighth - 1)
            edgeTopB = false;
        else if (tiles[x][y+1] != null)
            edgeTopB = false;
        else edgeTopB = true;

        //corners
        if (edgeTopB || edgeLeftB)
            cornerTopLeftB = false;
        else if (x == 0 || y == heighth - 1)
            cornerTopLeftB = false;
        else if(tiles[x - 1][y + 1] != null)
            cornerTopLeftB = false;
        else
            cornerTopLeftB = true;

        if (edgeBottomB || edgeLeftB)
            cornerBottomLeftB = false;
        else if (x == 0 || y == 0)
            cornerBottomLeftB = false;
        else if (tiles[x - 1][y - 1] != null)
            cornerBottomLeftB = false;
        else
            cornerBottomLeftB = true;


        if (edgeBottomB || edgeRightB)
            cornerBottomRightB = false;
        else if (x == width - 1 || y == 0)
            cornerBottomRightB = false;
        else if (tiles[x + 1][y - 1] != null)
            cornerBottomRightB = false;
        else
            cornerBottomRightB = true;

        if (edgeTopB || edgeRightB)
            cornerTopRightB = false;
        else if (x == width - 1 || y == heighth - 1)
            cornerTopRightB = false;
        else if (tiles[x + 1][y + 1] != null)
            cornerTopRightB = false;
        else
            cornerTopRightB = true;



        int helper = 0;
        if (edgeTopB)
            helper += 1;
        if (edgeLeftB)
            helper += 10;
        if (edgeBottomB)
            helper += 100;
        if (edgeRightB)
            helper += 1000;
        if (cornerTopLeftB)
            helper += 10000;
        if (cornerBottomLeftB)
            helper += 100000;
        if (cornerBottomRightB)
            helper += 1000000;
        if (cornerTopRightB)
            helper += 10000000;


        {
            switch (helper)
            {
                //nothing
                case (0):
                    tile.GetComponent<SpriteRenderer>().sprite = fullBlack;
                    break;
                //one edge
                case (1):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeTop;
                    break;
                case (10):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeLeft;
                    break;
                case (100):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeBottom;
                    break;
                case (1000):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeRight;
                    break;
                //two edge
                case (11):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeCornerTopLeft;
                    break;
                case (101):
                    tile.GetComponent<SpriteRenderer>().sprite = parralelEdgeTopBottom;
                    break;
                case (110):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeCornerBottomLeft;
                    break;
                case (1001):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeCornerTopRight;
                    break;
                case (1010):
                    tile.GetComponent<SpriteRenderer>().sprite = parralelEdgeLeftRight;
                    break;
                case (1100):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeCornerBottomRight;
                    break;
                //three edge
                case (111):
                    tile.GetComponent<SpriteRenderer>().sprite = threeEdgeGapRight;
                    break;
                case (1011):
                    tile.GetComponent<SpriteRenderer>().sprite = threeEdgeGapBottom;
                    break;
                case (1101):
                    tile.GetComponent<SpriteRenderer>().sprite = threeEdgeGapLeft;
                    break;
                case (1110):
                    tile.GetComponent<SpriteRenderer>().sprite = threeEdgeGapTop;
                    break;
                    //all edge
                case (1111):
                    tile.GetComponent<SpriteRenderer>().sprite = fullEdge;
                    break;



                //one corner
                case (10000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerTopLeft;
                    break;
                case (100000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerBottomLeft;
                    break;
                case (1000000):
                    tile.GetComponent<SpriteRenderer>().sprite = CornerBottomRight;
                    break;
                case (10000000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerTopRight;
                    break;

                //two corner
                case (01010000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerTLandBR;
                    break;
                case (10100000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerBLandTR;
                    break;
                case (00110000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerTLandBL;
                    break;
                case (01100000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerBLandBR;
                    break;
                case (11000000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerBRandTR;
                    break;
                case (10010000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerTRandTL;
                    break;

                //three corner
                case (1110000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerTLandBLandBR;
                    break;
                case (11100000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerBLandBRandTL;
                    break;
                case (11010000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerBRandTRandTL;
                    break;
                case (10110000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerTRandTLandBL;
                    break;

                //four corner
                case (11110000):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerAll;
                    break;

                //corner edge combos

                //twoedge one corner
                case (00011100):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerTopLeftTwoEdges;
                    break;
                case (00101001):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerBottomLeftTwoEdges;
                    break;
                case (01000011):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerBottomRightTwoEdges;
                    break;
                case (10000110):
                    tile.GetComponent<SpriteRenderer>().sprite = cornerTopRightTwoEdges;
                    break;
                //two corner one edge
                case (01100001):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeTopTwoCorners;
                    break;
                case (11000010):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeLeftTwoCorners;
                    break;
                case (10010100):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeBottomTwoCorners;
                    break;
                case (00111000):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeRightTwoCorners;
                    break;
                //one edge one corner
                case (00100001):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeTopCornerBL;
                    break;
                case (01000001):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeTopCornerBR;
                    break;
                case (01000010):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeLeftCornerBR;
                    break;
                case (10000010):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeLeftCornerTR;
                    break;
                case (10000100):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeBottomCornerTR;
                    break;
                case (00010100):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeBottomCornerTL;
                    break;
                case (00011000):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeRightCornerTL;
                    break;
                case (00101000):
                    tile.GetComponent<SpriteRenderer>().sprite = edgeRightCornerBL;
                    break;

            }
        }

    }
    public void UpdateDisplay()
    {
        int x, y;
        for (x = 0; x < width; x++)
        {
            for (y = 0; y < heighth; y++)
            {
                GameObject tile = tiles[x][y];
                if(tile != null)
                    FindTileDesign(tile,x,y);
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

        if (validSpace)
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
        Vector2 newVector = new Vector2(xMin + xTileSpace * (realX + 0.5f), yMin + yTileSpace * (realY + 0.5f));
        return newVector;
    }
    public Vector2 MapToTilePos(Vector2 oldVector)
    {
        //takes real pos(float,float) and creates tile position of it
        float realX = oldVector.x;
        float realY = oldVector.y;
        //check if its out of boudry
        if (realX > xMax || realX < xMin || realY > yMax || realY < yMin)
            return new Vector2(-1, -1);

        float xSpace = Mathf.Abs(xMin - xMax);
        float xTileSpace = xSpace / tiles.Length;
        float ySpace = Mathf.Abs(yMin - yMax);
        float yTileSpace = ySpace / tiles.Length;

        float xDistance = Mathf.Abs(xMin - realX);
        float yDistance = Mathf.Abs(yMin - realY);
        int xSpot = (int)Mathf.Floor(xDistance / xTileSpace);
        int ySpot = (int)Mathf.Floor(yDistance / yTileSpace);


        //the +0.5f is to make it not have it not go over edge
        Vector2 newVector = new Vector2(xSpot, ySpot);
        return newVector;
    }
}
