using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour {

    // units per second
    public float moveSpeed = 5.0f;
    public bool visibilityEvents = true;

    public delegate void VisibilityAction();
    public event VisibilityAction OnBecomeVisible;
    public event VisibilityAction OnBecomeInvisible;

    private SpriteRenderer spriteRenderer;
    private bool visibilityFlag = false;

	// Use this for initialization
	protected void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        visibilityFlag = isVisibleNow();
    }

    // Update is called once per frame
    protected void FixedUpdate () {
        // Calculate actual translation
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);

        if (visibilityEvents)
        {
            bool nowVisible = isVisibleNow();
            if (!nowVisible && visibilityFlag)
            {
                if (OnBecomeInvisible != null)
                {
                    OnBecomeInvisible();
                }
            }
            if (nowVisible && !visibilityFlag)
            {
                if (OnBecomeVisible != null)
                {
                    OnBecomeVisible();
                }
            }
            visibilityFlag = nowVisible;
        }
	}

    protected virtual bool isVisibleNow()
    {
        Camera cam = Camera.main;
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, spriteRenderer.bounds);
    }
}
