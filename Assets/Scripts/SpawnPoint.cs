using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool IsVisible { get; private set; }

    public void Update()
    {

        var screenPoint = Camera.main.WorldToViewportPoint(transform.position);

        IsVisible = (screenPoint.z > 0 
            && screenPoint.x > 0 
            && screenPoint.x < 1 
            && screenPoint.y > 0 
            && screenPoint.y < 1);

    }
}
