using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{
	[SerializeField] GameObject player;
	[SerializeField] Transform[] transforms;
	void Start()
	{
		iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
	}

    private void Update()
    {
		
	}
}

