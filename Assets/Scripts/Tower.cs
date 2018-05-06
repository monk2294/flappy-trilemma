using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MovableObject {

    public Vector3 boundsSize = new Vector3(0.4f, 14.0f, 0.0f);
    private Bounds bounds;

    protected void Start()
    {
        base.Start();
        bounds = new Bounds(transform.position, boundsSize);
    }

    protected override bool isVisibleNow()
    {
        Camera cam = Camera.main;
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        bounds.center = transform.position;
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }
}
