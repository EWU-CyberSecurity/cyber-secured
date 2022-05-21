using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCharacterInterface : MonoBehaviour
{
    public GameObject npBar;
    private float currentNP;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NPDamage(int damage)
    {
        float newNP = npBar.GetComponent<NPBar>().np - (damage * .05f);

        if(newNP <= 0)
        {
            npBar.GetComponent<NPBar>().ChangeNP(0);
            Fail();
        }
        else
        {
            npBar.GetComponent<NPBar>().ChangeNP(newNP);
        }
    }

    public void Fail()
    {
        Debug.Log("Lose.");
    }
}
