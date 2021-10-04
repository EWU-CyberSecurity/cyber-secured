using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class TweenYTo0 : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
        transform.DOLocalMoveY(0.0f, 0.5f);
	}
}
