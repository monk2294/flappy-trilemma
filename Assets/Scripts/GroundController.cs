using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

    public MovableObject ground;

    private MovableObject ground1;
    private MovableObject ground2;

    // Use this for initialization
    void Start () {
        ground1 = ground;
        ground2 = Instantiate(ground1, getPositionForNext(ground1), ground1.transform.rotation);

        ground1.OnBecomeInvisible += () => ground1.transform.position = getPositionForNext(ground2);
        ground2.OnBecomeInvisible += () => ground2.transform.position = getPositionForNext(ground1);
	}

    // Update is called once per frame
    void Update () {
		
	}

    private Vector3 getPositionForNext(MovableObject g)
    {
        Vector2 currentSize = g.GetComponent<SpriteRenderer>().bounds.size;
        Vector3 currentPosition = g.transform.position;
        currentPosition.x += currentSize.x;
        return currentPosition;
    }
}
