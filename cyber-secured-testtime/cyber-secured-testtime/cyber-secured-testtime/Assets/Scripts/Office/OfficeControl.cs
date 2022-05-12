using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OfficeControl : MonoBehaviour
{
    public GameObject officeCharacter;
    public Camera officeCamera;
    public Camera mainCamera;
    public int delay = 0;
    public bool firstChange = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !mainCamera.enabled)
        {
            ChangeGoal();
            if(firstChange)
            {
                ChangeGoal();
                firstChange = false;
            }
        }
        //if (Input.GetMouseButtonDown(1))
            //ChangeStart();
        //if (Input.GetMouseButtonDown(1))
            //ChangeValidSpace();
        MoveCharacterStart();
        if(delay == 1)
        {
            SceneReset();
            delay++;
        }
        if (PlayerPrefs.GetInt("ManualReset") == 1)
            delay++;
    }
    public void ChangeGoal()
    {
        Vector3 mPos = officeCamera.ScreenToWorldPoint(Input.mousePosition);
        bool inrange = this.GetComponent<TileDisplay>().ChangeGoal(mPos);
        if(inrange )
        {
            this.GetComponent<AStarPathfinder>().NewPath();
            this.GetComponent<AStarPathfinder>().UpdateDisplay();
        }
    }
    public void ChangeStart()
    {

        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.GetComponent<TileDisplay>().ChangeStart(mPos);
        this.GetComponent<AStarPathfinder>().NewPath();
        this.GetComponent<AStarPathfinder>().UpdateDisplay();
    }
    public void ChangeStart(Vector2 newStart)
    {
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.GetComponent<TileDisplay>().ChangeStart(mPos);
        this.GetComponent<AStarPathfinder>().UpdateDisplay();
    }
    public void ChangeValidSpace()
    {
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.GetComponent<TileDisplay>().ChangeValidSpace(mPos);
        this.GetComponent<AStarPathfinder>().UpdateDisplay();
    }


    public void MoveCharacterStart()
    {
        if (this.GetComponent<AStarPathfinder>().path != null && officeCharacter.GetComponent<OfficeCharacter>().path == null )
        {
            Vector2[] newVector = SwitchVector2ToFloat(this.GetComponent<AStarPathfinder>().path);
            officeCharacter.GetComponent<OfficeCharacter>().SetPath(newVector);
            this.gameObject.GetComponent<AStarPathfinder>().path = null;
        }
        else if(this.GetComponent<AStarPathfinder>().path != null && officeCharacter.GetComponent<OfficeCharacter>().path != null)
        {
            Vector2[] newVector = SwitchVector2ToFloat(this.GetComponent<AStarPathfinder>().path);
            officeCharacter.GetComponent<OfficeCharacter>().SetNextPath(newVector);
            this.gameObject.GetComponent<AStarPathfinder>().path = null;
        }
    }
    public Vector2[] SwitchVector2ToFloat(Vector2Int[] oldVector)
    {
        Vector2[] newVector = new Vector2[oldVector.Length];
        int x;
        for (x = 0; x < oldVector.Length; x++)
        {

            newVector[x] = new Vector2((float)oldVector[x].x, (float)oldVector[x].y);
            newVector[x] = this.GetComponent<TileDisplay>().MapToRealPos(newVector[x]);
        }
        return newVector; ;
    }

    public void SceneReset()
    {
        if (PlayerPrefs.HasKey("goalX"))
        {
            this.GetComponent<AStarPathfinder>().NewPathFromReload();
        }
    }
}
