using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public static Vector2 GetRelativeAim(Transform localTransform)
    {
        return new Vector2(localTransform.position.x + 10, localTransform.position.y);
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static float GetMouseAngle(Transform localTransform)
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -0.639f;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(localTransform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        return (Mathf.Atan2(mousePos.y, mousePos.x));
    }
}
